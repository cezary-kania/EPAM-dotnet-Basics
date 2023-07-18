using Library.Documents;
using System.Runtime.Caching;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace Library.Repositories;

public class FileSystemDocumentRepository : IDocumentRepository
{
    private readonly MemoryCache _cache = MemoryCache.Default;
    private readonly Dictionary<string, TimeSpan?>? _cacheDocumentExpirationTimeSpans;
    
    public FileSystemDocumentRepository(IConfigurationRoot config)
    {
        _cacheDocumentExpirationTimeSpans = config.GetSection("DocumentCacheExpirationTimeSpans")
            .Get<Dictionary<string, TimeSpan?>>();
    }

    private const string StoragePath = "documents/";
    public IEnumerable<IDocument?> GetByNumber(int documentNumber)
    {
        var matchingFileNames = Directory.GetFiles(StoragePath, $"*_{documentNumber}.json");
        if (matchingFileNames.Length == 0)
        {
            return Enumerable.Empty<IDocument>();
        }
        var types = AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .ToList();

        return matchingFileNames.Select(fileName =>
        {
            var documentTypeName = Path.GetFileNameWithoutExtension(fileName).Split('_')[0];
            var type = types.FirstOrDefault(t => t.Name == documentTypeName);

            var json = GetCardJson(fileName, documentTypeName);
            return (IDocument) JsonSerializer.Deserialize(json, type);
        });
    }

    private string GetCardJson(string fileName, string documentTypeName)
    {
        if (_cache.Contains(fileName))
        {
            return (string) _cache.Get(fileName);
        }
        var json = File.ReadAllText(fileName);
        var cacheTime = GetCacheTime(documentTypeName);
        if (!cacheTime.HasValue)
        {
            return json;
        }
        var cachePolicy = new CacheItemPolicy();
        cachePolicy.AbsoluteExpiration = DateTimeOffset.Now.Add(cacheTime.Value);
        _cache.Add(fileName, json, cachePolicy);
        return json;
    }

    private TimeSpan? GetCacheTime(string documentTypeName) =>
        _cacheDocumentExpirationTimeSpans is not null && 
        _cacheDocumentExpirationTimeSpans.ContainsKey(documentTypeName) 
            ? _cacheDocumentExpirationTimeSpans[documentTypeName] : null;
}
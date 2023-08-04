using System.Diagnostics.CodeAnalysis;
using System.Runtime.Caching;
using System.Text.Json;
using Library.Data.Interfaces;
using Library.Interfaces;
using Library.Models;
using Microsoft.Extensions.Configuration;

namespace Library.Data;

public class FileSystemDocumentRepository : IDocumentRepository
{
    private readonly MemoryCache _cache = MemoryCache.Default;
    private readonly Dictionary<string, int?> _cacheDocumentExpirationInSeconds;
    
    public FileSystemDocumentRepository(IConfiguration config)
    {
        _cacheDocumentExpirationInSeconds = config.GetSection("DocumentCacheExpirationTimeSpans")
            .Get<Dictionary<string, int?>>() ?? new Dictionary<string, int?>();
    }

    private const string StoragePath = "documents/";
    public List<IDocument> GetByNumber(int documentNumber)
    {
        var matchingFileNames = Directory.GetFiles(StoragePath, $"*_{documentNumber}.json");
        if (matchingFileNames.Length == 0)
        {
            return Enumerable.Empty<IDocument>().ToList();
        }
        
        return matchingFileNames.Select(fileName =>
        {
            var documentTypeName = Path.GetFileNameWithoutExtension(fileName).Split('_')[0];
            string json;
            var readFromFile = false;
            if (_cache.Contains(fileName))
            {
                json = (string) _cache.Get(fileName);
            }
            else
            {
                json = File.ReadAllText(fileName);
                readFromFile = true;
            }
            
            var deserialized = documentTypeName switch
            {
                "Book" => (IDocument?) JsonSerializer.Deserialize<Book>(json),
                "Magazine" => (IDocument?) JsonSerializer.Deserialize<Magazine>(json),
                "Patent" => (IDocument?) JsonSerializer.Deserialize<Patent>(json),
                "LocalizedBook" => (IDocument?) JsonSerializer.Deserialize<LocalizedBook>(json),
                _ => throw new ArgumentOutOfRangeException(nameof(documentTypeName))
            };
            
            if (readFromFile 
                && deserialized is not null 
                && _cacheDocumentExpirationInSeconds.TryGetValue(documentTypeName, out var cacheTimeInSeconds)
                && cacheTimeInSeconds.HasValue)
            {
                CacheFile(cacheTimeInSeconds, fileName, json);
            }
            
            return deserialized;
        })
        .OfType<IDocument>()
        .Select(document =>
        {
            document.DocumentNumber = documentNumber;
            return document;
        })
        .ToList();
    }

    private void CacheFile([DisallowNull] int? cacheTimeInSeconds, string fileName, string json)
    {
        var cachePolicy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(cacheTimeInSeconds.Value)
        };
        _cache.Add(fileName, json, cachePolicy);
    }
}
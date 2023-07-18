using System.Runtime.Caching;
using Library.Documents;
using Library.Repositories;
using Microsoft.Extensions.Configuration;

namespace Library.Services;

public class DocumentService : IDocumentService
{
    private readonly IDocumentRepository _repository;

    public DocumentService(IConfigurationRoot config)
    {
        _repository = new FileSystemDocumentRepository(config);
    }

    public IEnumerable<IDocument?> SearchByNumber(int documentNumber)
    {
        return _repository.GetByNumber(documentNumber);
    }
}
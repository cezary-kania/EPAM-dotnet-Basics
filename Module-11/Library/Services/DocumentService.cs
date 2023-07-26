using Library.Data;
using Library.Data.Interfaces;
using Library.Interfaces;
using Library.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Library.Services;

public class DocumentService : IDocumentService
{
    private readonly IDocumentRepository _repository;

    public DocumentService(IDocumentRepository repository)
    {
        _repository = repository;
    }

    public List<IDocument> SearchByNumber(int documentNumber)
    {
        return _repository.GetByNumber(documentNumber);
    }
}
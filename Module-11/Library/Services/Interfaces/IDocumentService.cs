using Library.Interfaces;

namespace Library.Services.Interfaces;

public interface IDocumentService
{
    public List<IDocument> SearchByNumber(int documentNumber);
}
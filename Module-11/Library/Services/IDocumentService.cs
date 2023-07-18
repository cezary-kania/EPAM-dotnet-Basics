using Library.Documents;

namespace Library.Services;

public interface IDocumentService
{
    public IEnumerable<IDocument?> SearchByNumber(int documentNumber);
}
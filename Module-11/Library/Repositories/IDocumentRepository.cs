using Library.Documents;

namespace Library.Repositories;

public interface IDocumentRepository
{
    public IEnumerable<IDocument?> GetByNumber(int documentNumber);
}
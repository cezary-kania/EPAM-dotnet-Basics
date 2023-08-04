using Library.Interfaces;

namespace Library.Data.Interfaces;

public interface IDocumentRepository
{
    public List<IDocument> GetByNumber(int documentNumber);
}
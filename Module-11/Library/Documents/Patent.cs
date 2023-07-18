namespace Library.Documents;

public record Patent(
    string Title, 
    string Authors, 
    DateTime DatePublished, 
    DateTime ExpirationDate,
    int UniqueId) : IDocument;
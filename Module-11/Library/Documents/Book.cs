namespace Library.Documents;

public record Book(
    string ISBN,
    string Title,
    string Authors,
    int NumberOfPages,
    string Publisher,
    DateTime DatePublished) : IDocument;
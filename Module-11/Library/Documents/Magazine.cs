namespace Library.Documents;

public record Magazine(
    string Title,
    string Publisher,
    string ReleaseNumber,
    DateTime PublishDate) : IDocument;
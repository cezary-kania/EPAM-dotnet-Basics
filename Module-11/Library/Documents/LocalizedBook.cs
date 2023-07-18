namespace Library.Documents;

public record LocalizedBook(
    string ISBN,
    string Title,
    string Authors,
    int NumberOfPages,
    string OriginalPublisher,
    string CountryOfLocalization,
    string LocalPublisher,
    DateTime DatePublished) : IDocument;
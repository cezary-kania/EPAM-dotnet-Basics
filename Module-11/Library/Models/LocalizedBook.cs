using System.Security.Policy;
using System.Text;
using Library.Interfaces;
namespace Library.Models;

public record LocalizedBook(
    string ISBN,
    string Title,
    string Authors,
    int NumberOfPages,
    string OriginalPublisher,
    string CountryOfLocalization,
    string LocalPublisher,
    DateTime DatePublished) : IDocument
{
    public int DocumentNumber { get; set; }

    public string GetCardInfo() =>
        new StringBuilder()
            .Append($"{nameof(LocalizedBook)} - No.{DocumentNumber}: {{")
            .Append($"{nameof(ISBN)}: {ISBN}, ")
            .Append($"{nameof(Title)}: {Title}, ")
            .Append($"{nameof(Authors)}: {Authors}, ")
            .Append($"{nameof(NumberOfPages)}: {NumberOfPages}, ")
            .Append($"{nameof(OriginalPublisher)}: {OriginalPublisher}, ")
            .Append($"{nameof(CountryOfLocalization)}: {CountryOfLocalization}, ")
            .Append($"{nameof(LocalPublisher)}: {LocalPublisher}, ")
            .Append($"{nameof(DatePublished)}: {DatePublished}")
            .Append('}')
            .ToString();
}
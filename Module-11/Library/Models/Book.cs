using System.Text;
using Library.Interfaces;

namespace Library.Models;

public record Book(
    string ISBN,
    string Title,
    string Authors,
    int NumberOfPages,
    string Publisher,
    DateTime DatePublished) : IDocument
{
    public int DocumentNumber { get; set; }

    public string GetCardInfo() =>
        new StringBuilder()
            .Append($"{nameof(Book)} - No.{DocumentNumber}: {{")
            .Append($"{nameof(ISBN)}: {ISBN}, ")
            .Append($"{nameof(Title)}: {Title}, ")
            .Append($"{nameof(Authors)}: {Authors}, ")
            .Append($"{nameof(NumberOfPages)}: {NumberOfPages}, ")
            .Append($"{nameof(Publisher)}: {Publisher}")
            .Append('}')
            .ToString();
}
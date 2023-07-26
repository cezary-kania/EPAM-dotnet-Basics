using System.Text;
using Library.Interfaces;

namespace Library.Models;

public record Magazine(
    string Title,
    string Publisher,
    string ReleaseNumber,
    DateTime PublishDate) : IDocument
{
    public int DocumentNumber { get; set; }

    public string GetCardInfo() =>
        new StringBuilder()
            .Append($"{nameof(Magazine)} - No.{DocumentNumber}: {{")
            .Append($"{nameof(Title)}: {Title}, ")
            .Append($"{nameof(Publisher)}: {Publisher}, ")
            .Append($"{nameof(ReleaseNumber)}: {ReleaseNumber}, ")
            .Append($"{nameof(PublishDate)}: {PublishDate}")
            .Append('}')
            .ToString();
}
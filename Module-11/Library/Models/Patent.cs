using System.Security.Policy;
using System.Text;
using Library.Interfaces;

namespace Library.Models;

public record Patent(
    string Title,
    string Authors, 
    DateTime DatePublished, 
    DateTime ExpirationDate,
    int UniqueId) : IDocument
{
    public int DocumentNumber { get; set; }

    public string GetCardInfo() =>
        new StringBuilder()
            .Append($"{nameof(Patent)} - No.{DocumentNumber}: {{")
            .Append($"{nameof(Title)}: {Title}, ")
            .Append($"{nameof(Authors)}: {Authors}, ")
            .Append($"{nameof(DatePublished)}: {DatePublished}, ")
            .Append($"{nameof(ExpirationDate)}: {ExpirationDate}, ")
            .Append($"{nameof(UniqueId)}: {UniqueId}")
            .Append('}')
            .ToString();
}
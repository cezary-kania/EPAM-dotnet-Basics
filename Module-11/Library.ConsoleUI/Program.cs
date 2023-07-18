using System.Text.Json;
using Library.Documents;
using Library.Services;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

IDocumentService documentService = new DocumentService(config);

while (true)
{
    Console.WriteLine("Enter document number or press enter to quit:");
    var input = Console.ReadLine();

    if (input?.Length == 0)
    {
        break;
    }

    var inputIsValidNumber = int.TryParse(input, out var documentNumber);
    if (!inputIsValidNumber)
    {
        Console.WriteLine("Invalid input.");
        continue;
    }
    var documents = documentService.SearchByNumber(documentNumber)
        .ToList();

    if (documents.Count > 0)
    {
        Console.WriteLine("Found documents:");
        for (var i = 0; i < documents.Count; i++)
        {
            Console.WriteLine($"{i + 1}. Card Info: {documents[i]}");
        }
    }
    else
    {
        Console.WriteLine("Document not found.");
    }
}
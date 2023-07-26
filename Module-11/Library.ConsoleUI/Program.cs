using Library.Data;
using Library.Data.Interfaces;
using Library.Services;
using Library.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var services = new ServiceCollection();
services.AddSingleton(configuration);
services.AddSingleton<IDocumentRepository,FileSystemDocumentRepository>();
services.AddSingleton<IDocumentService,DocumentService>();
var serviceProvider = services.BuildServiceProvider();

var documentService = serviceProvider.GetRequiredService<IDocumentService>();

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
            Console.WriteLine($"{i + 1}. Card Info: {documents[i].GetCardInfo()}");
        }
    }
    else
    {
        Console.WriteLine("Document not found.");
    }
}
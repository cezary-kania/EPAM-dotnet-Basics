using FileSystemLib;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var rootPath = config["RootFolderPath"];

var fileSystemVisitor = new FileSystemVisitor(rootPath, x => x.EndsWith(".pdf"));
fileSystemVisitor.OnStart += (sender, e) =>
{
    Console.WriteLine("Start");
};

fileSystemVisitor.OnFileFound += (sender, e) =>
{
    Console.WriteLine($"File found: {e.Entry}");
};

fileSystemVisitor.OnDirectoryFound += (sender, e) =>
{
    if (e.Entry.Contains("Ang"))
    {
        e.Exclude = true;
    }
    Console.WriteLine($"Directory found: {e.Entry}");
};

foreach (var item in fileSystemVisitor)
{
    Console.WriteLine(item);
}

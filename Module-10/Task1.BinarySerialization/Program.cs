using System.Runtime.Serialization.Formatters.Binary;
using Task1.BinarySerialization;

var department = new Department
(
    "Sample department 1 ",
    new List<Employee>
    {
        new("SD-1 Employee 1"),
        new("SD-1 Employee 2")
    }
);
Console.WriteLine($"Department before serialization: {department}");

const string fileName = "data.dat";
var formatter = new BinaryFormatter();
using (var stream = File.OpenWrite(fileName))
{
    formatter.Serialize(stream, department);
}

using (var stream = File.OpenRead(fileName))
{
    department = (Department)formatter.Deserialize(stream);
    Console.WriteLine($"Deserialized department: {department}");
}
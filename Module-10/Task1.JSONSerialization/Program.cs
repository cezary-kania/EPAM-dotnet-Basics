using System.Text.Json;
using Task1.JSONSerialization;

var department = new Department
{
    DepartmentName = "Sample department 1 ",
    Employees = new List<Employee>
    {
        new() { EmployeeName = "SD-1 Employee 1" },
        new() { EmployeeName = "SD-1 Employee 2" },
    }
};
Console.WriteLine($"Department before serialization: {department}");

const string fileName = "data.json";
var serialized = JsonSerializer.Serialize(department);
File.WriteAllText(fileName, serialized);

serialized = File.ReadAllText(fileName);
var deserialized = JsonSerializer.Deserialize<Department>(serialized);
Console.WriteLine($"Deserialized department: {deserialized}");
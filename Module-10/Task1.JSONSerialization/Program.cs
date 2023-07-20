using System.Text.Json;
using Task1.JSONSerialization;

var department = new Department
{
    DepartmentName = "IT",
    Employees = new List<Employee>
    {
        new() { EmployeeName = "Jack Daniels" },
        new() { EmployeeName = "Frodo Baggins" },
    }
};
Console.WriteLine($"Department before serialization: {department}");

const string fileName = "data.json";
var serialized = JsonSerializer.Serialize(department);
File.WriteAllText(fileName, serialized);

serialized = File.ReadAllText(fileName);
var deserialized = JsonSerializer.Deserialize<Department>(serialized);
Console.WriteLine($"Deserialized department: {deserialized}");
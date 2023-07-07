using System.Xml.Serialization;
using Task1.XMLSerialization;

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

const string fileName = "data.xml";
var serializer = new XmlSerializer(typeof(Department));
TextWriter writer = new StreamWriter(fileName);
serializer.Serialize(writer, department);
writer.Close();

TextReader reader = new StreamReader(fileName);
var deserialized = (Department) serializer.Deserialize(reader);
reader.Close();
Console.WriteLine($"Deserialized department: {deserialized}");
using System.Text;
using System.Xml.Serialization;

namespace Task1.XMLSerialization;

public record Department
{
    [XmlElement(ElementName = "name")]
    public string? DepartmentName { get; set; }
    
    public List<Employee>? Employees { get; set; }

    public override string ToString()
    {
        return new StringBuilder()
            .Append("")
            .Append($"{{DepartmentName = {DepartmentName}, Employees = {{ ")
            .Append($"{string.Join(", ", Employees)}")
            .Append("}}")
            .ToString();
    }
}
using System.Xml.Serialization;

namespace Task1.XMLSerialization;

public record Employee
{
    [XmlElement(ElementName = "Name")]
    public string EmployeeName { get; set; }
}
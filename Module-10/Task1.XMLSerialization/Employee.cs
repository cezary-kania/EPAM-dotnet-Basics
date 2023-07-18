using System.Xml.Serialization;

namespace Task1.XMLSerialization;

public record Employee
{
    [XmlElement(ElementName = "name")]
    public string EmployeeName { get; set; }
}
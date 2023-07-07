using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Task3;

[Serializable]
public record Department
{
    public string DepartmentName { get; set; }
    public List<Employee> Employees { get; set; }
    
    public override string ToString()
    {
        return new StringBuilder()
            .Append("")
            .Append($"{{DepartmentName = {DepartmentName}, Employees = {{ ")
            .Append($"{string.Join(", ", Employees)}")
            .Append("}}")
            .ToString();
    }
    
    public Department DeepClone()
    {
        using var stream = new MemoryStream();
        var formatter = new BinaryFormatter();
        formatter.Serialize(stream, this);
        stream.Position = 0;
        return (Department) formatter.Deserialize(stream);
    }
}
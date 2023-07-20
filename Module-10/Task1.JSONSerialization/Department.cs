using System.Text;
using System.Text.Json.Serialization;

namespace Task1.JSONSerialization;

public record Department
{
    [JsonPropertyName("name")]
    [JsonPropertyOrder(1)]
    public string DepartmentName { get; init; }
    
    [JsonPropertyOrder(2)]
    public List<Employee> Employees { get; init; }
    
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
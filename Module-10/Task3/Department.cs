using System.Text;
using System.Text.Json;

namespace Task3;

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
        var jsonString = JsonSerializer.Serialize(this);
        return JsonSerializer.Deserialize<Department>(jsonString)!;
    }
}
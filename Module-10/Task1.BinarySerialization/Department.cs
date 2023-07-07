using System.Text;

namespace Task1.BinarySerialization;

[Serializable]
public record Department(string DepartmentName, List<Employee> Employees)
{
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
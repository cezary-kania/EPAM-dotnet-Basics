using System.Text.Json.Serialization;

namespace Task1.JSONSerialization;

public record Employee
{
    [JsonPropertyName("Name")]
    public string EmployeeName { get; init; }
}
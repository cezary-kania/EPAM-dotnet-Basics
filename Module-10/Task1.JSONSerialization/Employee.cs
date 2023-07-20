using System.Text.Json.Serialization;

namespace Task1.JSONSerialization;

public record Employee
{
    [JsonPropertyName("name")]
    public string EmployeeName { get; init; }
}
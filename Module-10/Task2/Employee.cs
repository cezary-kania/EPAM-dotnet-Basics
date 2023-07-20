using System.Runtime.Serialization;

namespace Task2;

[Serializable]
public record Employee(int EmployeeId, string EmployeeName) : ISerializable
{
    public int EmployeeId { get; set; } = EmployeeId;
    public string EmployeeName { get; set; } = EmployeeName;

    public Employee(SerializationInfo info, StreamingContext context) 
        : this((int) info.GetValue(nameof(EmployeeId), typeof(int)), 
            (string) info.GetValue(nameof(EmployeeName), typeof(string)))
    {
    }
    
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue(nameof(EmployeeId), EmployeeId);
        info.AddValue(nameof(EmployeeName), EmployeeName);
    }
}
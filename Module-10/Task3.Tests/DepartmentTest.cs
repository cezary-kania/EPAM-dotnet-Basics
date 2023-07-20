using FluentAssertions;
using Xunit;

namespace Task3.Tests;

public class DepartmentTest
{
    [Fact]
    public void DeepClone_ShouldCreateDeepCopyWithSameValuesButDifferentReferences()
    {
        // Arrange
        var department = new Department
        {
            DepartmentName = "IT",
            Employees = new List<Employee>
            {
                new("Frodo Baggins"),
                new("Jack Daniels")
            }
        };
        
        // Act
        var clone = department.DeepClone();
        
        // Assert
        clone.Should().NotBeSameAs(department);
        clone.Should().BeEquivalentTo(department);
        
        clone.Employees.Should().NotBeSameAs(department.Employees);
        clone.Employees.Should().BeEquivalentTo(department.Employees);
    }
}
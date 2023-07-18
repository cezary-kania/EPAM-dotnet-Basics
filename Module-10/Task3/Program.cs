using Task3;

var department = new Department
{
    DepartmentName = "IT",
    Employees = new List<Employee>
    {
        new("Frodo Baggins"),
        new("Jack Daniels")
    }
};

var clonedDepartment = department.DeepClone();

clonedDepartment.DepartmentName = "HR";
clonedDepartment.Employees
    .Add(new Employee("Simon Jackson"));

Console.WriteLine(department);
Console.WriteLine(clonedDepartment);
    
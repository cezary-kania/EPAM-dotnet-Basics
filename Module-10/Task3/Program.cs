using Task3;

var department = new Department
{
    DepartmentName = "Sample department 1 ",
    Employees = new List<Employee>
    {
        new("SD-1 Employee 1"),
        new("SD-1 Employee 2")
    }
};

var clonedDepartment = department.DeepClone();

clonedDepartment.DepartmentName = "Updated Name";
clonedDepartment.Employees
    .Add(new Employee("SD-1 Employee 3"));

Console.WriteLine(department);
Console.WriteLine(clonedDepartment);
    
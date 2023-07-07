using System.Runtime.Serialization.Formatters.Binary;
using Task2;

var employee = new Employee(1, "John Doe");

const string fileName = "employee.dat";
var formatter = new BinaryFormatter();

var stream = File.OpenWrite(fileName);
formatter.Serialize(stream, employee);
stream.Close();

stream = File.OpenRead(fileName);
employee = (Employee) formatter.Deserialize(stream);
stream.Close();

Console.WriteLine(employee);



using OddEven;

var result = OddEvenNumberConverter.GetPrintableNumbers(0, 30);

foreach(var item in result)
{
    Console.Write($"\"{item}\", ");    
}
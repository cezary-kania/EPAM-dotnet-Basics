using System;

while (true)
{
    try
    {
        Console.WriteLine("Enter new line:");
        var input = Console.ReadLine();
        var firstCharacter = input[0];
        Console.WriteLine($"Output: {firstCharacter}");
    }
    catch (IndexOutOfRangeException ex)
    {
        Console.WriteLine("Error: Input cannot be empty.");
    }
}

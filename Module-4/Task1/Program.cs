using System;

namespace Task1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter new line:");
                    var input = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        throw new ArgumentException("Input cannot be empty.", nameof(input));
                    }
                    var firstCharacter = input[0];
                    Console.WriteLine($"Output: {firstCharacter}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}
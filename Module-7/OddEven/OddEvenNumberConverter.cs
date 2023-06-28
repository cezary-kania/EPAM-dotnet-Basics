namespace OddEven;

public static class OddEvenNumberConverter
{
    public static IEnumerable<string> GetPrintableNumbers(int start, int end)
    {
        if (start < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(start));
        }
        if (end < 0 || end < start)
        {
            throw new ArgumentOutOfRangeException(nameof(end));
        }

        return Enumerable.Range(start, end - start + 1)
            .Select(number => IsPrime(number) ? number.ToString() : number % 2 == 0 ? "Even" : "Odd");
    }

    private static bool IsPrime(int number)
    {
        if (number < 2)
        {
            return false;
        }

        for (var i = 2; i <= Math.Sqrt(number); i++)
        {
            if (number % i == 0)
            {
                return false;
            }
        }

        return true;
    }
}
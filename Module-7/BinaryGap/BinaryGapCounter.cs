namespace BinaryGap;

public static class BinaryGapCounter
{
    public static int Solution(int n)
    {
        var binary = Convert.ToString(n, 2);

        var maxGap = 0;
        var currentGap = 0;
        foreach (var digit in binary)
        {
            if (digit is '1')
            {
                maxGap = Math.Max(maxGap, currentGap);
                currentGap = 0;
            }
            else
            {
                currentGap++;
            }
        }
        return maxGap;
    }
}
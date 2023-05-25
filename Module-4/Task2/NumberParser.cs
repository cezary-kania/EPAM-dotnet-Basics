using System;

namespace Task2;

public class NumberParser : INumberParser
{
    private const string invalidFormatMessage = "Input string was not in a correct format.";
    public int Parse(string stringValue)
    {
        ArgumentNullException.ThrowIfNull(stringValue, nameof(stringValue));
        var trimmedStringValue = stringValue.Trim();
        if (trimmedStringValue.Length == 0)
        {
            throw new FormatException(invalidFormatMessage);
        }
        var firstChar = trimmedStringValue[0];
        var startIndex = char.IsNumber(firstChar) ? 0 : 1;
        var sign = firstChar is '-' ? -1 : 1;
        if (startIndex >= stringValue.Length)
        {
            throw new FormatException(invalidFormatMessage);
        }
        long result = 0;
        for (var i = startIndex; i < trimmedStringValue.Length; ++i)
        {
            if (trimmedStringValue[i] < '0' || trimmedStringValue[i] > '9')
            {
                throw new FormatException(invalidFormatMessage);
            }
            result = result * 10 + (trimmedStringValue[i] - '0');
            if (sign * result is > int.MaxValue or < int.MinValue)
            {
                throw new OverflowException("Value was either too large or too small for an Int32.");
            }
        }
        return sign * (int) result;
    }
}
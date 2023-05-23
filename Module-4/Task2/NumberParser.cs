using System;

namespace Task2
{
    public class NumberParser : INumberParser
    {
        private const string invalidFormatMessage = "Input string was not in a correct format.";
        public int Parse(string stringValue)
        {
            if (stringValue == null)
            {
                throw new ArgumentNullException(nameof(stringValue));
            }
            var trimmedStringValue = stringValue.Trim();
            if (trimmedStringValue.Length == 0)
            {
                throw new FormatException(invalidFormatMessage);
            }
            var isNegativeNumber = false;
            var startIndex = 0;
            var firstChar = trimmedStringValue[0];
            if (firstChar == '-' || firstChar == '+')
            {
                startIndex = 1;
                if (firstChar == '-')
                {
                    isNegativeNumber = true;
                }
            }
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

                if (!isNegativeNumber && result > int.MaxValue || isNegativeNumber && -result < int.MinValue)
                {
                    throw new OverflowException("Value was either too large or too small for an Int32.");
                }
            }
            return isNegativeNumber ? (int) -result : (int) result;
        }
    }
}
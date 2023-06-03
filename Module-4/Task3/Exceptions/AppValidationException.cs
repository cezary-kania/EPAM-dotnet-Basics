using System;

namespace Task3.Exceptions;

public class AppValidationException : Exception
{
    public AppValidationException(string message) : base(message)
    {
    }
}
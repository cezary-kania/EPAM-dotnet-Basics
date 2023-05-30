using System;

namespace Task3.Exceptions;

public class InvalidUserIdException : AppValidationException
{
    public InvalidUserIdException() : base("Invalid userId")
    {
    }
}
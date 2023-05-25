using System;

namespace Task3.Exceptions;

public class InvalidUserIdException : ArgumentException
{
    public InvalidUserIdException() : base("Invalid userId")
    {
    }
}
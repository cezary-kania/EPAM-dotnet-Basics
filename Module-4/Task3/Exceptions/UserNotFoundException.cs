using System;

namespace Task3.Exceptions;

public class UserNotFoundException : AppValidationException
{
    public UserNotFoundException() : base("User not found")
    {
    }
}
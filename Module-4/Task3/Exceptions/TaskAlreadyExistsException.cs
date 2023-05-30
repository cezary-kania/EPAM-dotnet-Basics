using System;

namespace Task3.Exceptions;

public class TaskAlreadyExistsException : AppValidationException
{
    public TaskAlreadyExistsException() : base("The task already exists")
    {
    }
}
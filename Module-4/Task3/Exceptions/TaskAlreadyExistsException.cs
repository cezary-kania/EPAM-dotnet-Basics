using System;

namespace Task3.Exceptions
{
    public class TaskAlreadyExistsException : Exception
    {
        public TaskAlreadyExistsException() : base("The task already exists")
        {
        }
    }
}
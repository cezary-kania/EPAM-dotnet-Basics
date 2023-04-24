using System;
using System.Collections.Generic;
using System.Text;

namespace Task2_GreetingLibrary
{
    public class GreetingService : IGreetingService
    {
        public string GetMessage(string username)
            => $"{DateTime.Now:HH:mm:ss} Hello, {username}!";
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Task2_GreetingLibrary
{
    public class ConcatenationService : IConcatenationService
    {
        public string GetMessage(DateTime currentTime, string username)
            => $"{currentTime.ToString("HH:mm:ss")} Hello, {username}!";
    }
}

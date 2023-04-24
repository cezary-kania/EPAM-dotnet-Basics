using System;
using System.Collections.Generic;
using System.Text;

namespace Task2_GreetingLibrary
{
    public interface IGreetingService
    {
        string GetMessage(string username);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Task2_GreetingLibrary
{
    public interface IConcatenationService
    {
        string GetMessage(DateTime currentTime, string username);
    }
}

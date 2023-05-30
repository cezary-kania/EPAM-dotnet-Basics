using System.Collections.Generic;
using Task3.DoNotChange;

namespace Task3.Tests.Stubs;

internal class UserStab : IUser
{
    public IList<UserTask> Tasks { get; } = new List<UserTask>
    {
        new("task1"),
        new("task2"),
        new("task3")
    };
}
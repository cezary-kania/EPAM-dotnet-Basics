using System.Collections.Generic;
using Task3.DoNotChange;

namespace Task3.Tests.Stubs;

internal class UserDaoStub : IUserDao
{
    private readonly IDictionary<int, IUser> _data = new Dictionary<int, IUser>
    {
        { 1, new UserStab() }
    };

    public IUser GetUser(int id)
    {
        return _data.TryGetValue(id, out var value) ? value : null;
    }
}
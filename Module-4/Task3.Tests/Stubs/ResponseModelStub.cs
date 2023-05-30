using System.Collections.Generic;
using Task3.DoNotChange;

namespace Task3.Tests.Stubs;

internal class ResponseModelStub : IResponseModel
{
    private readonly IDictionary<string, string> _data = new Dictionary<string, string>();

    public void AddAttribute(string key, string value)
    {
        _data.Add(key, value);
    }

    public string GetAttribute(string key)
    {
        return _data[key];
    }

    public string GetActionResult()
    {
        return _data.TryGetValue("action_result", out var value) ? value : null;
    }
}
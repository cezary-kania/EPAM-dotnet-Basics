namespace Reflection.Providers;

public interface IConfigurationProvider
{
    void SaveSetting(string key, object? value);
    object? LoadSetting(string key);
}
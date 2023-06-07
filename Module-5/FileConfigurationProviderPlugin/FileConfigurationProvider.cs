using Newtonsoft.Json;
using Reflection.Providers;

namespace FileConfigurationProviderPlugin;

public class FileConfigurationProvider : IConfigurationProvider
{
    private readonly string _filePath = Path.Combine(Environment.CurrentDirectory, "appsettings.json");

    public void SaveSetting(string settingName, object? value)
    {
        var settings = LoadSettings();
        if (settings is null)
        {
            return;
        }
        settings[settingName] = value;
        
        var json = JsonConvert.SerializeObject(settings);
        File.WriteAllText(_filePath, json);
    }

    public object? LoadSetting(string settingName)
    {
        var settings = LoadSettings();
        if (settings is null)
        {
            return null;
        }
        return settings.TryGetValue(settingName, out var value) ? value : null;
    }

    private Dictionary<string, object>? LoadSettings()
    {
        var json = File.ReadAllText(_filePath);
        return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
    }
}
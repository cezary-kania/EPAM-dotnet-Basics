using Reflection.Providers;
using System.Configuration;

namespace ConfigurationManagerConfigurationProviderPlugin;

public class ConfigurationManagerConfigurationProvider : IConfigurationProvider
{
    public void SaveSetting(string key, object? value)
    {
        var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        config.AppSettings.Settings[key].Value = value?.ToString();
        config.Save(ConfigurationSaveMode.Modified);
        ConfigurationManager.RefreshSection("appSettings");
    }

    public object? LoadSetting(string key)
    {
        return ConfigurationManager.AppSettings[key];
    }
}
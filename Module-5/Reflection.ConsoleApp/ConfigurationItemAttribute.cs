namespace Reflection.ConsoleApp;

[AttributeUsage(AttributeTargets.Property)]
public class ConfigurationItemAttribute : Attribute
{
    public string SettingName { get; }
    public string ProviderName { get; }
    
    public ConfigurationItemAttribute(string settingName, string providerName)
    {
        SettingName = settingName;
        ProviderName = providerName;
    }   
}
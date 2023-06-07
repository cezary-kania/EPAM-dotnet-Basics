namespace Reflection.ConsoleApp;

public class ExternalServiceConfigurationFromManager : ConfigurationComponentBase
{
    [ConfigurationItem("ApiKey", "ConfigurationManagerConfigurationProvider")]
    public string ApiKey { get; set; }
    
    [ConfigurationItem("MaxRetryAttempts", "ConfigurationManagerConfigurationProvider")]
    public int MaxRetryAttempts { get; set; }
    
    [ConfigurationItem("Timeout", "ConfigurationManagerConfigurationProvider")]
    public TimeSpan Timeout { get; set; }
    
    [ConfigurationItem("SomeFloatSetting", "ConfigurationManagerConfigurationProvider")]
    public float SomeFloatSetting { get; set; }
}
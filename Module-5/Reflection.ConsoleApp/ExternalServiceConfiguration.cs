namespace Reflection.ConsoleApp;

public class ExternalServiceConfiguration : ConfigurationComponentBase
{
    [ConfigurationItem("ApiKey", "FileConfigurationProvider")]
    public string ApiKey { get; set; }
    
    [ConfigurationItem("MaxRetryAttempts", "FileConfigurationProvider")]
    public int MaxRetryAttempts { get; set; }
    
    [ConfigurationItem("Timeout", "FileConfigurationProvider")]
    public TimeSpan Timeout { get; set; }
    
    [ConfigurationItem("SomeFloatSetting", "FileConfigurationProvider")]
    public float SomeFloatSetting { get; set; }
}
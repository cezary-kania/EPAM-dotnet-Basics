using Reflection.ConsoleApp;

var config = new ExternalServiceConfiguration();

config.LoadSettings();
Console.WriteLine(config.ApiKey);
Console.WriteLine(config.Timeout);

config.ApiKey = "ABC";
config.Timeout = TimeSpan.FromHours(15);
config.SaveSettings();

// Manager
var managerConfig = new ExternalServiceConfigurationFromManager();
managerConfig.LoadSettings();
Console.WriteLine(managerConfig.ApiKey);
Console.WriteLine(managerConfig.Timeout);

managerConfig.MaxRetryAttempts = 15;
managerConfig.SaveSettings();

Console.ReadKey();
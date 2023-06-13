using System.Reflection;
using Reflection.ConsoleApp.Exceptions;
using Reflection.Providers;

namespace Reflection.ConsoleApp;

public class ConfigurationComponentBase
{
    Dictionary<string, IConfigurationProvider> _configProviders = new();

    private Dictionary<Type, Func<object,object>> _customTypeConverters = new()
    {
        { typeof(TimeSpan), value => TimeSpan.Parse((string) value) }
    };

    protected ConfigurationComponentBase()
    {
        foreach (var file in Directory.GetFiles(@".\Plugins", "*.dll"))
        {
            var asm = Assembly.LoadFrom(Directory.GetCurrentDirectory() + file);
            foreach (var type in asm.GetTypes())
            {
                if (type.IsAssignableTo(typeof(IConfigurationProvider)))
                {
                    var configProvider = Activator.CreateInstance(type) as IConfigurationProvider;
                    _configProviders.Add(type.Name, configProvider);
                }
            }
        }
    }
    public void SaveSettings()
    {
        var properties = GetType().GetProperties();
        foreach (var property in properties)
        {
            var configItemAttribute = property.GetCustomAttribute<ConfigurationItemAttribute>();
            if (configItemAttribute is null)
            {
                continue;
            }
            if(!_configProviders.TryGetValue(configItemAttribute.ProviderName, out var provider))
            {
                throw new InvalidConfigurationProviderException();
            }
            provider.SaveSetting(configItemAttribute.SettingName, property.GetValue(this));
        }
    }
    
    public void LoadSettings()
    {
        var properties = GetType().GetProperties();
        foreach (var property in properties)
        {
            var configItemAttribute = property.GetCustomAttribute<ConfigurationItemAttribute>();
            if (configItemAttribute is null)
            {
                continue;
            }

            if (!_configProviders.TryGetValue(configItemAttribute.ProviderName, out var provider))
            {
                throw new InvalidConfigurationProviderException();
            }
            var value = provider.LoadSetting(configItemAttribute.SettingName);
            var convertedValue = GetConvertedValue(property.PropertyType, value);
            property.SetValue(this, convertedValue);
        }
    }
    private object? GetConvertedValue(Type propertyType, object value)
    {
        return _customTypeConverters.TryGetValue(propertyType, out var converter) 
            ? converter.Invoke(value) : Convert.ChangeType(value, propertyType);
    }
}
namespace Reflection.ConsoleApp.Exceptions;

public class InvalidConfigurationProviderException : Exception
{
    public InvalidConfigurationProviderException() : base("Configuration Provider not found")
    {
    }
}
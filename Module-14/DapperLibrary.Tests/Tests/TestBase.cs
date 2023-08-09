using Dapper;
using DapperLibrary.Repositories;
using DapperLibrary.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DapperLibrary.Tests.Tests;

public class TestBase : IDisposable
{
    private readonly string _connectioonString;
    protected ServiceProvider ServiceProvider;
    protected TestBase()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.test.json")
            .Build();

        _connectioonString = configuration.GetConnectionString("Default");
        
        var services = new ServiceCollection();
        services.AddScoped<IOrderRepository, OrderRepository>(_ => new OrderRepository(_connectioonString));
        services.AddScoped<IProductRepository, ProductRepository>(_ => new ProductRepository(_connectioonString));
        
        ServiceProvider = services.BuildServiceProvider();
        DbInitializer.Initialize(_connectioonString);
    }
    
    protected int GetLastOrderId()
    {
        using var connection = new SqlConnection(_connectioonString);
        var result = (int) connection.ExecuteScalar("SELECT MAX(Id) FROM [dbo].[Order]");
        connection.Close();
        return result;
    }
    
    protected int GetLastProductId()
    {
        using var connection = new SqlConnection(_connectioonString);
        var result = (int) connection.ExecuteScalar("SELECT MAX(Id) FROM [dbo].[Product]");
        connection.Close();
        return result;
    }

    public void Dispose()
    {
        using var connection = new SqlConnection(_connectioonString);
        connection.Execute("DELETE FROM [dbo].[Order]");
        connection.Execute("DELETE FROM [dbo].[Product]");
        connection.Close();
    }
}
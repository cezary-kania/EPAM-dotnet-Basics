using Azure.Core;
using EFLibrary.Contexts;
using EFLibrary.Repositories;
using EFLibrary.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EFLibrary.Tests.Tests;

public class TestBase : IDisposable
{
    protected ServiceProvider ServiceProvider;
    protected TestBase()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.test.json")
            .Build();
        
        var services = new ServiceCollection();
        services.AddDbContext<LibraryDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Default")));
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        
        ServiceProvider = services.BuildServiceProvider();
        var context = ServiceProvider.GetRequiredService<LibraryDbContext>();
        DbInitializer.Initialize(context);
    }
    
    protected int GetLastOrderId()
    {
        var context = ServiceProvider.GetRequiredService<LibraryDbContext>();
        return context.Orders.Max(order => order.Id);
    }
    
    protected int GetLastProductId()
    {
        var context = ServiceProvider.GetRequiredService<LibraryDbContext>();
        return context.Products.Max(product => product.Id);
    }

    public void Dispose()
    {
        var context = ServiceProvider.GetRequiredService<LibraryDbContext>();
        context.Orders.ExecuteDelete();
        context.Products.ExecuteDelete();
    }
}
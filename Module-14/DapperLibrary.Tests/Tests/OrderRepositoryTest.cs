using DapperLibrary.Models;
using DapperLibrary.Repositories.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace DapperLibrary.Tests.Tests;

public class OrderRepositoryTest : TestBase
{
    [Fact]
    public void GetFiltered_ShouldReturnAllRecords_WhenNoFiltersProvided()
    {
        // Arrange
        var repository = ServiceProvider.GetRequiredService<IOrderRepository>();
        
        // Act
        var result = repository.GetFiltered();
        
        // Assert
        Assert.NotEmpty(result);
    }
    
    [Theory]
    [InlineData("Loading", false)]
    [InlineData("NotStarted", true)]
    public void GetFiltered_ShouldReturnAllRecordsWithGivenStatus(string status, bool expected)
    {
        // Arrange
        var repository = ServiceProvider.GetRequiredService<IOrderRepository>();
        
        // Act
        var result = repository.GetFiltered(status: status);
        
        // Assert
        Assert.Equal(expected, result.Any());
    }
    
    [Theory]
    [InlineData("Loading", -1)]
    public void DeleteFiltered_ShouldDeleteAllRecordsWithGivenStatus(string status, int expected)
    {
        // Arrange
        var repository = ServiceProvider.GetRequiredService<IOrderRepository>();
        
        // Act
        var result = repository.DeleteFiltered(status: status);
        
        // Assert
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void Get_ShouldReturnOrderData_WhenGivenIdIsValid()
    {
        // Arrange
        var repository = ServiceProvider.GetRequiredService<IOrderRepository>();
        var orderId = GetLastOrderId();
        
        // Act
        var result = repository.Get(orderId);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void Create_ShouldInsertNewOrder()
    {
        // Arrange
        var repository = ServiceProvider.GetRequiredService<IOrderRepository>();
        var order = new Order
        {
            Status = "NotStarted",
            CreatedDate = DateTime.Today,
            UpdatedDate = DateTime.Today,
            ProductId = GetLastProductId()
        };
        
        // Act
        var id = repository.Create(order);
        var insertedOrder = repository.Get(id);
        // Assert
        insertedOrder.Should()
            .BeEquivalentTo(order, options => options.Excluding(o => o.Id));
    }
    
    [Fact]
    public void Update_ShouldUpdateOrderData()
    {
        // Arrange
        var repository = ServiceProvider.GetRequiredService<IOrderRepository>();
        var orderId = GetLastOrderId();
        const string newStatus = "InProgress";
        var order = repository.Get(orderId);
        order.Status = newStatus;
        
        // Act
        repository.Update(order);
        
        // Assert
        var updatedOrder = repository.Get(orderId);
        Assert.Equal(newStatus, updatedOrder.Status);
    }
    
    [Fact]
    public void Delete_ShouldDeleteOrderWithGivenId()
    {
        // Arrange
        var repository = ServiceProvider.GetRequiredService<IOrderRepository>();
        var orderId = GetLastOrderId();
        
        // Act
        repository.Delete(orderId);
        
        // Assert
        var deletedOrder = repository.Get(orderId);
        Assert.Null(deletedOrder);
    }
}
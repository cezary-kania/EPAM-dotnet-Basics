using Library.DAL;
using Library.Models;

namespace Library.Test.Tests;

public class OrderDataAccessTest : TestBase
{
    private OrderDataAccess _orderDataAccess;

    public OrderDataAccessTest()
    {
        _orderDataAccess = new OrderDataAccess(ConnectionString);
    }
    
    [Fact]
    public void GetFiltered_ShouldReturnAllRecords_WhenNoFiltersProvided()
    {
        // Act
        var result = _orderDataAccess.GetFiltered();
        
        // Assert
        Assert.NotEmpty(result);
    }
    
    [Theory]
    [InlineData("Loading", false)]
    [InlineData("NotStarted", true)]
    public void GetFiltered_ShouldReturnAllRecordsWithGivenStatus(string status, bool expected)
    {
        // Act
        var result = _orderDataAccess.GetFiltered(status: status);
        
        // Assert
        Assert.Equal(expected, result.Any());
    }
    
    [Theory]
    [InlineData("Loading", -1)]
    public void DeleteFiltered_ShouldDeleteAllRecordsWithGivenStatus(string status, int affectedRows)
    {
        // Act
        var result = _orderDataAccess.DeleteFiltered(status: status);
        
        // Assert
        Assert.Equal(affectedRows, result);
    }

    [Fact]
    public void GetById_ShouldReturnOrderData_WhenGivenIdIsValid()
    {
        // Act
        var orderId = GetLastOrderId();
        var result = _orderDataAccess.GetById(orderId);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void Create_ShouldInsertNewOrder()
    {
        // Arrange
        var product = new Order
        {
            Status = "NotStarted",
            CreatedDate = DateTime.Today,
            UpdatedDate = DateTime.Today,
            ProductId = GetLastProductId()
        };
        
        // Act
        var result = _orderDataAccess.Create(product);
        
        // Assert
        Assert.Equal(1, result);
    }
    
    [Fact]
    public void Update_ShouldUpdateOrderData()
    {
        // Arrange
        var orderId = GetLastOrderId();
        const string newStatus = "InProgress";
        var order = _orderDataAccess.GetById(orderId);
        order.Status = newStatus;
        
        // Act
        var result = _orderDataAccess.Update(order);
        
        // Assert
        Assert.Equal(1, result);
        var updatedOrder = _orderDataAccess.GetById(orderId);
        Assert.Equal(newStatus, updatedOrder.Status);
    }
    
    [Fact]
    public void Delete_ShouldDeleteProductWithGivenId()
    {
        // Arrange
        var orderId = GetLastOrderId();
        
        // Act
        var result = _orderDataAccess.Delete(orderId);
        
        // Assert
        Assert.Equal(1, result);
        
        var deletedOrder = _orderDataAccess.GetById(orderId);
        Assert.Null(deletedOrder);
    }

    private int GetLastOrderId()
    {
        using var connection = new SqlConnection(ConnectionString);
        connection.Open();
        var command = new SqlCommand("SELECT MAX(Id) FROM [dbo].[Order]", connection);
        return (int) command.ExecuteScalar();
    }
    
    private int GetLastProductId()
    {
        using var connection = new SqlConnection(ConnectionString);
        connection.Open();
        var command = new SqlCommand("SELECT MAX(Id) FROM [dbo].[Product]", connection);
        return (int) command.ExecuteScalar();
    }
}
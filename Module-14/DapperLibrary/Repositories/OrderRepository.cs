using System.Data;
using Dapper;
using DapperLibrary.Models;
using DapperLibrary.Repositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace DapperLibrary.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly string _connectioonString;
    
    public OrderRepository(string connectioonString)
    {
        _connectioonString = connectioonString;
    }
    
    public Order? Get(int orderId)
    {
        using var connection = new SqlConnection(_connectioonString);
        const string sql = "SELECT * FROM [dbo].[Order] WHERE Id = @OrderId";
        var result = connection.QueryFirstOrDefault<Order>(sql, new { OrderId = orderId });
        connection.Close();
        return result;
    }

    public int Create(Order order)
    {
        using var connection = new SqlConnection(_connectioonString);
        const string sql = @"INSERT INTO [dbo].[Order] (Status, CreatedDate, UpdatedDate, ProductId) VALUES (@Status, @CreatedDate, @UpdatedDate, @ProductId);
        SELECT CAST(SCOPE_IDENTITY() AS INT)";
        var orderId = connection.Query<int>(sql, order).Single();
        connection.Close();
        return orderId;
    }

    public void Update(Order order)
    {
        using var connection = new SqlConnection(_connectioonString);
        const string sql = "UPDATE [dbo].[Order] SET Status=@Status, CreatedDate=@CreatedDate, UpdatedDate=@UpdatedDate, ProductId=@ProductId WHERE Id = @Id";
        connection.Execute(sql, order);
        connection.Close();
    }

    public void Delete(int orderId)
    {
        using var connection = new SqlConnection(_connectioonString);
        const string sql = "DELETE FROM [dbo].[Order] WHERE Id = @OrderId";
        connection.Execute(sql, new { OrderId = orderId });
        connection.Close();
    }

    public List<Order> GetFiltered(int month = -1, string status = "", int year = -1, int productId = -1)
    {
        using var connection = new SqlConnection(_connectioonString);
        const string sql = "dbo.spGetOrders";
        var parameters = GetOptionalOrderFilters(month, status, year, productId);
        var result = connection.Query<Order>(sql, parameters, commandType: CommandType.StoredProcedure)
            .ToList();
        connection.Close();
        return result;
    }

    public int DeleteFiltered(int month = -1, string status = "", int year = -1, int productId = -1)
    {
        using var connection = new SqlConnection(_connectioonString);
        const string sql = "dbo.spDeleteOrders";
        var parameters = GetOptionalOrderFilters(month, status, year, productId);
        var result = connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
        connection.Close();
        return result;
    }
    
    private static DynamicParameters GetOptionalOrderFilters(int month, string status, int year, int productId)
    {
        var parameters = new DynamicParameters();
        if (month != -1)
        {
            parameters.Add("@Month", month);
        }
        
        if (!string.IsNullOrWhiteSpace(status))
        {
            parameters.Add("@Status", status);
        }
        
        if (year != -1)
        {
            parameters.Add("@Year", year);
        }
        
        if (productId != -1)
        {
            parameters.Add("@ProductId", productId);
        }

        return parameters;
    }
}
using EFLibrary.Contexts;
using EFLibrary.Models;
using EFLibrary.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EFLibrary.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly LibraryDbContext _dbContext;

    public OrderRepository(LibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Order? Get(int orderId) 
        => _dbContext.Orders.FirstOrDefault(order => order.Id == orderId);

    public int Create(Order order)
    {
        _dbContext.Orders.Add(order);
        _dbContext.SaveChanges();
        return order.Id;
    }

    public void Update(Order order)
    {
        _dbContext.Orders.Update(order);
        _dbContext.SaveChanges();
    }

    public void Delete(int orderId)
    {
        var order = Get(orderId);
        if (order is null)
        {
            return;
        }
        _dbContext.Orders.Remove(order);
        _dbContext.SaveChanges();
    }

    public List<Order> GetFiltered(int month = -1, string status = "", int year = -1, int productId = -1)
    {
        var parameters = GetStoredProcedureParameters(month, status, year, productId);
        var sql = $"EXECUTE dbo.spGetOrders {string.Join(',', parameters)}";

        return _dbContext.Orders.FromSqlRaw(sql)
            .ToList();
    }

    public int DeleteFiltered(int month = -1, string status = "", int year = -1, int productId = -1)
    {
        var parameters = GetStoredProcedureParameters(month, status, year, productId);
        var sql = $"EXECUTE dbo.spDeleteOrders {string.Join(',', parameters)}";
        return _dbContext.Database.ExecuteSqlRaw(sql);
    }

    private static List<string> GetStoredProcedureParameters(int month = -1, string status = "", int year = -1, int productId = -1)
    {
        var parameters = new List<string>();

        if (month != -1)
        {
            parameters.Add($"@Month='{month}'");
        }
        if (!string.IsNullOrEmpty(status))
        {
            parameters.Add($"@Status='{status}'");
        }
        if (year != -1)
        {
            parameters.Add($"@Year='{year}'");
        }
        if (productId != -1)
        {
            parameters.Add($"@ProductId='{productId}'");
        }

        return parameters;
    }
}
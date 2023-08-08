using System.Data;
using System.Data.SqlClient;
using Library.Models;

namespace Library.DAL;

public class OrderDataAccess
{
    private readonly string _connectionString;

    public OrderDataAccess(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public Order? GetById(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        var command = new SqlCommand("SELECT * FROM [dbo].[Order] WHERE Id = @id", connection);
        command.Parameters.AddWithValue("@id", id);
        var reader = command.ExecuteReader(CommandBehavior.SingleRow);
        if (reader.Read())
        {
            return new Order
            {
                Id = Convert.ToInt32(reader["Id"]),
                Status = reader["Status"].ToString(),
                CreatedDate = DateTime.Parse(reader["CreatedDate"].ToString()),
                UpdatedDate = DateTime.Parse(reader["UpdatedDate"].ToString()),
                ProductId = Convert.ToInt32(reader["ProductId"])
            };
        }
        return null;
    }

    public int Create(Order order)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var command = new SqlCommand("INSERT INTO [dbo].[Order] (Status, CreatedDate, UpdatedDate, ProductId) VALUES (@Status, @CreatedDate, @UpdatedDate, @ProductId)", connection);
        command.Parameters.AddWithValue("@Status", order.Status);
        command.Parameters.AddWithValue("@CreatedDate", order.CreatedDate);
        command.Parameters.AddWithValue("@UpdatedDate", order.UpdatedDate);
        command.Parameters.AddWithValue("@ProductId", order.ProductId);
        return command.ExecuteNonQuery();
    }
    
    public int Update(Order order)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var command = new SqlCommand("UPDATE [dbo].[Order] SET Status=@Status, CreatedDate=@CreatedDate, UpdatedDate=@UpdatedDate, ProductId=@ProductId WHERE Id = @Id", connection);

        command.Parameters.AddWithValue("@Status", order.Status);
        command.Parameters.AddWithValue("@CreatedDate", order.CreatedDate);
        command.Parameters.AddWithValue("@UpdatedDate", order.UpdatedDate);
        command.Parameters.AddWithValue("@ProductId", order.ProductId);
        command.Parameters.AddWithValue("@Id", order.Id);

        return command.ExecuteNonQuery();
    }
    
    public int Delete(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var command = new SqlCommand("DELETE FROM [dbo].[Order] WHERE Id = @id", connection);
        command.Parameters.AddWithValue("@id", id);
        return command.ExecuteNonQuery();
    }

    public IEnumerable<Order> GetFiltered(int month = -1, string status = "", int year = -1, int productId = -1)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var command = new SqlCommand("dbo.spGetOrders", connection);
        command.CommandType = CommandType.StoredProcedure;
        AddOptionalOrderFilters(command, month, status, year, productId);

        var reader = command.ExecuteReader();
        var result = new List<Order>();

        while (reader.Read())
        {
            var order = new Order
            {
                Id = Convert.ToInt32(reader["Id"]),
                Status = reader["Status"].ToString(),
                CreatedDate = DateTime.Parse(reader["CreatedDate"].ToString()),
                UpdatedDate = DateTime.Parse(reader["UpdatedDate"].ToString()),
                ProductId = Convert.ToInt32(reader["ProductId"])
            };
            result.Add(order);
        }

        return result;
    }
    
    public int DeleteFiltered(int month = -1, string status = "", int year = -1, int productId = -1)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        var transaction = connection.BeginTransaction();
        int result;
        try
        {
            var command = new SqlCommand("dbo.spDeleteOrders", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = transaction;
            AddOptionalOrderFilters(command, month, status, year, productId);
            result = command.ExecuteNonQuery();
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw;
        }

        return result;
    }

    private static void AddOptionalOrderFilters(SqlCommand command, int month, string status, int year, int productId)
    {
        if (month != -1)
        {
            command.Parameters.AddWithValue("@Month", month);
        }
        
        if (!string.IsNullOrWhiteSpace(status))
        {
            command.Parameters.AddWithValue("@Status", status);
        }
        
        if (year != -1)
        {
            command.Parameters.AddWithValue("@Year", year);
        }
        
        if (productId != -1)
        {
            command.Parameters.AddWithValue("@ProductId", productId);
        }
    }
}
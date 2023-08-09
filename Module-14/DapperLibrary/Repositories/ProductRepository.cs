using Dapper;
using DapperLibrary.Models;
using DapperLibrary.Repositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace DapperLibrary.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly string _connectioonString;
    
    public ProductRepository(string connectioonString)
    {
        _connectioonString = connectioonString;
    }
    public Product? Get(int productId)
    {
        using var connection = new SqlConnection(_connectioonString);
        const string sql = "SELECT * FROM [dbo].[Product] WHERE Id = @ProductId";
        var result = connection.QueryFirstOrDefault<Product>(sql, new { ProductId = productId });
        connection.Close();
        return result;
    }

    public List<Product> GetAll()
    {
        using var connection = new SqlConnection(_connectioonString);
        const string sql = "SELECT * FROM [dbo].[Product]";
        var result = connection.Query<Product>(sql).ToList();
        connection.Close();
        return result;
    }

    public int Create(Product product)
    {
        using var connection = new SqlConnection(_connectioonString);
        const string sql = @"INSERT INTO [dbo].[Product] (Name, Description, Weight, Height, Width, Length) VALUES (@Name, @Description, @Weight, @Height, @Width, @Length);
        SELECT CAST(SCOPE_IDENTITY() AS INT)";
        var productId = connection.Query<int>(sql, product).Single();
        connection.Close();
        return productId;
    }

    public void Update(Product product)
    {
        using var connection = new SqlConnection(_connectioonString);
        const string sql = "UPDATE [dbo].[Product] SET Name=@Name, Description=@Description, Weight=@Weight, Height=@Height, Width=@Width, Length=@Length WHERE Id = @Id";
        connection.Execute(sql, product);
        connection.Close();
    }

    public void Delete(int productId)
    {
        using var connection = new SqlConnection(_connectioonString);
        const string sql = "DELETE FROM [dbo].[Product] WHERE Id = @ProductId";
        connection.Execute(sql, new { ProductId = productId });
        connection.Close();
    }
}
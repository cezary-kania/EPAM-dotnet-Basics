using System.Data;
using System.Data.SqlClient;
using Library.Models;

namespace Library.DAL;

public class ProductDataAccess
{
    private readonly string _connectionString;

    public ProductDataAccess(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DataSet GetAll()
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        var dataAdapter = new SqlDataAdapter("SELECT * FROM [dbo].[Product]", connection);
        var result = new DataSet();
        dataAdapter.Fill(result, "Product");
        return result;
    }
    
    public Product? GetById(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        var command = new SqlCommand("SELECT * FROM [dbo].[Product] WHERE Id = @id", connection);
        command.Parameters.AddWithValue("@id", id);
        var reader = command.ExecuteReader(CommandBehavior.SingleRow);
        if (reader.Read())
        {
            return new Product
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString(),
                Description = reader["Description"].ToString(),
                Weight = Convert.ToInt32(reader["Weight"]),
                Height = Convert.ToInt32(reader["Height"]),
                Width = Convert.ToInt32(reader["Width"]),
                Length = Convert.ToInt32(reader["Length"]),
            };
        }
        return null;
    }

    public int Create(Product product)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var command = new SqlCommand("INSERT INTO [dbo].[Product] (Name, Description, Weight, Height, Width, Length) VALUES (@Name, @Description, @Weight, @Height, @Width, @Length)", connection);

        command.Parameters.AddWithValue("@Name", product.Name);
        command.Parameters.AddWithValue("@Description", product.Description);
        command.Parameters.AddWithValue("@Weight", product.Weight);
        command.Parameters.AddWithValue("@Height", product.Height);
        command.Parameters.AddWithValue("@Width", product.Width);
        command.Parameters.AddWithValue("@Length", product.Length);

        return command.ExecuteNonQuery();
    }
    public int Update(Product product)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var command = new SqlCommand("UPDATE [dbo].[Product] SET Name=@Name, Description=@Description, Weight=@Weight, Height=@Height, Width=@Width, Length=@Length WHERE Id = @Id", connection);

        command.Parameters.AddWithValue("@Name", product.Name);
        command.Parameters.AddWithValue("@Description", product.Description);
        command.Parameters.AddWithValue("@Weight", product.Weight);
        command.Parameters.AddWithValue("@Height", product.Height);
        command.Parameters.AddWithValue("@Width", product.Width);
        command.Parameters.AddWithValue("@Length", product.Length);
        command.Parameters.AddWithValue("@Id", product.Id);

        return command.ExecuteNonQuery();
    }
    
    public int Delete(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var command = new SqlCommand("DELETE FROM [dbo].[Product] WHERE Id = @id", connection);
        command.Parameters.AddWithValue("@id", id);
        return command.ExecuteNonQuery();
    }
}
using System.Data;

namespace Library.Test.Tests;

public abstract class TestBase : IDisposable
{
    protected string ConnectionString;
    
    protected TestBase()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.test.json")
            .Build();
        ConnectionString = configuration.GetConnectionString("Default");
        SeedData();
    }

    private void SeedData()
    {
        SeedProducts();
        SeedOrders();
    }

    private void SeedProducts()
    {
        using var connection = new SqlConnection(ConnectionString);
        var dataAdapter = new SqlDataAdapter("SELECT * FROM [dbo].[Product]", connection);
        var dataSet = new DataSet();
        dataAdapter.Fill(dataSet, "Product");

        var productsTable = dataSet.Tables["Product"];
        foreach (var product in TestData.Products)
        {
            var newRow = productsTable.NewRow();
            newRow["Id"] = product.Id;
            newRow["Name"] = product.Name;
            newRow["Description"] = product.Description;
            newRow["Weight"] = product.Weight;
            newRow["Height"] = product.Height;
            newRow["Width"] = product.Width;
            newRow["Length"] = product.Length;
            productsTable.Rows.Add(newRow);
        }
        
        var builder = new SqlCommandBuilder(dataAdapter);
        builder.GetInsertCommand();
        dataAdapter.Update(dataSet, "Product");
    }

    private void SeedOrders()
    {
        using var connection = new SqlConnection(ConnectionString);
        var dataAdapter = new SqlDataAdapter("SELECT * FROM [dbo].[Order]", connection);
        var dataSet = new DataSet();
        dataAdapter.Fill(dataSet, "Order");

        var ordersTable = dataSet.Tables["Order"];
        var productIds = GetProductIds();
        var random = new Random();
        foreach (var order in TestData.Orders)
        {
            var newRow = ordersTable.NewRow();
            newRow["Status"] = order.Status;
            newRow["CreatedDate"] = order.CreatedDate;
            newRow["UpdatedDate"] = order.UpdatedDate;
            newRow["ProductId"] = productIds[random.Next(productIds.Count())];
            ordersTable.Rows.Add(newRow);
        }
        var builder = new SqlCommandBuilder(dataAdapter);
        builder.GetInsertCommand();
        dataAdapter.Update(dataSet, "Order");
    }

    private List<int> GetProductIds()
    {
        using var connection = new SqlConnection(ConnectionString);
        var dataAdapter = new SqlDataAdapter("SELECT Id FROM [dbo].[Product]", connection);
        var dataSet = new DataSet();
        dataAdapter.Fill(dataSet, "Product");
        return dataSet.Tables["Product"].Rows
            .Cast<DataRow>()
            .Select(row => (int)row["Id"])
            .ToList();
    }

    private void ClearTable(string tableName)
    {
        using var connection = new SqlConnection(ConnectionString);
        connection.Open();

        var command = new SqlCommand($"DELETE FROM {tableName}", connection);
        command.ExecuteNonQuery();
        
        connection.Close();
    }
    
    public void Dispose()
    {
        ClearTable("[dbo].[Order]");
        ClearTable("[dbo].[Product]");
    }
}
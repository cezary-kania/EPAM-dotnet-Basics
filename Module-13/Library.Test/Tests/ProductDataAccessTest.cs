using Library.DAL;
using Library.Models;

namespace Library.Test.Tests;

public class ProductDataAccessTest : TestBase
{
    private ProductDataAccess _productDataAccess;

    public ProductDataAccessTest() : base()
    {
        _productDataAccess = new ProductDataAccess(ConnectionString);
    }
    
    [Fact]
    public void GetById_ShouldReturnProductData_WhenGivenIdIsValid()
    {
        // Act
        var productId = GetLastProductId();
        var result = _productDataAccess.GetById(productId);
        
        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void GetAll_ShouldReturnAllProducts()
    {
        // Act
        var result = _productDataAccess.GetAll();
        
        // Assert
        var rows = result.Tables["Product"].Rows;
        
        Assert.NotEmpty(rows);
    }
    
    [Fact]
    public void Create_ShouldInsertNewProduct()
    {
        // Arrange
        var product = new Product
        {
            Name = "LOBERGET Swivel chair",
            Description = "For designer Carl Öjerstam, the challenge was to design a comfortable desk chair using just 1 kg of material.",
            Weight = 5,
            Height = 75,
            Width = 20,
            Length = 1,
        };
        
        // Act
        var result = _productDataAccess.Create(product);
        
        // Assert
        Assert.Equal(1, result);
    }
    
    [Fact]
    public void Update_ShouldUpdateProductData()
    {
        // Arrange
        var productId = GetLastProductId();
        const string newProductName = "Updated name";
        var product = _productDataAccess.GetById(productId);
        product.Name = newProductName;
        
        // Act
        var result = _productDataAccess.Update(product);
        
        // Assert
        Assert.Equal(1, result);
        var updatedProduct = _productDataAccess.GetById(productId);
        Assert.Equal(newProductName, updatedProduct.Name);
    }
    
    [Fact]
    public void Delete_ShouldDeleteProductWithGivenId()
    {
        // Arrange
        var productId = GetLastProductId();
        
        // Act
        var result = _productDataAccess.Delete(productId);
        
        // Assert
        Assert.Equal(1, result);
        
        var deletedProduct = _productDataAccess.GetById(productId);
        Assert.Null(deletedProduct);
    }

    private int GetLastProductId()
    {
        using var connection = new SqlConnection(ConnectionString);
        connection.Open();
        var command = new SqlCommand("SELECT MAX(Id) FROM [dbo].[Product]", connection);
        return (int) command.ExecuteScalar();
    }
}
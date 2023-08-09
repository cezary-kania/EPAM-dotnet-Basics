using DapperLibrary.Models;
using DapperLibrary.Repositories.Interfaces;
using DapperLibrary.Tests.Tests;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace EFLibrary.Tests.Tests;

public class ProductRepositoryTest : TestBase
{
    [Fact]
    public void Get_ShouldReturnProductData_WhenGivenIdIsValid()
    {
        // Arrange
        var repository = ServiceProvider.GetRequiredService<IProductRepository>();
        var productId = GetLastProductId();
        
        // Act
        var result = repository.Get(productId);

        // Assert
        Assert.NotNull(result);
    }
    [Fact]
    public void GetAll_ShouldReturnAllProductData()
    {
        // Arrange
        var repository = ServiceProvider.GetRequiredService<IProductRepository>();
        
        // Act
        var result = repository.GetAll();

        // Assert
        Assert.NotEmpty(result);
    }

    [Fact]
    public void Create_ShouldInsertNewProduct()
    {
        // Arrange
        var repository = ServiceProvider.GetRequiredService<IProductRepository>();
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
        var id = repository.Create(product);
        var insertedProduct = repository.Get(id);
        // Assert
        insertedProduct.Should()
            .BeEquivalentTo(product, options => options.Excluding(p => p.Id));
    }
    
    [Fact]
    public void Update_ShouldUpdateProductData()
    {
        // Arrange
        var repository = ServiceProvider.GetRequiredService<IProductRepository>();
        var productId = GetLastProductId();
        const string newProductName = "Updated name";
        var product = repository.Get(productId);
        product.Name = newProductName;
        
        // Act
        repository.Update(product);
        
        // Assert
        var updatedProduct = repository.Get(productId);
        Assert.Equal(newProductName, updatedProduct.Name);
    }
    
    [Fact]
    public void Delete_ShouldDeleteProductWithGivenId()
    {
        // Arrange
        var repository = ServiceProvider.GetRequiredService<IOrderRepository>();
        var productId = GetLastProductId();
        
        // Act
        repository.Delete(productId);
        
        // Assert
        var deletedProduct = repository.Get(productId);
        Assert.Null(deletedProduct);
    }
}
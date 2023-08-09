using EFLibrary.Contexts;
using EFLibrary.Models;
using EFLibrary.Repositories.Interfaces;

namespace EFLibrary.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly LibraryDbContext _dbContext;

    public ProductRepository(LibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Product? Get(int productId)
        => _dbContext.Products.FirstOrDefault(product => product.Id == productId);

    public List<Product> GetAll()
        => _dbContext.Products.ToList();

    public int Create(Product product)
    {
        _dbContext.Products.Add(product);
        _dbContext.SaveChanges();
        return product.Id;
    }

    public void Update(Product product)
    {
        _dbContext.Products.Update(product);
        _dbContext.SaveChanges();
    }

    public void Delete(int productId)
    {
        var product = Get(productId);
        if (product is null)
        {
            return;
        }
        _dbContext.Products.Remove(product);
        _dbContext.SaveChanges();
    }
}
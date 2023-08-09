using DapperLibrary.Models;

namespace DapperLibrary.Repositories.Interfaces;

public interface IProductRepository
{
    Product? Get(int productId);
    List<Product> GetAll();
    int Create(Product product);
    void Update(Product product);
    void Delete(int productId);
}
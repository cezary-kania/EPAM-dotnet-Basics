using EFLibrary.Models;

namespace EFLibrary.Repositories.Interfaces;

public interface IOrderRepository
{
    Order? Get(int orderId);
    int Create(Order order);
    void Update(Order order);
    void Delete(int orderId);
    List<Order> GetFiltered(int month = -1, string status = "", int year = -1, int productId = -1);
    int DeleteFiltered(int month = -1, string status = "", int year = -1, int productId = -1);
}
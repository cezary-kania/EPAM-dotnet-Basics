using EFLibrary.Contexts;
using EFLibrary.Models;

namespace EFLibrary.Tests;

public static class DbInitializer
{
    public static void Initialize(LibraryDbContext context)
    {
        if (context.Products.Any())
        {
            return;
        }

        var products = new List<Product>
        {
            new()
            {
                Name = "LOBERGET Swivel chair",
                Description =
                    "For designer Carl Öjerstam, the challenge was to design a comfortable desk chair using just 1 kg of material.",
                Weight = 5,
                Height = 75,
                Width = 20,
                Length = 1,
            },
            new()
            {
                Name = "Red chair",
                Description = "For designer Red chair, the challenge was to design a comfortable desk chair",
                Weight = 3,
                Height = 60,
                Width = 20,
                Length = 10,
            },
            new()
            {
                Name = "Brown chair",
                Description = "For designer Brown chair, the challenge was to design a comfortable desk chair",
                Weight = 3,
                Height = 60,
                Width = 20,
                Length = 10,
            },
            new()
            {
                Name = "Extra Brown chair",
                Description = "For designer Extra Brown chair, the challenge was to design a comfortable desk chair",
                Weight = 3,
                Height = 60,
                Width = 20,
                Length = 10,
            }
        };
        
        context.Products.AddRange(products);
        context.SaveChanges();

        var orders = new List<Order>
        {
            new()
            {
                Status = "Done",
                CreatedDate = DateTime.Parse("2020-01-15"),
                UpdatedDate = DateTime.Parse("2020-01-24"),
                ProductId = products[1].Id
            },
            new()
            {
                Status = "NotStarted",
                CreatedDate = DateTime.Parse("2022-04-16"),
                UpdatedDate = DateTime.Parse("2022-04-18"),
                ProductId = products[0].Id
            },
            new()
            {
                Status = "Arrived",
                CreatedDate = DateTime.Parse("2023-03-02"),
                UpdatedDate = DateTime.Parse("2023-03-15"),
                ProductId = products[2].Id
            },
        };
        
        context.Orders.AddRange(orders);
        context.SaveChanges();
    }
}
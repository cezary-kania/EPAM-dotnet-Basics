using Library.Models;

namespace Library.Test;

public static class TestData
{
    public static readonly List<Product> Products = new()
    {
        new Product
        {
            Name = "LOBERGET Swivel chair",
            Description = "For designer Carl Öjerstam, the challenge was to design a comfortable desk chair using just 1 kg of material.",
            Weight = 5,
            Height = 75,
            Width = 20,
            Length = 1,
        },
        new Product
        {
            Name = "Red chair",
            Description = "For designer Red chair, the challenge was to design a comfortable desk chair",
            Weight = 3,
            Height = 60,
            Width = 20,
            Length = 10,
        },
        new Product
        {
            Name = "Brown chair",
            Description = "For designer Brown chair, the challenge was to design a comfortable desk chair",
            Weight = 3,
            Height = 60,
            Width = 20,
            Length = 10,
        },
        new Product
        {
            Name = "Extra Brown chair",
            Description = "For designer Extra Brown chair, the challenge was to design a comfortable desk chair",
            Weight = 3,
            Height = 60,
            Width = 20,
            Length = 10,
        },
    };
    
    public static readonly List<Order> Orders = new()
    {
        new Order
        {
            Status = "Done",
            CreatedDate = DateTime.Parse("2020-01-15"),
            UpdatedDate = DateTime.Parse("2020-01-24"),
        },
        new Order
        {
            Status = "NotStarted",
            CreatedDate = DateTime.Parse("2022-04-16"),
            UpdatedDate = DateTime.Parse("2022-04-18"),
        },
        new Order
        {
            Status = "Arrived",
            CreatedDate = DateTime.Parse("2023-03-02"),
            UpdatedDate = DateTime.Parse("2023-03-15"),
        },
    };
}
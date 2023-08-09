using EFLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace EFLibrary.Contexts;

public class LibraryDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    
    public LibraryDbContext (DbContextOptions<LibraryDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().ToTable("Product");
        modelBuilder.Entity<Order>().ToTable("Order");
    }
}
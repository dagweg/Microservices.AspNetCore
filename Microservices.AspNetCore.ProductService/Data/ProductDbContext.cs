using Microservices.AspNetCore.ProductService.Models;
using Microsoft.EntityFrameworkCore;

namespace Microservices.AspNetCore.ProductService.Data;

public class ProductDbContext : DbContext
{
  public ProductDbContext(DbContextOptions<ProductDbContext> options)
    : base(options) { }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    base.OnConfiguring(optionsBuilder);
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder
      .Entity<Product>()
      .HasData(
        new List<Product>()
        {
          new Product
          {
            Id = 1,
            Name = "Acer Aspire 5 Slim Laptop",
            Price = 349.99m,
            Stock = 100,
          },
          new Product
          {
            Id = 2,
            Name = "Apple MacBook Air",
            Price = 999.99m,
            Stock = 50,
          },
          new Product
          {
            Id = 3,
            Name = "Dell XPS 13",
            Price = 1199.99m,
            Stock = 25,
          },
          new Product
          {
            Id = 4,
            Name = "HP Pavilion 15",
            Price = 549.99m,
            Stock = 75,
          },
        }
      );
    base.OnModelCreating(modelBuilder);
  }

  public DbSet<Product> Products { get; set; } = null!;
}

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
            ThumbUrl = "https://c1.neweggimages.com/productimage/nb640/A8X5S210204h0KxK.jpg",
          },
          new Product
          {
            Id = 2,
            Name = "Apple MacBook Air",
            Price = 999.99m,
            Stock = 50,
            ThumbUrl = "https://wapcomputer.com/wp-content/uploads/2020/12/images-1-19.jpeg",
          },
          new Product
          {
            Id = 3,
            Name = "Dell XPS 13",
            Price = 1199.99m,
            Stock = 25,
            ThumbUrl =
              "https://cdn11.bigcommerce.com/s-o9pppsyjzh/images/stencil/1280x1280/products/471705/12169085/N5302460__1__06999.1690009763.jpg?c=1",
          },
          new Product
          {
            Id = 4,
            Name = "HP Pavilion 15",
            Price = 549.99m,
            Stock = 75,
            ThumbUrl =
              "https://hk-media.apjonlinecdn.com/catalog/product/cache/b3b166914d87ce343d4dc5ec5117b502/c/0/c07961278_1_2.png",
          },
        }
      );
    base.OnModelCreating(modelBuilder);
  }

  public DbSet<Product> Products { get; set; } = null!;
}

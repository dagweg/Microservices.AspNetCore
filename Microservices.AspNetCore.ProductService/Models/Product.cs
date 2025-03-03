namespace Microservices.AspNetCore.ProductService.Models;

public class Product
{
  public int Id { get; set; }
  public string Name { get; set; } = null!;
  public decimal Price { get; set; }
  public int Stock { get; set; }
  public string ThumbUrl { get; set; } = null!;
}

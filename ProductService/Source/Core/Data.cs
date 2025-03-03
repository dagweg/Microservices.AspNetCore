using Domain.Common.ValueObjects;
using Domain.ProductAggregate;

namespace Core;

public static class InMemoryData
{
  public static List<Product> Products = new List<Product>
  {
    Product.CreateProduct(
      "P001",
      "Product 1",
      "Description for Product 1",
      Price.Create(100, "USD"),
      Dimensions.Create(10, 5, 5, "cm"),
      4
    ),
    Product.CreateProduct(
      "P002",
      "Product 2",
      "Description for Product 2",
      Price.Create(200, "USD"),
      Dimensions.Create(15, 10, 5, "cm"),
      3
    ),
    Product.CreateProduct(
      "P003",
      "Product 3",
      "Description for Product 3",
      Price.Create(300, "USD"),
      Dimensions.Create(20, 15, 5, "cm"),
      11
    ),
  };
}

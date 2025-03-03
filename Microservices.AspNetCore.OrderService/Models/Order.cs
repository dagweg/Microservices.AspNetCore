namespace Microservices.AspNetCore.OrderService.Models;

public class Order
{
  public int Id { get; set; }
  public int ProductId { get; set; }
  public int Quantity { get; set; }
}

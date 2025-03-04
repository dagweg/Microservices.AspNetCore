namespace Microservices.AspNetCore.OrderService.Models;

public enum OrderStatus
{
  Pending,
  Completed,
  Cancelled,
}

public class Order
{
  public int Id { get; set; }
  public int ProductId { get; set; }
  public int Quantity { get; set; }
  public OrderStatus Status { get; set; } = OrderStatus.Pending;
}

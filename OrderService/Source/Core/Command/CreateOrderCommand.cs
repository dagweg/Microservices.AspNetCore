using OrderService.Domain.Common.ValueObjects;

namespace Core.Command;

public class CreateOrderCommand
{
  public required Guid CustomerId { get; set; }
  public required IEnumerable<OrderItemCommand> OrderItems { get; set; }
  public required Address ShippingAddress { get; set; }

  public class OrderItemCommand
  {
    public required Guid ProductId { get; set; }
    public required int Quantity { get; set; }
  }
}

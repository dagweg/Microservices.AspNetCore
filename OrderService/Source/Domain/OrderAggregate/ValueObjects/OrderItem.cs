namespace OrderService.Domain.OrderAggregate.ValueObjects;

public class OrderItem
{
  public Guid ProductId { get; private set; }
  public int Quantity { get; private set; }

  private OrderItem(Guid productId, int quantity)
  {
    ProductId = productId;
    Quantity = quantity;
  }

  public static OrderItem Create(Guid productId, int quantity)
  {
    if (quantity <= 0)
    {
      throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be a positive value.");
    }

    return new OrderItem(productId, quantity);
  }
}

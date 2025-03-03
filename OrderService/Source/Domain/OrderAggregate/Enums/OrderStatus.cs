namespace OrderService.Domain.OrderAggregate.Enums;

public enum OrderStatus
{
  Draft,
  Submitted,
  Paid,
  Shipped,
  Delivered,
  Cancelled,
  Refunded,
}

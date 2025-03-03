using Domain.OrderAggregate.Interfaces;

namespace Domain.OrderAggregate.Events;

public class OrderCancelledEvent : IOrderDomainEvent
{
  public Order Order { get; }

  public OrderCancelledEvent(Order order)
  {
    Order = order;
  }
}

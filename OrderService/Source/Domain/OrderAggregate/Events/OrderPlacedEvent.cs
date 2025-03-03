using Domain.OrderAggregate.Interfaces;

namespace Domain.OrderAggregate.Events;

public class OrderPlacedEvent : IOrderDomainEvent
{
  public Order Order { get; }

  public OrderPlacedEvent(Order order)
  {
    Order = order;
  }
}

namespace Domain.OrderAggregate.Interfaces;

public interface IOrderDomainEvent : IDomainEvent
{
  Order Order { get; }
}

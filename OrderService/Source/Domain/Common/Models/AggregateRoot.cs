using Domain.OrderAggregate.Interfaces;

namespace Domain.Common.Models;

public class AggregateRoot
{
  private List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

  public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

  protected void AddDomainEvent(IDomainEvent domainEvent)
  {
    _domainEvents.Add(domainEvent);
  }
}

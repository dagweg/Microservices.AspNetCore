using Domain.ProductAggregate.Interfaces;

namespace Domain.ProductAggregate.Events;

public class ProductMarkedInactiveEvent : IProductDomainEvent
{
  public Guid ProductId { get; }

  public ProductMarkedInactiveEvent(Guid productId)
  {
    ProductId = productId;
  }
}

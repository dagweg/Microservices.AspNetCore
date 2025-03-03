using Domain.ProductAggregate.Interfaces;

namespace Domain.ProductAggregate.Events;

public class ProductCreatedEvent : IProductDomainEvent
{
  public Guid ProductId { get; }

  public ProductCreatedEvent(Guid productId)
  {
    ProductId = productId;
  }
}

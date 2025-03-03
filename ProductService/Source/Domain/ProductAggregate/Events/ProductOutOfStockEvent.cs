using Domain.ProductAggregate.Interfaces;

namespace Domain.ProductAggregate.Events;

public class ProductOutOfStockEvent : IProductDomainEvent
{
  public Guid ProductId { get; }

  public ProductOutOfStockEvent(Guid productId)
  {
    ProductId = productId;
  }
}

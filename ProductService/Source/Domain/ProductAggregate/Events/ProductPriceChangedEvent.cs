namespace Domain.ProductAggregate.Events;

using Domain.Common.ValueObjects;
using Domain.ProductAggregate.Interfaces;

public class ProductPriceChangedEvent : IProductDomainEvent
{
  public Guid ProductId { get; }
  public Price NewPrice { get; }

  public ProductPriceChangedEvent(Guid productId, Price newPrice)
  {
    ProductId = productId;
    NewPrice = newPrice;
  }
}

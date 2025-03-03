using Domain.ProductAggregate.Interfaces;

public class ProductMarkedActiveEvent : IProductDomainEvent
{
  public Guid ProductId { get; }

  public ProductMarkedActiveEvent(Guid productId)
  {
    ProductId = productId;
  }
}

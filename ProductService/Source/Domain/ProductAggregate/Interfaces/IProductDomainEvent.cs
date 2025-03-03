namespace Domain.ProductAggregate.Interfaces;

public interface IProductDomainEvent
{
  Guid ProductId { get; }
}

using Domain.Common.ValueObjects;
using Domain.ProductAggregate;

namespace Core.Queries;

public class GetAllProductsQueryHandler
{
  public Task<IEnumerable<Product>> Handle(
    GetAllProductsQuery query,
    CancellationToken cancellationToken
  )
  {
    return Task.FromResult(InMemoryData.Products.AsEnumerable());
  }
}

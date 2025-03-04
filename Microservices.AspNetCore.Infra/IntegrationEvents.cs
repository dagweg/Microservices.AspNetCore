namespace Microservices.AspNetCore.Infra;

public static class IntegrationEvents
{
  public record OrderPlacedEvent(int OrderId, int ProductId, int Quantity);

  public record OrderCreatedEvent(int OrderId, int ProductId, int Quantity);

  public record OrderRejectedEvent(int OrderId);
}

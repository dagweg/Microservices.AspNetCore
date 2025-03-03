using Shared.Events.OrderEvents.Models;

namespace Shared.Events.OrderEvents;

public record OrderPlacedEvent : IEvent
{
  public required Guid OrderId { get; init; }
  public required Guid CustomerId { get; init; }
  public required List<OrderItem> OrderItems { get; init; }
}

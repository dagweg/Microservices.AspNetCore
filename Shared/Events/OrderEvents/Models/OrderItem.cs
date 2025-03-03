namespace Shared.Events.OrderEvents.Models;

public record OrderItem(Guid ProductId, int Quantity);

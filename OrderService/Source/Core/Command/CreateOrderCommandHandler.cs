using Shared.Events;
using Shared.Events.OrderEvents;
using Shared.Events.OrderEvents.Models;
using Shared.Infra.Kafka;

namespace Core.Command;

public class CreateOrderCommandHandler(IKafkaProducer kafkaProducer)
{
  public async Task Handle(CreateOrderCommand command, CancellationToken cancellationToken)
  {
    var order = Order.CreateOrder(command.CustomerId, command.ShippingAddress);

    foreach (var orderItemCommand in command.OrderItems)
    {
      order.AddOrderItem(orderItemCommand.ProductId, orderItemCommand.Quantity);
    }

    order.SubmitOrder(); // this will raise domain event

    // TODO: Remaining logic to save order to database

    await kafkaProducer.ProduceAsync(
      KafkaTopics.OrderTopic,
      (IEvent)
        new OrderPlacedEvent
        {
          CustomerId = order.CustomerId,
          OrderId = order.OrderId,
          OrderItems = order
            .OrderItems.Select(x => new Shared.Events.OrderEvents.Models.OrderItem(
              x.ProductId,
              x.Quantity
            ))
            .ToList(),
        }
    );
  }
}

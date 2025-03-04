using System.Text.Json;
using Confluent.Kafka;
using Microservices.AspNetCore.Infra;
using Microservices.AspNetCore.Infra.Kafka;
using Microservices.AspNetCore.ProductService.Data;
using static Microservices.AspNetCore.Infra.IntegrationEvents;

namespace Microservices.AspNetCore.ProductService.Services.EventHandlers;

public class OrderPlacedEventHandler(IKafkaProducer kafkaProducer, IServiceScopeFactory ssf)
  : BackgroundService,
    IKafkaConsumer
{
  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    await Task.Factory.StartNew(() => ConsumerLoop(), TaskCreationOptions.LongRunning);
    async Task ConsumerLoop()
    {
      var consumerConfig = new ConsumerConfig
      {
        GroupId = "product-group",
        BootstrapServers = "localhost:9092",
        AutoOffsetReset = AutoOffsetReset.Earliest,
      };

      using var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();

      consumer.Subscribe(KafkaTopics.OrderPlaced);

      while (!stoppingToken.IsCancellationRequested)
      {
        var consumerResult = consumer.Consume(stoppingToken);

        var orderPlacedEvent = JsonSerializer.Deserialize<OrderPlacedEvent>(
          consumerResult.Message.Value
        );

        using var scope = ssf.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ProductDbContext>();

        if (orderPlacedEvent != null)
        {
          // Process the order placed event
          var query = db.Products.FirstOrDefault(p =>
            p.Id == orderPlacedEvent.ProductId && p.Stock >= orderPlacedEvent.Quantity
          );

          if (query is not null)
          {
            query.Stock -= orderPlacedEvent.Quantity;
            await db.SaveChangesAsync();

            /// Produce OrderCreatedEvent  - this means the order is accepted (the listening service will handle this)
            await kafkaProducer.ProduceAsync<OrderCreatedEvent>(
              KafkaTopics.OrderCreated,
              new Message<string, OrderCreatedEvent>
              {
                Key = orderPlacedEvent.OrderId.ToString(),
                Value = new OrderCreatedEvent(
                  orderPlacedEvent.OrderId,
                  orderPlacedEvent.ProductId,
                  orderPlacedEvent.Quantity
                ),
              }
            );
          }
          else
          {
            /// Produce OrderRejectedEvent  - this means the order is rejected (the listening service will handle this)
            await kafkaProducer.ProduceAsync<OrderRejectedEvent>(
              KafkaTopics.OrderRejected,
              new Message<string, OrderRejectedEvent>
              {
                Key = orderPlacedEvent.OrderId.ToString(),
                Value = new OrderRejectedEvent(orderPlacedEvent.OrderId),
              }
            );
          }
        }
      }
    }
  }
}

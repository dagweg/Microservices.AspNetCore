using System.Text.Json;
using Confluent.Kafka;
using Microservices.AspNetCore.Infra;
using Microservices.AspNetCore.Infra.Kafka;
using Microservices.AspNetCore.OrderService.Data;
using Microservices.AspNetCore.OrderService.Models;
using Microsoft.Extensions.Logging;
using static Microservices.AspNetCore.Infra.IntegrationEvents;

namespace Microservices.AspNetCore.OrderService.Services.EventHandlers;

public class OrderCreatedEventHandler(
  IServiceScopeFactory serviceScopeFactory,
  ILogger<OrderCreatedEventHandler> logger
) : BackgroundService, IKafkaConsumer
{
  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    await Task.Factory.StartNew(() => ConsumerLoop(), TaskCreationOptions.LongRunning);

    async Task ConsumerLoop()
    {
      var consumerConfig = new ConsumerConfig
      {
        GroupId = "order-group",
        BootstrapServers = "localhost:9092",
        AutoOffsetReset = AutoOffsetReset.Earliest,
      };

      logger.LogInformation("OrderCreatedEventHandler started.");

      using var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
      {
        consumer.Subscribe(KafkaTopics.OrderCreated);

        while (!stoppingToken.IsCancellationRequested)
        {
          var consumerResult = consumer.Consume(TimeSpan.FromSeconds(5));

          if (consumerResult == null)
            continue;

          logger.LogInformation($"Consumed message: {consumerResult.Message.Value}");

          var orderCreatedEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(
            consumerResult.Message.Value
          );

          if (orderCreatedEvent != null)
          {
            using (var scope = serviceScopeFactory.CreateScope())
            {
              var db = scope.ServiceProvider.GetRequiredService<OrderDbContext>();

              var query = await db.Orders.FindAsync(orderCreatedEvent.OrderId);

              if (query != null)
              {
                query.Status = OrderStatus.Completed;
                await db.SaveChangesAsync();
                logger.LogInformation($"Order {orderCreatedEvent.OrderId} updated to completed.");
              }
            }
          }
        }
        consumer.Close();
        logger.LogInformation("Kafka consumer closed.");
      }
    }
  }
}

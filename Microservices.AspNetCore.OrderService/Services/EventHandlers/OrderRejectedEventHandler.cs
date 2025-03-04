using System.Text.Json;
using Confluent.Kafka;
using Microservices.AspNetCore.Infra;
using Microservices.AspNetCore.Infra.Kafka;
using Microservices.AspNetCore.OrderService.Data;
using Microservices.AspNetCore.OrderService.Models;
using static Microservices.AspNetCore.Infra.IntegrationEvents;

namespace Microservices.AspNetCore.OrderService.Services.EventHandlers;

public class OrderRejectedEventHandler(IServiceScopeFactory ssf, ILogger<OrderRejectedEvent> logger)
  : BackgroundService,
    IKafkaConsumer
{
  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    await Task.Factory.StartNew(() => ConsumerLoop(stoppingToken), TaskCreationOptions.LongRunning);
  }

  async Task ConsumerLoop(CancellationToken stoppingToken)
  {
    logger.LogInformation("OrderRejectedEventHandler started.");

    var consumerConfig = new ConsumerConfig
    {
      GroupId = "order-group",
      BootstrapServers = "localhost:9092",
      AutoOffsetReset = AutoOffsetReset.Earliest,
    };

    using var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
    consumer.Subscribe(KafkaTopics.OrderRejected);

    while (!stoppingToken.IsCancellationRequested)
    {
      var consumerResult = consumer.Consume(TimeSpan.FromSeconds(5));

      if (consumerResult == null)
        continue;

      logger.LogInformation($"Consumed message: {consumerResult.Message.Value}");

      if (consumerResult is not null)
      {
        var orderRejectedEvent = JsonSerializer.Deserialize<OrderRejectedEvent>(
          consumerResult.Message.Value
        );

        if (orderRejectedEvent != null)
        {
          using (var scope = ssf.CreateScope())
          {
            var db = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
            var query = db.Orders.FirstOrDefault(o => o.Id == orderRejectedEvent.OrderId);

            if (query is not null)
            {
              query.Status = OrderStatus.Cancelled;
              db.Update(query);
              await db.SaveChangesAsync();
            }
          }
        }
      }
    }

    consumer.Close();
  }
}

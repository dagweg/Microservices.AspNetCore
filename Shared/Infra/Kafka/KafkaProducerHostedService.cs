using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Shared.Events;
using Shared.Events.OrderEvents;
using Shared.Events.OrderEvents.Models;

namespace Shared.Infra.Kafka;

public class KafkaProducerHostedService : IKafkaProducer
{
  private readonly KafkaOptions _kafkaOptions;
  private readonly IProducer<Null, string> _producer;

  public KafkaProducerHostedService(IOptions<KafkaOptions> options)
  {
    _kafkaOptions = options.Value;
    var config = new ProducerConfig() { BootstrapServers = _kafkaOptions.BootstrapServers };

    _producer = new ProducerBuilder<Null, string>(config)
      .SetValueSerializer(Serializers.Utf8)
      .Build();
  }

  /// <summary>
  /// Produce a message to a topic
  /// </summary>
  /// <param name="topic"></param>
  /// <param name="message"></param>
  /// <returns></returns>
  public async Task ProduceAsync(string topic, IEvent message)
  {
    await _producer.ProduceAsync(
      topic,
      new Message<Null, string> { Value = JsonSerializer.Serialize(message) }
    );
  }

  public async Task StartAsync(CancellationToken cancellationToken)
  {
    for (var i = 0; i < 100; i++)
    {
      await ProduceAsync(
        KafkaTopics.OrderTopic,
        (IEvent)
          new OrderPlacedEvent
          {
            CustomerId = Guid.NewGuid(),
            OrderId = Guid.NewGuid(),
            OrderItems = new List<OrderItem> { new OrderItem(Guid.NewGuid(), 1) },
          }
      );
    }
  }

  public Task StopAsync(CancellationToken cancellationToken)
  {
    throw new NotImplementedException();
  }
}

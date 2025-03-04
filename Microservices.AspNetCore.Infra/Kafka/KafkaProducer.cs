using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace Microservices.AspNetCore.Infra.Kafka;

public class KafkaProducer : IKafkaProducer
{
  private readonly KafkaOptions _kafkaOptions;
  private readonly ProducerConfig _producerConfig;
  private readonly IProducer<string, string> _producer;

  public KafkaProducer(IOptions<KafkaOptions> kafkaOptions)
  {
    _kafkaOptions = kafkaOptions.Value;

    _producerConfig = new ProducerConfig() { BootstrapServers = _kafkaOptions.BootstrapServers };

    _producer = new ProducerBuilder<string, string>(_producerConfig).Build();
  }

  public async Task ProduceAsync<T>(string topic, Message<string, T> message)
  {
    await _producer.ProduceAsync(
      topic,
      new Message<string, string>
      {
        Key = message.Key,
        Value = JsonSerializer.Serialize(message.Value),
      }
    );
  }
}

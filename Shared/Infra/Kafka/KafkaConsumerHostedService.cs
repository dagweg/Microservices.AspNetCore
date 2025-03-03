using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Shared.Events;

namespace Shared.Infra.Kafka;

public class KafkaConsumerHostedService : IKafkaConsumer
{
  private readonly KafkaOptions _kafkaOptions;
  private readonly IConsumer<Null, string> _consumer;

  public KafkaConsumerHostedService(IOptions<KafkaOptions> options)
  {
    _kafkaOptions = options.Value;
    var config = new ConsumerConfig() { BootstrapServers = _kafkaOptions.BootstrapServers };
    _consumer = new ConsumerBuilder<Null, string>(config)
      .SetValueDeserializer(Deserializers.Utf8)
      .Build();
  }

  public async Task ConsumeAsync(string topic, IEvent message)
  {
    await Task.CompletedTask;
  }

  public async Task StartAsync(CancellationToken cancellationToken)
  {
    await Task.CompletedTask;
  }

  public async Task StopAsync(CancellationToken cancellationToken)
  {
    await Task.CompletedTask;
  }
}

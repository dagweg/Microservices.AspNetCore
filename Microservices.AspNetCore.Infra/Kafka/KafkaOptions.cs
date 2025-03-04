using Confluent.Kafka;

namespace Microservices.AspNetCore.Infra.Kafka;

public record KafkaOptions
{
  public string GroupId { get; init; } = null!;
  public string BootstrapServers { get; init; } = null!;
  public AutoOffsetReset AutoOffsetReset { get; init; } = AutoOffsetReset.Earliest;
}

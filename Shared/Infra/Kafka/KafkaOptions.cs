namespace Shared.Infra.Kafka;

public record KafkaOptions
{
  public const string SectionName = "KafkaOptions";

  public required string BootstrapServers { get; init; }
}

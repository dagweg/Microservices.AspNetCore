using Confluent.Kafka;

namespace Microservices.AspNetCore.Infra.Kafka;

public interface IKafkaProducer
{
  Task ProduceAsync<T>(string topic, Message<string, T> message);
}

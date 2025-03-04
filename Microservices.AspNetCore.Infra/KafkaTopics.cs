namespace Microservices.AspNetCore.Infra;

public static class KafkaTopics
{
  public const string OrderPlaced = "order.placed";
  public const string OrderCreated = "order.created";
  public const string OrderRejected = "order.rejected";
}

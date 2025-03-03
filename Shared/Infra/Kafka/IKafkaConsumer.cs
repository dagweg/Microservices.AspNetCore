using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Shared.Events;

namespace Shared.Infra.Kafka;

public interface IKafkaConsumer : IHostedService
{
  Task ConsumeAsync(string topic, IEvent message);
}

using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Shared.Events;

namespace Shared.Infra.Kafka;

public interface IKafkaProducer : IHostedService
{
  Task ProduceAsync(string topic, IEvent message);
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infra.Kafka;

namespace Infra;

public static class DI
{
  public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    IConfiguration configuration
  )
  {
    services.Configure<KafkaOptions>(configuration.GetSection(KafkaOptions.SectionName));

    // register event bus

    // register cache

    // register repositories

    return services;
  }
}

using BuildingBlocks.Behaviors;
using BuildingBlocks.Messaging.MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ordering.Application
{
  //Static class is needed for this
  public static class DependencyInjection
  {
    public static IServiceCollection AddApplicationServices
      (this IServiceCollection services, IConfiguration configuration)
    {
      services.AddMediatR(config =>
      {
        config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        config.AddOpenBehavior(typeof(ValidationBehavior<,>));
      });

      services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());

      return services;
    }
  }
}

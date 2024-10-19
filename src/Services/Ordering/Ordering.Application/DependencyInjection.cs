using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Application
{
  //Static class is needed for this
  public static class DependencyInjection
  {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
      //services.AddMediatR(cfg =>
      //{
      //  cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
      //});

      return services;
    }
  }
}

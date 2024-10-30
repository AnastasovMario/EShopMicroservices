using BuildingBlocks.Exceptions.Handler;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Ordering.API
{
  public static class DependencyInjection
  {
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddCarter();

      services.AddExceptionHandler<CustomExceptionHandler>(); //1
      services.AddHealthChecks()
        .AddSqlServer(configuration.GetConnectionString("Database")!);

      return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
      app.MapCarter();
      
      //By adding these 2 lines of code, we are activating custom exception handling for the ordering
      app.UseExceptionHandler(options => { }); //2
      app.UseHealthChecks("/health",
        new HealthCheckOptions
        {
          ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

      return app;
    }
  }
}

using BuildingBlocks.Exceptions.Handler;
using Carter;

namespace Ordering.API
{
  public static class DependencyInjection
  {
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
      services.AddCarter();

      //1.
      services.AddExceptionHandler<CustomExceptionHandler>();

      return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
      app.MapCarter();
      
      //2
      //By adding these 2 lines of code, we are activating custom exception handling for the ordering
      app.UseExceptionHandler(options => { });

      return app;
    }
  }
}

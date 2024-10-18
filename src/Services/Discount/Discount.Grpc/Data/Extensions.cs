using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
  public static class Extensions
  {
    public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
    {
      using var scope = app.ApplicationServices.CreateScope();
      using var dbContext = scope.ServiceProvider.GetRequiredService<DiscountContext>();
      //Applies any pending migrations for the context to the database if the database doesn't already exists
      dbContext.Database.MigrateAsync();

      return app;
    }
  }
}

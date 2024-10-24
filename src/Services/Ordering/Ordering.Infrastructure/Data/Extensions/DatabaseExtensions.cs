using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure.Data.Extensions
{
  //class needs to be static in order to create an extension method
  public static class DatabaseExtensions
  {
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
      using var scope = app.Services.CreateScope();

      var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

      context.Database.MigrateAsync().GetAwaiter().GetResult();
    }
  }
}

using BuildingBlocks.Exceptions.Handler;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container
var assembly = typeof(Program).Assembly;
builder.Services.AddCarter(); //1. Add carter service
builder.Services.AddMediatR(config =>
{
  config.RegisterServicesFromAssembly(assembly);
  config.AddOpenBehavior(typeof(ValidationBehavior<,>)); //Add validation behaviour from our pipeline
  config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

//Register Marten
builder.Services.AddMarten(opts =>
{
  opts.Connection(builder.Configuration.GetConnectionString("Database")!);
  opts.Schema.For<ShoppingCart>().Identity(x => x.UserName); // setting the identity to be username;
}).UseLightweightSessions(); // For better performance;

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = builder.Configuration.GetConnectionString("Redis");
});
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

//Add healthcheks for two databases with AspNetHealthCheks
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);

var app = builder.Build();

//Configure the HTTP request pipeline
app.MapCarter(); //2. Then map it
app.UseExceptionHandler(options => { });
app.UseHealthChecks("/health", 
     new HealthCheckOptions
     {
         //Makes the response more readable (UIResponse library)
         ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
     });

app.Run();

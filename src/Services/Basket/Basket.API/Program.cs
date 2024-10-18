using BuildingBlocks.Exceptions.Handler;
using Discount.Grpc;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container

//Application Services
var assembly = typeof(Program).Assembly;
builder.Services.AddCarter(); //1. Add carter service
builder.Services.AddMediatR(config =>
{
  config.RegisterServicesFromAssembly(assembly);
  config.AddOpenBehavior(typeof(ValidationBehavior<,>)); //Add validation behaviour from our pipeline
  config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});


//Data Services
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

//Grpc Services
//TODO- Add Grpc Services
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
  options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
});

//Cross-Cutting Services
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

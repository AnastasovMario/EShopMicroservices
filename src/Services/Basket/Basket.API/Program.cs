using BuildingBlocks.Exceptions.Handler;

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

var app = builder.Build();

//Configure the HTTP request pipeline
app.MapCarter(); //2. Then map it
app.UseExceptionHandler(options => { });

app.Run();

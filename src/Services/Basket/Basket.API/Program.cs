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

var app = builder.Build();

//Configure the HTTP request pipeline
app.MapCarter(); //2. Then map it

app.Run();

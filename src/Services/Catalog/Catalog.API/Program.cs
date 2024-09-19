var builder = WebApplication.CreateBuilder(args);

//Add services to the container
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
   // opts.AutoCreateSchemaObjects Marten library attempt to create any missing database schema objects at runtime
}).UseLightweightSessions();

var app = builder.Build();

//Configure the HTTP Request Pipeline
app.MapCarter();

app.Run();

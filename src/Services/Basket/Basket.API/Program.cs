var builder = WebApplication.CreateBuilder(args);

//Addd services to the container

var app = builder.Build();

//Configure the HTTP request pipeline

app.Run();

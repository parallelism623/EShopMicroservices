

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
builder.Services.RegisterWebApplicationServices(config);

var app = builder.Build();

app.UseApplicationServices();

app.Run();

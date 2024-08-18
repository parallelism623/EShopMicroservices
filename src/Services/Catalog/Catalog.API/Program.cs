using Catalog.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterWebApplicationServices();

var app = builder.Build();

app.MapCarter();
app.ConfigWebApplication();
app.Run();



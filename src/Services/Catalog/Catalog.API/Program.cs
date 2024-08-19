using Catalog.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterWebApplicationServices(builder.Configuration);

var app = builder.Build();


app.ConfigWebApplication();

app.Run();



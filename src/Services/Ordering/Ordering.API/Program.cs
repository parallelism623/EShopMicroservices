using Ordering.Infrastructure;
var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
builder.Services.AddServicesInfrastructure(configuration);
var app = builder.Build();

app.UseHttpsRedirection();


app.Run();



using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data;
using Serilog;
var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
builder.Services.AddServicesInfrastructure(configuration);
var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    Log.Information("initial database");
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.MigrateAsync().GetAwaiter().GetResult();
}

app.Run();



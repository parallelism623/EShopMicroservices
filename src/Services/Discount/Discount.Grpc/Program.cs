

using Discount.Grpc;
using Discount.Grpc.Data;
using Discount.Grpc.Extensions;
using Discount.Grpc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGrpc();

builder.Services.AddGrpc();
var config = builder.Configuration;
builder.Services.AddDbContext<DiscountContext>(cfg =>
{
    cfg.UseSqlite(config.GetConnectionString("Sqlite"));
});
var app = builder.Build();


app.UseMigration();

app.MapGrpcService<DiscountServices>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

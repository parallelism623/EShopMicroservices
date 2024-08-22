



var builder = WebApplication.CreateBuilder(args);


builder.Services.RegisterWebApplicationServices(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<GlobalHandlingExceptionMiddleware>();
app.ConfigWebApplication();

app.Run();



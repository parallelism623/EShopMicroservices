

using BuildingBlocks.Middlewares;
using FluentValidation;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Catalog.API.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection RegisterWebApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        return services.RegisterMediatR()
                       .RegisterCarter()
                       .RegisterMartenForPostgresSql(config)
                       .RegisterFluentValidtion()
                       .RegisterMiddleware()
                       .RegisterHealthCheck(config);
    }

    private static IServiceCollection RegisterFluentValidtion(this IServiceCollection services)
    {
        return services.AddValidatorsFromAssembly(typeof(Program).Assembly);
    }
    private static IServiceCollection RegisterMediatR(this IServiceCollection services)
    {
        return services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(Program).Assembly);
            config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            config.AddOpenBehavior(typeof(LoggingPipelineBehavior<,>));
        });
    }
    private static IServiceCollection RegisterMiddleware(this IServiceCollection services)
    {
        services.AddSingleton<GlobalHandlingExceptionMiddleware>(); 
        return services;
    }
    private static IServiceCollection RegisterCarter(this IServiceCollection services)
    {
        services.AddCarter();
        return services;
    }
    private static IServiceCollection RegisterHealthCheck(this IServiceCollection services, IConfiguration config)
    {
        services.AddHealthChecks()
            .AddNpgSql(config.GetConnectionString("PostgreSqlDefault")!);
        return services;
    }
    private static IServiceCollection RegisterMartenForPostgresSql(this IServiceCollection services, IConfiguration config)
    {
        services.AddMarten(cfg =>
        {
            cfg.Connection(config.GetConnectionString("PostgreSqlDefault")!);
        }).UseLightweightSessions();
        return services;
    }
    public static void ConfigWebApplication(this WebApplication app)
    {
        app.MapCarter();
        app.UseHealthChecks("/health", 
            new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            }
        );
    }

}


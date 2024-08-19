namespace Catalog.API.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection RegisterWebApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        return services.RegisterMediatR()
                       .RegisterCarter()
                       .RegisterMartenForPostgresSql(config);
    }

    private static IServiceCollection RegisterMediatR(this IServiceCollection services)
    {
        return services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(Program).Assembly);
        });
    }
    private static IServiceCollection RegisterCarter(this IServiceCollection services)
    {
        services.AddCarter();
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
    }
}


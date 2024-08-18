namespace Catalog.API.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection RegisterWebApplicationServices(this IServiceCollection services)
    {
        return services.RegisterMediatR()
                       .RegisterCarter();
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
        return services.AddCarter(configurator: c =>
        {

        });
    }

    public static void ConfigWebApplication(this WebApplication app)
    {
        app.MapCarter();
    }
}


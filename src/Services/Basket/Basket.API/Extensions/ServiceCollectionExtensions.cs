

    using Discount.Grpc;
    using Microsoft.Extensions.DependencyInjection;

    namespace Basket.API.Extensions;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterWebApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.RegisterMartenServices(config)
                    .RegisterMediatRServices()
                    .RegisterFluentValidation()
                    .RegisterApplicationRepository()
                    .RegisterCarter()
                    .RegisterGRPCClient(config)
                    .RegisterApplicationDistributedCache(config)
                    .RegisterApplicationHealthCheck(config);
            return services;
        }

        private static IServiceCollection RegisterCarter(this IServiceCollection service)
        {
            service.AddCarter();
            return service;
        }
        private static IServiceCollection RegisterMartenServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddMarten(cfg =>
            {
                cfg.Connection(config.GetConnectionString("PostgreSqlDefault")!);
                cfg.Schema.For<ShoppingCart>().Identity(s => s.UserName);
                                         
            }).UseLightweightSessions();
            return services;
        }

        private static IServiceCollection RegisterMediatRServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
                cfg.AddOpenBehavior(typeof(LoggingPipelineBehavior<,>));
                cfg.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            });
            return services;
        }
        private static IServiceCollection RegisterGRPCClient(this IServiceCollection services, IConfiguration config)
        {
            services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(opt =>
            {
                opt.Address = new Uri(config.GetRequiredSection("gRPCOptions:Connection").Value!);
            });
            return services;
        }
        private static IServiceCollection RegisterFluentValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(Program).Assembly);
            return services;
        }

        private static IServiceCollection RegisterApplicationRepository(this IServiceCollection services)
        {
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.Decorate<IBasketRepository, CachedBasketRepository>();
            return services;
        }

        private static IServiceCollection RegisterApplicationHealthCheck(this IServiceCollection services, IConfiguration config)
        {
            services.AddHealthChecks()
                .AddNpgSql(config.GetConnectionString("PostgreSqlDefault")!)
                .AddRedis(config.GetRequiredSection("DistributedCache:Connection").Value!);
            return services;
        }
        private static IServiceCollection RegisterApplicationDistributedCache(this IServiceCollection services, IConfiguration config)
        {
        var resu = config.GetRequiredSection("DistributedCache:Connection").Value!;
            services.AddStackExchangeRedisCache(cfg =>
            {
                cfg.Configuration = config.GetRequiredSection("DistributedCache:Connection").Value!;
                
            });
            return services;
        }
        public static WebApplication UseApplicationServices(this WebApplication app)
        {
            app.MapCarter();
            app.UseApplicationHealthCheck();
            return app;
        }
        private static WebApplication UseApplicationHealthCheck(this WebApplication app)
        {
    
        app.UseHealthChecks("/health", 
            new HealthCheckOptions
            { 
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            return app;
        }
    
    }

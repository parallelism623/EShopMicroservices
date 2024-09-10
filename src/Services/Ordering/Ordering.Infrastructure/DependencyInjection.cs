using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddServicesInfrastructure(this IServiceCollection services,  IConfiguration config)
    {
        var connectionString = config.GetConnectionString("Default");
        services.AddDbContext<ApplicationDbContext>(cfg =>
        {
            cfg.UseSqlServer(connectionString, builder =>
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
        });

        return services;
    }
}

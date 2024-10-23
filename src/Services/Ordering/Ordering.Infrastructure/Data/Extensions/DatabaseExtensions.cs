using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Ordering.Infrastructure.Data.Extensions;
public static class DatabaseExtensions
{
    public static async Task SeedDataAsync(ApplicationDbContext context)
    {
        await SeedCustomerAsync(context);
        await SeedProductAsync(context);
        await SeedOrderAsync(context);
    }

    private static async Task SeedCustomerAsync(ApplicationDbContext context)
    {
        if (!context.Customers.Any()) {
            await context.AddRangeAsync(InitialiseData.InitiliseCustomerData());
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedProductAsync(ApplicationDbContext context)
    {
        if (!context.Products.Any())
        {
            await context.AddRangeAsync(InitialiseData.InitiliseProductData());
            await context.SaveChangesAsync();
        }
    }
    private static async Task SeedOrderAsync(ApplicationDbContext context)
    {
        if (!context.Orders.Any()) {
            await context.AddRangeAsync(InitialiseData.InitiliseOrderData());
            await context.SaveChangesAsync();
        }
    }
}



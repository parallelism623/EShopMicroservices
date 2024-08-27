using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public class DiscountContext : DbContext
{
    public DiscountContext(DbContextOptions<DiscountContext> options) : base(options) { }
    public DbSet<Coupon> Coupons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().ToTable("Coupon").HasKey(x => x.Id);
        modelBuilder.Entity<Coupon>().HasData(
            new Coupon
            {
                Id = 1,
                ProductName = "Discount 10%",
                Description = "10% discount on all electronics",
                Amount = 10
            },
            new Coupon
            {
                Id = 2,
                ProductName = "Free Shipping",
                Description = "Free shipping on orders over $50",
                Amount = 100
            },
            new Coupon
            {
                Id = 3,
                ProductName = "Buy 1 Get 1 Free",
                Description = "Buy one, get one free on selected items",
                Amount = 50
            });
    }
}

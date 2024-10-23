using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Abstraction;
using Ordering.Domain.Models;
using System.Reflection;

namespace Ordering.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Order> Orders => Set<Order>(); 
        public DbSet<OrderItem> Items => Set<OrderItem>();  
        public DbSet<Product> Products => Set<Product>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var trackingListObject = ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged);

            foreach (var tracking in trackingListObject)
            {
                if (tracking.State == EntityState.Added)
                {
                    var item = tracking.Entity.GetType().GetRuntimeProperties();
                }
                if (tracking.State == EntityState.Modified)
                {

                }
            }
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}

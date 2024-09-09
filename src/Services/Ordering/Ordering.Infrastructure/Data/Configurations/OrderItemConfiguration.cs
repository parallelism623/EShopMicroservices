using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Data.Configurations;
public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(oi => oi.Id);
        builder.Property(oi => oi.Id)
            .HasConversion(k => k.Value,
            kdb => OrderItemId.Of(kdb));

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(k => k.ProductId);

        builder.Property(oi => oi.Price).IsRequired();
        builder.Property(oi => oi.Quantity).IsRequired();
    }
}

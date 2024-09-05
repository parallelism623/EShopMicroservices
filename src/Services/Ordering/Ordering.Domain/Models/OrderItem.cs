using Ordering.Domain.Abstraction;
using Ordering.Domain.ValueObjects;
namespace Ordering.Domain.Models;
public class OrderItem : Entity<OrderItemId>
{
    internal OrderItem(ProductId productId, OrderId orderId, int quantity, decimal price)
    {
        Id = new OrderItemId();
        ProductId = productId;
        OrderId = orderId;
        Quantity = quantity;
        Price = price;
    }

    public ProductId ProductId { get; private set; } = default!;
    public OrderId OrderId { get; private set; } = default!;
    public int Quantity { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;
}

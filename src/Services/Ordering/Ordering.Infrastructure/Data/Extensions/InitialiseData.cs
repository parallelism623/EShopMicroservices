using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Extensions;
public static class InitialiseData
{
    public static IEnumerable<Customer> InitiliseCustomerData()
       => new List<Customer>
            {
                Customer.Create(CustomerId.Of(Guid.Parse("2276537e-9e79-4e53-be44-f012509c1a6f")), "john.doe@example.com", "John Doe"),
                Customer.Create(CustomerId.Of(Guid.Parse("5f846243-39eb-4e6c-8edf-ffa826ab7536")), "jane.smith@example.com", "Jane Smith"),
                Customer.Create(CustomerId.Of(Guid.NewGuid()), "alice.wong@example.com", "Alice Wong"),
                Customer.Create(CustomerId.Of(Guid.NewGuid()), "bob.jones@example.com", "Bob Jones"),
                Customer.Create(CustomerId.Of(Guid.NewGuid()), "carla.green@example.com", "Carla Green")
            };
    public static IEnumerable<Order> InitiliseOrderData()
    {
        var orders = new List<Order>();
        var shippingAddress1 = Address.Of(
                    firstName: "John",
                    lastName: "Doe",
                    emailAddress: "john.doe@example.com",
                    addressLine: "123 Main St",
                    country: "USA",
                    state: "California",
                    zipCode: "90001");

        var billingAddress1 = Address.Of(
                    firstName: "Jane",
                    lastName: "Doe",
                    emailAddress: "jane.doe@example.com",
                    addressLine: "456 Maple Ave",
                    country: "USA",
                    state: "New York",
                    zipCode: "10001");

        var payment1 = Payment.Of(
                    cardName: "John Doe",
                    cardNumber: "4111111111111111", 
                    expiration: "12/25",           
                    cvv: "123",                     
                    paymentMethod: 1);
        var order1 = Order.Create(
                    OrderId.Of(Guid.NewGuid()),
                    CustomerId.Of(Guid.Parse("2276537e-9e79-4e53-be44-f012509c1a6f")),
                    OrderName.Of("Buy Laptop"),
                    shippingAddress1,
                    billingAddress1,
                    payment1,
                    OrderStatus.Pending);
        var shippingAddress2 = Address.Of(
                    firstName: "Jane",
                    lastName: "Smith",
                    emailAddress: "jane.smith@example.com",
                    addressLine: "789 Oak St",
                    country: "USA",
                    state: "Florida",
                    zipCode: "33101");

        var billingAddress2 = Address.Of(
                    firstName: "Jane",
                    lastName: "Smith",
                    emailAddress: "jane.smith@example.com",
                    addressLine: "789 Oak St",
                    country: "USA",
                    state: "Florida",
                    zipCode: "33101");

        var payment2 = Payment.Of(
                    cardName: "Jane Smith",
                    cardNumber: "5500000000000004",
                    expiration: "11/26",
                    cvv: "456",
                    paymentMethod: 1);

        var order2 = Order.Create(
                    OrderId.Of(Guid.NewGuid()),
                    CustomerId.Of(Guid.Parse("5f846243-39eb-4e6c-8edf-ffa826ab7536")), // Jane Smith's customer ID
                    OrderName.Of("Buy Smartphone"),
                    shippingAddress2,
                    billingAddress2,
                    payment2,
                    OrderStatus.Pending);
        orders.Add(order1);
        orders.Add(order2);
        return orders;
    }
    public static IEnumerable<OrderItem> InitiliseOrderItemData()
         => default!;
    public static IEnumerable<Product> InitiliseProductData()
        => new List<Product>
            {
                Product.Create(ProductId.Of(Guid.NewGuid()), "Laptop", 999.99m),
                Product.Create(ProductId.Of(Guid.NewGuid()), "Smartphone", 599.99m),
                Product.Create(ProductId.Of(Guid.NewGuid()), "Tablet", 399.99m),
                Product.Create(ProductId.Of(Guid.NewGuid()), "Monitor", 199.99m),
                Product.Create(ProductId.Of(Guid.NewGuid()), "Keyboard", 49.99m)
            };
}

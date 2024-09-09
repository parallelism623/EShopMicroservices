using Ordering.Domain.Abstraction;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Models;
public class Customer : Entity<CustomerId>
{
    public string Email { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public static Customer Create(CustomerId customerId, string name, string email)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(email);
        var customer = new Customer
        {
            Email = email,
            Name = name,
            Id = customerId
        };
        return customer;
    }
}

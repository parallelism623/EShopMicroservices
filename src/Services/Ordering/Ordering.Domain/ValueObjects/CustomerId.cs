using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObjects;
public record CustomerId
{
    public Guid Value { get; }
    private CustomerId(Guid value) => Value = value;
    public static CustomerId Of(Guid id)
    {
        ArgumentNullException.ThrowIfNull(id);
        if(id == Guid.Empty)
        {
            throw new DomainException("Customer Id not be empty");
        }
        return new CustomerId(id);
    }
}

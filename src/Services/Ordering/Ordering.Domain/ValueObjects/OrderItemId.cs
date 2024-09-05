using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects;
public record OrderItemId
{
    public OrderItemId()
    {
        Value = Guid.NewGuid();
    }
    public Guid Value { get; }
}

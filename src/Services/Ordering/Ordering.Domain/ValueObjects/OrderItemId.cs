﻿using Ordering.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects;
public record OrderItemId
{
    public Guid Value { get; }
    private OrderItemId(Guid value) => Value = value;
    public static OrderItemId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if(value == Guid.Empty)
        {
            throw new DomainException("Order item id cannot be empty.");
        }
        return new(value);
    }
}

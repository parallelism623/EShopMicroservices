﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Abstraction;
public abstract class Aggregate<T> : Entity<T>, IAggregate<T>
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public IDomainEvent[] ClearDomainEvents()
    {
        IDomainEvent[] dequeueEvents = _domainEvents.ToArray();

        _domainEvents.Clear();

        return dequeueEvents;
    }
}

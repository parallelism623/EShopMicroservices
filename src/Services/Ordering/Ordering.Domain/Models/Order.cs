﻿using Ordering.Domain.Abstraction;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Models;
public class Order : Aggregate<OrderId> 
{
    private readonly List<OrderItem> _orderItems = new();

    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public CustomerId CustomerId { get; private set; } = default!;
    public OrderName OrderName { get; private set; } = default!;
    public Address ShippingAddress { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;

    public OrderStatus Status { get; private set; } = OrderStatus.Pending; 

    public decimal TotalPrice
    {
        get => OrderItems.Sum(oi => oi.Quantity * oi.Price);
        private set { }
    }

}

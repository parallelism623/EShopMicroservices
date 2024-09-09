using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects;
public record Payment
{
    public string? CardName { get; } = default!;
    public string CardNumber { get; } = default!;
    public string Expiration { get; } = default!;
    public string CVV { get; } = default!;
    public int PaymentMethod { get; } = default!;

    protected Payment() { }
    private Payment(string cardName, string cardNumber, string expiration, string cvv, int paymentMethod)
    {
        CardName = cardName; 
        CardNumber = cardNumber; 
        Expiration = expiration; 
        CVV = cvv;
        PaymentMethod = paymentMethod;
    }
    public static Payment Of(string cardName, string cardNumber, string expiration, string cvv, int paymentMethod)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(cardName);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(cardNumber);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(cvv);
        ArgumentOutOfRangeException.ThrowIfNotEqual(cvv.Length, 3);
        return new(cardName,  cardNumber, expiration, cvv, paymentMethod);
    }
}

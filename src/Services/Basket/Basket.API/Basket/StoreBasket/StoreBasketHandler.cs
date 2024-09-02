
using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket;


public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;


public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {

    }
}

public record StoreBasketResult(string UserName);

public class StoreBasketHandler(IBasketRepository basketRepository,
    DiscountProtoService.DiscountProtoServiceClient discountProto,
    ILogger<StoreBasketCommand> logger)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("StoreBasketHandler.Handle with called {@Command}", command);
       // await DeductDiscount(command.Cart, cancellationToken);
        await basketRepository.StoreShoppingCartAsync(command.Cart, cancellationToken);

        return new StoreBasketResult(command.Cart.UserName);
    }

    private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
    {
        foreach(var item in cart.Items)
        {
            var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName });
            item.Price -= coupon.Amount;
        }
    }
}

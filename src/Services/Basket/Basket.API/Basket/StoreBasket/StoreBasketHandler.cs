
namespace Basket.API.Basket.StoreBasket;


public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;


public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {

    }
}

public record StoreBasketResult(string UserName);

public class StoreBasketHandler(IBasketRepository basketRepository, ILogger<StoreBasketCommand> logger)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("StoreBasketHandler.Handle with called {@Command}", command);
        await basketRepository.StoreShoppingCartAsync(command.Cart, cancellationToken);

        return new StoreBasketResult(command.Cart.UserName);
    }
}

namespace Basket.API.Basket.DeleteBasket;


public record DeleteBasketCommand(string UserName) : IQuery<DeleteBasketResult>;

public record DeleteBasketResult(bool IsSuccess);

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("Tên người dùng đang trống");
    }
}
public class DeleteBasketHandler (IBasketRepository basketRepository, ILogger<DeleteBasketHandler> logger)
    : IQueryHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteBasketHandler.Hanle called with {@Command}", command);
        var cart = await basketRepository.GetShoppingCartAsync(command.UserName, cancellationToken);
        if (cart == null)
        {
            throw new ShoppingCartNotFoundException();
        }

        await basketRepository.DeleteShoppingCartAsync(command.UserName, cancellationToken);
        return new DeleteBasketResult(true);
    }
}

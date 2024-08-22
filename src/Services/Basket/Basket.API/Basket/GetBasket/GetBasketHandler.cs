namespace Basket.API.Basket.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCart ShoppingCart);
public class GetBasketHandler(ILogger<GetBasketHandler> logger, IBasketRepository basketRepository): 
    IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetBasketHandler.Handle called with {@Query}", query);
        var shoppingCart = await basketRepository.GetShoppingCartAsync(query.UserName, cancellationToken);

        if(shoppingCart == null)
        {
            throw new ShoppingCartNotFoundException();
        }

        return new GetBasketResult(shoppingCart!);
    }
}

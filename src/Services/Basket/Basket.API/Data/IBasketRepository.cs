namespace Basket.API.Data;

public interface IBasketRepository
{
    Task<ShoppingCart> GetShoppingCartAsync(string userName, CancellationToken cancellationToken);
    ShoppingCart GetShoppingCart(string userName);

    Task StoreShoppingCartAsync(ShoppingCart cart, CancellationToken cancellationToken);
    void StoreShoppingCart(ShoppingCart cart);

    Task DeleteShoppingCartAsync(string userName, CancellationToken cancellationToken);
    void DeleteShoppingCart(string userName);
}

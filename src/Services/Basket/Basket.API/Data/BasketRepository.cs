
namespace Basket.API.Data;

public class BasketRepository(IDocumentSession session) : IBasketRepository
{
    public void DeleteShoppingCart(string userName)
    {
        session.Delete<ShoppingCart>(userName); 
        session.SaveChanges();
    }

    public async Task DeleteShoppingCartAsync(string userName, CancellationToken cancellationToken = default!)
    {
        session.Delete<ShoppingCart>(userName);
        await session.SaveChangesAsync(cancellationToken);
    }

    public ShoppingCart GetShoppingCart(string userName)
    {
        var cart = session.Load<ShoppingCart>(userName);
        return cart;
    }

    public async Task<ShoppingCart> GetShoppingCartAsync(string userName, CancellationToken cancellationToken = default!)
    {
        var cart = await session.LoadAsync<ShoppingCart>(userName, cancellationToken);
        return cart;
    }

    public void StoreShoppingCart(ShoppingCart cart)
    {
        session.Store(cart);
        session.SaveChanges();
    }

    public async Task StoreShoppingCartAsync(ShoppingCart cart, CancellationToken cancellationToken = default!)
    {
        session.Store(cart);
        await session.SaveChangesAsync(cancellationToken);
    }
}

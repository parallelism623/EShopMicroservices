
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Threading;

namespace Basket.API.Data;

public class CachedBasketRepository(IBasketRepository basketRepository,
    IDistributedCache cachedBasket) : IBasketRepository
{
    public void DeleteShoppingCart(string userName)
    {
        basketRepository.DeleteShoppingCart(userName);
        cachedBasket.Remove(userName);
    }

    public async Task DeleteShoppingCartAsync(string userName, CancellationToken cancellationToken = default!)
    {
        await basketRepository.DeleteShoppingCartAsync(userName, cancellationToken);
        await cachedBasket.RemoveAsync(userName, cancellationToken);
    }

    public ShoppingCart GetShoppingCart(string userName)
    {
        var basketCached = cachedBasket.GetString(userName);
        if (!string.IsNullOrEmpty(basketCached))
        {
            return JsonSerializer.Deserialize<ShoppingCart>(basketCached)!;
        }
        var basket = basketRepository.GetShoppingCart(userName);
        return basket;
    }

    public async Task<ShoppingCart> GetShoppingCartAsync(string userName, CancellationToken cancellationToken = default!)
    {
        var basketCached = await cachedBasket.GetStringAsync(userName, cancellationToken);
        if(!string.IsNullOrEmpty(basketCached))
        {
            return JsonSerializer.Deserialize<ShoppingCart>(basketCached)!;
        }
        var basket = await basketRepository.GetShoppingCartAsync(userName, cancellationToken);
        return basket;
    }

    public void StoreShoppingCart(ShoppingCart cart)
    {
        basketRepository.StoreShoppingCart(cart);
        cachedBasket.SetString(cart.UserName, JsonSerializer.Serialize<ShoppingCart>(cart));
    }

    public async Task StoreShoppingCartAsync(ShoppingCart cart, CancellationToken cancellationToken = default!)
    {
        await basketRepository.StoreShoppingCartAsync(cart, cancellationToken);
        await cachedBasket.SetStringAsync(cart.UserName, JsonSerializer.Serialize<ShoppingCart>(cart), cancellationToken);
    }
}

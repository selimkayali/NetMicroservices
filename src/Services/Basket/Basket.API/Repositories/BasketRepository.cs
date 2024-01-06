using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Basket.API.Repositories;

public sealed class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache _cache;

    public BasketRepository(IDistributedCache cache)
    {
        ArgumentNullException.ThrowIfNull(cache);

        _cache = cache;
    }

    public async Task<ShoppingCart> GetBasketAsync(string username)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(username);

        var basket = await _cache.GetStringAsync(username);

        if (basket == null)
        {
            return new ShoppingCart(username);
        }

        return JsonSerializer.Deserialize<ShoppingCart>(basket);
    }

    public async Task<ShoppingCart> UpdateBasketAsync(ShoppingCart basket)
    {
        ArgumentNullException.ThrowIfNull(basket);

        await _cache.SetStringAsync(basket.Username, JsonSerializer.Serialize(basket));

        return await GetBasketAsync(basket.Username);
    }

    public async Task DeleteBasket(string username)
    {
        await _cache.RemoveAsync(username);
    }

}

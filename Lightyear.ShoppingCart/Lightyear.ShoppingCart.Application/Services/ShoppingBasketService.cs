using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lightyear.ShoppingCart.Application.Abstractions;
using Lightyear.ShoppingCart.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Lightyear.ShoppingCart.Application.Services
{
    public class ShoppingBasketService : IShoppingBasketService
    {
        private readonly IDistributedCache _cache;
        private const string BASKET_REGISTRY = "BASKET_REGISTRY";

        public ShoppingBasketService(IDistributedCache cache)
        {
            _cache = cache;

            // Lista de cestas de compra existentes.
            _cache.Set(BASKET_REGISTRY, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new List<string>())));
        }

        private async Task<List<string>> RecoverBasketRegistryAsync()
        {
            var basketRegistryBytes = await _cache.GetAsync(BASKET_REGISTRY);
            return JsonConvert.DeserializeObject<List<string>>(Encoding.UTF8.GetString(basketRegistryBytes));
        }

        private async Task AddToBasketRegistryAsync(string newBasketId)
        {
            var basketRegistry = await RecoverBasketRegistryAsync();
            basketRegistry.Add(newBasketId);
            _cache.Set(BASKET_REGISTRY, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(basketRegistry)));
        }

        private async Task RemoveFromBasketRegistryAsync(string basketId)
        {
            var basketRegistry = await RecoverBasketRegistryAsync();
            basketRegistry.Remove(basketId);
            _cache.Set(BASKET_REGISTRY, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(basketRegistry)));
        }

        public async Task AddAsync(ShoppingBasket basket)
        {
            basket.Id = Guid.NewGuid();
            await AddToBasketRegistryAsync(basket.Id.ToString());
            await _cache.SetAsync(basket.Id.ToString(), Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(basket)));
        }

        public async Task<IEnumerable<ShoppingBasket>> GetAllAsync()
        {
            var basketRegistry = await RecoverBasketRegistryAsync();
            var shoppingBasketList = new List<ShoppingBasket>(basketRegistry.Count);

            foreach (var basketId in basketRegistry)
            {
                shoppingBasketList.Add(await FindAsync(basketId));
            }

            return shoppingBasketList;
        }

        public async Task<ShoppingBasket> FindAsync(string id)
        {
            var basketBytes = await _cache.GetAsync(id);
            if (basketBytes == null)
                return null;

            return JsonConvert.DeserializeObject<ShoppingBasket>(Encoding.UTF8.GetString(basketBytes));
        }

        public async Task UpdateAsync(ShoppingBasket basket)
        {
            await _cache.SetAsync(basket.Id.ToString(), Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(basket)));
        }

        public async Task UpdateRangeAsync(IEnumerable<ShoppingBasket> shoppingBasketList)
        {
            foreach (var basket in shoppingBasketList)
            {
                await UpdateAsync(basket);
            }
        }

        public async Task RemoveAsync(ShoppingBasket basket)
        {
            await RemoveFromBasketRegistryAsync(basket.Id.ToString());
            await _cache.RemoveAsync(basket.Id.ToString());
        }
    }
}

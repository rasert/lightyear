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

        public ShoppingBasketService(IDistributedCache cache)
        {
            _cache = cache;
        }

        private async Task AddAsync(ShoppingBasket basket)
        {
            await _cache.SetAsync(basket.Id.ToString(), Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(basket.Items)));
        }

        public async Task<ShoppingBasket> FindAsync(string id)
        {
            var basketBytes = await _cache.GetAsync(id);
            return JsonConvert.DeserializeObject<ShoppingBasket>(Encoding.UTF8.GetString(basketBytes));
        }

        public async Task UpdateAsync(ShoppingBasket basket)
        {
            await RemoveAsync(basket);
            await AddAsync(basket);
        }

        public async Task RemoveAsync(ShoppingBasket basket)
        {
            await _cache.RemoveAsync(basket.Id.ToString());
        }

        public async Task<ShoppingBasket> NewBasketAsync()
        {
            var basket = new ShoppingBasket()
            {
                Id = Guid.NewGuid(),
                Items = new List<ShoppingItem>()
            };

            await AddAsync(basket);

            return basket;
        }
    }
}

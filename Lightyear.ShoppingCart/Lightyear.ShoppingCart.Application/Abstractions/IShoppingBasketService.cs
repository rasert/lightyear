using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lightyear.ShoppingCart.Domain.Entities;

namespace Lightyear.ShoppingCart.Application.Abstractions
{
    public interface IShoppingBasketService
    {
        Task AddAsync(ShoppingBasket basket);
        Task UpdateAsync(ShoppingBasket basket);
        Task UpdateRangeAsync(IEnumerable<ShoppingBasket> shoppingBasketList);
        Task RemoveAsync(ShoppingBasket basket);
        Task<ShoppingBasket> FindAsync(string id);
        Task<IEnumerable<ShoppingBasket>> GetAllAsync();
    }
}

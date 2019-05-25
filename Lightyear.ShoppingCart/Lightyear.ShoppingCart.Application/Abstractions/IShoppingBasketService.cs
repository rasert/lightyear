using System;
using System.Threading.Tasks;
using Lightyear.ShoppingCart.Domain.Entities;

namespace Lightyear.ShoppingCart.Application.Abstractions
{
    public interface IShoppingBasketService
    {
        Task AddAsync(ShoppingBasket basket);
        Task UpdateAsync(ShoppingBasket basket);
        Task RemoveAsync(ShoppingBasket basket);
        Task<ShoppingBasket> FindAsync(string id);
    }
}

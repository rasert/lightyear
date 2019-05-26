using System;
using System.Threading.Tasks;
using Lightyear.ShoppingCart.Domain.Entities;

namespace Lightyear.ShoppingCart.Application.Abstractions
{
    public interface IShoppingBasketService
    {
        Task<ShoppingBasket> NewBasketAsync();
        Task UpdateAsync(ShoppingBasket basket);
        Task RemoveAsync(ShoppingBasket basket);
        Task<ShoppingBasket> FindAsync(string id);
    }
}

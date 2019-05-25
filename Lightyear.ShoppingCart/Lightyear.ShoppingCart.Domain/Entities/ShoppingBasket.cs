using System.Collections.Generic;
using Lightyear.ShoppingCart.Domain.Abstractions;

namespace Lightyear.ShoppingCart.Domain.Entities
{
    public class ShoppingBasket: IEntity
    {
        public int Id { get; set; }
        public List<ShoppingItem> Items { get; set; }
    }
}

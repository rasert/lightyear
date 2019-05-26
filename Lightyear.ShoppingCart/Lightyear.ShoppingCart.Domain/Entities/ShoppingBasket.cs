using System;
using System.Collections.Generic;

namespace Lightyear.ShoppingCart.Domain.Entities
{
    public class ShoppingBasket
    {
        public Guid Id { get; set; }
        public List<ShoppingItem> Items { get; set; }
    }
}

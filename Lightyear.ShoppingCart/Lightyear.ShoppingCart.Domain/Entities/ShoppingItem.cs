namespace Lightyear.ShoppingCart.Domain.Entities
{
    public class ShoppingItem
    {
        public int ProductId { get; set; }
        public decimal ProductValue { get; set; }
        public int Quantity { get; set; }
        // TODO: Total value
    }
}

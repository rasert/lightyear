using System;
using System.Linq;
using System.Threading.Tasks;
using Lightyear.EventBus.Contracts;
using Lightyear.ShoppingCart.Application.Abstractions;
using MassTransit;

namespace Lightyear.ShoppingCart.API.Consumers
{
    public class UpdatedPriceConsumer : IConsumer<IUpdatedPrice>
    {
        private readonly IShoppingBasketService _shoppingBasketService;

        public UpdatedPriceConsumer(IShoppingBasketService shoppingBasketService)
        {
            _shoppingBasketService = shoppingBasketService;
        }

        public async Task Consume(ConsumeContext<IUpdatedPrice> context)
        {
            var shoppingBasketList = await _shoppingBasketService.GetAllAsync();
            var filteredBasketList = shoppingBasketList.Where(basket => basket.Items.Any(item => item.ProductId == context.Message.ProductID));

            foreach (var basket in filteredBasketList)
            {
                var shoppingItemList = basket.Items.Where(item => item.ProductId == context.Message.ProductID);
                shoppingItemList.ToList().ForEach(item => item.ProductValue = context.Message.NewValue);
            }

            await _shoppingBasketService.UpdateRangeAsync(filteredBasketList);
        }
    }
}

using System;
using System.Threading.Tasks;
using Lightyear.EventBus.Contracts;
using MassTransit;

namespace Lightyear.ShoppingCart.API.Consumers
{
    public class UpdatedPriceConsumer : IConsumer<IUpdatedPrice>
    {
        public UpdatedPriceConsumer()
        {
        }

        public Task Consume(ConsumeContext<IUpdatedPrice> context)
        {
            throw new NotImplementedException();
        }
    }
}

using MassTransit;
using System.Threading.Tasks;
using Lightyear.EventBus.Contracts;
using Lightyear.Catalog.API.Abstractions;

namespace Lightyear.Catalog.API.Publishers
{
    public class ProductEventPublisher : IProductEventPublisher
    {
        private readonly IBus _eventBus;

        public ProductEventPublisher(IBus bus)
        {
            _eventBus = bus;
        }

        public async Task NotifyUpdatedPriceAsync(object updatedPrice)
        {
            await _eventBus.Publish<IUpdatedPrice>(updatedPrice);
        }
    }
}

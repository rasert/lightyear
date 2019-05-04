using System.Threading.Tasks;

namespace Lightyear.Catalog.API.Abstractions
{
    public interface IProductEventPublisher
    {
        Task NotifyUpdatedPriceAsync(object updatedPrice);
    }
}

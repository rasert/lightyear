using System.Threading.Tasks;
using Lightyear.Catalog.Domain.Entities;

namespace Lightyear.Catalog.Application.Abstractions
{
    public interface ICatalogUow
    {
        IRepository<Product> ProductRep { get; set; }

        Task<int> SaveChangesAsync();
    }
}

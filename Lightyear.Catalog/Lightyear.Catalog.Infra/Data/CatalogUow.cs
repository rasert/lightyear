using System.Threading.Tasks;
using Lightyear.Catalog.Application.Abstractions;
using Lightyear.Catalog.Domain.Entities;

namespace Lightyear.Catalog.Infra.Data
{
    public class CatalogUow : ICatalogUow
    {
        private readonly CatalogContext _context;
        public IRepository<Product> ProductRep { get; set; }

        public CatalogUow(CatalogContext context, IRepository<Product> productRep)
        {
            _context = context;
            ProductRep = productRep;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

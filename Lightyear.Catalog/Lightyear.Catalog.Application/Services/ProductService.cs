using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Lightyear.Catalog.Application.Abstractions;
using Lightyear.Catalog.Domain.Entities;

namespace Lightyear.Catalog.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly ICatalogUow _catalogUow;
        private readonly IRepository<Product> _productRep;

        public ProductService(ICatalogUow catalogUow)
        {
            _catalogUow = catalogUow;
            _productRep = catalogUow.ProductRep;
        }

        public async Task AddAsync(Product product)
        {
            _productRep.Add(product);
            await _catalogUow.SaveChangesAsync();
        }

        public Product Find(params object[] keyValues)
        {
            return _productRep.Find(keyValues);
        }

        public IEnumerable<Product> Get(Expression<Func<Product, bool>> filter = null)
        {
            return _productRep.Get(filter);
        }

        public async Task RemoveAsync(Product product)
        {
            _productRep.Remove(product);
            await _catalogUow.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _productRep.Update(product);
            await _catalogUow.SaveChangesAsync();
        }
    }
}

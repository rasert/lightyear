using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Lightyear.Catalog.Domain.Entities;

namespace Lightyear.Catalog.Application.Abstractions
{
    public interface IProductService
    {
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task RemoveAsync(Product product);
        IEnumerable<Product> Get(Expression<Func<Product, bool>> filter = null);
        Product Find(params object[] keyValues);
    }
}

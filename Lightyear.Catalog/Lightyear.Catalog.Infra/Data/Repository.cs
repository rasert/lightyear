using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lightyear.Catalog.Application.Abstractions;
using Lightyear.Catalog.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Lightyear.Catalog.Infra.Data
{
    public class Repository<T> : IRepository<T> where T: class, IEntity
    {
        private readonly DbSet<T> _dbSet;

        public Repository(CatalogContext context)
        {
            _dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public T Find(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null)
        {
            var query = _dbSet.AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            return query.ToList();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}

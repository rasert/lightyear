using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Lightyear.Catalog.Domain.Abstractions;

namespace Lightyear.Catalog.Application.Abstractions
{
    public interface IRepository<T> where T : class, IEntity
    {
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        T Find(params object[] keyValues);
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null);
    }
}

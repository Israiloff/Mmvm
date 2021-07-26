using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Israiloff.Mmvm.Net.Core.DataAccessLayer.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        TEntity Insert(TEntity entity);

        IEnumerable<TEntity> InsertAll(IEnumerable<TEntity> entities);

        TEntity Update(TEntity entity);

        IEnumerable<TEntity> UpdateAll(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);

        void DeleteAll();

        void DeleteAll(IEnumerable<TEntity> entities);

        TEntity Find(long id);

        TEntity FindFirst(Expression<Func<TEntity, bool>> predicate);

        TEntity FindLast(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> FindAll();

        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);

        long Count();

        long Count(Expression<Func<TEntity, bool>> predicate);
    }
}
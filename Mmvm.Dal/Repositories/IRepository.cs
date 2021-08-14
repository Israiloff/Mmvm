using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Israiloff.Mmvm.Net.Dal.Model;

namespace Israiloff.Mmvm.Net.Dal.Repositories
{
    public interface IRepository<TEntity, TEntityId> : IDisposable
        where TEntity : class, IEntity<TEntityId>
    {
        TEntity Insert(TEntity entity);

        ICollection<TEntity> InsertAll(ICollection<TEntity> entities);

        TEntity Update(TEntity entity);

        ICollection<TEntity> UpdateAll(ICollection<TEntity> entities);

        void Delete(TEntity entity);

        void DeleteAll(ICollection<TEntity> entities);

        TEntity Find(TEntityId id);

        TEntity FindFirst(Expression<Func<TEntity, bool>> predicate);

        TEntity FindLast(Expression<Func<TEntity, bool>> predicate);

        ICollection<TEntity> FindAll();

        ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);

        long Count();

        long Count(Expression<Func<TEntity, bool>> predicate);

        void SaveChanges();
    }
}
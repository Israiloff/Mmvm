using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Israiloff.Mmvm.Net.Dal.Services
{
    public interface IDbContext<TSet, TSetGeneric, TEntityEntry, TValidationResult> : IDisposable
        where TSet : class
        where TEntityEntry : class
        where TValidationResult : class
    {
        TEntityEntry Entry(object entity);
        IEnumerable<TValidationResult> GetValidationErrors();
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        TSet Set(Type entityType);
        TSetGeneric Set<TEntity>() where TEntity : class;
    }
}
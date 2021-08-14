using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Israiloff.Mmvm.Net.Container.Attributes;
using Israiloff.Mmvm.Net.Core.Services.Logger;
using Israiloff.Mmvm.Net.Dal.Enums;
using Israiloff.Mmvm.Net.Dal.Impl.Utils;
using Israiloff.Mmvm.Net.Dal.Model;
using Israiloff.Mmvm.Net.Dal.Repositories;
using Israiloff.Mmvm.Net.Dal.Services;
using Israiloff.Mmvm.Net.Dal.Utils;

namespace Israiloff.Mmvm.Net.Dal.Impl.Repositories
{
    // todo - test injection for generics
    [Service]
    public class Repository<TEntity, TEntityId> : IRepository<IEntity<TEntityId>, TEntityId>, IDisposable
        where TEntity : class, IEntity<TEntityId>
    {
        #region Constructors

        public Repository(ILogger logger, IEntityValidator<TEntity, TEntityId> entityValidator,
            IDbContext<DbSet, DbSet<IEntity<TEntityId>>, DbEntityEntry, DbEntityValidationResult> dbContext,
            EntityUtil<TEntityId> entityUtil)
        {
            Logger = logger;
            EntityValidator = entityValidator;
            DbContext = dbContext;
            EntityUtil = entityUtil;
        }

        #endregion

        #region Private properties

        private IEntityValidator<TEntity, TEntityId> EntityValidator { get; }

        private ILogger Logger { get; }

        private IDbContext<DbSet, DbSet<IEntity<TEntityId>>, DbEntityEntry, DbEntityValidationResult> DbContext { get; }

        private EntityUtil<TEntityId> EntityUtil { get; }

        #endregion

        #region IRepository impl

        public IEntity<TEntityId> Insert(IEntity<TEntityId> entity)
        {
            Logger.Info("Inserting entity : {0}", entity);
            EntityValidator.Validate(entity as TEntity);
            EntityUtil.ResolveEntityState(entity, DbContext, ObjectState.Created);
            return entity;
        }

        public ICollection<IEntity<TEntityId>> InsertAll(ICollection<IEntity<TEntityId>> entities)
        {
            Logger.Info("Inserting entities. Entities count : {0}", entities.Count);
            Parallel.ForEach(entities, entity => Insert(entity));
            return entities;
        }

        public IEntity<TEntityId> Update(IEntity<TEntityId> entity)
        {
            Logger.Info("Updating entity : {0}", entity);
            EntityValidator.Validate(entity as TEntity);
            EntityUtil.ResolveEntityState(entity, DbContext, ObjectState.Modified);
            return entity;
        }

        public ICollection<IEntity<TEntityId>> UpdateAll(ICollection<IEntity<TEntityId>> entities)
        {
            Logger.Info($"Updating entities. Entities count : {entities.Count}");
            Parallel.ForEach(entities, entity => Update(entity));
            return entities;
        }

        public void Delete(IEntity<TEntityId> entity)
        {
            Logger.Warn("Deleting entity : {0}", entity);
            EntityValidator.Validate(entity as TEntity);
            EntityUtil.ResolveEntityState(entity, DbContext, ObjectState.Deleted);
        }

        public void DeleteAll(ICollection<IEntity<TEntityId>> entities)
        {
            Logger.Warn("Deleting entities. Deleting data count : {0}", entities.Count);
            Parallel.ForEach(entities, Delete);
        }

        public IEntity<TEntityId> Find(TEntityId id)
        {
            Logger.Info($"Getting entity with id : {id}");

            EntityValidator.Validate(id);
            var result = DbContext.Set<TEntity>()
                .FirstOrDefault(entity => entity.Id.Equals(id));

            Logger.Debug("Result found : {0}", result);
            return result;
        }

        public IEntity<TEntityId> FindFirst(Expression<Func<IEntity<TEntityId>, bool>> predicate)
        {
            Logger.Info("Getting entity by criteria");
            var result = DbContext.Set<TEntity>().Where(predicate).FirstOrDefault();
            Logger.Debug("Result found : {0}", result);
            return result;
        }

        public IEntity<TEntityId> FindLast(Expression<Func<IEntity<TEntityId>, bool>> predicate)
        {
            Logger.Info("Getting last entity by criteria");
            var result = DbContext.Set<TEntity>().Where(predicate).LastOrDefault();
            Logger.Debug("Result found : {0}", result);
            return result;
        }

        public ICollection<IEntity<TEntityId>> FindAll()
        {
            Logger.Info("Getting all entites");
            var result = DbContext.Set<TEntity>().ToList();
            Logger.Debug("Result count : {0}", result.Count);
            return result;
        }

        public ICollection<IEntity<TEntityId>> FindAll(Expression<Func<IEntity<TEntityId>, bool>> predicate)
        {
            Logger.Info("Getting range of entities by criteria");
            var result = DbContext.Set<TEntity>().Where(predicate).ToList();
            Logger.Debug("Result count : {0}", result.Count);
            return result;
        }

        public long Count()
        {
            Logger.Info("Getting count of all entities");
            var result = DbContext.Set<TEntity>().Count();
            Logger.Debug("Result count : {0}", result);
            return result;
        }

        public long Count(Expression<Func<IEntity<TEntityId>, bool>> predicate)
        {
            Logger.Info("Getting count of all entities by criteria");
            var result = DbContext.Set<TEntity>().Where(predicate).Count();
            Logger.Debug("Result count : {0}", result);
            return result;
        }

        public void SaveChanges()
        {
            Logger.Debug("Commiting changes");
            DbContext.SaveChanges();
        }

        #endregion

        #region IDisposable impl

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            Logger.Warn($"Parametrized Dispose started with disposing param : {disposing}");

            if (_disposed || !disposing) return;

            DbContext.Dispose();
            _disposed = true;

            Logger.Info("Dispose process finished");
        }

        public void Dispose()
        {
            Logger.Warn("Dispose started");

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
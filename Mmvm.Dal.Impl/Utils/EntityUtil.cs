using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using Israiloff.Mmvm.Net.Container.Attributes;
using Israiloff.Mmvm.Net.Core.Services.Logger;
using Israiloff.Mmvm.Net.Dal.Enums;
using Israiloff.Mmvm.Net.Dal.Model;
using Israiloff.Mmvm.Net.Dal.Services;
using Israiloff.Mmvm.Net.Dal.Utils;

namespace Israiloff.Mmvm.Net.Dal.Impl.Utils
{
    [Component(Name = nameof(EntityUtil<TEntityId>))]
    public class EntityUtil<TEntityId> : IEntityUtil<TEntityId, DbSet, DbSet<IEntity<TEntityId>>, DbEntityEntry,
        DbEntityValidationResult>
    {
        #region Constructors

        public EntityUtil(ILogger logger)
        {
            Logger = logger;
        }

        #endregion

        #region private properties

        private ILogger Logger { get; }

        #endregion

        #region IEntityUtil impl

        public IEntity<TEntityId> ResolveEntityState(IEntity<TEntityId> entity,
            IDbContext<DbSet, DbSet<IEntity<TEntityId>>, DbEntityEntry, DbEntityValidationResult> dbContext,
            ObjectState state = ObjectState.Detached)
        {
            Logger.Debug("Changing entity\'s property state");
            ResolveUntrackedEntityState(entity, dbContext, state);

            Logger.Debug("Changing entity childes' property state");
            entity.GetRelations()?.ToList().ForEach(related =>
            {
                ResolveUntrackedEntityState(related, dbContext, ObjectState.Modified);
                related.GetRelations()?.ToList()
                    .ForEach(number =>
                        ResolveUntrackedEntityState(number, dbContext));
            });

            return entity;
        }

        #endregion

        #region Private methods

        private void ResolveUntrackedEntityState(IEntity<TEntityId> entity,
            IDbContext<DbSet, DbSet<IEntity<TEntityId>>, DbEntityEntry, DbEntityValidationResult> dbContext,
            ObjectState state = ObjectState.Detached)
        {
            Logger.Debug("EntityStateResolver started for entity : {0}", entity);
            var resolvedState = entity.Id == null ? EntityState.Added : ResolveState(state);
            Logger.Debug("State resolved to : {0:G}", resolvedState);
            dbContext.Entry(entity).State = resolvedState;
        }

        private EntityState ResolveState(ObjectState state)
        {
            Logger.Trace("ResolveState started for object state : {0}", state);

            switch (state)
            {
                case ObjectState.Created:
                    return EntityState.Added;
                case ObjectState.Modified:
                    return EntityState.Modified;
                case ObjectState.Detached:
                    return EntityState.Detached;
                case ObjectState.Deleted:
                    return EntityState.Deleted;
                default:
                    return EntityState.Unchanged;
            }
        }

        #endregion
    }
}
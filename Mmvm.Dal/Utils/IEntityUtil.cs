using Israiloff.Mmvm.Net.Dal.Enums;
using Israiloff.Mmvm.Net.Dal.Model;
using Israiloff.Mmvm.Net.Dal.Services;

namespace Israiloff.Mmvm.Net.Dal.Utils
{
    public interface IEntityUtil<TId, TSet, TSetGeneric, TEntityEntry, TValidationResult>
        where TSet : class
        where TEntityEntry : class
        where TValidationResult : class
    {
        IEntity<TId> ResolveEntityState(IEntity<TId> entity,
            IDbContext<TSet, TSetGeneric, TEntityEntry, TValidationResult> dbContext, ObjectState state);
    }
}
namespace Israiloff.Mmvm.Net.Dal.Utils
{
    public interface IEntityValidator<TEntity, TEntityId>
    {
        void Validate(TEntity entity);
        void Validate(TEntityId id);
    }
}
using Israiloff.Mmvm.Net.Container.Attributes;
using Israiloff.Mmvm.Net.Core.Exceptions;
using Israiloff.Mmvm.Net.Core.Services.Logger;
using Israiloff.Mmvm.Net.Dal.Utils;

namespace Israiloff.Mmvm.Net.Dal.Impl.Utils
{
    [Component]
    public class EntityValidator<TEntity, TEntityId> : IEntityValidator<TEntity, TEntityId>
    {
        #region Constructors

        public EntityValidator(ILogger logger)
        {
            Logger = logger;
        }

        #endregion

        #region Constants

        private static string NullArgumentText => "Argument is null";

        #endregion

        #region Private properties

        private ILogger Logger { get; }

        #endregion

        #region IEntityValidator impl

        public void Validate(TEntity entity)
        {
            Logger.Trace("Entity validation started");

            if (entity == null)
            {
                Logger.Error("Validating entity is null");
                throw new DataValidationException(NullArgumentText);
            }
        }

        public void Validate(TEntityId id)
        {
            Logger.Trace("Id validation started");

            if (id == null)
            {
                Logger.Error("Validating id is null");
                throw new DataValidationException(NullArgumentText);
            }
        }

        #endregion
    }
}
using System.Collections.Generic;

namespace Israiloff.Mmvm.Net.Dal.Model
{
    /// <summary>
    ///     Parent interface for all entities
    /// </summary>
    /// <typeparam name="TId">Type of unique primary key</typeparam>
    public interface IEntity<TId>
    {
        /// <summary>
        ///     Unique primary key
        /// </summary>
        TId Id { get; set; }

        /// <summary>
        ///     Method for getting instances of entity's relations (i.e. childs)
        /// </summary>
        /// <returns>Entity's relations</returns>
        ICollection<IEntity<TId>> GetRelations();
    }
}
using System.Collections.Generic;
using System.Linq;

namespace Israiloff.Mmvm.Net.Dal.Model
{
    public abstract class Entity<TId> : IEntity<TId>
    {
        public virtual TId Id { get; set; }

        public ICollection<IEntity<TId>> GetRelations()
        {
            return GetType()
                .GetProperties()
                .Where(info => info.PropertyType == typeof(Entity<TId>))
                .Select(info => info.GetValue(this, null) as IEntity<TId>)
                .ToList();
        }
    }
}
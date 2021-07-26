using System;
using System.Collections.Generic;
using Israiloff.Mmvm.Net.Container.Model;

namespace Israiloff.Mmvm.Net.Container.Configs
{
    public interface IContainerConfig<TContainer>
    {
        TContainer GetContainer(IMapperProfile mapperProfile, ICollection<Type> registrationTypes);
    }
}
using System;
using System.Collections.Generic;
using Autofac;
using Israiloff.Mmvm.Net.Container.Impl.Config;
using Israiloff.Mmvm.Net.Core.Impl.Services.AssemblyLoader;
using Israiloff.Mmvm.Net.Core.Impl.Services.Logger;
using Israiloff.Mmvm.Net.Core.Impl.Services.Serializer;
using Israiloff.Mmvm.Net.Core.Services.AssemblyLoader;
using Israiloff.Mmvm.Net.Core.Services.Logger;
using Israiloff.Mmvm.Net.Mvvm.Core.Model;
using Israiloff.Mmvm.Net.Mvvm.Core.Services.ApplicationInitializer;
using Israiloff.Mmvm.Net.Mvvm.View.Services.ResourceInitializer;
using Israiloff.Mmvm.Net.Navigation.Services;

namespace Israiloff.Mmvm.Net.Mvvm.Core.Impl.Services.ApplicationInitializer
{
    public class ApplicationInitializer : IApplicationInitializer
    {
        #region Private properties

        private ILogger Logger => new NLogLogger(typeof(ApplicationInitializer), new JsonSerializer());

        #endregion

        #region IApplicationInitializer impl

        public INavigationService Initialize(string moduleRegex, IMapperProfile profile)
        {
            var types = InitializeAppResources(moduleRegex);

            var container = new ContainerConfig()
                .Config
                .GetContainer(profile, types);

            var assemblyLoader = container.Resolve<IAssemblyLoader>();
            var resourceInitializer = container.Resolve<IResourceInitializer>();
            resourceInitializer.Initialize(assemblyLoader.GetAllLoadedTypes());

            return container.Resolve<INavigationService>();
        }

        #endregion

        #region Private methods

        private ICollection<Type> InitializeAppResources(string moduleRegex)
        {
            Logger.Debug("Adding VM binding dictionary to merged dictionaries");
            var types = new CommonAssemblyLoader(Logger).Load(moduleRegex)?.LoadedTypes;
            return types;
        }

        #endregion
    }
}
using Israiloff.Mmvm.Net.Container.Attributes;
using Israiloff.Mmvm.Net.Container.Enums;
using Israiloff.Mmvm.Net.Container.Services.IocContainer;
using Israiloff.Mmvm.Net.Core.Model.NavigationEngine;
using Israiloff.Mmvm.Net.Core.Services.Logger;
using Israiloff.Mmvm.Net.Navigation.Utilities;

namespace Israiloff.Mmvm.Net.Navigation.Impl.Utilities
{
    [Service(Name = nameof(NavigationUtilityService), LifetimeScope = LifetimeScope.SingleInstance)]
    public class NavigationUtilityService : INavigationUtilityService
    {
        #region Constructors

        public NavigationUtilityService(ILogger logger, IIocContainer container)
        {
            Logger = logger;
            Container = container;
        }

        #endregion

        #region INavigationUtilityService

        public bool TryGetPageObject(string pageName, out INavigationNode navigatingPageObject)
        {
            Logger.Debug("GetPageObject method started");

            Logger.Debug("Searching for requested page object");
            navigatingPageObject = Container.ResolveOptional<INavigationNode>(pageName);

            if (navigatingPageObject == null)
            {
                Logger.Warn("Requested page not found. Operation terminated");
                return false;
            }

            return true;
        }

        #endregion

        #region Private properties

        private ILogger Logger { get; }

        private IIocContainer Container { get; }

        #endregion
    }
}
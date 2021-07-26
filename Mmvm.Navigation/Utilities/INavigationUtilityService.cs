using Israiloff.Mmvm.Net.Core.Model.NavigationEngine;

namespace Israiloff.Mmvm.Net.Navigation.Utilities
{
    public interface INavigationUtilityService
    {
        bool TryGetPageObject(string pageName, out INavigationNode navigatingPageObject);
    }
}
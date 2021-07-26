using Israiloff.Mmvm.Net.Core.Model.NavigationEngine;

namespace Mmvm.Net.Navigation.Utilities
{
    public interface INavigationUtilityService
    {
        bool TryGetPageObject(string pageName, out INavigationNode navigatingPageObject);
    }
}
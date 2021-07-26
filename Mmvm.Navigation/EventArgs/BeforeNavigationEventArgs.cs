using Israiloff.Mmvm.Net.Core.Model.NavigationEngine.StructuralModels;

namespace Mmvm.Net.Navigation.EventArgs
{
    public class BeforeNavigationEventArgs : System.EventArgs
    {
        #region Constructors

        public BeforeNavigationEventArgs(BranchInfo branchInfo, string currentPageName, string navigatingPageName)
        {
            BranchInfo = branchInfo;
            CurrentPageName = currentPageName;
            NavigatingPageName = navigatingPageName;
        }

        #endregion

        #region Properties

        public BranchInfo BranchInfo { get; }

        public string CurrentPageName { get; }

        public string NavigatingPageName { get; }

        #endregion
    }
}
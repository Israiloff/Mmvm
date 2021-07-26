using Israiloff.Mmvm.Net.Core.Model.NavigationEngine.StructuralModels;

namespace Israiloff.Mmvm.Net.Navigation.EventArgs
{
    public class AfterNavigationEventArgs : System.EventArgs
    {
        #region Constructors

        public AfterNavigationEventArgs(string navigatedPageName, BranchInfo branchInfo)
        {
            NavigatedPageName = navigatedPageName;
            BranchInfo = branchInfo;
        }

        #endregion

        #region Properties

        public string NavigatedPageName { get; }

        public BranchInfo BranchInfo { get; }

        #endregion
    }
}
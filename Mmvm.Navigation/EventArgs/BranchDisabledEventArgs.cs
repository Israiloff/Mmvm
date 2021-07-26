using Israiloff.Mmvm.Net.Core.Model.NavigationEngine.StructuralModels;

namespace Israiloff.Mmvm.Net.Navigation.EventArgs
{
    public class BranchDisabledEventArgs
    {
        #region Constructors

        public BranchDisabledEventArgs(BranchInfo branchInfo)
        {
            BranchInfo = branchInfo;
        }

        #endregion

        #region Properties

        public BranchInfo BranchInfo { get; }

        #endregion
    }
}
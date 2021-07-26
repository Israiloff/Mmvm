namespace Israiloff.Mmvm.Net.Core.Model.NavigationEngine.StructuralModels
{
    public class NavigateArgs
    {
        #region Constructors

        public NavigateArgs(BranchInfo branchInfo, string nodeName)
        {
            BranchInfo = branchInfo;
            NodeName = nodeName;
        }

        #endregion

        #region Public properties

        public BranchInfo BranchInfo { get; }

        public string NodeName { get; }

        #endregion
    }
}
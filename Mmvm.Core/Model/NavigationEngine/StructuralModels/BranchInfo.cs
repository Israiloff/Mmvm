using System;

namespace Israiloff.Mmvm.Net.Core.Model.NavigationEngine.StructuralModels
{
    public class BranchInfo : ICloneable
    {
        #region Constructors

        public BranchInfo(string parentNodeName, string branchName)
        {
            ParentNodeName = parentNodeName;
            BranchName = branchName;
        }

        #endregion

        #region ICloneable impl

        public object Clone()
        {
            return new BranchInfo(ParentNodeName, BranchName);
        }

        #endregion

        #region Public properties

        public string ParentNodeName { get; }

        public string BranchName { get; }

        #endregion
    }
}
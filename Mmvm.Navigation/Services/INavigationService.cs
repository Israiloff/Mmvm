using Israiloff.Mmvm.Net.Core.Model.NavigationEngine;
using Israiloff.Mmvm.Net.Core.Model.NavigationEngine.StructuralModels;
using Mmvm.Net.Navigation.Delegates;
using Mmvm.Net.Navigation.Enums;

namespace Mmvm.Net.Navigation.Services
{
    /// <summary>
    /// Navigation engine for manage and control GUI's navigation 
    /// </summary>
    public interface INavigationService
    {
        #region Events

        /// <summary>
        /// Notifies subscribers about page change process is about to start
        /// </summary>
        event BeforePageChangeEventHandler BeforeNavigation;

        /// <summary>
        /// Notifies subscribers about page change process is finished
        /// </summary>
        event AfterPageChangeEventHandler AfterNavigation;

        #endregion

        #region Methods

        INavigationNode CreateTree(string nodeName);

        void CreateBranch(BranchModel branchModel);

        void ChangeBranchState(BranchInfo branchInfo, ElementState state);

        void NavigateBack(BranchInfo branchInfo);

        void NavigateBack(BranchInfo branchInfo, uint backStepCount);

        void NavigateBack(BranchInfo branchInfo, string nodeName);

        void NavigateTo(NavigateArgs navigateArgs);

        #endregion
    }
}
namespace Israiloff.Mmvm.Net.Core.Model.NavigationEngine.StructuralModels
{
    public abstract class BaseModel
    {
        #region Constructors

        protected BaseModel(string name)
        {
            Name = name;
        }

        #endregion

        #region Public properties

        public string Name { get; }

        public bool IsActive { get; set; }

        #endregion
    }
}
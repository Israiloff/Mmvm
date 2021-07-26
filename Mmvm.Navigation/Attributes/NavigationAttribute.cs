using System;
using Israiloff.Mmvm.Net.Container.Attributes;

namespace Mmvm.Net.Navigation.Attributes
{
    /// <summary>
    /// Definition of page, which will be acted in navigation process 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class NavigationAttribute : InjectableAttribute
    {
        public NavigationAttribute()
        {
        }
    }
}
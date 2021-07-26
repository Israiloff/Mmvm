using System;
using System.Collections.Generic;
using Israiloff.Mmvm.Net.Core.Exceptions.Enums;
using Israiloff.Mmvm.Net.Core.Exceptions.Models;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Israiloff.Mmvm.Net.Core.Exceptions
{
    public class NavigationException : Exception
    {
        #region Public properties

        public ICollection<CultureSpecifiedMessage> CultureSpecifiedMessages { get; }

        public ErrorLevel ErrorLevel { get; }

        #endregion

        #region Constructors

        public NavigationException(string message) : base(message)
        {
            CultureSpecifiedMessages = new List<CultureSpecifiedMessage>();
            ErrorLevel = ErrorLevel.Unknown;
        }

        public NavigationException(string message, Exception innerException) : base(message, innerException)
        {
            CultureSpecifiedMessages = new List<CultureSpecifiedMessage>();
            ErrorLevel = ErrorLevel.Unknown;
        }

        public NavigationException(ICollection<CultureSpecifiedMessage> cultureSpecifiedMessages, ErrorLevel errorLevel)
        {
            CultureSpecifiedMessages = cultureSpecifiedMessages;
            ErrorLevel = errorLevel;
        }

        public NavigationException(ICollection<CultureSpecifiedMessage> cultureSpecifiedMessages, ErrorLevel errorLevel,
            Exception innerException) : base(innerException.Message, innerException)
        {
            CultureSpecifiedMessages = cultureSpecifiedMessages;
            ErrorLevel = errorLevel;
        }

        public NavigationException(ICollection<CultureSpecifiedMessage> cultureSpecifiedMessages, ErrorLevel errorLevel,
            string additionalTextData) : base(additionalTextData)
        {
            CultureSpecifiedMessages = cultureSpecifiedMessages;
            ErrorLevel = errorLevel;
        }

        public NavigationException(ICollection<CultureSpecifiedMessage> cultureSpecifiedMessages, ErrorLevel errorLevel,
            string additionalTextData, Exception innerException) : base(additionalTextData, innerException)
        {
            CultureSpecifiedMessages = cultureSpecifiedMessages;
            ErrorLevel = errorLevel;
        }

        #endregion
    }
}
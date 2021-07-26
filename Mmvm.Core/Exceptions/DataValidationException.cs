﻿using System;

namespace Israiloff.Mmvm.Net.Core.Exceptions
{
    public class DataValidationException : Exception
    {
        public DataValidationException()
        {
        }

        public DataValidationException(string message) : base(message)
        {
        }

        public DataValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
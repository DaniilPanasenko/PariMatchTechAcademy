using System;
using DepsWebApp.Models;

namespace DepsWebApp.Mappings
{
    public static class ExceptionCodeMapper
    {
        public static Error ToErrorInfo(Exception exception)
        {
            var type = exception.GetType();

            if (type.Equals(typeof(NotImplementedException)))
                return new Error(10,"Action not implemented.");
            else if (type.Equals(typeof(ArgumentNullException)))
                return new Error(11, "Argument is null.");
            else if (type.Equals(typeof(ArgumentOutOfRangeException)))
                return new Error(12, "The argument is not valid.");
            else if (type.Equals(typeof(InvalidOperationException)))
                return new Error(13, "Invalid operation.");

            else
                return new Error(101, "Something went wrong.");
        }
    }
}

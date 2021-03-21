using System;
using DepsWebApp.Models;

namespace DepsWebApp.Mappings
{
    /// <summary>
    /// ExceptionCodeMapper class for mapping exception to custom error model.
    /// </summary>
    public static class ExceptionCodeMapper
    {
        /// <summary>
        /// Mapping Exception to custom error model.
        /// </summary>
        /// <param name="exception"></param>
        /// <returns><see cref="Error"/> custom error model</returns>
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

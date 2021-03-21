using System;
namespace DepsWebApp.Models
{
    /// <summary>
    /// Custom error model.
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public Error(int code, string message)
        {
            Code = code;
            Message = message;
        }

        /// <summary>
        /// Code of error.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Message of error.
        /// </summary>
        public string Message { get; set; }
    }
}

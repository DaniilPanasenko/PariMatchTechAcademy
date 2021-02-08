using System;

namespace RequestProcessor.App.Exceptions
{
    /// <summary>
    /// Reading options exception.
    /// </summary>
    [Serializable]
    internal class OptionsSourceException : Exception
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public OptionsSourceException()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception details.</param>
        public OptionsSourceException(string message) 
            : base(message)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception details.</param>
        /// <param name="innerException">Inner exception.</param>
        public OptionsSourceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

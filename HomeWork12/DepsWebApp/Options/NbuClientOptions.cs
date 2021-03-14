using System;

namespace DepsWebApp.Options
{
    /// <summary>
    /// NbuClientOptions class.
    /// </summary>
    public class NbuClientOptions
    {
        /// <summary>
        /// Base address of API
        /// </summary>
        public string BaseAddress { get; set; }

        /// <summary>
        /// Is valid base adress
        /// </summary>
        public bool IsValid => !string.IsNullOrWhiteSpace(BaseAddress) &&
                               Uri.TryCreate(BaseAddress, UriKind.Absolute, out _);
    }
}

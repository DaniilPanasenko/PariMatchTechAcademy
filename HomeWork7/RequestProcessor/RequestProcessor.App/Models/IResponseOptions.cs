using System.Text.Json.Serialization;

namespace RequestProcessor.App.Models
{
    /// <summary>
    /// Response handling options.
    /// </summary>
    internal interface IResponseOptions
    {
        /// <summary>
        /// Path to save response data.
        /// </summary>
        [JsonPropertyName("path")]
        string Path { get; }

        /// <summary>
        /// Indicates that options are valid.
        /// </summary>
        bool IsValid { get; }
    }
}

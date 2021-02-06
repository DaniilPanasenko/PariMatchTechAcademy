using System.Text.Json.Serialization;

namespace RequestProcessor.App.Models
{
    /// <summary>
    /// Request options.
    /// </summary>
    internal interface IRequestOptions
    {
        /// <summary>
        /// Optional friendly name.
        /// </summary>
        [JsonPropertyName("name")]
        string Name { get; }

        /// <summary>
        /// Request address.
        /// Should be valid Uri.
        /// </summary>
        [JsonPropertyName("address")]
        string Address { get; }

        /// <summary>
        /// Request method.
        /// </summary>
        [JsonPropertyName("method")]
        RequestMethod Method { get; }

        /// <summary>
        /// Request content type.
        /// Can be <c>null</c> when <see cref="Body"/> is null.
        /// </summary>
        [JsonPropertyName("contentType")]
        string ContentType { get; }

        /// <summary>
        /// Request content.
        /// Optional property.
        /// </summary>
        [JsonPropertyName("body")]
        string Body { get; }

        /// <summary>
        /// Indicates that options are valid.
        /// </summary>
        bool IsValid { get; }
    }
}

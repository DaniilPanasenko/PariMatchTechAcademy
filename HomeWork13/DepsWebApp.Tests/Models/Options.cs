using System.Text.Json.Serialization;

namespace DepsWebApp.Tests.Models
{
    class Options
    {
        [JsonPropertyName("baseAddress")]
        public string BaseAddress { get; set; }
    }
}

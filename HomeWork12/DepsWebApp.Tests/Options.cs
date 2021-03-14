using System.Text.Json.Serialization;

namespace DepsWebApp.Tests
{
    class Options
    {
        [JsonPropertyName("baseAddress")]
        public string BaseAddress { get; set; }
    }
}

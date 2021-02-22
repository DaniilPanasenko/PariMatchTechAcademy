using System.Text.Json.Serialization;

namespace PrimeNumbers.Tests
{
    class Options
    {
        [JsonPropertyName("baseAddress")]
        public string BaseAddress { get; set; }
    }
}
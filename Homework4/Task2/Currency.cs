using System;
using Newtonsoft.Json;

namespace Task2
{
    public class Currency
    {
        [JsonProperty("r030")]
        public int Id { get; set; }

        [JsonProperty("txt")]
        public string Name { get; set; }

        [JsonProperty("cc")]
        public string Code { get; set; }

        [JsonProperty("rate")]
        public decimal Rate { get; set; }

        [JsonProperty("exchangedate")]
        public string ExchangeDate { get; set; }
    }
}

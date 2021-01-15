using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Task1
{
    public class Result
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("error")]
        public string Error { get; set; }

        [JsonPropertyName("duration")]
        public string Duration { get; set; }

        [JsonPropertyName("primes")]
        public List<int> Primes  {get; set;}

        public Result(bool success, string error, string duration, List<int> primes)
        {
            Success = success;
            Error = error;
            Duration = duration;
            Primes = primes;
        }
    }
}

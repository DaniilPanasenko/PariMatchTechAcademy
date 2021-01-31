using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Task2.JsonObjects
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
        public HashSet<int> Primes { get; set; }

        public Result(bool success, string error, string duration, HashSet<int> primes)
        {
            Success = success;
            Error = error;
            Duration = duration;
            Primes = primes;
        }
    }
}

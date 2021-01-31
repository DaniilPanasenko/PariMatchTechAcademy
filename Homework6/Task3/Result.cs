using System;
using System.Text.Json.Serialization;

namespace Task3
{
    public class Result
    {
        [JsonPropertyName("success")]
        public int Success { get; set; }

        [JsonPropertyName("failed")]
        public int Failed { get; set; }

        public Result(int success, int failed)
        {
            Success = success;
            Failed = failed;
        }
    }
}

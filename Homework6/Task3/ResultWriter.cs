using System;
using System.IO;
using System.Text.Json;

namespace Task3
{
    public static class ResultWriter
    {
        public static void Write(Result result)
        {
            var resultJson = JsonSerializer.Serialize(result);
            File.WriteAllText("result.json", resultJson);
        }
    }
}

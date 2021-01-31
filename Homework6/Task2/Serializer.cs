using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Task2.JsonObjects;

namespace Task2
{
    public static class Serializer
    {
        public static List<Settings> ReadSettings()
        {
            var settingsJson = File.ReadAllText("settings.json");
            var settings = JsonSerializer.Deserialize<List<Settings>>(settingsJson);
            settings = settings.Where(x => x != null).ToList();
            return settings;
        }

        public static void WriteResult(Result result)
        {
            var resultJson = JsonSerializer.Serialize(result);
            File.WriteAllText("result.json", resultJson);
        }
    }
}

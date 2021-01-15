using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace Task1
{
    class Program
    {
        static List<int> GetPrimes(Settings settings)
        {
            int min = settings.PrimesFrom;
            int max = settings.PrimesTo;
            List<int> result = new List<int>();
            if (min < 2) min = 2;
            for (int i = min; i <max; i++)
            {
                bool isPrime = true;
                for (int j = 2; j <= Math.Sqrt(i); j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    result.Add(i);
                }
            }
            return result;
        }

        static void Main(string[] args)
        {
            Settings settings = null;
            Result result = null;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            try
            {
                var settingsJson = File.ReadAllText("settings.json");
                settings = JsonSerializer.Deserialize<Settings>(settingsJson);
            }
            catch (Exception ex)
            {
                stopWatch.Stop();
                TimeSpan time = stopWatch.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    time.Hours, time.Minutes, time.Seconds, time.Milliseconds / 10);
                result = new Result(false, ex.Message, elapsedTime, null);
            }
            if (result == null)
            {
                var primes = GetPrimes(settings);
                stopWatch.Stop();
                TimeSpan time = stopWatch.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    time.Hours, time.Minutes, time.Seconds, time.Milliseconds / 10);
                result = new Result(true, null, elapsedTime, primes);
            }
            var resultJson = JsonSerializer.Serialize(result);
            File.WriteAllText("result.json", resultJson);
        }
    }
}

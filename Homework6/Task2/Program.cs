using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using Task2.JsonObjects;

namespace Task2
{
    class Program
    {

        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            List<Settings> settingsList = null;
            Result result = null;
            try
            {
                settingsList = Serializer.ReadSettings();
            }
            catch (FileNotFoundException)
            {
                stopWatch.Stop();
                result = new Result(false, "settings.json file not found", stopWatch.Elapsed.ToString(), null);
            }
            catch (JsonException)
            {
                stopWatch.Stop();
                result = new Result(false, "settings.json file was corrupted", stopWatch.Elapsed.ToString(), null);
            }
            if (result == null)
            {
                PrimeNumbers primeNumbers = new PrimeNumbers();
                var primes = primeNumbers.GetPrimes(settingsList);
                stopWatch.Stop();
                result = new Result(true, null, stopWatch.Elapsed.ToString(), primes);
            }
            Serializer.WriteResult(result);
        }
    }
}

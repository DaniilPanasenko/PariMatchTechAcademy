using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace Task2
{
    class Program
    {
        

        static string InputOriginalCurrency()
        {
            while (true)
            {
                Console.WriteLine("Enter original currency...");
                string originalCurrency = Console.ReadLine().Trim().ToUpper();
                if (originalCurrency.Length != 3)
                {
                    Console.WriteLine("You entered incorrect value");
                    continue;
                }
                return originalCurrency;
            }
        }

        static string InputDesiredCurrency()
        {
            while (true)
            {
                Console.WriteLine("Enter desired currency...");
                string desiredCurrency = Console.ReadLine().Trim().ToUpper();
                if (desiredCurrency.Length != 3)
                {
                    Console.WriteLine("You entered incorrect value");
                    continue;
                }
                return desiredCurrency;
            }
        }

        static decimal InputConvertedValue()
        {
            while (true)
            {
                decimal value;
                Console.WriteLine("Enter value for converting...");
                if (!decimal.TryParse(Console.ReadLine(), out value))
                {
                    Console.WriteLine("You entered not decimal value");
                    continue;
                }
                if (value < 0)
                {
                    Console.WriteLine("You entered negative value");
                    continue;
                }
                return value;
            }
        }

        static async Task UpdateCacheAsync()
        {
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(10);
            HttpResponseMessage response = await client.GetAsync("https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            File.WriteAllText("cache.json", json);
        }

        static List<Currency> GetCurrencies()
        {
            var json = File.ReadAllText("cache.json");
            var currencies = JsonConvert.DeserializeObject<List<Currency>>(json);
            return currencies;
        }

        static string GetConvertedString(string originalCurrencyCode, string desiredCurrencyCode,decimal value, List<Currency> currencies)
        {
            Currency originalCurrency = currencies.Where(x => x.Code == originalCurrencyCode).FirstOrDefault();
            Currency desiredCurrency = currencies.Where(x => x.Code == desiredCurrencyCode).FirstOrDefault();
            if ((originalCurrency == null && originalCurrencyCode != "UAH") ||
                (desiredCurrency == null && desiredCurrencyCode != "UAH")) 
            {
                throw new Exception($"Unknown pair {originalCurrencyCode} {desiredCurrencyCode}");
            }
            decimal rate = 1;
            if(originalCurrencyCode != "UAH")
            {
                rate *= originalCurrency.Rate;
            }
            if (desiredCurrencyCode != "UAH")
            {
                rate /= desiredCurrency.Rate;
            }
            decimal result = value * rate;
            string date = currencies.First().ExchangeDate;
            return $"{Math.Round(value,2)} {originalCurrencyCode} x {Math.Round(rate,2)} = {Math.Round(result, 2)} {desiredCurrencyCode} (by {date})";
            
        }

        static async Task Main(string[] args)
        {
            Console.WriteLine("Task2. Currency Converter by Daniil Panasenko\n");
            string originalCurrency = InputOriginalCurrency();
            string desiredCurrency = InputDesiredCurrency();
            decimal value = InputConvertedValue();
            try
            {
                await UpdateCacheAsync();
            }
            catch (Exception)
            {
                Console.WriteLine("Cache hasn't been updated");
            }
            List<Currency> currencies = null;
            try
            {
                currencies = GetCurrencies();
            }
            catch (Exception)
            {
                Console.WriteLine("Can't read the cache");
                return;
            }
            try
            {
                Console.WriteLine(GetConvertedString(originalCurrency, desiredCurrency, value, currencies));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
    }
}

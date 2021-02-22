using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrimeNumbers.Tests
{
    public static class PrimeNumbersAPITest
    {
        public static async Task<bool> AppInfoTest(HttpClient client)
        {
            Console.WriteLine("Test: AppInfo");
            var response = await client.GetAsync(client.BaseAddress);
            Console.WriteLine($"Status code (expected): {HttpStatusCode.OK}");
            Console.WriteLine($"Status code (actual): {response.StatusCode}");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }
            var content = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(content))
            {
                return false;
            }
            Console.WriteLine($"Content: {content}");
            return true;
        }

        public static async Task<bool> IsPrimeTest(HttpClient client, int number)
        {
            Console.WriteLine("Test: IsPrime");
            var response = await client.GetAsync(client.BaseAddress + "primes/" + number);
            bool isPrime = await PrimeNumbers.IsPrimeAsync(number);
            var expectedSatusCode = isPrime
                ? HttpStatusCode.OK
                : HttpStatusCode.NotFound;
            Console.WriteLine($"Status code (expected): {expectedSatusCode}");
            Console.WriteLine($"Status code (actual): {response.StatusCode}");
            return expectedSatusCode == response.StatusCode;
        }

        public static async Task<bool> GetPrimesTest(HttpClient client, string from, string to)
        {
            Console.WriteLine("Test: GetPrimes");
            var address = client.BaseAddress + "primes?";
            if (!string.IsNullOrEmpty(to))
            {
                address += "to=" + to;
            }
            if (!string.IsNullOrEmpty(to) && !string.IsNullOrEmpty(from))
            {
                address += "&";
            }
            if (!string.IsNullOrEmpty(from))
            {
                address += "from=" + from;
            }
            var response = await client.GetAsync(address);
            int toValue = 0;
            int fromValue = 0;
            var expectedSatusCode =
                int.TryParse(to, out toValue) && int.TryParse(from, out fromValue)
                ? HttpStatusCode.OK
                : HttpStatusCode.BadRequest;
            Console.WriteLine($"Status code (expected): {expectedSatusCode}");
            Console.WriteLine($"Status code (actual): {response.StatusCode}");
            if (response.StatusCode != expectedSatusCode)
            {
                return false;
            }
            if (expectedSatusCode != HttpStatusCode.BadRequest)
            {
                var expectedPrimes = await PrimeNumbers.GetPrimesAsync(fromValue, toValue);
                var expectedContent = string.Join(',', expectedPrimes);
                var actualContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Content (expected): {expectedContent}");
                Console.WriteLine($"Content (actual): {actualContent}");
                if (expectedContent != actualContent)
                {
                    return false;
                }
            }
            return true;
        }
    }
}

using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DepsWebApp.Tests
{
    public static class DepsWebAppTests
    {
        public static async Task<bool> Test1(HttpClient client)
        {
            Console.WriteLine("Test: Exchange with correct currencies");
            const string srcCurrency = "USD";
            const string dstCurrency = "UAH";
            var response = await client.GetAsync(client.BaseAddress + $"rates/{srcCurrency}/{dstCurrency}?amount=1000");
            Console.WriteLine($"Status code (expected): {HttpStatusCode.OK}");
            Console.WriteLine($"Status code (actual): {response.StatusCode}");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }
            return true;
        }

        public static async Task<bool> Test2(HttpClient client)
        {
            Console.WriteLine("Test: Exchange with invalid currency");
            const string srcCurrency = "xxx";
            const string dstCurrency = "yyy";
            var response = await client.GetAsync(client.BaseAddress + $"rates/{srcCurrency}/{dstCurrency}?amount=1000");
            Console.WriteLine($"Status code (expected): {HttpStatusCode.BadRequest}");
            Console.WriteLine($"Status code (actual): {response.StatusCode}");
            if (response.StatusCode != HttpStatusCode.BadRequest)
            {
                return false;
            }
            return true;
        }

        public static async Task<bool> Test3(HttpClient client)
        {
            Console.WriteLine("Test: Registration");

            var requestContent = new StringContent(
                JsonSerializer.Serialize(
                    new User("login", "password")),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(client.BaseAddress + "auth/register", requestContent);
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
    }
}

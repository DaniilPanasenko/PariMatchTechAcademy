using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DepsWebApp.Tests
{
    public static class RatesTests
    {
        private static void Authentication(HttpClient client, User user)
        {
            var byteArray = Encoding.ASCII.GetBytes($"{user.Login}:{user.Password}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }

        private static async Task<bool> RegistrationAsync(HttpClient client,User user)
        {
            var requestContent = new StringContent(
                JsonSerializer.Serialize(
                    user),
                Encoding.UTF8,
                "application/json");
            var response = await client.PostAsync(client.BaseAddress + "auth/register", requestContent);
            return response.StatusCode == HttpStatusCode.OK;
        }

        public static async Task<bool> TestSuccessfullyExchange(HttpClient client, UserGenerator generator)
        {
            Console.WriteLine("Test: Exchange successfully");

            client.DefaultRequestHeaders.Clear();
            if(!await RegistrationAsync(client, generator.GetNewUser()))
            {
                return false;
            }
            Authentication(client, generator.GetExistingUser());

            var response = await client.GetAsync(client.BaseAddress + $"rates/USD/UAH?amount=1000");

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

        public static async Task<bool> TestUnauthorizedExchange(HttpClient client, UserGenerator generator)
        {
            Console.WriteLine("Test: Exchange without authentication");

            client.DefaultRequestHeaders.Clear();

            var response = await client.GetAsync(client.BaseAddress + $"rates/USD/UAH?amount=1000");

            Console.WriteLine($"Status code (expected): {HttpStatusCode.Unauthorized}");
            Console.WriteLine($"Status code (actual): {response.StatusCode}");
            if (response.StatusCode != HttpStatusCode.Unauthorized)
            {
                return false;
            }
            return true;
        }

        public static async Task<bool> TestInvalidLoginExchange(HttpClient client, UserGenerator generator)
        {
            Console.WriteLine("Test: Exchange without invalid login");

            client.DefaultRequestHeaders.Clear();
            Authentication(client, generator.GetInvalidLoginUser());

            var response = await client.GetAsync(client.BaseAddress + $"rates/USD/UAH?amount=1000");

            Console.WriteLine($"Status code (expected): {HttpStatusCode.Unauthorized}");
            Console.WriteLine($"Status code (actual): {response.StatusCode}");
            if (response.StatusCode != HttpStatusCode.Unauthorized)
            {
                return false;
            }
            return true;
        }

        public static async Task<bool> TestInvalidPasswordExchange(HttpClient client, UserGenerator generator)
        {
            Console.WriteLine("Test: Exchange without invalid password");

            client.DefaultRequestHeaders.Clear();
            if (!await RegistrationAsync(client, generator.GetNewUser()))
            {
                return false;
            }
            Authentication(client, generator.GetExistingUserWithInvalidPassword());

            var response = await client.GetAsync(client.BaseAddress + $"rates/USD/UAH?amount=1000");

            Console.WriteLine($"Status code (expected): {HttpStatusCode.Unauthorized}");
            Console.WriteLine($"Status code (actual): {response.StatusCode}");
            if (response.StatusCode != HttpStatusCode.Unauthorized)
            {
                return false;
            }
            return true;
        }

    }
}

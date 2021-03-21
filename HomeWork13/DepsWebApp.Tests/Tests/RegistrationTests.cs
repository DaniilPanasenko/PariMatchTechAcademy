using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DepsWebApp.Tests.Helpers;
using DepsWebApp.Tests.Models;

namespace DepsWebApp.Tests.Tests
{
    public static class RegistrationTests
    {
        private static async Task<HttpResponseMessage> RegistrationAsync(HttpClient client, User user)
        {
            var requestContent = new StringContent(
                JsonSerializer.Serialize(
                    user),
                Encoding.UTF8,
                "application/json");
            var response = await client.PostAsync(client.BaseAddress + "auth/register", requestContent);
            return response;
        }

        public static async Task<bool> TestSuccessRegistration(HttpClient client, UserGenerator generator)
        {
            Console.WriteLine("Test: Success Registration");

            var response = await RegistrationAsync(client, generator.GetNewUser());

            Console.WriteLine($"Status code (expected): {HttpStatusCode.OK}");
            Console.WriteLine($"Status code (actual): {response.StatusCode}");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }
            return true;
        }

        public static async Task<bool> TestInvalidRegistration(HttpClient client, UserGenerator generator)
        {
            Console.WriteLine("Test: Registration with invalid parametres");

            var response = await RegistrationAsync(client, generator.GetNotValidUser());

            Console.WriteLine($"Status code (expected): {HttpStatusCode.BadRequest}");
            Console.WriteLine($"Status code (actual): {response.StatusCode}");
            if (response.StatusCode != HttpStatusCode.BadRequest)
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

        public static async Task<bool> TestRegistrationWithExistingLogin(HttpClient client, UserGenerator generator)
        {
            Console.WriteLine("Test: Registration with existing login");
            if ((await RegistrationAsync(client, generator.GetNewUser())).StatusCode!=HttpStatusCode.OK)
            {
                return false;
            }
            var response = await RegistrationAsync(client, generator.GetExistingUserWithInvalidPassword());

            Console.WriteLine($"Status code (expected): {HttpStatusCode.Conflict}");
            Console.WriteLine($"Status code (actual): {response.StatusCode}");
            if (response.StatusCode != HttpStatusCode.Conflict)
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

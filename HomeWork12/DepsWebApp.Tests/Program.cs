using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using DepsWebApp.Tests;

namespace DepsWebApp.Tests
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new HttpClient();

            var json = File.ReadAllText("options.json");
            var options = JsonSerializer.Deserialize<Options>(json);

            client.BaseAddress = new Uri(options.BaseAddress);

            UserGenerator generator = new UserGenerator();

            bool testResult;

            PrintHeader(1);
            testResult = await RegistrationTests.TestSuccessRegistration(client, generator);
            PrintResult(testResult);

            PrintHeader(2);
            testResult = await RegistrationTests.TestInvalidRegistration(client, generator);
            PrintResult(testResult);

            PrintHeader(3);
            testResult = await RegistrationTests.TestRegistrationWithExistingLogin(client, generator);
            PrintResult(testResult);

            PrintHeader(4);
            testResult = await RatesTests.TestSuccessfullyExchange(client, generator);
            PrintResult(testResult);

            PrintHeader(5);
            testResult = await RatesTests.TestUnauthorizedExchange(client, generator);
            PrintResult(testResult);

            PrintHeader(6);
            testResult = await RatesTests.TestInvalidLoginExchange(client, generator);
            PrintResult(testResult);

            PrintHeader(7);
            testResult = await RatesTests.TestInvalidPasswordExchange(client, generator);
            PrintResult(testResult);

        }

        static void PrintHeader(int number)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Test {number}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void PrintResult(bool result)
        {
            if (result)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Test result: OK\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Test result: Failed\n");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}

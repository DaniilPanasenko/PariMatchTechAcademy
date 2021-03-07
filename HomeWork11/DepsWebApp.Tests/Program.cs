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

            bool testResult;

            PrintHeader(1);
            testResult = await DepsWebAppTests.Test1(client);
            PrintResult(testResult);

            PrintHeader(2);
            testResult = await DepsWebAppTests.Test2(client);
            PrintResult(testResult);

            PrintHeader(3);
            testResult = await DepsWebAppTests.Test3(client);
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

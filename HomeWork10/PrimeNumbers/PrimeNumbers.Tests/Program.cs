using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PrimeNumbers.Tests
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
            testResult = await PrimeNumbersAPITest.AppInfoTest(client);
            PrintResult(testResult);

            PrintHeader(2);
            testResult = await PrimeNumbersAPITest.IsPrimeTest(client, 7, true);
            PrintResult(testResult);

            PrintHeader(3);
            testResult = await PrimeNumbersAPITest.IsPrimeTest(client, 10, false);
            PrintResult(testResult);

            PrintHeader(4);
            testResult = await PrimeNumbersAPITest.GetPrimesTest(client, "0", "20", new int[] { 2, 3, 5, 7, 11, 13, 17, 19 });
            PrintResult(testResult);

            PrintHeader(5);
            testResult = await PrimeNumbersAPITest.GetPrimesTest(client, "-5", "1", new int[] { });
            PrintResult(testResult);

            PrintHeader(6);
            testResult = await PrimeNumbersAPITest.GetPrimesTest(client, null, "20");
            PrintResult(testResult);

            PrintHeader(7);
            testResult = await PrimeNumbersAPITest.GetPrimesTest(client, "10", "");
            PrintResult(testResult);

            PrintHeader(8);
            testResult = await PrimeNumbersAPITest.GetPrimesTest(client, null, null);
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

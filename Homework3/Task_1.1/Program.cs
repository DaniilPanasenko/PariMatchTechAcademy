using System;
using System.Linq;

namespace Task_1._1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Task 1.1 Array statisic LINQ by Daniil Panasenko\n");
            Console.WriteLine("The program will display statics for the array entered by the user\n");
            int[] array = null;
            do
            {
                Console.WriteLine("Enter values of array, separated by ','...");
                string input = Console.ReadLine();
                string[] stringArray = input.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                try
                {
                    array = stringArray.Select(x => x.Trim()).Where(x => x != "").Select(x => int.Parse(x)).ToArray();
                    if (array.Length == 0)
                    {
                        Console.WriteLine("You entered empty array\n");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("You entered array in incorrect format\n");
                }
            }
            while (array == null || array.Length==0);
            int min = array.Min();
            int max = array.Max();
            int sum = array.Sum();
            double average = array.Average();
            double standardDeviation = Math.Sqrt(array.Select(x => (x - average) * (x - average)).Sum() / array.Count());
            var sorted = array.OrderBy(x => x).Distinct().ToList();
            Console.WriteLine($"\nMin: {min}");
            Console.WriteLine($"Max: {max}");
            Console.WriteLine($"Sum: {sum}");
            Console.WriteLine($"Average: {average}");
            Console.WriteLine($"Standard deviation: {standardDeviation}");
            Console.Write($"Sorted array of unique elements: ");
            sorted.ForEach(x => Console.Write(x + " "));

        }
    }
}

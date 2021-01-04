using System;

namespace Task1_2
{
    class Program
    {
        static void Margin(double p1, double x, double p2, out double probP1, out double probX, out double probP2, out double margin)
        {
            double sum = 1 / p1 + 1 / x + 1 / p2;
            margin = Math.Round((sum - 1) / sum * 100, 1);
            probP1 = Math.Round(1 / p1 / sum * 100, 1);
            probX = Math.Round(1 / x / sum * 100, 1);
            probP2 = Math.Round(1 / p2 / sum * 100, 1);
        }
        static void Main(string[] args)
        {
            double p1, x, p2;
            double probP1, probX, probP2, margin;
            string name1, name2;
            Console.WriteLine("Task 1.2 Calculation of margin by Daniil Panasenko\n");
            Console.WriteLine("Enter names of the participants...");
            Console.Write("First participant: ");
            name1 = Console.ReadLine();
            Console.Write("Second participant: ");
            name2 = Console.ReadLine();
            Console.WriteLine("\nEnter coefficients...");
            Console.Write($"{name1} win: ");
            p1 = double.Parse(Console.ReadLine());
            Console.Write($"Draw: ");
            x = double.Parse(Console.ReadLine());
            Console.Write($"{name2} win: ");
            p2 = double.Parse(Console.ReadLine());
            Margin(p1, x, p2, out probP1, out probX, out probP2, out margin);
            Console.WriteLine($"\n{name1} win: {probP1}%");
            Console.WriteLine($"Draw: {probX}%");
            Console.WriteLine($"{name2} win: {probP2}%");
            Console.WriteLine($"Bookmaker's margin: {margin}%");
            Console.ReadKey();
        }
    }
}

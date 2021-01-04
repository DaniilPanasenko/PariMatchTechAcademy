using System;

namespace Task1_1
{
    class Program
    {
        static double Function (double a, double b, double c, double d)
        {
            return (Math.Exp(a) + 4 * Math.Log10(c)) / Math.Sqrt(b) * Math.Abs(Math.Atan(d)) + (5 / Math.Sin(a));
        }

        static void Main(string[] args)
        {
            const double b = 2001;
            const double c = 6;
            const double d = 1;
            Console.WriteLine("Task 1.1 Expression calculation by Daniil Panasenko\n");
            Console.WriteLine("y = (e^a + 4lg(c)) / sqrt(b) * |arctg(d)| + 5 / sin(a)");
            Console.WriteLine($"b = {b}, c = {c}, d = {d}");
            Console.WriteLine("Enter parameter a...");
            double a = double.Parse(Console.ReadLine());
            double y = Function(a, b, c, d);
            Console.WriteLine($"\ny = {y}");
            Console.ReadKey();
        }
    }
}

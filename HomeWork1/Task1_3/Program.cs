using System;

namespace Task1_3
{
    class Program
    {
        static double RowSum(double epsilon)
        {
            double result = 0;
            for(double i=1; ; i++)
            {
                double value = 1 / (i * (i + 1));
                result += value;
                if (value < epsilon) break;
            }
            return result;
        }

        static void Main(string[] args)
        {
            const double year = 2001;
            const double epsilon = 1 / year;
            Console.WriteLine("Task 1.3 Row sum calculation by Daniil Panasenko\n");
            Console.WriteLine("sum(i=1,inf) = 1/(i * (i + 1)), 1/(i * (i + 1)) >= epsilon");
            Console.WriteLine($"year of birth = {year}, epsilon = {epsilon}");
            double sum = RowSum(epsilon);
            Console.WriteLine($"\nsum = {sum}");
            Console.ReadKey();
        }
    }
}
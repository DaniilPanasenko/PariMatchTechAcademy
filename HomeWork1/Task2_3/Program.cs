using System;

namespace Task2_3
{
    class Program
    {
        static void GetArrayStatistics(int[] array)
        {
            int min = array[0];
            int max = array[0];
            int sum = 0;
            double avg = 0;
            double standardDeviation = 0;
            for(var i=0; i<array.Length; i++)
            {
                if (array[i] < min) min = array[i];
                if (array[i] > max) max = array[i];
                sum += array[i];
            }
            avg = (double)sum / array.Length;
            for (var i = 0; i < array.Length; i++)
            {
                standardDeviation += Math.Pow(array[i] - avg, 2);
            }
            standardDeviation /= array.Length;
            standardDeviation = Math.Sqrt(standardDeviation);
            Console.WriteLine($"Min: {min}");
            Console.WriteLine($"Max: {max}");
            Console.WriteLine($"Sum: {sum}");
            Console.WriteLine($"Average: {avg}");
            Console.WriteLine($"Standard Deviation: {standardDeviation}");
            int temp;
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    if (array[j + 1] < array[j])
                    {
                        temp = array[j + 1];
                        array[j + 1] = array[j];
                        array[j] = temp;
                    }
                }
            }
            Console.WriteLine("Sorted array:");
            foreach (var element in array)
            {
                Console.Write(element + " ");
            }
        }

        static int Main(string[] args)
        {
            if (args != null && args.Length != 0)
            {
                string command = "";
                foreach (var argument in args)
                {
                    command += argument+" ";
                }
                string[] stringArray = command.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int[] array = new int[stringArray.Length];
                for(var i=0; i<array.Length; i++)
                {
                    if (!Int32.TryParse(stringArray[i], out array[i])) return -1;
                }
                GetArrayStatistics(array);
                return 200;
            }
            else
            {
                Console.WriteLine("Task 2.3 Array statistics by Daniil Panasenko\n");
                int length;
                Console.WriteLine("Enter array length...");
                while (!Int32.TryParse(Console.ReadLine(), out length) || length <= 0)
                {
                    Console.WriteLine("You entered an incorrect length\n");
                    Console.WriteLine("Enter array length...");
                }
                bool validArray = false;
                int[] array = new int[length];
                while (!validArray)
                {
                    Console.WriteLine($"Enter {length} array elements separated by spaces...");
                    string[] stringArray = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (stringArray.Length == length)
                    {
                        validArray = true;
                        for (var i = 0; i < length; i++)
                        {
                            if (!Int32.TryParse(stringArray[i], out array[i])) validArray = false;
                        }
                    }
                    if (!validArray)
                    {
                        Console.WriteLine($"You entered an incorrect array\n");
                    }
                }
                GetArrayStatistics(array);

            }
            return 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Task_1._3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Task 1.3 Dictionary with key by Daniil Panasenko\n");
            Console.WriteLine("You need enter N - number of dictionary values and N key-value pairs {brand, country}: {web-site} \n");
            int n;
            Console.WriteLine("Enter count of elements...");
            while (!int.TryParse(Console.ReadLine(), out n) || n <= 0)
            {
                Console.WriteLine("Invalid count of elements\n");
                Console.WriteLine("Enter count of elements...");
            }
            Dictionary<Region, RegionSettings> regions = new Dictionary<Region, RegionSettings>();
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\nEnter {i+1} element.");
                Console.WriteLine("Enter brand...");
                string brand = Console.ReadLine();
                Console.WriteLine("Enter country...");
                string country = Console.ReadLine();
                Console.WriteLine("Enter web-site...");
                string webSite = Console.ReadLine();
                if(!regions.TryAdd(new Region(brand, country), new RegionSettings(webSite)))
                {
                    Console.WriteLine("Dictionary already contains this key, plese repeat input");
                    i--;
                }
            }
            Console.WriteLine("\nRegions dictionary:");
            foreach(var region in regions)
            {
                Console.WriteLine(region.Key + ": " + region.Value);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Task_2._1.Entities;

namespace Task_2._1
{
    class Program
    {
        static List<Product> Products;
        static List<Tag> Tags;
        static List<Leftover> Inventory;

        static void ReadFiles()
        {
            Products = File.ReadLines("Products.csv")
                .Skip(1)
                .Select(x => new Product(x.Split(';')[0], x.Split(';')[1], x.Split(';')[2], decimal.Parse(x.Split(';')[3])))
                .ToList();
            Tags = File.ReadLines("Tags.csv")
                .Skip(1)
                .Select(x => new Tag(x.Split(';')[0],x.Split(';')[1]))
                .ToList();
            Inventory = File.ReadLines("Inventory.csv")
                .Skip(1)
                .Select(x => new Leftover(x.Split(';')[0], x.Split(';')[1], int.Parse(x.Split(';')[2])))
                .ToList();
            Products.ForEach(x => x.Tags = Tags.Where(y => y.ProductId == x.Id).ToList())
;        }

        static int InputCommand(string menuName, string[] commands)
        {
            Console.WriteLine($"\n{menuName} Menu:");
            for (int i = 0; i < commands.Length; i++)
            {
                Console.WriteLine($"{i+1} - {commands[i]}");
            }
            Console.WriteLine("\nEnter the command number...");
            int command;
            while (!int.TryParse(Console.ReadLine(), out command) || command < 1 || command > commands.Length)
            {
                Console.WriteLine("You entered incorrect command");
                Console.WriteLine("\nEnter the command number...");
            }
            return command;
        }

        static void MainMenu()
        {
            while (true)
            {
                int command = InputCommand("Main", new string[] { "Exit", "Products", "Lefovers" });
                switch (command)
                {
                    case 1:
                        Environment.Exit(0);
                        break;
                    case 2:
                        ProductsMenu();
                        break;
                    case 3:
                        LeftoversMenu();
                        break;
                }
            }
        }

        static void ProductsMenu()
        {
            while (true)
            {
                int command = InputCommand("Products", new string[] {
                    "Back to main menu",
                    "Search product",
                    "List all products ordered by ascending price",
                    "List all products ordered by descending price" });
                switch (command)
                {
                    case 1:
                        return;
                    case 2:
                        SearchProduct();
                        break;
                    case 3:
                        ProductsOrderedByPriceAscending();
                        break;
                    case 4:
                        ProductsOrderedByPriceDescending();
                        break;
                }
            }
        }

        static void LeftoversMenu()
        {
            while (true)
            {
                int command = InputCommand("Leftovers", new string[] {
                    "Back to main menu",
                    "Missing products",
                    "Leftovers ordered by ascending",
                    "Leftovers ordered by descending",
                    "Leftovers by ID" });
                switch (command)
                {
                    case 1:
                        return;
                    case 2:
                        MissingProducts();
                        break;
                    case 3:
                        LeftoversAscending();
                        break;
                    case 4:
                        LeftoversDescending();
                        break;
                    case 5:
                        LeftoversById();
                        break;
                }
            }
        }

        static void SearchProduct()
        {
            string match = "";
            while (match == "")
            {
                Console.WriteLine("Enter value for search...");
                match = Console.ReadLine();
                if (match == "") Console.WriteLine("Empty value");
            }
            var searchById = Products.Where(x => x.Id.ToLower().Contains(match.ToLower()));
            var searchByBrand = Products.Where(x => x.Brand.ToLower().Contains(match.ToLower()));
            var searchByModel = Products.Where(x => x.Model.ToLower().Contains(match.ToLower()));
            var searchByTag = Products.Where(x => x.Tags.Where(y => y.TagValue.ToLower().Contains(match.ToLower())).Count() != 0);
            var searchResult = searchById.Concat(searchByBrand).Concat(searchByModel).Concat(searchByTag).Distinct();
            if (searchResult.Count() == 0)
            {
                Console.WriteLine("Products not found");
                return;
            }
            Console.WriteLine("Search result:");
            searchResult.ToList().ForEach(Console.WriteLine);
        }

        static void ProductsOrderedByPriceAscending()
        {
            Console.WriteLine("All products ordered by price ascending:");
            Products.OrderBy(x => x.Price).ToList().ForEach(Console.WriteLine);
        }

        static void ProductsOrderedByPriceDescending()
        {
            Console.WriteLine("All products ordered by price descending:");
            Products.OrderByDescending(x => x.Price).ToList().ForEach(Console.WriteLine);
        }

        static void MissingProducts()
        {
            Console.WriteLine("Missing products:");
            Products.Where(x => !Inventory.Any(y => y.ProductId == x.Id && y.LeftoverCount != 0)).ToList().ForEach(Console.WriteLine);
        }

        static void LeftoversAscending()
        {
            Console.WriteLine("Leftovers by ascending:");
            Products
                .Select(x => new {
                    product = x,
                    count = Inventory
                        .Where(y => y.ProductId == x.Id)
                        .Sum(x => x.LeftoverCount) })
                .OrderBy(x => x.count)
                .ToList()
                .ForEach(x => Console.WriteLine(x.product + " count:  " + x.count));
        }

        static void LeftoversDescending()
        {
            Console.WriteLine("Leftovers by descending:");
            Products
                .Select(x => new {
                    product = x,
                    count = Inventory
                        .Where(y => y.ProductId == x.Id)
                        .Sum(x => x.LeftoverCount)
                })
                .OrderByDescending(x => x.count)
                .ToList()
                .ForEach(x => Console.WriteLine(x.product + " count:  " + x.count));
        }

        static void LeftoversById()
        {
            string id = "";
            while (id == "")
            {
                Console.WriteLine("Enter id for search...");
                id = Console.ReadLine();
                if (id == "") Console.WriteLine("Empty value");
            }
            var product = Products.Where(x => x.Id.ToLower() == id.ToLower()).FirstOrDefault();
            if (product == null)
            {
                Console.WriteLine("Product not found");
                return;
            }
            Inventory
                .Where(x => x.ProductId == product.Id)
                .OrderByDescending(x => x.LeftoverCount)
                .ToList()
                .ForEach(x => Console.WriteLine(x.Location + ": " + x.LeftoverCount));
        }

        static int Main(string[] args)
        {
            Console.WriteLine("Task 2.1 ERP Reports Bot by Daniil Panasenko\n");
            Console.WriteLine("To use ERP Reports bot you must choose one of the available commands (only number) and follow instructions");
            try
            {
                ReadFiles();
            }
            catch
            {
                Console.WriteLine("File read error");
                return -1;
            }
            MainMenu();
            return 0;
        }
    }
}

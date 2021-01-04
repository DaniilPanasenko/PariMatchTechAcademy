using System;
using System.Collections.Generic;
using System.Linq;

namespace Task_1._2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Task 1.2 Three sorts and one comparator by Daniil Panasenko");

            List<Player> players = new List<Player>() {
                new Player(29, "Ivan", "Ivanenko", PlayerRank.Captain),
                new Player(19, "Peter", "Petrenko", PlayerRank.Private),
                new Player(59, "Ivan", "Ivanov", PlayerRank.General),
                new Player(52, "Ivan", "Snezko", PlayerRank.Lieutenant),
                new Player(34, "Alex", "Zeshko", PlayerRank.Colonel),
                new Player(29, "Ivan", "Ivanenko", PlayerRank.Captain),
                new Player(19, "Peter", "Petrenko", PlayerRank.Private),
                new Player(34, "Vasiliy", "Sokol", PlayerRank.Major),
                new Player(31, "Alex", "Alexeenko", PlayerRank.Major)
            };

            Console.WriteLine("\nSorted by names:");
            players.Sort(new NameComparer());
            players.Distinct(new PlayerEqualityComparer()).ToList().ForEach(x => Console.WriteLine(x.ToString()));

            Console.WriteLine("\nSorted by age:");
            players.Sort(new AgeComparer());
            players.Distinct(new PlayerEqualityComparer()).ToList().ForEach(x => Console.WriteLine(x.ToString()));

            Console.WriteLine("\nSorted by rank:");
            players.Sort(new RankComparer());
            players.Distinct(new PlayerEqualityComparer()).ToList().ForEach(x => Console.WriteLine(x.ToString()));
        }
    }
}

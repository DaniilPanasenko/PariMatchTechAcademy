using System;
using System.Collections.Generic;

namespace Task2_1
{
    class Round
    {
        public string User { get; private set; }

        public string Computer { get; private set; }

        public string Result => Comparator();

        public Round(string user, string computer)
        {
            User = user;
            Computer = computer;
        }

        public string Comparator()
        {
            if (User == Computer) return "Draw";
            switch (User)
            {
                case "rock":
                    if (Computer == "scissors") return "Win";
                    return "Lose";
                case "paper":
                    if (Computer == "rock") return "Win";
                    return "Lose";
                case "scissors":
                    if (Computer == "paper") return "Win";
                    return "Lose";
            }
            return "";
        }

        public void Print()
        {
            Console.WriteLine($"You: {User}, Computer: {Computer}, Result: {Result}");
        }
    }

    class Program
    { 
        static void PrintCommandsList()
        {
            Console.WriteLine("Commands list:");
            Console.WriteLine("rock");
            Console.WriteLine("paper");
            Console.WriteLine("scissors");
            Console.WriteLine("exit\n");
        }

        static void Game()
        {
            Random rand = new Random();
            string command = "";
            List<Round> rounds = new List<Round>();
            while (command != "exit")
            {
                Console.Write("Enter command: ");
                command = Console.ReadLine();
                if (command == "paper" || command == "rock" || command == "scissors") 
                {
                    int random = rand.Next(0, 3);
                    string computer = "";
                    switch (random)
                    {
                        case 0:
                            computer = "rock";
                            break;
                        case 1:
                            computer = "paper";
                            break;
                        case 2:
                            computer = "scissors";
                            break;
                    }
                    Round round = new Round(command, computer);
                    round.Print();
                    rounds.Add(round);
                }
                else if (command != "exit")
                { 
                    Console.WriteLine("You entered the wrong command");
                    PrintCommandsList();
                }
            }
            PrintHistory(rounds);
        }

        static void PrintHistory(List<Round> rounds)
        {
            int games = rounds.Count;
            int wins = 0, loses = 0, draws = 0;
            Console.WriteLine("\nThank you for the game!");
            Console.WriteLine("History:");
            foreach (var round in rounds)
            {
                round.Print();
                switch (round.Result)
                {
                    case "Draw":
                        draws++;
                        break;
                    case "Win":
                        wins++;
                        break;
                    case "Lose":
                        loses++;
                        break;
                }
            }
            Console.WriteLine("Statistics:");
            Console.WriteLine($"Count of games: {games}");
            Console.WriteLine($"Count of wins: {wins} ({Math.Round((double)wins / games * 100)}%)");
            Console.WriteLine($"Count of draws: {draws} ({Math.Round((double)draws / games * 100)}%)");
            Console.WriteLine($"Count of loses: {loses} ({Math.Round((double)loses / games * 100)}%)");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Task 2.1 Rock - Paper - Scissors Game by Daniil Panasenko\n");
            Console.WriteLine("Rules:\n" +
                "1.The user enters one of the commands (rock, scissors, paper).\n" +
                "2.The computer generates its own command.\n" +
                "3.The winner of the game is determined:\n" +
                "  - rock > scissors\n" +
                "  - scissors > paper\n" +
                "  - paper > rock\n" +
                "4.To exit the game, enter the command exit.\n");
            PrintCommandsList();
            Game();
            Console.ReadKey();
        }
    }
}

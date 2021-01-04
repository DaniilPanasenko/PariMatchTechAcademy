using System;
using System.Collections.Generic;
using System.Linq;

namespace Task_1._4
{
    class Program
    {
        static char[] brackets = { '{', '(', '<', '[', '}', ')', '>', ']' };

        struct BracketPosition
        {
            public int BracketType { get; set; }

            public bool IsOpen { get; set; }

            public int Position { get; set; }

            public BracketPosition(char bracket, int position)
            {
                Position = position;
                IsOpen = Array.IndexOf(brackets, bracket) < 4;
                BracketType = Array.IndexOf(brackets, bracket) % 4;
            }
        }

        static int BracketChecking(string expression)
        {
            Stack<BracketPosition> bracketChecking = new Stack<BracketPosition>();
            for (int i = 0; i < expression.Length; i++)
            {
                if (brackets.Contains(expression[i]))
                {
                    var bracket = new BracketPosition(expression[i], i);
                    if (bracket.IsOpen)
                    {
                        bracketChecking.Push(bracket);
                    }
                    else
                    {
                        if (bracketChecking.Count == 0) return bracket.Position;
                        if (bracketChecking.Peek().BracketType != bracket.BracketType) return bracketChecking.Peek().Position;
                        bracketChecking.Pop();
                    }
                }
            }
            if (bracketChecking.Count != 0) return bracketChecking.Peek().Position;
            return -1;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Task 1.4 Pairing parentheses by Daniil Panasenko\n");
            Console.WriteLine("Input expression and program  will check your expression\n");
            string expression = "";
            while (expression == "")
            {
                Console.WriteLine("Enter expression...");
                expression = Console.ReadLine().Trim();
                if (expression == "") Console.WriteLine("Empty expression");
            }
            int result = BracketChecking(expression);
            if (result == -1)
            {
                Console.WriteLine("Excpression is correct");
            }
            else
            {
                Console.WriteLine($"Excpression has mistake in {result} position");
            }
        }
    }
}

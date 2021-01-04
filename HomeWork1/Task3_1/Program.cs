using System;
using System.Linq;

namespace Task3_1
{
    class Program
    {
        static void PrintCommandsList()
        {
            Console.WriteLine("\nList of commands:");
            Console.WriteLine("1. a + b");
            Console.WriteLine("2. a - b");
            Console.WriteLine("3. a * b");
            Console.WriteLine("4. a / b or a \\ b");
            Console.WriteLine("5. a % b");
            Console.WriteLine("6. a pow b");
            Console.WriteLine("7. a & b");
            Console.WriteLine("8. a | b");
            Console.WriteLine("9. a ^ b");
            Console.WriteLine("10. !a");
            Console.WriteLine("11. a!");
            Console.WriteLine("12. a");
            Console.WriteLine("13. -a");
            Console.WriteLine("* all operations must contain only one or two arguments");
            Console.WriteLine("* all arguments must be double or integer values");
            Console.WriteLine("* commands 7-11 must contain only positive integer values");
            Console.WriteLine("* commands can have or don't have spaces");
            Console.WriteLine("-----------");
            Console.WriteLine("help");
            Console.WriteLine("exit\n");
        }

        static bool Calculate(string command, out double result)
        {
            checked
            {
                result = 0;
                if (command.IndexOfAny(new char[] { '@', '#' }) != -1) return false;
                char[] operations = { '+', '@', '*', '/', '%', '#' };
                command = command.Replace(" ", ""); 
                command = command.Replace("pow", "#");
                command = command.Replace("\\", "/");
                command = command.Replace("x", "*");
                for (var i = 0; i < command.Length; i++)
                {
                    if (command[i] == '-')
                    {
                        if (i != 0 && !operations.Contains(command[i - 1]))
                        {
                            string firstPart = command.Substring(0, i);
                            string secondPart = command.Substring(i + 1);
                            command = firstPart + '@' + secondPart;
                        }
                    }
                }
                int operationPosition = command.IndexOfAny(operations);
                if (operationPosition != -1)
                {
                    string firstValue = command.Substring(0, operationPosition);
                    string secondValue = command.Substring(operationPosition + 1);
                    double a, b;
                    if (!Double.TryParse(firstValue, out a)) return false;
                    if (!Double.TryParse(secondValue, out b)) return false;
                    switch (command[operationPosition])
                    {
                        case '+':
                            result = a + b;
                            return true;
                        case '@':
                            result = a - b;
                            return true;
                        case '*':
                            result = a * b;
                            return true;
                        case '/':
                            const double epsilon = 0.000000000000000001;
                            if (Math.Abs(b) < epsilon) throw new DivideByZeroException();
                            result = a / b;
                            return true;
                        case '%':
                            result = a % b;
                            return true;
                        case '#':
                            result = Math.Pow(a, b);
                            return true;
                    }
                }
                operationPosition = command.IndexOfAny(new char[] {'&','|','^'});
                if (operationPosition != -1)
                {
                    string firstValue = command.Substring(0, operationPosition);
                    string secondValue = command.Substring(operationPosition + 1);
                    uint a, b;
                    if (!UInt32.TryParse(firstValue, out a)) return false;
                    if (!UInt32.TryParse(secondValue, out b)) return false;
                    switch (command[operationPosition])
                    {
                        case '&':
                            result = a & b;
                            return true;
                        case '|':
                            result = a | b;
                            return true;
                        case '^':
                            result = a ^ b;
                            return true;
                    }
                }
                operationPosition = command.IndexOf('!');
                if (operationPosition != -1)
                {
                    if (operationPosition == 0)
                    {
                        uint a;
                        if (!UInt32.TryParse(command.Substring(1), out a)) return false;
                        result = ~a;
                        return true;
                    }
                    if (operationPosition == command.Length - 1)
                    {
                        uint a;
                        if (!UInt32.TryParse(command.Substring(0,command.Length-1), out a)) return false;
                        int factorial = 1;
                        for (var i = 2; i <= a; i++)
                        {
                            factorial = factorial * i;
                        }
                        result = factorial;
                        return true;
                    }
                }
                if (Double.TryParse(command, out result)) return true;
                return false;
            }
        }

        static int Main(string[] args)
        {
            if (args != null && args.Length != 0)
            {
                string command = "";
                foreach (var argument in args)
                {
                    command += argument;
                }
                double result;
                try
                {
                    if (!Calculate(command, out result)) return -1;
                    Console.WriteLine(result);
                }
                catch(OverflowException ex)
                {
                    return -1;
                }
                catch (DivideByZeroException ex)
                {
                    return -1;
                }
                return 200;
            }
            else
            {
                Console.WriteLine("Task 3.1 Calculator by Daniil Panasenko\n");
                PrintCommandsList();
                string command = "";
                do
                {
                    Console.WriteLine("Enter the command...");
                    command = Console.ReadLine();
                    if (command == "help") PrintCommandsList();
                    else if (command != "exit")
                    {
                        try
                        {
                            double result;
                            if (!Calculate(command, out result))
                            {
                                Console.WriteLine("Unknown command. If you want to view the list of commands enter help command.");
                            }
                            else
                            {
                                Console.WriteLine(result);
                            }
                        }
                        catch(OverflowException ex)
                        {
                            Console.WriteLine("Overflowing has happened, please enter another command");
                        }
                        catch(DivideByZeroException ex)
                        {
                            Console.WriteLine("Divide by zero");
                        }
                    }
                }
                while (command != "exit");

            }
            return 0;
        }
    }
}

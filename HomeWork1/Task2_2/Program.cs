using System;

namespace Task2_2
{
    class Rect
    {
        public double A { get; private set; }

        public double B { get; private set; }

        public double Area => A * B;

        public bool IsCorrect => A >= 0 && B >= 0;

        public Rect(double a, double b)
        {
            A = a;
            B = b;
        }
    }

    class Square: Rect
    {
        public Square(double a) : base(a, a)
        {
        }
    }

    class Triangle
    {
        public double A { get; private set; }

        public double B { get; private set; }

        public double C { get; private set; }

        private double Perimeter => A + B + C;

        public double Area => Math.Sqrt(Perimeter / 2 * (Perimeter / 2 - A) * (Perimeter / 2 - B) * (Perimeter / 2 - C));

        public bool IsCorrect => A >= 0 && B >= 0 && C >= 0 && (C + B >= A) && (A + B >= C) && (C + A >= B);

        public Triangle(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;
        }
    }

    class Circle
    {
        public double Radius { get; private set; }

        public double Area => Math.PI * Math.Pow(Radius, 2);

        public bool IsCorrect => Radius>=0;

        public Circle(double radius)
        {
            Radius = radius;
        }
    }

    class Program
    {
        static string CommandExecution(string command)
        {
            string[] array = command.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            double a, b, c, radius;
            if (array.Length == 0) return "Empty command";
            switch (array[0])
            {
                case "rect":
                    if (array.Length != 3) return "Wrong number of arguments";
                    if (!Double.TryParse(array[1], out a)) return "Wrong argument";
                    if (!Double.TryParse(array[2], out b)) return "Wrong argument";
                    Rect rect = new Rect(a, b);
                    if (!rect.IsCorrect) return "The rectangle can't contain such arguments";
                    return rect.Area.ToString();
                case "square":
                    if (array.Length != 2) return "Wrong number of arguments";
                    if (!Double.TryParse(array[1], out a)) return "Wrong argument";
                    Square square = new Square(a);
                    if (!square.IsCorrect) return "The square can't contain such arguments";
                    return square.Area.ToString();
                case "triangle":
                    if (array.Length != 4) return "Wrong number of arguments";
                    if (!Double.TryParse(array[1], out a)) return "Wrong argument";
                    if (!Double.TryParse(array[2], out b)) return "Wrong argument";
                    if (!Double.TryParse(array[3], out c)) return "Wrong argument";
                    Triangle triangle = new Triangle(a, b, c);
                    if (!triangle.IsCorrect) return "The triangle can't contain such arguments";
                    return triangle.Area.ToString();
                case "circle":
                    if (array.Length != 2) return "Wrong number of arguments";
                    if (!Double.TryParse(array[1], out radius)) return "Wrong argument";
                    Circle circle = new Circle(radius);
                    if (!circle.IsCorrect) return "The circle can't contain such arguments";
                    return circle.Area.ToString();
                default:
                    return "Unknown command";
            }
        }

        static void PrintCommandsList()
        {
            Console.WriteLine("Commands list:");
            Console.WriteLine("rect {a} {b}");
            Console.WriteLine("square {a}");
            Console.WriteLine("triangle {a} {b} {c}");
            Console.WriteLine("circle {radius}");
            Console.WriteLine("exit");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("* a, b, c, radius - double values");
            Console.WriteLine("* parameters can't be negative");
            Console.WriteLine("* sum of any two sides of a triangle can't be shorter then third\n");
        }

        static int Main(string[] args)
        {
            double area;
            if (args!=null && args.Length != 0)
            {
                string command = "";
                foreach(var argument in args)
                {
                    command += argument+" ";
                }
                Console.WriteLine(command);
                string result = CommandExecution(command);
                if (!Double.TryParse(result, out area)) return -1;
                Console.WriteLine(area);
                return 200;
            }
            else
            {
                Console.WriteLine("Task 2.2 Сalculation of the area of ​​figures by Daniil Panasenko\n");
                PrintCommandsList();
                string command = "";
                while (command != "exit")
                {
                    Console.WriteLine("Enter the command...");
                    command = Console.ReadLine();
                    if (command != "exit")
                    {
                        string result = CommandExecution(command);
                        Console.WriteLine(result+"\n");
                        if (!Double.TryParse(result, out area)) PrintCommandsList();
                    }
                }
            }
            return 0;
        }
    }
}

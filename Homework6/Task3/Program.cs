using System;
using System.Collections.Generic;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Task 3. Service for issuing unique logins\n");
            LoginGenerator.Generate(1000);
            List<User> users = UsersReader.Read();
            int countThreads;
            do
            {
                Console.WriteLine("Enter count of threads for testing authentication service");
            }
            while (!int.TryParse(Console.ReadLine(), out countThreads));
            AuthenticaionServiceTesting testing = new AuthenticaionServiceTesting(users);
            var result = testing.Run(countThreads);
            ResultWriter.Write(result);
        }
    }
}

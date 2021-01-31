using System;
using System.IO;
using System.Linq;

namespace Task3
{
    public static class LoginGenerator
    {
        private static string GenerateWord()
        {
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            Random random = new Random();
            int length = random.Next(1, 17);
            string result = "";
            for(int i=0;i<length; i++)
            {
                int charNumber = random.Next(chars.Length);
                result += chars[charNumber];
            }
            return result;
        }

        public static void Generate(int count)
        {
            string[] users = new string[count+1];
            users[0] = "login;password";
            for(int i=1; i<count+1; i++)
            {
                users[i] = GenerateWord() + ";" + GenerateWord();
            }
            File.WriteAllLines("users.csv", users);
        }
    }
}

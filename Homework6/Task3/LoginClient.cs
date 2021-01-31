using System;
using System.Threading;

namespace Task3
{
    public class LoginClient
    {
        public LoginClient()
        {
        }

        public Guid? Login(string login, string password)
        {
            Random random = new Random();
            Thread.Sleep(random.Next(1000));
            if (random.Next(2) == 0)
            {
                return Guid.NewGuid();
            }
            return null;
        }
    }
}

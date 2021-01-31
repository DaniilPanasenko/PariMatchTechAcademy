using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Task3
{
    public static class UsersReader
    {
        public static List<User> Read()
        {
            var users = File.ReadLines("users.csv")
                .Skip(1)
                .Select(x => new User(x.Split(';')[0], x.Split(';')[1]))
                .ToList();
            return users;
        }
    }
}

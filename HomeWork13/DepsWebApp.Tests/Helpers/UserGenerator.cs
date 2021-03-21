using System;
using System.IO;
using System.Text.Json;
using DepsWebApp.Tests.Models;

namespace DepsWebApp.Tests.Helpers
{
    public class UserGenerator
    {
        private string _userCountPath = "Json/users.json";

        public UserGenerator()
        {
        }

        private int UserCount
        {
            get
            {
                var json = File.ReadAllText(_userCountPath);
                var userCount = JsonSerializer.Deserialize<int>(json);
                return userCount;
            }
            set
            {
                var json = JsonSerializer.Serialize(value);
                File.WriteAllText(_userCountPath, json);
            }
        }

        public User GetNewUser()
        {
            UserCount++;
            return new User($"Login{UserCount}", "Password");
        }

        public User GetExistingUser()
        {
            if (UserCount == 0) return null;
            return new User("Login1", "Password");
        }

        public User GetExistingUserWithInvalidPassword()
        {
            if (UserCount == 0) return null;
            return new User("Login1", "InvalidPassword");
        }

        public User GetNotValidUser()
        {
            return new User("Login", "Pass");
        }

        public User GetInvalidLoginUser()
        {
            return new User("InvalidLogin", "Password");
        }
    }
}

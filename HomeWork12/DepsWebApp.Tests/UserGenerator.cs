using System;
namespace DepsWebApp.Tests
{
    public class UserGenerator
    {
        private int _userCount;

        public UserGenerator()
        {
            _userCount = 0;
        }

        public User GetNewUser()
        {
            _userCount++;
            return new User($"Login{_userCount}", "Password");
        }

        public User GetExistingUser()
        {
            if (_userCount == 0) return null;
            return new User("Login1", "Password");
        }

        public User GetExistingUserWithInvalidPassword()
        {
            if (_userCount == 0) return null;
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

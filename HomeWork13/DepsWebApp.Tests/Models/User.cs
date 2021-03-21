using System;
using System.Text.Json.Serialization;

namespace DepsWebApp.Tests.Models
{
    public class User
    {
        [JsonPropertyName("login")]
        public string Login { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        public User() { }

        public User(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}

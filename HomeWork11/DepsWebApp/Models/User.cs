﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DepsWebApp.Models
{
    /// <summary>
    /// User model.
    /// </summary>
    public class User
    {
        /// <summary>
        /// User login.
        /// </summary>
        [Required]
        public string Login { get; set; }

        /// <summary>
        /// User password.
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        public User() { }

        /// <summary>
        /// Constructor <see cref="User"/> with <paramref name="login"/> and <paramref name="password"/>.
        /// </summary>
        /// <param name="login">The <see cref="Login"/> value.</param>
        /// <param name="password">The <see cref="Password"/> value.</param>
        public User(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}

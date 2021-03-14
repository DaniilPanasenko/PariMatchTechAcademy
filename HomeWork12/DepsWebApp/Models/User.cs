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
        public string Login { get; set; }

        /// <summary>
        /// User password hash.
        /// </summary>
        public int PasswordHash { get; set; }

        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        public User() { }

        /// <summary>
        /// Constructor <see cref="User"/> with <paramref name="login"/> and <paramref name="passwordHash"/>.
        /// </summary>
        /// <param name="login">The <see cref="Login"/> value.</param>
        /// <param name="passwordHash">The <see cref="PasswordHash"/> value.</param>
        public User(string login, int passwordHash)
        {
            Login = login;
            PasswordHash = passwordHash;
        }
    }
}

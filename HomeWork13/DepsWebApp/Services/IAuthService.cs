using System;
using System.Threading.Tasks;

namespace DepsWebApp.Services
{
    /// <summary>
    /// Authentication service
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Registers user.
        /// </summary>
        /// <param name="login">User login.</param>
        /// <param name="password">User password.</param>
        /// <returns>Returns <c>true</c> if account successfully created or <c>false</c> if login already existed.</returns>
        /// <exception cref="ArgumentNullException">Throws when one of the arguments is null.</exception>
        Task<bool> RegisterAsync(string login, string password);

        /// <summary>
        /// Authenticate user.
        /// </summary>
        /// <param name="login">User login.</param>
        /// <param name="password">User password.</param>
        /// <returns>Returns <c>true</c> if account was found or <c>false</c> if user wasn't found or password is invalid.</returns>
        Task<bool> AuthenticateAsync(string login, string password);
    }
}

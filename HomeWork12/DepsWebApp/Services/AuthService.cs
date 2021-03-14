using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using DepsWebApp.Models;

namespace DepsWebApp.Services
{
    /// <summary>
    /// Implementation of <see cref="IAuthService"/>.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly ConcurrentDictionary<string, User> _accounts = new ConcurrentDictionary<string, User>();

        /// <inheritdoc/>
        public Task<bool> RegisterAsync(string login, string password)
        {
            if (login == null) throw new ArgumentNullException(nameof(login));
            if (password == null) throw new ArgumentNullException(nameof(password));

            if (!_accounts.TryAdd(login, new User(
                login,
                password.GetHashCode())))
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        /// <inheritdoc/>
        public Task<bool> AuthenticateAsync(string login, string password)
        {
            if (login == null || password == null) return Task.FromResult(false);
            if (!_accounts.TryGetValue(login, out var account) || account.PasswordHash != password.GetHashCode())
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }
    }
}

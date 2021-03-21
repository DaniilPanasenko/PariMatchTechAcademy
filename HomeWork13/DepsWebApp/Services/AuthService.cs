using System;
using System.Linq;
using System.Threading.Tasks;
using DepsWebApp.Contexts;
using DepsWebApp.Database.Models;

namespace DepsWebApp.Services
{
    /// <summary>
    /// Implementation of <see cref="IAuthService"/>.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly DepsWebAppContext _dbContext;

        public AuthService(DepsWebAppContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<bool> RegisterAsync(string login, string password)
        {
            if (login == null) throw new ArgumentNullException(nameof(login));
            if (password == null) throw new ArgumentNullException(nameof(password));

            if(_dbContext.Users.FirstOrDefault(x => x.Login == login) != null)
            {
                return false;
            }

            var user = new User
            {
                Login = login,
                Password = password
            };

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        /// <inheritdoc/>
        public Task<bool> AuthenticateAsync(string login, string password)
        {
            if (login == null || password == null) return Task.FromResult(false);
            if(_dbContext.Users
                .FirstOrDefault(x => x.Login == login &&
                    x.Password == password) == null)
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }
    }
}

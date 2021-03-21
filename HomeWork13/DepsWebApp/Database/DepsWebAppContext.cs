using DepsWebApp.Database.Models;
using DepsWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DepsWebApp.Contexts
{
    /// <summary>
    /// DepsWebApp Database context
    /// </summary>
    public class DepsWebAppContext : DbContext
    {
        /// <summary>
        /// Users DbSet <see cref="User"/>.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options"></param>
        public DepsWebAppContext(DbContextOptions<DepsWebAppContext> options) : base(options)
        {
        }

        /// <summary>
        /// On model creating
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
        }
    }
}
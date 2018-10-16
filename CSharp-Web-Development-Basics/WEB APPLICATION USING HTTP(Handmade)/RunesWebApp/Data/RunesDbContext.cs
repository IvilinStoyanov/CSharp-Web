using RunesWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace RunesWebApp.Data
{
    public class RunesDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Track> Tracks { get; set; }

        protected override void OnConfiguring(
           DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(DbConfig.ConnectionString)
                .UseLazyLoadingProxies();
        }
    }
}

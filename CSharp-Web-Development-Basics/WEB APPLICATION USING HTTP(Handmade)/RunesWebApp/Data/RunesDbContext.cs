using RunesWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace RunesWebApp.Data
{
    public class RunesDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Track> Tracks { get; set; }

        public DbSet<TrackAlbum> TrackAlbums { get; set; }

        protected override void OnConfiguring(
           DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(DbConfig.ConnectionString)
                .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<TrackAlbum>()
                .HasKey(x => new { x.AlbumId, x.TrackId });
        }
    }
}

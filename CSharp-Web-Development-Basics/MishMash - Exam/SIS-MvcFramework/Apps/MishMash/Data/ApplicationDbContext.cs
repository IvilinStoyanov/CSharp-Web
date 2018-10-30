using Microsoft.EntityFrameworkCore;
using MishMash.Models;

namespace MishMash.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Channel> Channels { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<UserInChannel> UserInChannel { get; set; }

        public ApplicationDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=GIGABYTE-PC\SQLEXPRESS;Database=MishMash;Integrated Security=True;");
        }
    }
}

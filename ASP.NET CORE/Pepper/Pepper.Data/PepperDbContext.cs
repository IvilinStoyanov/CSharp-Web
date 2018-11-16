using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pepper.Models;

namespace Pepper.Data.Web.Data
{
    public class PepperDbContext : IdentityDbContext
    {
        public PepperDbContext(DbContextOptions<PepperDbContext> options)
            : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }
    }
}

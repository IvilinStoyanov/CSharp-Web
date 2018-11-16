using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Pepper.Data.Web.Data
{
    public class PepperDbContext : IdentityDbContext
    {
        public PepperDbContext(DbContextOptions<PepperDbContext> options)
            : base(options)
        {
        }
    }
}

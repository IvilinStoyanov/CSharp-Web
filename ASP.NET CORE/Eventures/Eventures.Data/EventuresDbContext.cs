using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Eventures.Web.Data
{
    public class EventuresDbContext : IdentityDbContext
    {
        public EventuresDbContext(DbContextOptions<EventuresDbContext> options)
            : base(options)
        {
        }
    }
}

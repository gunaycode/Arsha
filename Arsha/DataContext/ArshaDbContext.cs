using Arsha.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Arsha.DataContext
{
    public class ArshaDbContext:IdentityDbContext<AppUser>
    {
        public ArshaDbContext(DbContextOptions<ArshaDbContext> options) : base(options) { }
      public DbSet<Worker> Workers { get; set; }
      public DbSet<Team> Teams { get; set; }

    }
}

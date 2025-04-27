using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Camping.Data.Entities;

namespace Camping.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Camping.Data.Entities.CampSite> CampSite { get; set; } = default!;
        public DbSet<Camping.Data.Entities.Review> Review { get; set; } = default!;
        public DbSet<Camping.Data.Entities.User> User { get; set; } = default!;
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace PSST.Models
{
    public class PSSTContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Treat> Treats { get; set; }
        public DbSet<Flavor> Flavors { get; set; }
        public DbSet<TreatFlavor> TreatFlavors { get; set; }
        public PSSTContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.NavigationBaseIncludeIgnored));
        }
    }
}
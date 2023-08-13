using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace PSST.Models 
{
  public class PSSTContext : IdentityDbContext <ApplicationUser>
  {
    public DbSet<Flavor> Flavors { get; set; }
    public DbSet<Treat> Treats { get; set; }
    public DbSet<FlavorTreat> FlavorTreats { get; set; }
    public PSSTContext(DbContextOptions options) : base(options) { }
  }
}
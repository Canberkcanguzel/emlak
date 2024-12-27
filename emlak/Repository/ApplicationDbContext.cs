using Microsoft.EntityFrameworkCore;
using emlak.Models;
using emlak.Models.user;

namespace emlak.Repository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Properties> Propertie { get; set; }
        public DbSet<ContactRequests> ContactRequest { get; set; }
        public DbSet<PropertyImages> PropertyImage { get; set; }

        public DbSet<Users> User { get; set; }


    }
}

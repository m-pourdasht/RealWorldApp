using Microsoft.EntityFrameworkCore;
using RealWorldApp.Shared.Models;

namespace RealWorldApp.Server.Data // Adjust namespace as necessary
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        // Add other DbSet properties as needed
    }

}


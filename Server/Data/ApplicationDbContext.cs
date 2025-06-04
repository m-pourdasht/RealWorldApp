using Microsoft.EntityFrameworkCore;
//using RealWorldApp.Client.Pages;
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
        public DbSet<ProductDto> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Specify precision and scale for decimal property
            modelBuilder.Entity<ProductDto>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18, 2)");
        }
    }

}


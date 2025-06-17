using RealWorldApp.Server.Data; // Ensure this namespace contains AppDbContext
using Microsoft.EntityFrameworkCore; // Ensure this namespace is correctly referenced
using RealWorldApp.Shared.Models;

namespace RealWorldApp.Server.Data
{
    public static class DataSeeder
    {
        public static void SeedRoles(ApplicationDbContext context)
        {
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new Role { Name = "Admin" },
                    new Role { Name = "Moderator" },
                    new Role { Name = "User" }
                );

                context.SaveChanges();
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using RealWorldApp.Server.Data;
using RealWorldApp.Shared.Models;
using RealWorldApp.Shared.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq; // Add this using directive

namespace RealWorldApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _context.Users.Include(u => u.Role).ToListAsync();

            var userDtos = users.Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.Username ?? string.Empty,
                Email = u.Email ?? string.Empty,
                PasswordHash = u.PasswordHash ?? string.Empty, // Ensure PasswordHash is mapped
                Role = u.Role != null
                    ? new RoleDto { Id = u.Role.Id, Name = u.Role.Name }
                    : new RoleDto { Id = 0, Name = string.Empty } // Map RoleDto properly
            }).ToList();

            return Ok(userDtos);
        }
    }
}
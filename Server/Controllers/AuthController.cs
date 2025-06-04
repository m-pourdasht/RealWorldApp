using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealWorldApp.Server.Data;
using RealWorldApp.Shared.Models;
using System.Security.Cryptography;
using System.Text;

namespace RealWorldApp.Server.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Find user by username or email
            var user = await _context.Users
                .FirstOrDefaultAsync(u =>
                    (!string.IsNullOrEmpty(model.Username) && u.Username == model.Username) ||
                    (!string.IsNullOrEmpty(model.Email) && u.Email == model.Email));

            if (user == null)
                return Unauthorized(new { message = "Invalid username/email or password." });

            // Check password using BCrypt
            if (!BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                return Unauthorized(new { message = "Invalid username/email or password." });

            // Optionally: Generate and return a JWT token here

            return Ok(new { message = "Login successful." });
        }

    }
}


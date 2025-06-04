using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RealWorldApp.Server.Data;
using RealWorldApp.Shared.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static RealWorldApp.Client.Components.UserProfile;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RealWorldApp.Server.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public AuthController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join("; ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                return BadRequest(errors);
            }

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

            // Create JWT token
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email)
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
             issuer: _config["Jwt:Issuer"],
             audience: _config["Jwt:Audience"],
             claims: claims,
             expires: DateTime.Now.AddHours(1),
             signingCredentials: creds
            );


            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { token = tokenString });
        }
    }
}


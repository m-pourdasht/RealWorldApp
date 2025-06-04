using Microsoft.AspNetCore.Mvc;
using RealWorldApp.Shared.Models;
using RealWorldApp.Server.Data;
using Microsoft.EntityFrameworkCore; // Ensure this is present


[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AuthController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState); // Returns specific validation errors

        if (await _context.Users.AnyAsync(u => u.Username == model.Username))
            return BadRequest(new { message = "Username already exists." });

        var user = new User
        {
            Username = model.Username,
            Email = model.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { message = "User registered successfully." });
    }
}

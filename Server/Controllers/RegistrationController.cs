using Microsoft.AspNetCore.Mvc;
using RealWorldApp.Shared.Models;
using RealWorldApp.Server.Data;
using Microsoft.EntityFrameworkCore;

[Route("api/auth")]
[ApiController]
public class RegistrationController : ControllerBase // Fixed class name
{
    private readonly ApplicationDbContext _context;

    public RegistrationController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] Registeration register)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (await _context.Users.AnyAsync(u => u.Username == register.Username))
            return BadRequest(new { message = "Username already exists." });

        var user = new User
        {
            Username = register.Username, // Use 'register' not 'model'
            Email = register.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(register.Password),
            RoleId = 2 // نقش پیش‌فرض برای ثبت‌نام
        };

        try
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok("ثبت‌نام موفق بود");
        }
        catch (DbUpdateException ex)
        {
            return BadRequest($"خطا در ثبت‌نام: {ex.InnerException?.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"خطای غیرمنتظره: {ex.Message}");
        }
        //return Ok(new { message = "User registered successfully." });
    }
}

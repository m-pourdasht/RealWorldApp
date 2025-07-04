﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RealWorldApp.Shared.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Collections.Generic;
using static RealWorldApp.Client.Components.UserProfile;
using RealWorldApp.Shared.Dto;

namespace RealWorldApp.Server.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserDto userDto)
        {
            if (userDto == null)
                return BadRequest("Invalid client request");

            // Assign roles manually (example: Admin for a specific email)
            if (userDto.Email == "admin@example.com")
            {
                userDto.Role = new RoleDto { Name = "Admin" };
            }
            else
            {
                userDto.Role = new RoleDto { Name = "User" };
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecretKey"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userDto.Username),
                new Claim(ClaimTypes.Email, userDto.Email),
                new Claim(ClaimTypes.Role, userDto.Role.Name)
            };

            var tokenOptions = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: signinCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new { Token = token });
        }
    }
}
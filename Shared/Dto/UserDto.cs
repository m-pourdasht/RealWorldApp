using RealWorldApp.Shared.Dto;
using System.ComponentModel.DataAnnotations;

namespace RealWorldApp.Shared.Models
{
    public class UserDto
    {
        public int Id { get; set; }  // Primary Key

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        public RoleDto Role { get; set; }
    }
}

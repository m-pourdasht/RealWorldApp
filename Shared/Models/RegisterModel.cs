using System.ComponentModel.DataAnnotations;

namespace RealWorldApp.Shared.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Username must be between 5 and 20 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Password must be between 8 and 20 characters.")]
        public string Password { get; set; }
    }
}

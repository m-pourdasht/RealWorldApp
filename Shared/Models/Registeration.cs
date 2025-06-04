using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealWorldApp.Shared.Models
{
    public class Registeration
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Username must be between 5 and 20 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 20 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
    }
}


using System;
using System.ComponentModel.DataAnnotations;

namespace Workify.DTOs
{
    public class UserDto
    {
        [Required(ErrorMessage = "User ID is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        [RegularExpression("Employer|JobSeeker", ErrorMessage = "Role must be either 'Employer' or 'JobSeeker'.")]
        public string Role { get; set; }
    }
}


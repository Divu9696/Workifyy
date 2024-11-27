using System;
using System.ComponentModel.DataAnnotations;

namespace Workify.DTOs;

public class UserUpdateDto
{
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
}

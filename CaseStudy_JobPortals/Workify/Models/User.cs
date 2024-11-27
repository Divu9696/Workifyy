using System;

namespace Workify.Models
{
    public class User
    {
        public int UserId { get; set; } // Primary Key
        public string Name { get; set; }
        public string Email { get; set; } // Unique
        public string Password { get; set; }
        // public int RoleId { get; set; }
        public string Role { get; set; } // Either "Employer" or "JobSeeker"

        // Navigation properties
        public Employer Employer { get; set; } // One-to-One
        public JobSeeker JobSeeker { get; set; } // One-to-One
        // public Role Role { get; set; } 
    }
}


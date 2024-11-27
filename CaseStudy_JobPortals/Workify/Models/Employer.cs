using System;

namespace Workify.Models
{
    public class Employer
    {
        public int EmployerId { get; set; } // Primary Key
        public int UserId { get; set; } // Foreign Key
        public int CompanyId { get; set; } // Foreign Key

        // Navigation properties
        public User User { get; set; } // Many-to-One
        public Company Company { get; set; } // Many-to-One
        public ICollection<JobListing> JobListings { get; set; } // One-to-Many
    }
}


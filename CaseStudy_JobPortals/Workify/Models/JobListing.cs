using System;

namespace Workify.Models
{
    public class JobListing
    {
        public int JobId { get; set; } // Primary Key
        public string Title { get; set; }
        public string Description { get; set; }
        public string Qualifications { get; set; }
        public decimal Salary { get; set; }
        public string JobType { get; set; }
        public string RequiredSkills { get; set; }
        public int EmployerId { get; set; } // Foreign Key
        public string Location { get; set; }
        public string Industry { get; set; }

        // Navigation properties
        public Employer Employer { get; set; } // Many-to-One
        // public Company Company { get; set; } // Many-to-One
        public ICollection<Application> Applications { get; set; } // One-to-Many
    }
}


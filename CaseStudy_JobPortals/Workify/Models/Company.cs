using System;

namespace Workify.Models
{
    public class Company
    {
        public int CompanyId { get; set; } // Primary Key
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }

        // Navigation properties
        public ICollection<Employer> Employers { get; set; } // One-to-Many
        // public ICollection<JobListing> JobListings { get; set; } // One-to-Many
    }
}


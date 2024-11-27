using System;

namespace Workify.Models
{
    public class Application
    {
        public int ApplicationId { get; set; } // Primary Key
        public int SeekerId { get; set; } // Foreign Key
        public int JobId { get; set; } // Foreign Key
        public byte[] Resume { get; set; } // Binary
        public string Status { get; set; }
        public DateTime AppliedOn { get; set; }

        // Navigation properties
        public JobSeeker JobSeeker { get; set; } // Many-to-One
        public JobListing JobListing { get; set; } // Many-to-One
    }
}


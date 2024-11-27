using System;

namespace Workify.Models
{
    public class JobSeeker
    {
        public int SeekerId { get; set; } // Primary Key
        public int UserId { get; set; } // Foreign Key
        public string ProfileSummary { get; set; }
        public int Experience { get; set; } // in years
        public string Skills { get; set; }

        // Navigation properties
        public User User { get; set; } // One-to-One
        public ICollection<Resume> Resumes { get; set; } // One-to-Many
        public ICollection<Application> Applications { get; set; } // One-to-Many
        public ICollection<SearchHistory> SearchHistories { get; set; } // One-to-Many
    }
}


using System;

namespace Workify.Models
{
    public class SearchHistory
    {
        public int SearchId { get; set; } // Primary Key
        public int SeekerId { get; set; } // Foreign Key
        public string SearchCriteria { get; set; }
        public DateTime SearchDate { get; set; }

        // Navigation properties
        public JobSeeker JobSeeker { get; set; } // Many-to-One
    }
}


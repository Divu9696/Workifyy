using System;

namespace Workify.Models
{
    public class Resume
    {
        public int ResumeId { get; set; } // Primary Key
        public int SeekerId { get; set; } // Foreign Key
        // public byte[] ResumeData { get; set; } // Binary
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public DateTime LastUpdated { get; set; }

        // Navigation properties
        public JobSeeker JobSeeker { get; set; } // Many-to-One
    }
}

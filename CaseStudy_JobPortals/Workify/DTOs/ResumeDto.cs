using System;

using System.ComponentModel.DataAnnotations;

namespace Workify.DTOs
{
    public class ResumeDto
    {
        [Required]
        public int ResumeId { get; set; }

        [Required]
        public int SeekerId { get; set; }

        [Required]
        // public byte[] ResumeData { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }
    }
}


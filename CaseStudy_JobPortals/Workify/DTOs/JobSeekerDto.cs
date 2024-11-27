using System;


using System.ComponentModel.DataAnnotations;

namespace Workify.DTOs
{
    public class JobSeekerDto
    {
        [Required]
        public int SeekerId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(500)]
        public string ProfileSummary { get; set; }

        [Required]
        [Range(0, 50, ErrorMessage = "Experience must be between 0 and 50 years.")]
        public int Experience { get; set; }

        [Required]
        public string Skills { get; set; } // Comma-separated values
    }
}


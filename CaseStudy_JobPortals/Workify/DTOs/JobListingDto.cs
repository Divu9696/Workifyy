using System;

using System.ComponentModel.DataAnnotations;

namespace Workify.DTOs
{
    public class JobListingDto
    {
        [Required]
        public int JobId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [StringLength(500)]
        public string Qualifications { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Salary { get; set; }

        [Required]
        [StringLength(50)]
        public string JobType { get; set; }

        [Required]
        public string RequiredSkills { get; set; } // Comma-separated values

        [Required]
        public int EmployerId { get; set; }

        [Required]
        [StringLength(100)]
        public string Location { get; set; }

        [Required]
        [StringLength(100)]
        public string Industry { get; set; }
    }
}


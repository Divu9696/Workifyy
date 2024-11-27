using System;

using System.ComponentModel.DataAnnotations;

namespace Workify.DTOs
{
    public class CompanyDto
    {
        [Required]
        public int CompanyId { get; set; }

        [Required]
        [StringLength(100)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Url]
        public string Website { get; set; }
    }
}


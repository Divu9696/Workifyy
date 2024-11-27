using System;

using System.ComponentModel.DataAnnotations;

namespace Workify.DTOs
{
    public class ApplicationDto
    {
        [Required]
        public int ApplicationId { get; set; }

        [Required]
        public int SeekerId { get; set; }

        [Required]
        public int JobId { get; set; }

        [Required]
        public byte[] Resume { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        [Required]
        public DateTime AppliedOn { get; set; }
    }
}

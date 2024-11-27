using System;
using System.ComponentModel.DataAnnotations;

namespace Workify.DTOs
{
    public class EmployerDto
    {
        [Required]
        public int EmployerId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int CompanyId { get; set; }
    }
}

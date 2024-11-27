using System;

using System.ComponentModel.DataAnnotations;

namespace Workify.DTOs
{
    public class SearchHistoryDto
    {
        [Required]
        public int SearchId { get; set; }

        [Required]
        public int SeekerId { get; set; }

        [Required]
        [StringLength(500)]
        public string SearchCriteria { get; set; }

        [Required]
        public DateTime SearchDate { get; set; }
    }
}


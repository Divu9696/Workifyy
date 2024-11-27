using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Workify.Models;
using Workify.DTOs;

namespace Workify.Services
{
    public interface IJobListingService
    {
        Task<JobListingDto> CreateJobListingAsync(JobListingDto jobListingCreateDTO);
        Task<List<JobListingDto>> GetAllJobListingsAsync();
        Task<JobListingDto> GetJobListingByIdAsync(int jobId);
        Task<JobListingDto> UpdateJobListingAsync(int jobId, JobListingDto jobListingUpdateDTO);
        Task<bool> DeleteJobListingAsync(int jobId);
    }
}

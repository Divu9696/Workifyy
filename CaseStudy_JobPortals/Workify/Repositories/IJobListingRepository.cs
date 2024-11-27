using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Workify.Models;

namespace Workify.Repositories
{
    public interface IJobListingRepository
    {
        Task<JobListing> CreateJobListingAsync(JobListing jobListing);
        Task<List<JobListing>> GetAllJobListingsAsync();
        Task<JobListing> GetJobListingByIdAsync(int jobId);
        Task<JobListing> UpdateJobListingAsync(int jobId, JobListing jobListing);
        Task<bool> DeleteJobListingAsync(int jobId);
    }
}


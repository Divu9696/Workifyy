using System;
using Workify.Models;

namespace Workify.Repositories
{
    public interface IJobSeekerRepository
    {
        Task<JobSeeker> CreateJobSeekerAsync(JobSeeker jobSeeker);
        Task<JobSeeker> GetJobSeekerByIdAsync(int jobSeekerId);
        Task<IEnumerable<JobSeeker>> GetAllJobSeekersAsync();
        Task<JobSeeker> UpdateJobSeekerAsync(int jobSeekerId, JobSeeker jobSeeker);
        Task<bool> DeleteJobSeekerAsync(int jobSeekerId);
        Task<IEnumerable<Application>> GetApplicationsByJobSeekerIdAsync(int jobSeekerId);
        Task<IEnumerable<Resume>> GetResumesByJobSeekerIdAsync(int jobSeekerId);
        Task<IEnumerable<SearchHistory>> GetSearchHistoryByJobSeekerIdAsync(int jobSeekerId);
    }
}


using System;

using Workify.Models;
using Workify.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Workify.Services
{
    public interface IJobSeekerService
    {
        Task<JobSeekerDto> CreateJobSeekerAsync(JobSeekerDto jobSeekerCreateDTO);
        Task<JobSeekerDto> GetJobSeekerByIdAsync(int seekerId);
        Task<IEnumerable<JobSeekerDto>> GetAllJobSeekersAsync();
        Task<JobSeekerDto> UpdateJobSeekerAsync(int seekerId, JobSeekerUpdateDTO jobSeekerUpdateDTO);
        Task<bool> DeleteJobSeekerAsync(int seekerId);
        Task<IEnumerable<ApplicationDto>> GetApplicationsByJobSeekerIdAsync(int seekerId);
        Task<IEnumerable<ResumeDto>> GetResumesByJobSeekerIdAsync(int seekerId);
    }
}


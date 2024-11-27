using System;

using Workify.Repositories;
using Workify.Models;
using Workify.DTOs;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Workify.Services
{
    public class JobSeekerService : IJobSeekerService
    {
        private readonly IJobSeekerRepository _jobSeekerRepository;
        private readonly IMapper _mapper;

        public JobSeekerService(IJobSeekerRepository jobSeekerRepository, IMapper mapper)
        {
            _jobSeekerRepository = jobSeekerRepository;
            _mapper = mapper;
        }

        public async Task<JobSeekerDto> CreateJobSeekerAsync(JobSeekerDto jobSeekerCreateDTO)
        {
            // Map DTO to Entity
            var jobSeeker = _mapper.Map<JobSeeker>(jobSeekerCreateDTO);

            // Call repository to save the job seeker
            jobSeeker = await _jobSeekerRepository.CreateJobSeekerAsync(jobSeeker);

            // Map Entity to DTO and return
            return _mapper.Map<JobSeekerDto>(jobSeeker);
        }

        public async Task<JobSeekerDto> GetJobSeekerByIdAsync(int seekerId)
        {
            var jobSeeker = await _jobSeekerRepository.GetJobSeekerByIdAsync(seekerId);
            if (jobSeeker == null)
                return null;

            // Map Entity to DTO and return
            return _mapper.Map<JobSeekerDto>(jobSeeker);
        }

        public async Task<IEnumerable<JobSeekerDto>> GetAllJobSeekersAsync()
        {
            var jobSeekers = await _jobSeekerRepository.GetAllJobSeekersAsync();

            // Map Entities to DTOs and return
            return _mapper.Map<IEnumerable<JobSeekerDto>>(jobSeekers);
        }

        public async Task<JobSeekerDto> UpdateJobSeekerAsync(int seekerId, JobSeekerUpdateDTO jobSeekerUpdateDTO)
        {
            // Map DTO to Entity
            var jobSeeker = _mapper.Map<JobSeeker>(jobSeekerUpdateDTO);

            // Call repository to update the job seeker
            jobSeeker = await _jobSeekerRepository.UpdateJobSeekerAsync(seekerId, jobSeeker);

            if (jobSeeker == null)
                return null;

            // Map Entity to DTO and return
            return _mapper.Map<JobSeekerDto>(jobSeeker);
        }

        public async Task<bool> DeleteJobSeekerAsync(int seekerId)
        {
            // Call repository to delete the job seeker
            return await _jobSeekerRepository.DeleteJobSeekerAsync(seekerId);
        }

        public async Task<IEnumerable<ApplicationDto>> GetApplicationsByJobSeekerIdAsync(int seekerId)
        {
            var applications = await _jobSeekerRepository.GetApplicationsByJobSeekerIdAsync(seekerId);

            // Map Entities to DTOs and return
            return _mapper.Map<IEnumerable<ApplicationDto>>(applications);
        }

        public async Task<IEnumerable<ResumeDto>> GetResumesByJobSeekerIdAsync(int seekerId)
        {
            var resumes = await _jobSeekerRepository.GetResumesByJobSeekerIdAsync(seekerId);

            // Map Entities to DTOs and return
            return _mapper.Map<IEnumerable<ResumeDto>>(resumes);
        }
    }
}


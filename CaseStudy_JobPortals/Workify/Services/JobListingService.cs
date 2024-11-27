using System;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Workify.DTOs;
using Workify.Models;
using Workify.Repositories;

namespace Workify.Services
{
    public class JobListingService : IJobListingService
    {
        private readonly IJobListingRepository _jobListingRepository;
        private readonly IMapper _mapper;

        public JobListingService(IJobListingRepository jobListingRepository, IMapper mapper)
        {
            _jobListingRepository = jobListingRepository;
            _mapper = mapper;
        }

        // Create a new job listing
        public async Task<JobListingDto> CreateJobListingAsync(JobListingDto jobListingCreateDTO)
        {
            var jobListing = _mapper.Map<JobListing>(jobListingCreateDTO);
            var createdJobListing = await _jobListingRepository.CreateJobListingAsync(jobListing);
            return _mapper.Map<JobListingDto>(createdJobListing);
        }

        // Get all job listings
        public async Task<List<JobListingDto>> GetAllJobListingsAsync()
        {
            var jobListings = await _jobListingRepository.GetAllJobListingsAsync();
            return _mapper.Map<List<JobListingDto>>(jobListings);
        }

        // Get job listing by ID
        public async Task<JobListingDto> GetJobListingByIdAsync(int jobId)
        {
            var jobListing = await _jobListingRepository.GetJobListingByIdAsync(jobId);
            if (jobListing == null)
            {
                return null;
            }
            return _mapper.Map<JobListingDto>(jobListing);
        }

        // Update an existing job listing
        public async Task<JobListingDto> UpdateJobListingAsync(int jobId, JobListingDto jobListingUpdateDTO)
        {
            var jobListing = _mapper.Map<JobListing>(jobListingUpdateDTO);
            var updatedJobListing = await _jobListingRepository.UpdateJobListingAsync(jobId, jobListing);
            if (updatedJobListing == null)
            {
                return null;
            }
            return _mapper.Map<JobListingDto>(updatedJobListing);
        }

        // Delete a job listing
        public async Task<bool> DeleteJobListingAsync(int jobId)
        {
            return await _jobListingRepository.DeleteJobListingAsync(jobId);
        }
    }
}


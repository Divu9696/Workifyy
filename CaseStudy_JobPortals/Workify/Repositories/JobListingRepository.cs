using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Workify.Data;
using Workify.Models;

namespace Workify.Repositories
{
    public class JobListingRepository : IJobListingRepository
    {
        private readonly WorkifyDbContext _context;

        public JobListingRepository(WorkifyDbContext context)
        {
            _context = context;
        }

        // Create a new job listing
        public async Task<JobListing> CreateJobListingAsync(JobListing jobListing)
        {
            await _context.JobListings.AddAsync(jobListing);
            await _context.SaveChangesAsync();
            return jobListing;
        }

        // Get all job listings
        public async Task<List<JobListing>> GetAllJobListingsAsync()
        {
            return await _context.JobListings.Include(j => j.Employer).ToListAsync();
        }

        // Get job listing by ID
        public async Task<JobListing> GetJobListingByIdAsync(int jobId)
        {
            return await _context.JobListings.Include(j => j.Employer)
                                              .FirstOrDefaultAsync(j => j.JobId == jobId);
        }

        // Update an existing job listing
        public async Task<JobListing> UpdateJobListingAsync(int jobId, JobListing jobListing)
        {
            var existingJobListing = await _context.JobListings.FindAsync(jobId);
            if (existingJobListing == null)
            {
                return null;
            }

            existingJobListing.Title = jobListing.Title;
            existingJobListing.Description = jobListing.Description;
            existingJobListing.Qualifications = jobListing.Qualifications;
            existingJobListing.Salary = jobListing.Salary;
            existingJobListing.JobType = jobListing.JobType;
            existingJobListing.RequiredSkills = jobListing.RequiredSkills;
            existingJobListing.Location = jobListing.Location;
            existingJobListing.Industry = jobListing.Industry;

            await _context.SaveChangesAsync();
            return existingJobListing;
        }

        // Delete a job listing
        public async Task<bool> DeleteJobListingAsync(int jobId)
        {
            var jobListing = await _context.JobListings.FindAsync(jobId);
            if (jobListing == null)
            {
                return false;
            }

            _context.JobListings.Remove(jobListing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

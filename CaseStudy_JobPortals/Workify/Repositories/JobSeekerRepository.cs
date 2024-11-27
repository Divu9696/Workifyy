using System;

using Microsoft.EntityFrameworkCore;
using Workify.Data;
using Workify.Models;

namespace Workify.Repositories
{
    public class JobSeekerRepository : IJobSeekerRepository
    {
        private readonly WorkifyDbContext _context;

        public JobSeekerRepository(WorkifyDbContext context)
        {
            _context = context;
        }

        public async Task<JobSeeker> CreateJobSeekerAsync(JobSeeker jobSeeker)
        {
            _context.JobSeekers.Add(jobSeeker);
            await _context.SaveChangesAsync();
            return jobSeeker;
        }

        public async Task<JobSeeker> GetJobSeekerByIdAsync(int jobSeekerId)
        {
            return await _context.JobSeekers
                .Include(js => js.Resumes)
                .Include(js => js.Applications)
                .Include(js => js.SearchHistories)
                .FirstOrDefaultAsync(js => js.SeekerId == jobSeekerId);
        }

        public async Task<IEnumerable<JobSeeker>> GetAllJobSeekersAsync()
        {
            return await _context.JobSeekers.ToListAsync();
        }

        public async Task<JobSeeker> UpdateJobSeekerAsync(int jobSeekerId, JobSeeker jobSeeker)
        {
            var existingJobSeeker = await _context.JobSeekers.FindAsync(jobSeekerId);
            if (existingJobSeeker == null) return null;

            existingJobSeeker.ProfileSummary = jobSeeker.ProfileSummary;
            existingJobSeeker.Experience = jobSeeker.Experience;
            existingJobSeeker.Skills = jobSeeker.Skills;
            await _context.SaveChangesAsync();

            return existingJobSeeker;
        }

        public async Task<bool> DeleteJobSeekerAsync(int jobSeekerId)
        {
            var jobSeeker = await _context.JobSeekers.FindAsync(jobSeekerId);
            if (jobSeeker == null) return false;

            _context.JobSeekers.Remove(jobSeeker);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Application>> GetApplicationsByJobSeekerIdAsync(int jobSeekerId)
        {
            return await _context.Applications
                .Where(a => a.SeekerId == jobSeekerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Resume>> GetResumesByJobSeekerIdAsync(int jobSeekerId)
        {
            return await _context.Resumes
                .Where(r => r.SeekerId == jobSeekerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<SearchHistory>> GetSearchHistoryByJobSeekerIdAsync(int jobSeekerId)
        {
            return await _context.SearchHistories
                .Where(sh => sh.SeekerId == jobSeekerId)
                .ToListAsync();
        }
    }
}


using System;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Workify.Models;
using Workify.Data;

namespace Workify.Repositories
{
    public class ResumeRepository : IResumeRepository
    {
        private readonly WorkifyDbContext _context;
        private readonly string _resumeStoragePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "resumes");
        public ResumeRepository(WorkifyDbContext context)
        {
            _context = context;
        }

        // Create a new resume
        public async Task<Resume> CreateAsync(Resume resume)
        {
            
            _context.Resumes.Add(resume);
            await _context.SaveChangesAsync();
            return resume;
        }

        // Get a resume by its ID
        public async Task<Resume> GetByIdAsync(int resumeId)
        {
            return await _context.Resumes
                                 .Include(r => r.JobSeeker)
                                 .FirstOrDefaultAsync(r => r.ResumeId == resumeId);
        }

        // Get all resumes
        public async Task<IEnumerable<Resume>> GetAllAsync()
        {
            return await _context.Resumes
                                 .Include(r => r.JobSeeker)
                                 .ToListAsync();
        }

        // Update an existing resume
        public async Task<Resume> UpdateAsync(Resume resume)
        {
            _context.Resumes.Update(resume);
            await _context.SaveChangesAsync();
            return resume;
        }

        // Delete a resume by its ID
        public async Task<bool> DeleteAsync(int resumeId)
        {
            var resume = await _context.Resumes.FindAsync(resumeId);
            if (resume == null)
            {
                return false;
            }

            _context.Resumes.Remove(resume);
            await _context.SaveChangesAsync();
            return true;
        }
        // Download the resume file
        // public async Task<byte[]> DownloadResumeAsync(int resumeId)
        // {
        //     var resume = await _context.Resumes.FindAsync(resumeId);
        //     if (resume == null)
        //     {
        //         return null; // Handle file not found case
        //     }

        //     // Read the file from storage
        //     return await File.ReadAllBytesAsync(resume.FilePath);
        // }
    }
}


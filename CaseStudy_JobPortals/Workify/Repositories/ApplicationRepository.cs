using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workify.Data;
using Workify.Models;

namespace Workify.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly WorkifyDbContext _context;

        public ApplicationRepository(WorkifyDbContext context)
        {
            _context = context;
        }

        // Create a new application
        public async Task<Application> CreateApplicationAsync(Application application)
        {
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();
            return application;
        }

        // Get all applications
        public async Task<IEnumerable<Application>> GetAllApplicationsAsync()
        {
            return await _context.Applications
                                 .Include(a => a.JobListing)
                                 .Include(a => a.JobSeeker)
                                 .ToListAsync();
        }

        // Get application by ID
        public async Task<Application> GetApplicationByIdAsync(int applicationId)
        {
            return await _context.Applications
                                 .Include(a => a.JobListing)
                                 .Include(a => a.JobSeeker)
                                 .FirstOrDefaultAsync(a => a.ApplicationId == applicationId);
        }

        // Update the status of an application
        public async Task<Application> UpdateApplicationStatusAsync(int applicationId, string status)
        {
            var application = await _context.Applications
                                            .FirstOrDefaultAsync(a => a.ApplicationId == applicationId);

            if (application != null)
            {
                application.Status = status;
                _context.Applications.Update(application);
                await _context.SaveChangesAsync();
            }

            return application;
        }

        // Delete an application
        public async Task<bool> DeleteApplicationAsync(int applicationId)
        {
            var application = await _context.Applications
                                            .FirstOrDefaultAsync(a => a.ApplicationId == applicationId);

            if (application != null)
            {
                _context.Applications.Remove(application);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}


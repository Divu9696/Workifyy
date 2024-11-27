using System;
using Microsoft.EntityFrameworkCore;
using Workify.Data;
using Workify.Models;

namespace Workify.Repositories
{
    public class EmployerRepository : IEmployerRepository
    {
        private readonly WorkifyDbContext _context;

        public EmployerRepository(WorkifyDbContext context)
        {
            _context = context;
        }

        // Create a new employer profile
        public async Task<Employer> CreateEmployerAsync(Employer employer)
        {
            _context.Employers.Add(employer);
            await _context.SaveChangesAsync();
            return employer;
        }

        // Get an employer by ID
        public async Task<Employer> GetEmployerByIdAsync(int employerId)
        {
            return await _context.Employers
                .Include(e => e.Company)  // Including related Company
                .FirstOrDefaultAsync(e => e.EmployerId == employerId);
        }

        // Get all employers
        public async Task<IEnumerable<Employer>> GetAllEmployersAsync()
        {
            return await _context.Employers
                .Include(e => e.Company)  // Including related Company
                .ToListAsync();
        }

        // Update an employer's profile
        public async Task<Employer> UpdateEmployerAsync(int employerId, Employer employer)
        {
            var existingEmployer = await _context.Employers
                                                           .Include(e => e.User) // Include the User entity
                                                            .FirstOrDefaultAsync(e => e.EmployerId == employerId);
            if (existingEmployer == null)
            {
                throw new KeyNotFoundException("Employer not found.");
                // return null;
            }

            // Update fields in the User entity
            if (!string.IsNullOrEmpty(employer.User.Name))
            {
                existingEmployer.User.Name = employer.User.Name;
            }
            if (!string.IsNullOrEmpty(employer.User.Email))
            {
                existingEmployer.User.Email = employer.User.Email;
            }
            existingEmployer.CompanyId = employer.CompanyId;  // Update other fields as needed

            _context.Employers.Update(existingEmployer);
            await _context.SaveChangesAsync();

            return existingEmployer;
        }

        // Delete an employer profile
        public async Task<bool> DeleteEmployerAsync(int employerId)
        {
            var employer = await _context.Employers.FindAsync(employerId);
            if (employer == null)
            {
                return false;
            }

            _context.Employers.Remove(employer);
            await _context.SaveChangesAsync();

            return true;
        }

        // Get all job listings posted by an employer
        public async Task<IEnumerable<JobListing>> GetJobListingsByEmployerIdAsync(int employerId)
        {
            return await _context.JobListings
                .Where(j => j.EmployerId == employerId)
                .ToListAsync();
        }
    }
}

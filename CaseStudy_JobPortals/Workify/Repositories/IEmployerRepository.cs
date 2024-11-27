using System;

using Workify.Models;

namespace Workify.Repositories
{
    public interface IEmployerRepository
    {
        Task<Employer> CreateEmployerAsync(Employer employer);
        Task<Employer> GetEmployerByIdAsync(int employerId);
        Task<IEnumerable<Employer>> GetAllEmployersAsync();
        Task<Employer> UpdateEmployerAsync(int employerId, Employer employer);
        Task<bool> DeleteEmployerAsync(int employerId);
        Task<IEnumerable<JobListing>> GetJobListingsByEmployerIdAsync(int employerId);
    }
}


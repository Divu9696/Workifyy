using System;

using Workify.DTOs;

namespace Workify.Services
{
    public interface IEmployerService
    {
        Task<EmployerDto> CreateEmployerAsync(EmployerDto employerDTO);
        Task<EmployerDto> GetEmployerByIdAsync(int employerId);
        Task<IEnumerable<EmployerDto>> GetAllEmployersAsync();
        Task<EmployerDto> UpdateEmployerAsync(int employerId, EmployerDto employerDTO);
        Task<bool> DeleteEmployerAsync(int employerId);
        Task<IEnumerable<JobListingDto>> GetJobListingsByEmployerIdAsync(int employerId);
    }
}


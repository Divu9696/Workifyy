using AutoMapper;
using Workify.Repositories;
using Workify.Models;
using Workify.DTOs;

namespace Workify.Services
{
    public class EmployerService : IEmployerService
    {
        private readonly IEmployerRepository _employerRepository;
        private readonly IMapper _mapper;

        public EmployerService(IEmployerRepository employerRepository, IMapper mapper)
        {
            _employerRepository = employerRepository;
            _mapper = mapper;
        }

        // Create a new employer
        public async Task<EmployerDto> CreateEmployerAsync(EmployerDto employerDTO)
        {
            var employer = _mapper.Map<Employer>(employerDTO);
            var createdEmployer = await _employerRepository.CreateEmployerAsync(employer);
            return _mapper.Map<EmployerDto>(createdEmployer);
        }

        // Get an employer by ID
        public async Task<EmployerDto> GetEmployerByIdAsync(int employerId)
        {
            var employer = await _employerRepository.GetEmployerByIdAsync(employerId);
            return employer == null ? null : _mapper.Map<EmployerDto>(employer);
        }

        // Get all employers
        public async Task<IEnumerable<EmployerDto>> GetAllEmployersAsync()
        {
            var employers = await _employerRepository.GetAllEmployersAsync();
            return _mapper.Map<IEnumerable<EmployerDto>>(employers);
        }

        // Update an existing employer
        public async Task<EmployerDto> UpdateEmployerAsync(int employerId, EmployerDto employerDTO)
        {
            var employer = _mapper.Map<Employer>(employerDTO);
            var updatedEmployer = await _employerRepository.UpdateEmployerAsync(employerId, employer);
            return updatedEmployer == null ? null : _mapper.Map<EmployerDto>(updatedEmployer);
        }

        // Delete an employer
        public async Task<bool> DeleteEmployerAsync(int employerId)
        {
            return await _employerRepository.DeleteEmployerAsync(employerId);
        }

        // Get job listings posted by an employer
        public async Task<IEnumerable<JobListingDto>> GetJobListingsByEmployerIdAsync(int employerId)
        {
            var jobListings = await _employerRepository.GetJobListingsByEmployerIdAsync(employerId);
            return _mapper.Map<IEnumerable<JobListingDto>>(jobListings);
        }
    }
}

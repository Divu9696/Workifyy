using System;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Workify.Models;
using Workify.DTOs;
using Workify.Repositories;

namespace Workify.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IMapper _mapper;

        public ApplicationService(IApplicationRepository applicationRepository, IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _mapper = mapper;
        }

        // Create a new application
        public async Task<ApplicationDto> CreateApplicationAsync(ApplicationDto applicationCreateDto)
        {
            // Map the ApplicationCreateDto to Application entity
            var application = _mapper.Map<Application>(applicationCreateDto);
            var createdApplication = await _applicationRepository.CreateApplicationAsync(application);
            
            // Map the created Application entity to ApplicationDto for response
            var applicationDto = _mapper.Map<ApplicationDto>(createdApplication);
            return applicationDto;
        }

        // Get all applications
        public async Task<IEnumerable<ApplicationDto>> GetAllApplicationsAsync()
        {
            var applications = await _applicationRepository.GetAllApplicationsAsync();
            return _mapper.Map<IEnumerable<ApplicationDto>>(applications);
        }

        // Get application by ID
        public async Task<ApplicationDto> GetApplicationByIdAsync(int applicationId)
        {
            var application = await _applicationRepository.GetApplicationByIdAsync(applicationId);
            return _mapper.Map<ApplicationDto>(application);
        }

        // Update the status of an application
        public async Task<ApplicationDto> UpdateApplicationStatusAsync(int applicationId, string status)
        {
            var updatedApplication = await _applicationRepository.UpdateApplicationStatusAsync(applicationId, status);
            return _mapper.Map<ApplicationDto>(updatedApplication);
        }

        // Delete an application
        public async Task<bool> DeleteApplicationAsync(int applicationId)
        {
            return await _applicationRepository.DeleteApplicationAsync(applicationId);
        }
    }
}


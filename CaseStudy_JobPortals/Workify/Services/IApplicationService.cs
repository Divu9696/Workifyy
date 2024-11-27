using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Workify.Models;
using Workify.DTOs;

namespace Workify.Services
{
    public interface IApplicationService
    {
        Task<ApplicationDto> CreateApplicationAsync(ApplicationDto applicationCreateDto);
        Task<IEnumerable<ApplicationDto>> GetAllApplicationsAsync();
        Task<ApplicationDto> GetApplicationByIdAsync(int applicationId);
        Task<ApplicationDto> UpdateApplicationStatusAsync(int applicationId, string status);
        Task<bool> DeleteApplicationAsync(int applicationId);
    }
}


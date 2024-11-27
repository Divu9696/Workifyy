using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Workify.Models;

namespace Workify.Repositories
{
    public interface IApplicationRepository
    {
        Task<Application> CreateApplicationAsync(Application application);
        Task<IEnumerable<Application>> GetAllApplicationsAsync();
        Task<Application> GetApplicationByIdAsync(int applicationId);
        Task<Application> UpdateApplicationStatusAsync(int applicationId, string status);
        Task<bool> DeleteApplicationAsync(int applicationId);
    }
}


using System;

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Workify.Models;

namespace Workify.Repositories
{
    public interface IResumeRepository
    {
        Task<Resume> CreateAsync(Resume resume);
        Task<Resume> GetByIdAsync(int resumeId);
        Task<IEnumerable<Resume>> GetAllAsync();
        Task<Resume> UpdateAsync(Resume resume);
        Task<bool> DeleteAsync(int resumeId);
        // Task<FileResult> DownloadResumeAsync(int resumeId);  // Method to download the resume
    }
}


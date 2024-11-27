using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Workify.DTOs;
using Workify.Models;

namespace Workify.Services
{
    public interface IResumeService
    {
        Task<ResumeDto> UploadResumeAsync(ResumeDto resumeDTO);
        Task<ResumeDto> GetResumeByIdAsync(int resumeId);
        Task<IEnumerable<ResumeDto>> GetAllResumesAsync();
        Task<ResumeDto> UpdateResumeAsync(int resumeId, ResumeDto resumeDTO);
        Task<bool> DeleteResumeAsync(int resumeId);
        Task<FileResult> DownloadResumeAsync(int resumeId);
        // Task<byte[]> DownloadResumeAsync(int resumeId);
    }
}


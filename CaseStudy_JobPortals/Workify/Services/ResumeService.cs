using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Workify.DTOs;
using Workify.Models;
using Workify.Repositories;

namespace Workify.Services
{
    public class ResumeService : IResumeService
    {
        private readonly IResumeRepository _resumeRepository;
        private readonly IMapper _mapper;

        public ResumeService(IResumeRepository resumeRepository, IMapper mapper)
        {
            _resumeRepository = resumeRepository;
            _mapper = mapper;
        }

        // Upload a new resume
        public async Task<ResumeDto> UploadResumeAsync(ResumeDto resumeDTO)
        {
            // Validate file content (e.g., check file size, file type)
            // Validate the file path
            if (string.IsNullOrEmpty(resumeDTO.FilePath))
            {
                throw new ArgumentException("The file path cannot be null or empty.");
            }

            if (!File.Exists(resumeDTO.FilePath))
            {
                throw new ArgumentException("The specified file path does not exist.");
            }

            // Map the DTO to the Resume entity
            var resume = _mapper.Map<Resume>(resumeDTO);

            // Save file to a designated location
            var fileName = Path.GetFileName(resumeDTO.FilePath);
            var destinationPath = Path.Combine("Uploads", fileName);

            Directory.CreateDirectory("Uploads");
            // Copy the file to the destination
            File.Copy(resumeDTO.FilePath, destinationPath);

            // Update the file path in the entity
            resume.FilePath = destinationPath;

            // Call the repository to save the resume
            var createdResume = await _resumeRepository.CreateAsync(resume);
            
            // Call the repository to save the resume
            // return await _resumeRepository.CreateAsync(resume);
            return _mapper.Map<ResumeDto>(createdResume);
        }

        // Get resume details by ID
        public async Task<ResumeDto> GetResumeByIdAsync(int resumeId)
        {
            var resume = await _resumeRepository.GetByIdAsync(resumeId);
            if (resume == null)
            {
                throw new KeyNotFoundException($"Resume with ID {resumeId} not found.");
                // throw new KeyNotFoundException("Resume not found.");
            }
            return _mapper.Map<ResumeDto>(resume);
        }

        // Get all resumes
        public async Task<IEnumerable<ResumeDto>> GetAllResumesAsync()
        {
            var resumes = await _resumeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ResumeDto>>(resumes);
            // return _mapper.Map<List<ResumeDto>>(resumes);
            // return await _resumeRepository.GetAllAsync();
        }

        // Update an existing resume
        public async Task<ResumeDto> UpdateResumeAsync(int resumeId, ResumeDto resumeDTO)
        {
            var resume = await _resumeRepository.GetByIdAsync(resumeId);
            if (resume == null)
            {
                throw new KeyNotFoundException("Resume not found.");
            }

            // Update resume properties
            resume.FileName = resumeDTO.FileName ?? resume.FileName;
            resume.FileType = resumeDTO.FileType ?? resume.FileType;
            // resume.FileSize = resumeDTO.FileSize ?? resume.FileSize;
            // If updating file content, save it to the server
            if (!string.IsNullOrEmpty(resumeDTO.FilePath) && File.Exists(resumeDTO.FilePath))
            {
                var fileName = Path.GetFileName(resumeDTO.FilePath);
                var destinationPath = Path.Combine("Uploads", fileName);
                Directory.CreateDirectory("Uploads");
                File.Copy(resumeDTO.FilePath, destinationPath, true);

                resume.FilePath = destinationPath;
            }
            var updatedResume = await _resumeRepository.UpdateAsync(resume);
            return _mapper.Map<ResumeDto>(updatedResume);
            // _mapper.Map(resumeDTO, resume);
            // await _resumeRepository.UpdateAsync(resume);

            // Save updated resume to repository
            // return await _resumeRepository.UpdateAsync(resume);
        }

        // Delete a resume by ID
        public async Task<bool> DeleteResumeAsync(int resumeId)
        {
            var existingResume = await _resumeRepository.GetByIdAsync(resumeId);
            if (existingResume == null)
            throw new KeyNotFoundException($"Resume with ID {resumeId} not found.");
            // throw new KeyNotFoundException("Resume not found.");

            if (File.Exists(existingResume.FilePath))
            {
                File.Delete(existingResume.FilePath);
            }

            return await _resumeRepository.DeleteAsync(resumeId);
        }

        // Download the resume by ID
        public async Task<FileResult> DownloadResumeAsync(int resumeId)
        {
            var resume = await _resumeRepository.GetByIdAsync(resumeId);
            if (resume == null)
            throw new KeyNotFoundException($"Resume with ID {resumeId} not found.");
                // throw new KeyNotFoundException("Resume not found.");

            // Ensure the file exists on disk
            if (!File.Exists(resume.FilePath))
                throw new FileNotFoundException("The resume file does not exist.");

            var fileContent = await File.ReadAllBytesAsync(resume.FilePath);

            return new FileContentResult(fileContent, resume.FileType)
            {
                FileDownloadName = resume.FileName
            };
        }
    }
}

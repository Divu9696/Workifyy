using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Workify.Services;
using Workify.DTOs;
using Workify.Data;
using Workify.Models;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

[ApiController]
[Route("api/[controller]")]
public class ResumeController : ControllerBase
{
    private readonly IResumeService _resumeService;
    private readonly IMapper _mapper;
    private readonly string _uploadsFolder = "Uploads";

    public ResumeController(IResumeService resumeService)
    {
        _resumeService = resumeService;
    }

    // POST: api/resumes
    [HttpPost]
    public async Task<IActionResult> UploadResume([FromForm] IFormFile file, [FromForm] int userId, [FromForm] string title)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), _uploadsFolder);

        // Ensure the upload folder exists
        if (!Directory.Exists(uploadsPath))
        {
            Directory.CreateDirectory(uploadsPath);
        }

        // Generate a unique file name
        var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
        var filePath = Path.Combine(uploadsPath, uniqueFileName);

        // Save the file to the server
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Create a resume entity and save it in the database
        var resume = new ResumeDto
        {
            SeekerId = userId,
            FileName = file.FileName,
            FilePath = filePath,
            LastUpdated = DateTime.UtcNow,
            
        };

        await _resumeService.UploadResumeAsync(resume);
        var resumeDto = _mapper.Map<ResumeDto>(resume);
        return CreatedAtAction(nameof(GetResumeById), new { resumeId = resumeDto.ResumeId }, resumeDto);
        // return CreatedAtAction(nameof(GetResumeById), new { resumeId = resume.Id }, resume);
    }
    // public async Task<IActionResult> UploadResume([FromForm] ResumeDto resumeDto)
    // {
    //     try
    //     {
    //         var createdResume = await _resumeService.UploadResumeAsync(resumeDto);
    //         return CreatedAtAction(nameof(GetResumeById), new { resumeId = createdResume.ResumeId }, createdResume);
    //     }
    //     catch (ArgumentException ex)
    //     {
    //         return BadRequest(new { message = ex.Message });
    //     }
    // }

    // GET: api/resumes/{resumeId}
    [HttpGet("{resumeId}")]
    public async Task<IActionResult> GetResumeById(int resumeId)
    {
        try
        {
            var resume = await _resumeService.GetResumeByIdAsync(resumeId);
            return Ok(resume);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    // GET: api/resumes
    [HttpGet]
    public async Task<IActionResult> GetAllResumes()
    {
        var resumes = await _resumeService.GetAllResumesAsync();
        return Ok(resumes);
    }

    // PUT: api/resumes/{resumeId}
    [HttpPut("{resumeId}")]
    public async Task<IActionResult> UpdateResume(int resumeId, [FromForm] ResumeDto resumeDto)
    {
        try
        {
            var updatedResume = await _resumeService.UpdateResumeAsync(resumeId, resumeDto);
            return Ok(updatedResume);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // DELETE: api/resumes/{resumeId}
    [HttpDelete("{resumeId}")]
    public async Task<IActionResult> DeleteResume(int resumeId)
    {
        try
        {
            var success = await _resumeService.DeleteResumeAsync(resumeId);
            if (success)
            {
                return NoContent();
            }
            return BadRequest(new { message = "Failed to delete the resume." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    // GET: api/resumes/{resumeId}/download
    [HttpGet("{resumeId}/download")]
    public async Task<IActionResult> DownloadResume(int resumeId)
    {
        try
        {
            var fileResult = await _resumeService.DownloadResumeAsync(resumeId);
            return fileResult;
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (FileNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}



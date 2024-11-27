using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Workify.Services;
using Workify.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Workify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]  // Apply authorization globally to ensure only authenticated users can access these endpoints
    public class JobSeekerController : ControllerBase
    {
        private readonly IJobSeekerService _jobSeekerService;

        public JobSeekerController(IJobSeekerService jobSeekerService)
        {
            _jobSeekerService = jobSeekerService;
        }

        // POST /api/jobseekers: Create a job seeker profile
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateJobSeeker([FromBody] JobSeekerDto jobSeekerCreateDTO)
        {
            if (jobSeekerCreateDTO == null)
            {
                return BadRequest("JobSeeker details are required.");
            }

            var createdJobSeeker = await _jobSeekerService.CreateJobSeekerAsync(jobSeekerCreateDTO);

            if (createdJobSeeker == null)
            {
                return StatusCode(500, "Error occurred while creating job seeker.");
            }

            return CreatedAtAction(nameof(GetJobSeekerById), new { seekerId = createdJobSeeker.SeekerId }, createdJobSeeker);
        }

        // GET /api/jobseekers/{seekerId}: Get job seeker details by ID
        [HttpGet("{seekerId}")]
        public async Task<IActionResult> GetJobSeekerById(int seekerId)
        {
            var jobSeeker = await _jobSeekerService.GetJobSeekerByIdAsync(seekerId);

            if (jobSeeker == null)
            {
                return NotFound($"Job seeker with ID {seekerId} not found.");
            }

            return Ok(jobSeeker);
        }

        // GET /api/jobseekers: List all job seekers
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllJobSeekers()
        {
            var jobSeekers = await _jobSeekerService.GetAllJobSeekersAsync();

            if (jobSeekers == null)
            {
                return NotFound("No job seekers found.");
            }

            return Ok(jobSeekers);
        }

        // PUT /api/jobseekers/{seekerId}: Update job seeker profile
        [HttpPut("{seekerId}")]
        public async Task<IActionResult> UpdateJobSeeker(int seekerId, [FromBody] JobSeekerUpdateDTO jobSeekerUpdateDTO)
        {
            if (jobSeekerUpdateDTO == null)
            {
                return BadRequest("Updated details are required.");
            }

            var updatedJobSeeker = await _jobSeekerService.UpdateJobSeekerAsync(seekerId, jobSeekerUpdateDTO);

            if (updatedJobSeeker == null)
            {
                return NotFound($"Job seeker with ID {seekerId} not found.");
            }

            return Ok(updatedJobSeeker);
        }

        // GET /api/jobseekers/{seekerId}/applications: Get all applications submitted by a job seeker
        [HttpGet("{seekerId}/applications")]
        public async Task<IActionResult> GetApplicationsByJobSeekerId(int seekerId)
        {
            var applications = await _jobSeekerService.GetApplicationsByJobSeekerIdAsync(seekerId);

            if (applications == null || applications.Count() == 0)
            {
                return NotFound($"No applications found for job seeker with ID {seekerId}.");
            }

            return Ok(applications);
        }

        // DELETE /api/jobseekers/{seekerId}: Delete a job seeker
        [HttpDelete("{seekerId}")]
        public async Task<IActionResult> DeleteJobSeeker(int seekerId)
        {
            var deleted = await _jobSeekerService.DeleteJobSeekerAsync(seekerId);

            if (!deleted)
            {
                return NotFound($"Job seeker with ID {seekerId} not found.");
            }

            return NoContent(); // 204 No Content
        }

        // GET /api/jobseekers/{seekerId}/resumes: Get all resumes uploaded by a job seeker
        [HttpGet("{seekerId}/resumes")]
        public async Task<IActionResult> GetResumesByJobSeekerId(int seekerId)
        {
            var resumes = await _jobSeekerService.GetResumesByJobSeekerIdAsync(seekerId);

            if (resumes == null || resumes.Count() == 0)
            {
                return NotFound($"No resumes found for job seeker with ID {seekerId}.");
            }

            return Ok(resumes);
        }
    }
}


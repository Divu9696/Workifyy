using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Workify.DTOs;
using Workify.Services;

namespace Workify.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JobListingController : ControllerBase
    {
        private readonly IJobListingService _jobListingService;

        public JobListingController(IJobListingService jobListingService)
        {
            _jobListingService = jobListingService;
        }

        // POST: api/joblistings
        // [Authorize(Roles = "Employer")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateJobListing([FromBody] JobListingDto jobListingCreateDTO)
        {
            if (jobListingCreateDTO == null)
            {
                return BadRequest("Job listing data is required.");
            }

            var createdJobListing = await _jobListingService.CreateJobListingAsync(jobListingCreateDTO);

            if (createdJobListing == null)
            {
                return BadRequest("Failed to create job listing.");
            }

            return CreatedAtAction(nameof(GetJobListingById), new { jobId = createdJobListing.JobId }, createdJobListing);
        }

        // GET: api/joblistings
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllJobListings()
        {
            var jobListings = await _jobListingService.GetAllJobListingsAsync();
            if (jobListings == null || jobListings.Count == 0)
            {
                return NotFound("No job listings found.");
            }

            return Ok(jobListings);
        }

        // GET: api/joblistings/{jobId}
        [HttpGet("{jobId}")]
        public async Task<IActionResult> GetJobListingById(int jobId)
        {
            var jobListing = await _jobListingService.GetJobListingByIdAsync(jobId);

            if (jobListing == null)
            {
                return NotFound($"Job listing with ID {jobId} not found.");
            }

            return Ok(jobListing);
        }

        // PUT: api/joblistings/{jobId}
        [Authorize(Roles = "Employer")]
        [HttpPut("{jobId}")]
        public async Task<IActionResult> UpdateJobListing(int jobId, [FromBody] JobListingDto jobListingUpdateDTO)
        {
            if (jobListingUpdateDTO == null)
            {
                return BadRequest("Job listing data is required.");
            }

            var updatedJobListing = await _jobListingService.UpdateJobListingAsync(jobId, jobListingUpdateDTO);

            if (updatedJobListing == null)
            {
                return NotFound($"Job listing with ID {jobId} not found.");
            }

            return Ok(updatedJobListing);
        }

        // DELETE: api/joblistings/{jobId}
        [Authorize(Roles = "Employer")]
        [HttpDelete("{jobId}")]
        public async Task<IActionResult> DeleteJobListing(int jobId)
        {
            var success = await _jobListingService.DeleteJobListingAsync(jobId);

            if (!success)
            {
                return NotFound($"Job listing with ID {jobId} not found.");
            }

            return NoContent(); // Successfully deleted
        }
    }
}


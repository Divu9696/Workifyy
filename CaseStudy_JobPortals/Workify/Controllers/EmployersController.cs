using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workify.DTOs;
using Workify.Services;

namespace Workify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployersController : ControllerBase
    {
        private readonly IEmployerService _employerService;

        public EmployersController(IEmployerService employerService)
        {
            _employerService = employerService;
        }

        // Create an employer profile
        [HttpPost]
        public async Task<IActionResult> CreateEmployer([FromBody] EmployerDto employerDTO)
        {
            var employer = await _employerService.CreateEmployerAsync(employerDTO);
            return CreatedAtAction(nameof(GetEmployerById), new { employerId = employer.EmployerId }, employer);
        }

        // Get an employer by ID
        [HttpGet("{employerId}")]
        public async Task<IActionResult> GetEmployerById(int employerId)
        {
            var employer = await _employerService.GetEmployerByIdAsync(employerId);
            if (employer == null)
            {
                return NotFound();
            }
            return Ok(employer);
        }

        // Get all employers
        [HttpGet]
        public async Task<IActionResult> GetAllEmployers()
        {
            var employers = await _employerService.GetAllEmployersAsync();
            return Ok(employers);
        }

        // Update an employer profile
        [HttpPut("{employerId}")]
        public async Task<IActionResult> UpdateEmployer(int employerId, [FromBody] EmployerDto employerDTO)
        {
            var employer = await _employerService.UpdateEmployerAsync(employerId, employerDTO);
            if (employer == null)
            {
                return NotFound();
            }
            return Ok(employer);
        }

        // Delete an employer
        [HttpDelete("{employerId}")]
        public async Task<IActionResult> DeleteEmployer(int employerId)
        {
            var result = await _employerService.DeleteEmployerAsync(employerId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        // Get job listings posted by an employer
        [HttpGet("{employerId}/joblistings")]
        public async Task<IActionResult> GetJobListingsByEmployerId(int employerId)
        {
            var jobListings = await _employerService.GetJobListingsByEmployerIdAsync(employerId);
            return Ok(jobListings);
        }
    }
}


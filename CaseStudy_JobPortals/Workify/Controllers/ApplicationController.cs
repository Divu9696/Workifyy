using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using Workify.DTOs;
using Workify.Services;

namespace Workify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        // POST: api/applications
        [HttpPost]
        // [Authorize]
        public async Task<ActionResult<ApplicationDto>> CreateApplicationAsync(ApplicationDto applicationCreateDto)
        {
            var createdApplication = await _applicationService.CreateApplicationAsync(applicationCreateDto);
            return CreatedAtAction(nameof(GetApplicationByIdAsync), new { applicationId = createdApplication.ApplicationId }, createdApplication);
        }

        // GET: api/applications
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<ApplicationDto>>> GetAllApplicationsAsync()
        {
            var applications = await _applicationService.GetAllApplicationsAsync();
            return Ok(applications);
        }

        // GET: api/applications/{applicationId}
        [HttpGet("{applicationId}")]
        [Authorize]
        public async Task<ActionResult<ApplicationDto>> GetApplicationByIdAsync(int applicationId)
        {
            var application = await _applicationService.GetApplicationByIdAsync(applicationId);
            if (application == null)
            {
                return NotFound();
            }
            return Ok(application);
        }

        // PUT: api/applications/{applicationId}
        [HttpPut("{applicationId}")]
        [Authorize(Roles = "Employer")]
        public async Task<ActionResult<ApplicationDto>> UpdateApplicationStatusAsync(int applicationId, [FromBody] string status)
        {
            var updatedApplication = await _applicationService.UpdateApplicationStatusAsync(applicationId, status);
            if (updatedApplication == null)
            {
                return NotFound();
            }
            return Ok(updatedApplication);
        }

        // DELETE: api/applications/{applicationId}
        [HttpDelete("{applicationId}")]
        [Authorize]
        public async Task<IActionResult> DeleteApplicationAsync(int applicationId)
        {
            var result = await _applicationService.DeleteApplicationAsync(applicationId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}


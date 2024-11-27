using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Workify.DTOs;
using Workify.Services;

namespace Workify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        // POST /api/companies: Create a new company
        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyDto companyDto)
        {
            if (companyDto == null)
            {
                return BadRequest("Company data is required.");
            }

            var createdCompany = await _companyService.CreateCompanyAsync(companyDto);
            if (createdCompany == null)
            {
                return BadRequest("Failed to create the company.");
            }

            return CreatedAtAction(nameof(GetCompanyById), new { companyId = createdCompany.CompanyId }, createdCompany);
        }

        // GET /api/companies: Retrieve all companies
        [HttpGet]
        public async Task<IActionResult> GetAllCompanies()
        {
            var companies = await _companyService.GetAllCompaniesAsync();
            return Ok(companies);
        }

        // GET /api/companies/{companyId}: Get company details by ID
        [HttpGet("{companyId}")]
        public async Task<IActionResult> GetCompanyById(int companyId)
        {
            var company = await _companyService.GetCompanyByIdAsync(companyId);
            if (company == null)
            {
                return NotFound($"Company with ID {companyId} not found.");
            }

            return Ok(company);
        }

        // PUT /api/companies/{companyId}: Update company details
        [HttpPut("{companyId}")]
        public async Task<IActionResult> UpdateCompany(int companyId, [FromBody] CompanyDto companyDto)
        {
            if (companyDto == null)
            {
                return BadRequest("Company data is required.");
            }

            var updatedCompany = await _companyService.UpdateCompanyAsync(companyId, companyDto);
            if (updatedCompany == null)
            {
                return NotFound($"Company with ID {companyId} not found.");
            }

            return Ok(updatedCompany);
        }

        // DELETE /api/companies/{companyId}: Deletes a company
        [HttpDelete("{companyId}")]
        public async Task<IActionResult> DeleteCompany(int companyId)
        {
            var result = await _companyService.DeleteCompanyAsync(companyId);
            if (!result)
            {
                return NotFound($"Company with ID {companyId} not found.");
            }

            return NoContent(); // Success, no content to return
        }
    }
}


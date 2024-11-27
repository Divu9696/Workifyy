using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workify.Models;
using Workify.Data;

namespace Workify.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly WorkifyDbContext _context;

        public CompanyRepository(WorkifyDbContext context)
        {
            _context = context;
        }

        // Create a new company
        public async Task<Company> CreateCompanyAsync(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return company;
        }

        // Get a company by ID
        public async Task<Company> GetCompanyByIdAsync(int companyId)
        {
            return await _context.Companies
                .FirstOrDefaultAsync(c => c.CompanyId == companyId);
        }

        // Get all companies
        public async Task<IEnumerable<Company>> GetAllCompaniesAsync()
        {
            return await _context.Companies.ToListAsync();
        }

        // Update company details
        public async Task<Company> UpdateCompanyAsync(int companyId, Company company)
        {
            var existingCompany = await _context.Companies
                .FirstOrDefaultAsync(c => c.CompanyId == companyId);

            if (existingCompany == null)
                return null;

            existingCompany.CompanyName = company.CompanyName;
            existingCompany.Address = company.Address;
            existingCompany.Description = company.Description;
            existingCompany.Website = company.Website;

            await _context.SaveChangesAsync();
            return existingCompany;
        }

        // Delete a company
        public async Task<bool> DeleteCompanyAsync(int companyId)
        {
            var company = await _context.Companies
                .FirstOrDefaultAsync(c => c.CompanyId == companyId);

            if (company == null)
                return false;

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}


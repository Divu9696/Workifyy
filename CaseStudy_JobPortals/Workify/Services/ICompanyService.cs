using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Workify.DTOs;

namespace Workify.Services
{
    public interface ICompanyService
    {
        Task<CompanyDto> CreateCompanyAsync(CompanyDto companyDto);
        Task<CompanyDto> GetCompanyByIdAsync(int companyId);
        Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync();
        Task<CompanyDto> UpdateCompanyAsync(int companyId, CompanyDto companyDto);
        Task<bool> DeleteCompanyAsync(int companyId);
    }
}


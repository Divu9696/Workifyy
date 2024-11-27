using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Workify.Models;

namespace Workify.Repositories
{
    public interface ICompanyRepository
    {
        Task<Company> CreateCompanyAsync(Company company);
        Task<Company> GetCompanyByIdAsync(int companyId);
        Task<IEnumerable<Company>> GetAllCompaniesAsync();
        Task<Company> UpdateCompanyAsync(int companyId, Company company);
        Task<bool> DeleteCompanyAsync(int companyId);
    }
}


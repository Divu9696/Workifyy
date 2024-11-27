using System;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Workify.DTOs;
using Workify.Models;
using Workify.Repositories;

namespace Workify.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        // Create a new company
        public async Task<CompanyDto> CreateCompanyAsync(CompanyDto companyDto)
        {
            var company = _mapper.Map<Company>(companyDto);
            var createdCompany = await _companyRepository.CreateCompanyAsync(company);
            return _mapper.Map<CompanyDto>(createdCompany);
        }

        // Get company by ID
        public async Task<CompanyDto> GetCompanyByIdAsync(int companyId)
        {
            var company = await _companyRepository.GetCompanyByIdAsync(companyId);
            if (company == null)
                return null;
            return _mapper.Map<CompanyDto>(company);
        }

        // Get all companies
        public async Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync()
        {
            var companies = await _companyRepository.GetAllCompaniesAsync();
            return _mapper.Map<IEnumerable<CompanyDto>>(companies);
        }

        // Update company details
        public async Task<CompanyDto> UpdateCompanyAsync(int companyId, CompanyDto companyDto)
        {
            var company = _mapper.Map<Company>(companyDto);
            var updatedCompany = await _companyRepository.UpdateCompanyAsync(companyId, company);
            if (updatedCompany == null)
                return null;
            return _mapper.Map<CompanyDto>(updatedCompany);
        }

        // Delete a company
        public async Task<bool> DeleteCompanyAsync(int companyId)
        {
            return await _companyRepository.DeleteCompanyAsync(companyId);
        }
    }
}


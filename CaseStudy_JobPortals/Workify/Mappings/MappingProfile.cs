using AutoMapper;
using Workify.Models;
using Workify.DTOs;

namespace Workify.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User
            CreateMap<User, UserDto>().ReverseMap();

            // Employer
            CreateMap<Employer, EmployerDto>().ReverseMap();

            // JobSeeker
            CreateMap<JobSeeker, JobSeekerDto>().ReverseMap();

            // Company
            CreateMap<Company, CompanyDto>().ReverseMap();

            // JobListing
            CreateMap<JobListing, JobListingDto>().ReverseMap();

            // Application
            CreateMap<Application, ApplicationDto>().ReverseMap();

            // Resume
            CreateMap<Resume, ResumeDto>().ReverseMap();

            // SearchHistory
            CreateMap<SearchHistory, SearchHistoryDto>().ReverseMap();
        }
    }
}


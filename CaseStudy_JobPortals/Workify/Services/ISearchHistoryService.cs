using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Workify.DTOs;

namespace Workify.Services
{
    public interface ISearchHistoryService
    {
        Task<IEnumerable<SearchHistoryDto>> GetSearchHistoryByUserIdAsync(int userId);
        Task<SearchHistoryDto> AddSearchHistoryAsync(SearchHistoryDto createSearchHistoryDto);
        Task<bool> DeleteSearchHistoryAsync(int searchHistoryId);
    }
}

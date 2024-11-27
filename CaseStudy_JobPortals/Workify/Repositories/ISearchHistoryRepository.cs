using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Workify.Models;

namespace Workify.Repositories
{
    public interface ISearchHistoryRepository
    {
        Task<IEnumerable<SearchHistory>> GetSearchHistoryByUserIdAsync(int userId);
        Task<SearchHistory> AddSearchHistoryAsync(SearchHistory searchHistory);
        Task<bool> DeleteSearchHistoryAsync(int searchHistoryId);
    }
}


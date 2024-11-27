using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workify.Models;
using Workify.Data;

namespace Workify.Repositories
{
    public class SearchHistoryRepository : ISearchHistoryRepository
    {
        private readonly WorkifyDbContext _context;

        public SearchHistoryRepository(WorkifyDbContext context)
        {
            _context = context;
        }

        // Get all search history entries for a specific user
        public async Task<IEnumerable<SearchHistory>> GetSearchHistoryByUserIdAsync(int userId)
        {
            return await _context.SearchHistories
                                 .Where(sh => sh.SeekerId == userId)
                                 .OrderByDescending(sh => sh.SearchDate) // Optional: order by search date
                                 .ToListAsync();
        }

        // Add a new search history entry
        public async Task<SearchHistory> AddSearchHistoryAsync(SearchHistory searchHistory)
        {
            _context.SearchHistories.Add(searchHistory);
            await _context.SaveChangesAsync();
            return searchHistory;
        }

        // Delete a search history entry by ID
        public async Task<bool> DeleteSearchHistoryAsync(int searchHistoryId)
        {
            var searchHistory = await _context.SearchHistories.FindAsync(searchHistoryId);
            if (searchHistory == null) return false;

            _context.SearchHistories.Remove(searchHistory);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}


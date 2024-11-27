using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workify.DTOs;
using Workify.Models;
using Workify.Repositories;

namespace Workify.Services
{
    public class SearchHistoryService : ISearchHistoryService
    {
        private readonly ISearchHistoryRepository _searchHistoryRepository;
        private readonly IMapper _mapper;

        public SearchHistoryService(ISearchHistoryRepository searchHistoryRepository, IMapper mapper)
        {
            _searchHistoryRepository = searchHistoryRepository;
            _mapper = mapper;
        }

        // Get all search history entries for a user
        public async Task<IEnumerable<SearchHistoryDto>> GetSearchHistoryByUserIdAsync(int userId)
        {
            var searchHistories = await _searchHistoryRepository.GetSearchHistoryByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<SearchHistoryDto>>(searchHistories);
        }

        // Add a new search history entry
        public async Task<SearchHistoryDto> AddSearchHistoryAsync(SearchHistoryDto createSearchHistoryDto)
        {
            // Map DTO to Entity
            var searchHistoryEntity = _mapper.Map<SearchHistory>(createSearchHistoryDto);

            // Add search history entry
            var createdSearchHistory = await _searchHistoryRepository.AddSearchHistoryAsync(searchHistoryEntity);

            // Map Entity back to DTO
            return _mapper.Map<SearchHistoryDto>(createdSearchHistory);
        }

        // Delete a search history entry by ID
        public async Task<bool> DeleteSearchHistoryAsync(int searchHistoryId)
        {
            return await _searchHistoryRepository.DeleteSearchHistoryAsync(searchHistoryId);
        }
    }
}


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Workify.DTOs;
using Workify.Services;

namespace Workify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchHistoryController : ControllerBase
    {
        private readonly ISearchHistoryService _searchHistoryService;

        public SearchHistoryController(ISearchHistoryService searchHistoryService)
        {
            _searchHistoryService = searchHistoryService;
        }

        /// <summary>
        /// Retrieve all search history entries for a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>List of search history entries.</returns>
        [HttpGet]
        [AllowAnonymous]
        // [Authorize]
        public async Task<ActionResult<IEnumerable<SearchHistoryDto>>> GetSearchHistoryByUserId([FromQuery] int userId)
        {
            var searchHistory = await _searchHistoryService.GetSearchHistoryByUserIdAsync(userId);

            if (searchHistory == null)
                return NotFound("No search history found for the specified user.");

            return Ok(searchHistory);
        }

        /// <summary>
        /// Add a new search history entry.
        /// </summary>
        /// <param name="createSearchHistoryDto">The details of the search history entry.</param>
        /// <returns>The created search history entry.</returns>
        [HttpPost]
        [AllowAnonymous]
        // [Authorize]
        public async Task<ActionResult<SearchHistoryDto>> AddSearchHistory([FromBody] SearchHistoryDto createSearchHistoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdSearchHistory = await _searchHistoryService.AddSearchHistoryAsync(createSearchHistoryDto);

            return CreatedAtAction(nameof(GetSearchHistoryByUserId), new { userId = createSearchHistoryDto.SeekerId }, createdSearchHistory);
        }

        /// <summary>
        /// Delete a search history entry by ID.
        /// </summary>
        /// <param name="searchId">The ID of the search history entry.</param>
        /// <returns>Status of the operation.</returns>
        [HttpDelete("{searchId}")]
        [Authorize]
        public async Task<IActionResult> DeleteSearchHistory(int searchId)
        {
            var result = await _searchHistoryService.DeleteSearchHistoryAsync(searchId);

            if (!result)
                return NotFound($"Search history with ID {searchId} not found.");

            return NoContent();
        }
    }
}

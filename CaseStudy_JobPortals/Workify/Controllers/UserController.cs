using Microsoft.AspNetCore.Mvc;
using Workify.DTOs;
using Workify.Services;
using Microsoft.AspNetCore.Authorization;
using Workify.Utilities;

namespace Workify.Controllers
{
    
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // POST /api/users/register
        // [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> RegisterUser([FromBody] UserDto userCreateDto)
        {
            try
            {
                var user = await _userService.RegisterUserAsync(userCreateDto);
                return CreatedAtAction(nameof(GetUserById), new { userId = user.UserId }, user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST /api/users/login
        [HttpPost("login")]
        public async Task<ActionResult<string>> AuthenticateUser([FromBody] UserLoginDto userLoginDto)
        {
            try
            {
                var token = await _userService.AuthenticateUserAsync(userLoginDto);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        // GET /api/users/{userId}
        [Authorize(Roles = "Employer")]
        [HttpGet("{userId}")]
        // [AllowAnonymous]
        
        public async Task<ActionResult<UserDto>> GetUserById(int userId)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(userId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT /api/users/{userId}
        [HttpPut("{userId}")]
        [Authorize]
        public async Task<ActionResult> UpdateUser(int userId, [FromBody] UserUpdateDto userUpdateDto)
        {
            try
            {
                var result = await _userService.UpdateUserAsync(userId, userUpdateDto);
                if (result)
                {
                    return NoContent(); // HTTP 204 No Content for successful update
                }
                return NotFound("User not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE /api/users/{userId}
        [HttpDelete("{userId}")]
        [Authorize]
        public async Task<ActionResult> DeleteUser(int userId)
        {
            try
            {
                var result = await _userService.DeleteUserAsync(userId);
                if (result)
                {
                    return NoContent(); // HTTP 204 No Content for successful deletion
                }
                return NotFound("User not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}


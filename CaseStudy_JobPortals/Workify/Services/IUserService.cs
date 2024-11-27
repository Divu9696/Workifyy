using System;
using Workify.Models;
using Workify.DTOs;

namespace Workify.Services
{
    public interface IUserService
    {
        Task<UserDto> RegisterUserAsync(UserDto userCreateDto);
        Task<string> AuthenticateUserAsync(UserLoginDto userLoginDto);
        Task<UserDto> GetUserByIdAsync(int userId);
        Task<bool> UpdateUserAsync(int userId, UserUpdateDto userUpdateDto);
        Task<bool> DeleteUserAsync(int userId);
    }
}


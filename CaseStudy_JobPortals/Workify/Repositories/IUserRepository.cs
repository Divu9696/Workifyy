using System;
using System.Collections.Generic;
using Workify.Models;

// using Workify.Models;

namespace Workify.Repositories
{
    public interface IUserRepository 
    {
        Task<User> RegisterUserAsync(User user);
        Task<User> AuthenticateUserAsync(string email, string password);
        Task<User> GetUserByIdAsync(int userId);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int userId);
        Task<bool> IsEmailUniqueAsync(string email);
    }
}


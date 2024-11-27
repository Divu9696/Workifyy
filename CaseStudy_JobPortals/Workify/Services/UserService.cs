using System;

using AutoMapper;
using Workify.DTOs;
using Workify.Models;
using Workify.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Workify.Utilities;

namespace Workify.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        // private readonly JwtTokenGenerator _utilities;

        public UserService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
            // _utilities = utilities;
        }

        public async Task<UserDto> RegisterUserAsync(UserDto userCreateDto)
        {
            if (!await _userRepository.IsEmailUniqueAsync(userCreateDto.Email))
                throw new Exception("Email already exists.");

            var user = _mapper.Map<User>(userCreateDto);
            var registeredUser = await _userRepository.RegisterUserAsync(user);

            return _mapper.Map<UserDto>(registeredUser);
        }

        public async Task<string> AuthenticateUserAsync(UserLoginDto userLoginDto)
        {
            var user = await _userRepository.AuthenticateUserAsync(userLoginDto.Email, userLoginDto.Password);

            if (user == null)
                throw new Exception("Invalid email or password.");

            
            // return _utilities.GenerateToken();
            // Generate JWT token
            // var jwtTokenGenerator = new JwtTokenGenerator();
            // string token = jwtTokenGenerator.GenerateToken(user.UserId.ToString(), user.Role.ToString());
            // return token;
            return JwtTokenGenerator.GenerateToken(user.UserId, user.Role, _configuration);
        }

        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
                throw new Exception("User not found.");

            return _mapper.Map<UserDto>(user);
        }

        public async Task<bool> UpdateUserAsync(int userId, UserUpdateDto userUpdateDto)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(userId);
            if (existingUser == null)
                throw new Exception("User not found.");

            _mapper.Map(userUpdateDto, existingUser);
            return await _userRepository.UpdateUserAsync(existingUser);
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            return await _userRepository.DeleteUserAsync(userId);
        }

        // private string GenerateJwtToken(User user)
        // {
        //     var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
        //     var issuer = _configuration["Jwt:Issuer"];
        //     var audience = _configuration["Jwt:Audience"];

        //     var claims = new[]
        //     {
        //         new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        //         new Claim(ClaimTypes.Role, user.Role),
        //         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //     };

        //     var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
        //     var tokenDescriptor = new JwtSecurityToken(issuer, audience, claims, expires: DateTime.UtcNow.AddHours(1), signingCredentials: credentials);

        //     return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        // }
    }
}


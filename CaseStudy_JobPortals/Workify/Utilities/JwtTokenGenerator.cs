using System;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Workify.Utilities;

public static class JwtTokenGenerator
{
    // private readonly IConfiguration _configuration;
    public static string GenerateToken(int userId, string role, IConfiguration configuration)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        // var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = Encoding.ASCII.GetBytes(configuration["JwtSettings:SecretKey"]);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userId.ToString()),
            new Claim(ClaimTypes.Role, role)
        };
        var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature
                )
            };
        // var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // var token = new JwtSecurityToken(
        //     issuer: jwtSettings["Issuer"],
        //     audience: jwtSettings["Audience"],
        //     claims: claims,
        //     expires: DateTime.UtcNow.AddHours(1),
        //     signingCredentials: credentials
        // );

        // return new JwtSecurityTokenHandler().WriteToken(token);
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}


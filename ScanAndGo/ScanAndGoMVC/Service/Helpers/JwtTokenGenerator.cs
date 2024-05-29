using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.Helpers
{
    public static class JwtTokenGenerator
    {
        public static string GenerateNewJsonWebToken(User user, IConfiguration configuration)
        {
            var claims = new List<Claim>
        {
            new("Id", user.Id.ToString()),
            new(ClaimTypes.Name, user.Email),
            new(ClaimTypes.Role, user.UserType.ToString()),
        };

            var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            var tokenObject = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(1),
                claims: claims,
                signingCredentials: new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenObject);
        }
    }
   
}

using System;
using EcommerceLite.Models.Domain;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace EcommerceLite.Models.Repositories
{
    public class TokenHandlerRepository : ITokenHandlerRepository
    {
        private readonly IConfiguration configuration;
        public TokenHandlerRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Task<string> CreateTokenAsync(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            //Create claims
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.GivenName, user.EmailAddress));

           

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
                );

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));

        }
    }
}

using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Users;
using Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    internal sealed class JwtProvider(
        IOptions<JwtOptions> options) : IJwtProvider
    {
        public Task<string> CreateTokenAsync(AppUser user, CancellationToken cancellationToken = default)
        {
            List<Claim> claims = new()
            {
                new Claim("user-id",user.Id.ToString()),
            };

            var expires = DateTime.Now.AddMinutes(30);

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(options.Value.SecretKey));
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha512);

            JwtSecurityToken securityToken = new(
                issuer : options.Value.Issuer,
                audience : options.Value.Audience,
                claims : claims,
                notBefore : DateTime.Now,
                expires : expires,
                signingCredentials : signingCredentials);

            JwtSecurityTokenHandler handler = new();

            string token = handler.WriteToken(securityToken);

            return Task.FromResult(token);
        }
    }
}

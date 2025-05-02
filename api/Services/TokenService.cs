using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace api.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey symmetricKey;
        private readonly JwtSettings _jwtSetting;

        public TokenService(IOptions<JwtSettings> jwtOptions)
        {
            _jwtSetting = jwtOptions.Value;
            symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Key ?? throw new ArgumentNullException("Key is null")));
        }

        public async Task<string> CreateTokenAsync(Client client)
        {
            // Basic Claims
            List<Claim> claims =
            [
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Unique identifier for the token
                new Claim(JwtRegisteredClaimNames.Email, client.Email ?? string.Empty), // Email
                new Claim("uid", client.Id.ToString()) // Custom claim (user ID)
            ];

            // Role claims
            // Get User Role for Database
            claims.Add(new Claim(ClaimTypes.Role, "User"));

            // Signing credentials
            var creds = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha512Signature);

            // Token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSetting.ExpireTime)),
                SigningCredentials = creds,
                Issuer = _jwtSetting.Issuer,
                Audience = _jwtSetting.Audience
            };

            // Convert tokenDescriptor to token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // convert token to string and return it
            return tokenHandler.WriteToken(token);
        }
    }
}
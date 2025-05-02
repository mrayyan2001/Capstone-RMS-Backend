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
using Microsoft.IdentityModel.Tokens;

namespace api.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey symmetricKey;
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;

            symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"] ?? throw new ArgumentNullException("Key is null")));
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
                Expires = DateTime.Now.AddMinutes(Convert.ToDouble(_config["JWT:ExpireTime"])),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };

            // Convert tokenDescriptor to token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // convert token to string and return it
            return tokenHandler.WriteToken(token);
        }
    }
}
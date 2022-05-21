using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PlaceAgregator.API.Services.Interfaces;
using PlaceAgregator.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using System.Text;

namespace PlaceAgregator.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly string _jwtSecret;
        private readonly int _jwtLifespan;
        private readonly string _jwtAudience;
        private readonly string _jwtIssuer;

        public AuthService(string jwtSecret, int jwtLifespan, string jwtAudience, string jwtIssuer)
        {
            _jwtSecret = jwtSecret;
            _jwtLifespan = jwtLifespan;
            _jwtAudience = jwtAudience;
            _jwtIssuer = jwtIssuer;
        }
        public string GetToken(int id, ClaimsIdentity claimsIdentity)
        {
            var expirationTime = DateTime.UtcNow.AddSeconds(_jwtLifespan);
            var jwt = new JwtSecurityToken(
                claims: claimsIdentity.Claims,
                notBefore: DateTime.UtcNow,
                expires: expirationTime,
                issuer: _jwtIssuer,
                audience: _jwtAudience,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret)),
                    SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}
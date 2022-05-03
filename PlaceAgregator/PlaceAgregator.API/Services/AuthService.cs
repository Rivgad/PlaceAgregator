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
        private IPasswordHasher<Account> _passwordHasher;
        private readonly string _jwtSecret;
        private readonly int _jwtLifespan;
        private readonly string _jwtAudience;
        private readonly string _jwtIssuer;

        public AuthService(string jwtSecret, int jwtLifespan)
        {
            _jwtSecret = jwtSecret;
            _jwtLifespan = jwtLifespan;
            _passwordHasher = new PasswordHasher<Account>(Options.Create(new PasswordHasherOptions()
            {
                CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV3,
            }));
        }
        public string GetToken(int id, ClaimsIdentity claimsIdentity)
        {
            var expirationTime = DateTime.UtcNow.AddSeconds(_jwtLifespan);
            var jwt = new JwtSecurityToken(
                claims:claimsIdentity.Claims,
                expires: expirationTime,
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret)), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public string HashPassword(Account account,string password)
        {
            return _passwordHasher.HashPassword(account, password);
        }

        public bool VerifyPassword(Account account, string actualPassword, string hashedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(account, hashedPassword, actualPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
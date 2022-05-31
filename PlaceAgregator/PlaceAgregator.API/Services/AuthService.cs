using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PlaceAgregator.API.Services.Interfaces;
using PlaceAgregator.Shared.DTOs;
using PlaceAgregator.Shared.DTOs.Authentication;
using PlaceAgregator.Shared.Extensions;
using PlaceAgregator.Shared.Models;
using PlaceAgregator.Shared.Models.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PlaceAgregator.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly string _jwtSecret;
        private readonly int _jwtLifespan;
        private readonly string _jwtAudience;
        private readonly string _jwtIssuer;
        private readonly UserManager<AppUser> _userManager;

        public AuthService(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _jwtSecret = configuration["JWT:Secret"];
            _jwtLifespan = configuration.GetValue<int>("JWT:Lifespan");
            _jwtAudience = configuration["JWT:Audience"];
            _jwtIssuer = configuration["JWT:Issuer"];
        }
        private string GetToken(IEnumerable<Claim> claims)
        {
            var expirationTime = DateTime.UtcNow.AddSeconds(_jwtLifespan);
            var jwt = new JwtSecurityToken(
                claims: claims,
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

        public async Task<Response<LoginResponse>> LoginAsync(string userNameOrEmail, string password)
        {
            var user = await _userManager.FindByNameAsync(userNameOrEmail);

            if (user == null)
                user = await _userManager.FindByEmailAsync(userNameOrEmail);

            if (user == null)
                return new Response<LoginResponse>()
                {
                    Succeeded = false,
                    Errors = new[]
                    {
                        new ResponseError(LoginErrors.UserNotFound, $"Пользователь с не найден")
                    }
                };

            return await LoginByUserAsync(user, password);
        }

        private async Task<Response<LoginResponse>> LoginByUserAsync(AppUser user, string password)
        {
            if (await _userManager.CheckPasswordAsync(user, password))
            {
                var claims = await _userManager.GetClaimsAsync(user);
                var roles = await _userManager.GetRolesAsync(user);
                claims = claims
                    .Concat(new Claim[]
                    {
                        new(ClaimTypes.Email, user.Email),
                        new(ClaimTypes.NameIdentifier, user.UserName),
                        new(ClaimTypes.Sid, user.Id)
                    })
                    .Concat(roles.Select(item => new Claim(ClaimTypes.Role, item)))
                    .ToList();

                var token = GetToken(claims);

                return new Response<LoginResponse>()
                {
                    Message = new LoginResponse(
                        userName: user.UserName,
                        accessToken: token,
                        roles: roles.Select(item => item.ToLower()).ToArray()),
                        Succeeded = true
                };
            }
            else
            {
                return new Response<LoginResponse>()
                {
                    Succeeded = false,
                    Errors = new ResponseError[] { new(LoginErrors.IncorrectPassword, $"Пароль неверный") }
                };
            }
        }

        /// <summary>
        /// Registrate new user with <see cref="Role.User"/>
        /// </summary>
        /// <param name="email"><see cref="IdentityUser{TKey}.Email"/></param>
        /// <param name="userName"><see cref="IdentityUser{TKey}.UserName"/></param>
        /// <param name="password"></param>
        /// <returns><see cref="Response"/> with message about operation</returns>
        public async Task<Response> RegistrationAsync(string email, string userName, string password)
        {
            AppUser newUser = new AppUser()
            {
                Email = email,
                NormalizedEmail = email.Normalize(),
                UserName = userName,
                NormalizedUserName = userName.Normalize()
            };

            IdentityResult result = await _userManager.CreateAsync(newUser, password);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(userName);
                await _userManager.AddToRoleAsync(user, Role.User.GetDescriptionAttribute());

                return new Response()
                {
                    Succeeded = true,
                    Message = "Новый пользователь успешно зарегистрирован"
                };
            }
            else
            {
                return new Response()
                {
                    Succeeded = false,
                    Errors = result.Errors
                        .Select(item => new ResponseError(item.Code, item.Description))
                        .ToArray()
                };
            }
        }
    }
}
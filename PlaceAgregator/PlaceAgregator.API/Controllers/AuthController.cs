using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PlaceAgregator.API.Services.Interfaces;
using PlaceAgregator.Entities;
using PlaceAgregator.EntityFramework;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PlaceAgregator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("[action]")]
        public IActionResult Login(string login, string password)
        {
            using var context = new ApplicationContext();
            var person = context.Accounts.FirstOrDefault(x => x.Login.ToLower() == login.ToLower());
            
            if (person == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }
            if (person.PasswordHash != password)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }


            var encodedJwt = _authService.GetToken(
                person.Id,
                new ClaimsIdentity(new List<Claim>()
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role.ToString())
                },
                "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType)
            );

            var response = new
            {
                access_token = encodedJwt,
                username = person.Login,
                role = person.Role.ToString()
            };

            return new JsonResult(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RegistrationAsync(string login, string password)
        {
            using var context = new ApplicationContext();

            var person = context.Accounts.FirstOrDefault(x => x.Login.ToLower() == login.ToLower());
            if (person != null)
            {
                return BadRequest(new { errorText = "User with this email already exists." });
            }

            var newAccount = new Account(login, password, Role.User);
            newAccount.User = new User();
            await context.Accounts.AddAsync(newAccount);
            await context.SaveChangesAsync();

            return new OkResult();
        }
    }
}

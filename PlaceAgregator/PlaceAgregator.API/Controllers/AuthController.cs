using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlaceAgregator.API.Services.Interfaces;
using PlaceAgregator.Shared.Models;

namespace PlaceAgregator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAuthService _authService;
        public AuthController(UserManager<AppUser> userManager, IAuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
        }

        [HttpGet("[Action]")]
        public async Task<IActionResult> LoginAsync(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName) ?? await _userManager.FindByEmailAsync(userName);
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
            if (!isPasswordValid)
            {
                return BadRequest();
            }
            var claims = await _userManager.GetClaimsAsync(user);
            var token = _authService.GetToken(claims);

            return Ok(new { token = token });
        }

        [HttpPost("Action")]
        public async Task<IActionResult> Registration(string userName, string email, string password)
        {
            var newUser = new AppUser() { Email = email, UserName=userName };
            var result = await _userManager.CreateAsync(newUser, password);
            if(result.Succeeded)
                return Ok(result);

            return BadRequest(result.Errors);
        }
    }
}

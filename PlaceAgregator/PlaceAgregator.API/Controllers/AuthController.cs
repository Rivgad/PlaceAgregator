using Microsoft.AspNetCore.Mvc;
using PlaceAgregator.API.Services.Interfaces;
using PlaceAgregator.Shared.DTOs;
using PlaceAgregator.Shared.DTOs.Authentication;

namespace PlaceAgregator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("[Action]")]
        [Produces(typeof(Response<LoginResponse>))]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest loginRequest)
        {
            var userNameOrEmail = loginRequest.UserNameOrEmail;
            var password = loginRequest.Password;

            if (string.IsNullOrEmpty(userNameOrEmail) || string.IsNullOrEmpty(password))
                return BadRequest();

            Response<LoginResponse> result = await _authService.LoginAsync(userNameOrEmail, loginRequest.Password);

            if (result.Succeeded)
                return Ok(result.Message);
            else
                return BadRequest(result);
        }

        [HttpPost("[Action]")]
        [Produces(typeof(Response))]
        public async Task<IActionResult> Registration([FromBody] RegistrationRequest request)
        {
            var result = await _authService.RegistrationAsync(
                email: request.Email,
                userName: request.UserName,
                password: request.Password);

            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result);
        }
    }
}

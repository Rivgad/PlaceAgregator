using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlaceAgregator.Shared.DTOs.Authentication;
using PlaceAgregator.Shared.DTOs.Users;
using PlaceAgregator.Shared.Models;
using System.Security.Claims;

namespace PlaceAgregator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public ProfileController(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserInfo()
        {
            string? accountId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (accountId == null)
                return Forbid();

            var user = await _userManager.FindByIdAsync(accountId);
            if(user == null)
                return NotFound();

            return Ok(_mapper.Map<AppUserGetDTO>(user));
        }

        [Authorize(Roles = "user, admin")]
        [HttpPost]
        public async Task<IActionResult> UpdateUserInfo(AppUserUpdateDTO appUser)
        {
            string? accountId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (accountId == null)
                return Forbid();

            var user = await _userManager.FindByIdAsync(accountId);
            if (user == null)
                return NotFound();

            user.UserName = appUser.UserName;
            user.Email = appUser.Email;
            user.FirstName = appUser.FirstName;
            user.LastName = appUser.LastName;
            user.Patronimyc = appUser.Patronimyc;

            await _userManager.UpdateAsync(user);
            await _userManager.UpdateNormalizedEmailAsync(user);
            await _userManager.UpdateNormalizedUserNameAsync(user);

            user = await _userManager.FindByIdAsync(accountId);

            return Ok(_mapper.Map<AppUserGetDTO>(user));
        }

        [Authorize]
        [HttpPost("[Action]")]
        public async Task<IActionResult> ChangePassword(PasswordUpdateDTO request)
        {
            string? accountId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (accountId == null)
                return Forbid();

            var user = await _userManager.FindByIdAsync(accountId);
            if (user == null)
                return NotFound();

            var result = await _userManager.ChangePasswordAsync(user, request.Password, request.NewPassword);
            if(result.Succeeded)
                return Ok();
            else
                return BadRequest(result.Errors);
        }
    }
}

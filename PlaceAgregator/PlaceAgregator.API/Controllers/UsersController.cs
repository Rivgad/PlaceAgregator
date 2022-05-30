using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlaceAgregator.Shared.DTOs;
using PlaceAgregator.Shared.DTOs.Authentication;
using PlaceAgregator.Shared.DTOs.Users;
using PlaceAgregator.Shared.Extensions;
using PlaceAgregator.Shared.Models;
using PlaceAgregator.Shared.Models.Enums;

namespace PlaceAgregator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UsersController(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IEnumerable<AppUserGetDTO>> GetAll([FromQuery] UserFilterDTO filter)
        {
            var query = _userManager.Users.AsQueryable();

            if (filter.OrderBy != null)
            {
                query = query.OrderBy(filter.OrderBy, filter.Desc ?? true);
            }

            if (!string.IsNullOrEmpty(filter.Email))
                query = query.Where(item => item.Email.ToLower().Contains(filter.Email.ToLower()));

            if (!string.IsNullOrEmpty(filter.UserId))
                query = query.Where(item => item.Id.ToLower().Contains(filter.UserId.ToLower()));

            if (!string.IsNullOrEmpty(filter.UserName))
                query = query.Where(item => item.UserName.ToLower().Contains(filter.UserName.ToLower()));

            if (filter.Page != null && filter.PageSize != null)
                query = query.Skip((int)((filter.Page - 1) * filter.PageSize)).Take((int)filter.PageSize);

            return await query.Select(item => _mapper.Map<AppUserGetDTO>(item)).ToListAsync();
        }

        [Authorize(Roles = "admin")]
        [HttpPost("[Action]")]
        [Produces(typeof(AppUserGetDTO))]
        public async Task<IActionResult> CreateModerator(RegistrationRequest request)
        {
            AppUser newModerator = new AppUser()
            {
                Email = request.Email,
                UserName = request.UserName
            };
            var result = await _userManager.CreateAsync(newModerator, request.Password);
            if (result.Succeeded)
            {
                newModerator = await _userManager.FindByNameAsync(request.UserName);
                await _userManager.AddToRoleAsync(newModerator, Role.Manager.GetDescriptionAttribute());

                return CreatedAtAction(nameof(CreateModerator), _mapper.Map<AppUserGetDTO>(newModerator));
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost("[Action]/{userId}")]
        [Produces(typeof(AppUserGetDTO))]
        public async Task<IActionResult> UpdateModerator(string userId, [FromBody] AppUserUpdateDTO appUser)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            if (!await _userManager.IsInRoleAsync(user, Role.Manager.GetDescriptionAttribute()))
                return BadRequest($"Пользователь не имеет роль {Role.Manager.GetDescriptionAttribute()}");

            user.UserName = appUser.UserName;
            user.Email = appUser.Email;
            user.FirstName = appUser.FirstName;
            user.LastName = appUser.LastName;
            user.Patronimyc = appUser.Patronimyc;

            await _userManager.UpdateAsync(user);
            await _userManager.UpdateNormalizedEmailAsync(user);
            await _userManager.UpdateNormalizedUserNameAsync(user);
            user = await _userManager.FindByIdAsync(userId);

            return Ok(_mapper.Map<AppUserGetDTO>(user));
        }

        [Authorize(Roles = "admin")]
        [HttpPost("[Action]/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            await _userManager.DeleteAsync(user);

            return Ok(new { id = userId });
        }
    }
}

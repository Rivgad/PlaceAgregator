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
using System.Security.Claims;

namespace PlaceAgregator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UsersController(UserManager<AppUser> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        [Authorize(Roles = RoleConstants.Admin)]
        [HttpGet]
        public async Task<IEnumerable<AppUserGetDTO>> GetAll([FromQuery] UserFilterDTO filter)
        {
            var query = _userManager.Users.AsQueryable();

            if (filter.OrderBy != null)
            {
                query = query.OrderBy(filter.OrderBy, filter.Desc ?? true);
            }

            if(filter.Role != null)
            {
                Role role;
                if (Enum.TryParse(filter.Role, true, out role))
                {
                    var roleId = (await _roleManager.FindByNameAsync(role.GetDescriptionAttribute())).Id;

                    query = query.Where(item => item.Roles.Any(r => r.RoleId == roleId));
                }
            }

            if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(item =>
                item.Email.ToLower().Contains(filter.Search.ToLower()) ||
                item.FirstName.ToLower().Contains(filter.Search.ToLower()) ||
                item.LastName.ToLower().Contains(filter.Search.ToLower()) ||
                item.Patronimyc.ToLower().Contains(filter.Search.ToLower()) ||
                item.UserName.ToLower().Contains(filter.Search.ToLower()));

            if (filter.Page != null && filter.PageSize != null)
                query = query.Skip((int)((filter.Page - 1) * filter.PageSize)).Take((int)filter.PageSize);

            return await query.Select(item => _mapper.Map<AppUserGetDTO>(item)).ToListAsync();
        }

        [Authorize(Roles = RoleConstants.Admin)]
        [HttpDelete("{id}")]
        [Produces(typeof(EntityDTO))]
        public async Task<IActionResult> DeleteUser(string id)
        {
            string? accountId = User.FindFirst(ClaimTypes.Sid)?.Value;

            if (id == accountId)
                return BadRequest("Вы не можете удалить себя");

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
                return Ok(new { id = id });

            return BadRequest(result.Errors);
        }

        [Authorize(Roles = RoleConstants.Admin)]
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
                await _userManager.AddToRoleAsync(newModerator, Role.Moderator.GetDescriptionAttribute());

                return CreatedAtAction(nameof(CreateModerator), _mapper.Map<AppUserGetDTO>(newModerator));
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [Authorize(Roles = RoleConstants.Admin)]
        [HttpPost("[Action]/{userId}")]
        [Produces(typeof(AppUserGetDTO))]
        public async Task<IActionResult> UpdateModerator(string userId, [FromBody] AppUserUpdateDTO appUser)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            if (!await _userManager.IsInRoleAsync(user, RoleConstants.Moderator))
                return BadRequest($"Пользователь не имеет роль {RoleConstants.Moderator}");

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
    }
}

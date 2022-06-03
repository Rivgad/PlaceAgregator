using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlaceAgregator.EntityFramework;
using PlaceAgregator.Shared.DTOs.Comments;
using PlaceAgregator.Shared.Models;
using System.ComponentModel.DataAnnotations;
using PlaceAgregator.Shared.Extensions;
using System.Security.Claims;
using PlaceAgregator.Shared.Models.Enums;

namespace PlaceAgregator.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CommentsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("Places/{placeId:int}/[controller]")]
        public async Task<IEnumerable<CommentGetDTO>> GetAllByPlace(int placeId)
        {
            var comments = await _context.Comments
                .Where(item => item.IsBlocked == false)
                .Where(item => item.PlaceId == placeId)
                .Include(item => item.User)
                .ToListAsync();

            var result = comments.Select(item => _mapper.Map<CommentGetDTO>(item));

            return result;
        }

        [Authorize(Roles = RoleConstants.User)]
        [HttpPost("Places/{placeId:int}/[controller]")]
        [Produces(typeof(CommentGetDTO))]
        public async Task<IActionResult> CreateOrUpdate(int placeId, [FromBody] CommentDTO comment)
        {
            string? accountId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (accountId == null)
                return Forbid();

            var place = await _context.Places.FirstOrDefaultAsync(item => item.Id == placeId && item.IsBlocked == false && item.IsActive == true);
            if (place == null)
                return NotFound();

            var existedComment = await _context.Comments.FirstOrDefaultAsync(item => item.PlaceId == placeId && item.UserId == accountId);

            if (existedComment == null)
            {
                existedComment = _mapper.Map<Comment>(comment);
                existedComment.UserId = accountId;
                existedComment.PlaceId = placeId;
                existedComment.LastEditTime = DateTime.UtcNow;
                existedComment.IsBlocked = false;

                existedComment = (await _context.AddAsync(existedComment)).Entity;
            }
            else
            {
                if (existedComment.IsBlocked == true)
                    return BadRequest();

                existedComment.Text = comment.Text;
                existedComment.Rating = comment.Rating;
                existedComment.LastEditTime = DateTime.UtcNow;

                existedComment = _context.Update(existedComment).Entity;
            }
            await _context.SaveChangesAsync();

            existedComment = await _context.Comments
                .Include(item => item.User)
                .FirstOrDefaultAsync(item => item.PlaceId == placeId && item.UserId == accountId);

            return Ok(_mapper.Map<CommentGetDTO>(existedComment));
        }

        [Authorize(Roles = RoleConstants.User)]
        [HttpDelete("Places/{placeId:int}/[controller]")]
        public async Task<IActionResult> Delete(int placeId)
        {
            string? accountId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (accountId == null)
                return Forbid();

            var place = await _context.Places.FirstOrDefaultAsync(item => item.Id == placeId && item.IsBlocked == false && item.IsActive == true);
            if (place == null)
                return NotFound();

            var existedComment = await _context.Comments.FirstOrDefaultAsync(item => item.PlaceId == placeId && item.UserId == accountId);
            if (existedComment == null)
                return NotFound();

            if (existedComment.IsBlocked == true)
                return BadRequest();

            _context.Comments.Remove(existedComment);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [Authorize(Roles = RoleConstants.Moderator)]
        [HttpGet("[controller]")]
        public async Task<IEnumerable<CommentGetDTO>> GetAll([FromQuery] CommentFilterDTO filter)
        {
            var query = _context.Comments
               .Include(item => item.User)
               .AsQueryable();

            if (filter.IsBlocked != null)
                query = query.Where(item => item.IsBlocked == filter.IsBlocked);

            if (!string.IsNullOrEmpty(filter.UserName))
                query = query.Where(item => item.User.UserName.ToLower().Contains(filter.UserName.ToLower()));

            if (!string.IsNullOrEmpty(filter.UserId))
                query = query.Where(item => item.User.Id == filter.UserId);

            if (filter.PlaceId != null)
                query = query.Where(item => item.PlaceId == filter.PlaceId);

            if (filter.OrderBy != null)
            {
                query = query.OrderBy(filter.OrderBy, filter.Desc ?? true);
            }

            if (filter.Page != null && filter.PageSize != null)
                query = query.Skip((int)((filter.Page - 1) * filter.PageSize)).Take((int)filter.PageSize);

            var result = await query.Select(item => _mapper.Map<CommentGetDTO>(item)).ToListAsync();

            return result;
        }

        [Authorize(Roles = RoleConstants.Moderator)]
        [HttpPost("Places/{placeId:int}/[controller]/{userId}/[Action]")]
        [Produces(typeof(CommentGetDTO))]
        public async Task<IActionResult> Block(int placeId, string userId)
        {
            var comment = await _context.Comments
                .Include(item => item.User)
                .FirstOrDefaultAsync(item => item.UserId == userId && item.PlaceId == placeId);
            if (comment == null)
                return NotFound();

            comment.IsBlocked = true;
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<CommentGetDTO>(comment));
        }

        [Authorize(Roles = RoleConstants.Moderator)]
        [HttpPost("Places/{placeId:int}/[controller]/{userId}/[Action]")]
        [Produces(typeof(CommentGetDTO))]
        public async Task<IActionResult> Unblock(int placeId, string userId)
        {
            var comment = await _context.Comments
                .Include(item => item.User)
                .FirstOrDefaultAsync(item => item.UserId == userId && item.PlaceId == placeId);
            if (comment == null)
                return NotFound();

            comment.IsBlocked = false;
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<CommentGetDTO>(comment));
        }
    }
}

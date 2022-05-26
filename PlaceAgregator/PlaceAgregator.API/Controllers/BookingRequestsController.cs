using Microsoft.AspNetCore.Mvc;
using PlaceAgregator.EntityFramework;
using PlaceAgregator.Shared.Extensions;
using PlaceAgregator.Shared.Models;
using System.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using PlaceAgregator.Shared.DTOs.Booking;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace PlaceAgregator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingRequestsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public BookingRequestsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [Authorize(Roles = "user")]
        [HttpGet("History")]
        [Produces(typeof(BookingRequestGetDTO[]))]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetAllUserBookingRequestsHistory(
            BookingRequest.RequestStatus? status,
            [Range(1, int.MaxValue)]
            int? page,
            [Range(2, 50)]
            int? pageSize,
            string? orderBy = $"{nameof(BookingRequest.CreationDateTime)}",
            bool? desc = true
            )
        {
            string? accountId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (accountId == null)
                return Forbid();

            var query = _context.BookingRequests
                .Include(item => item.ServiceItems)
                .ThenInclude(item => item.ServiceItem)
                .Where(item=>item.UserId == accountId)
                .AsQueryable();

            query = query.Where(item => item.UserId == accountId);

            if (status != null)
                query = query.Where(item => item.Status == status);

            if (orderBy != null)
            {
                query = query.OrderBy(orderBy, desc ?? true);
            }

            if (page != null && pageSize != null)
                query = query.Skip((int)((page - 1) * pageSize)).Take((int)pageSize);

            var result = await query.Select(item => _mapper.Map<BookingRequestGetDTO>(item)).ToListAsync();
            return Ok(result);
        }
        
        [Authorize(Roles = "user")]
        [HttpGet]
        [Produces(typeof(BookingRequestGetDTO[]))]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAllForLessorIdAsync(
            int? placeId,
            string? userId,
            BookingRequest.RequestStatus? status,
            [Range(1, int.MaxValue)]
            int? page,
            [Range(2, 50)]
            int? pageSize,
            string? orderBy = $"{nameof(BookingRequest.CreationDateTime)}",
            bool? desc = true
            )
        {
            string? accountId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (accountId == null)
                return Forbid();

            var query = _context.Places
                .Include(item => item.BookingRequests)
                .Where(item => item.UserId == accountId)
                .SelectMany(item => item.BookingRequests)
                .Include(item => item.ServiceItems)
                .ThenInclude(item => item.ServiceItem)
                .AsQueryable();

            if(!await query.AnyAsync())
                return NotFound();

            if (userId != null)
                query = query.Where(item => item.UserId == userId);

            if (placeId != null)
                query = query.Where(item => item.PlaceId == placeId);

            if (status != null)
                query = query.Where(item => item.Status == status);

            if (orderBy != null)
            {
                query = query.OrderBy(orderBy, desc ?? true);
            }

            if (page != null && pageSize != null)
                query = query.Skip((int)((page - 1) * pageSize)).Take((int)pageSize);

            var result = await query.Select(item => _mapper.Map<BookingRequestGetDTO>(item)).ToListAsync();
            return Ok(result);
        }
    }
}

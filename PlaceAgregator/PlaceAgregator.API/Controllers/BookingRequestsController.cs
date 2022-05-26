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

        [Authorize(Roles = "manager")]
        [HttpGet]
        public IEnumerable<BookingRequestGetDTO> GetBookingRequests(
            string? userId,
            BookingRequest.RequestStatus? status,
            [Range(1, int.MaxValue)]
            int? page = 1,
            [Range(2, 50)]
            int? pageSize = 20,
            string? orderBy = $"{nameof(BookingRequest.CreationDateTime)}",
            bool? desc = true
            )
        {
            var query = _context.BookingRequests
                .Include(item => item.ServiceItems)
                .ThenInclude(item => item.ServiceItem)
                .AsQueryable();

            if (userId != null)
                query = query.Where(item => item.UserId == userId);

            if (status != null)
                query = query.Where(item => item.Status == status);

            if (orderBy != null)
            {
                query = query.OrderBy(orderBy, desc ?? true);
            }

            if (page != null && pageSize != null)
                query = query.Skip((int)((page - 1) * pageSize)).Take((int)pageSize);

            var result = query.Select(item => _mapper.Map<BookingRequestGetDTO>(item)).ToList();
            return result;
        }

    }
}

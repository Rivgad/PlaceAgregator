using Microsoft.AspNetCore.Mvc;
using PlaceAgregator.EntityFramework;
using PlaceAgregator.Shared.Extensions;
using PlaceAgregator.Shared.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace PlaceAgregator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingRequestsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookingRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "manager")]
        [HttpGet]
        public IActionResult GetBookingRequests(
            [Range(1, int.MaxValue)]
            int? page,
            [Range(10, 50)]
            int? pageSize,
            string? userId,
            BookingRequest.RequestStatus? status,
            string? orderBy,
            bool? desc
            )
        {
            var query = _context.BookingRequests.AsQueryable();
            
            if (userId != null)
                query = query.Where(item=> item.UserId == userId);

            if (status != null)
                query = query.Where(item=> item.Status == status);

            if(page != null && pageSize != null)
                query = query.Skip((int)((page - 1)* pageSize)).Take((int)pageSize);

            try
            {
                if (orderBy != null)
                {
                    query = query.OrderBy(orderBy, desc ?? true);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(query);
        }
    }
}

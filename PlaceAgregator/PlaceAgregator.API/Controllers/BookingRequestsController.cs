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
using Itenso.TimePeriod;

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

        [Authorize(Roles = "user")]
        [HttpPost]
        [Produces(typeof(BookingRequestGetDTO))]
        public async Task<IActionResult> CreateBookingRequest([FromForm] BookingRequestCreateDTO request)
        {
            string? accountId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (accountId == null)
                return Forbid();
            
            var place = await _context.Places
                .Include(item=>item.ServiceItems)
                .Include(item=>item.Charges)
                .Include(item=>item.Discounts)
                .FirstOrDefaultAsync(item=>item.Id == request.PlaceId);
           
            if (place == null)
                return NotFound();

            if (place.BaseRate == null || place.IsBlocked || !place.IsActive)
                return BadRequest();
            
            if (place.UserId == accountId)
                return BadRequest("Вы являетесь владельцем площадки");

            //Обнуление минут и секунд
            request.StartDateTime = new DateTime(
                year: request.StartDateTime.Year, 
                month: request.StartDateTime.Month, 
                day: request.StartDateTime.Day, 
                hour: request.StartDateTime.Hour, 
                minute: 0, 
                second: 0)
                .ToUniversalTime();

            request.EndDateTime = new DateTime(
                year: request.EndDateTime.Year,
                month: request.EndDateTime.Month,
                day: request.EndDateTime.Day,
                hour: request.EndDateTime.Hour,
                minute: 0,
                second: 0)
                .ToUniversalTime();
            
            //Меняем местами, если диапозон перевернут
            if(request.StartDateTime > request.EndDateTime)
                (request.StartDateTime, request.EndDateTime) = (request.EndDateTime, request.StartDateTime);

            var requestTimeRange = new TimeRange(request.StartDateTime, request.EndDateTime);
            int duration = (int)requestTimeRange.Duration.TotalHours;

            if(duration < 1)
            {
                return BadRequest("Минимальное время бронирования = 1 час");
            }
            if (requestTimeRange.Start.ToUniversalTime() < DateTime.UtcNow.AddHours(3))
            {
                return BadRequest("Начало должно быть не раньше чем за 3 часа");
            }
            if(place.BookingHorizonInDays != null)
            {
                if (requestTimeRange.Start.ToUniversalTime() > DateTime.UtcNow.AddDays((double)place.BookingHorizonInDays))
                    return BadRequest($"Начало должно быть не позже {DateTime.Now.AddDays((double)place.BookingHorizonInDays).ToString("f")}");
            }

            // Получаем массив с возможными пересекающимися бронированиями
            var bookedTimeIntervals = (await _context.BookingRequests
                .Where(item =>
                // У которых начало или конец после сегодняшней даты
                (item.StartDateTime.ToUniversalTime() > DateTime.UtcNow ||
                item.EndDateTime.ToUniversalTime() > DateTime.UtcNow))
                .ToListAsync())
                // Которые приняты или созданы менее 4 часов назад
                .Select(item => new TimeRange(item.StartDateTime, item.EndDateTime));


            if(bookedTimeIntervals.Any())
            {
                foreach (var timeRange in bookedTimeIntervals)
                {
                    if (requestTimeRange.OverlapsWith(timeRange))
                        return BadRequest("Время уже занято");
                }
            }
            // Суммируем одинаковые услуги
            request.ServiceItems =  request.ServiceItems?
                .GroupBy(item=>item.ServiceItemId)
                .Select(item=> new BookingRequestServiceItemDTO()
                {
                    ServiceItemId = item.Key,
                    Quantity = item.Select(item=>item.Quantity).Sum()
                })
                .ToArray();
            
            // получаем услуги которые есть у площадки
            var serviceItemIds = request.ServiceItems?.Select(item => item.ServiceItemId);
            var validServiceItemIds = place.ServiceItems
                .Where(item => serviceItemIds
                .Contains(item.Id))
                .Select(item=>item.Id);

            if (serviceItemIds?.Count() != request.ServiceItems?.Length)
                return BadRequest("Запись неверных услуг");

            // записываем только валидные услуги
            request.ServiceItems = request.ServiceItems?
                .Where(item => validServiceItemIds
                .Contains(item.ServiceItemId))
                .ToArray();


            var newBookingRequest = _mapper.Map<BookingRequest>(request);
            newBookingRequest.Status = BookingRequest.RequestStatus.Created;
            newBookingRequest.CreationDateTime = DateTime.UtcNow;
            newBookingRequest.UserId = accountId;

            decimal totalPrice = await CalculateAndValidateOrderPrice(newBookingRequest.ServiceItems);
            decimal totalDiscount = place.Discounts
                .Where(item => item.FromHoursQuantity <= duration)
                .Select(item=>item.Procents)
                .Sum();
            decimal totalCharge = place.Charges
                .Where(item => item.FromGuestsQuantity <= request.GuestsQuantity)
                .Select(item => item.Procents)
                .Sum();

            // Просчитываем конечную сумму 
            totalPrice += (decimal)place.BaseRate * duration * (1 - totalDiscount/100) * (1 + totalCharge/100);

            newBookingRequest.TotalPrice = totalPrice; 

            var result = await _context.BookingRequests.AddAsync(newBookingRequest);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(CreateBookingRequest), 
                _mapper.Map<BookingRequestGetDTO>(result.Entity));
        }

        /// <summary>
        /// Checks <see cref="BookingRequest.Status"/> and <see cref="BookingRequest.Status"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private bool RequestIsAcceptedOrCreated3HoursAgo(BookingRequest request)
        {
            if(request.Status == BookingRequest.RequestStatus.Accepted)
                return true;
            if (request.Status == BookingRequest.RequestStatus.Created && 
            DateTime.UtcNow.Subtract(request.CreationDateTime.ToUniversalTime()).TotalHours < 3)
                return true;
            
            return false;
        }

        private async Task<decimal> CalculateAndValidateOrderPrice(IEnumerable<BookingRequestServiceItem> serviceItems)
        {
            var itemQuantityDict = serviceItems
                .GroupBy(item => item.ServiceItemId)
                .ToDictionary(t => t.Key, t => t.Select(item => item.Quantity).Sum());

            try
            {
                IEnumerable<int>? productIds = itemQuantityDict.Select(item => item.Key);
                var productPriceDict = await _context
                    .ServiceItems
                    .Where(item => productIds.Contains(item.Id) == true)
                    .ToDictionaryAsync(item => item.Id, item => item.Price);

                if (productPriceDict.Count != itemQuantityDict.Count)
                {
                    var products = itemQuantityDict
                        .Select(item => item.Key)
                        .Except(productPriceDict.Select(item => item.Key));
                    var productsStr = string.Join(",", products.Select(item => $"productId: {item}"));
                    throw new Exception($"Current products don't exist: {productsStr}");
                }

                decimal totalPrice = productPriceDict.Select(item => item.Value * itemQuantityDict[item.Key]).Sum();

                return totalPrice;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "user")]
        [HttpPost("{id}/[Action]")]
        [Produces(typeof(BookingRequestGetDTO))]
        public async Task<IActionResult> Cancel(int id)
        {
            string? accountId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (accountId == null)
                return Forbid();

            var bookingRequest = await _context.BookingRequests
                .FirstOrDefaultAsync(item => item.Id == id && item.UserId == accountId);

            if (bookingRequest == null)
                return NotFound();

            if (bookingRequest.UserId != accountId)
                return Unauthorized();

            if (bookingRequest.Status == BookingRequest.RequestStatus.Rejected)
                return BadRequest("Бронирование уже отклонено");

            if (bookingRequest.Status == BookingRequest.RequestStatus.Cancelled)
                return BadRequest("Бронирование уже отменено");

            if(DateTime.UtcNow >= bookingRequest.EndDateTime.ToUniversalTime())
                return BadRequest("Невозможно отменить бронирование так как оно уже закончилось");
            
            if (DateTime.UtcNow >= bookingRequest.StartDateTime.ToUniversalTime())
                return BadRequest("Невозможно отменить бронирование так как оно уже началось");

            if (DateTime.UtcNow.Subtract(bookingRequest.StartDateTime.ToUniversalTime()).TotalHours <= 3)
                return BadRequest("Невозможно отменить бронирование менее чем за 3 часа до начала");

            bookingRequest.Status = BookingRequest.RequestStatus.Rejected;
            _context.Update(bookingRequest);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<BookingRequestGetDTO>(bookingRequest));
        }

        [Authorize(Roles = "user")]
        [HttpPost("{id}/[Action]")]
        [Produces(typeof(BookingRequestGetDTO))]
        public async Task<IActionResult> Accept(int id)
        {
            string? accountId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (accountId == null)
                return Forbid();
            
            var bookingRequest = await _context.BookingRequests
                .FirstOrDefaultAsync(item => item.Id == id && item.UserId == accountId);
            if (bookingRequest == null)
                return NotFound();

            if (!await UserIsOwner(bookingRequest.PlaceId, accountId))
                return Unauthorized("Вы не являетесь владельцем площадки");

            if (bookingRequest.Status != BookingRequest.RequestStatus.Created)
                return BadRequest();
            
            bookingRequest.Status = BookingRequest.RequestStatus.Accepted;
            _context.Update(bookingRequest);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<BookingRequestGetDTO>(bookingRequest));
        }

        [Authorize(Roles = "user")]
        [HttpPost("{id}/[Action]")]
        [Produces(typeof(BookingRequestGetDTO))]
        public async Task<IActionResult> Reject(int id)
        {
            string? accountId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (accountId == null)
                return Forbid();

            var bookingRequest = await _context.BookingRequests
                .FirstOrDefaultAsync(item => item.Id == id && item.UserId == accountId);
            if (bookingRequest == null)
                return NotFound();

            if (!await UserIsOwner(bookingRequest.PlaceId, accountId))
                return Unauthorized("Вы не являетесь владельцем площадки");

            if (bookingRequest.Status != BookingRequest.RequestStatus.Created)
                return BadRequest();

            bookingRequest.Status = BookingRequest.RequestStatus.Rejected;
            _context.Update(bookingRequest);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<BookingRequestGetDTO>(bookingRequest));
        }

        private async Task<bool> UserIsOwner(int placeId, string userId)
        {
            var result = await _context.Places.AnyAsync(place => place.Id == placeId && place.UserId == userId);
            return result;
        }
    }
}

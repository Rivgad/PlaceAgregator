using AutoMapper;
using Itenso.TimePeriod;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlaceAgregator.EntityFramework;
using PlaceAgregator.Shared.DTOs.Booking;
using PlaceAgregator.Shared.Extensions;
using PlaceAgregator.Shared.Models;
using System.Data;
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

        #region Get handles

        [Authorize(Roles = "user")]
        [HttpGet("History")]
        [Produces(typeof(BookingRequestGetDTO[]))]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetAllUserBookingRequestsHistory(
            [FromQuery] BookingRequestFilterDTO filter)
        {
            string? accountId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (accountId == null)
                return Forbid();

            var query = _context.BookingRequests
                .Include(item => item.ServiceItems)
                .ThenInclude(item => item.ServiceItem)
                .Where(item => item.UserId == accountId)
                .AsQueryable();

            query = query.Where(item => item.UserId == accountId);

            if (filter.Status != null)
                query = query.Where(item => item.Status == filter.Status);

            if (filter.OrderBy != null)
            {
                query = query.OrderBy(filter.OrderBy, filter.Desc ?? true);
            }

            if (filter.Page != null && filter.PageSize != null)
                query = query.Skip((int)((filter.Page - 1) * filter.PageSize)).Take((int)filter.PageSize);

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
            [FromQuery] BookingRequestFilterDTO filter)
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

            if (!await query.AnyAsync())
                return NotFound();

            if (userId != null)
                query = query.Where(item => item.UserId == userId);

            if (placeId != null)
                query = query.Where(item => item.PlaceId == placeId);

            if (filter.Status != null)
                query = query.Where(item => item.Status == filter.Status);

            if (filter.OrderBy != null)
            {
                query = query.OrderBy(filter.OrderBy, filter.Desc ?? true);
            }

            if (filter.Page != null && filter.PageSize != null)
                query = query.Skip((int)((filter.Page - 1) * filter.PageSize)).Take((int)filter.PageSize);

            var result = await query.Select(item => _mapper.Map<BookingRequestGetDTO>(item)).ToListAsync();
            return Ok(result);
        }

        #endregion

        #region Change status handles

        /// <summary>
        /// Changes <see cref="BookingRequest.Status"/> to Cancelled if there are at least 3 hours left before the start
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

            if (DateTime.UtcNow >= bookingRequest.EndDateTime)
                return BadRequest("Невозможно отменить бронирование так как оно уже закончилось");

            if (DateTime.UtcNow >= bookingRequest.StartDateTime)
                return BadRequest("Невозможно отменить бронирование так как оно уже началось");

            if (bookingRequest.StartDateTime.Subtract(DateTime.UtcNow).TotalHours <= 3)
                return BadRequest("Невозможно отменить бронирование менее чем за 3 часа до начала");

            bookingRequest.Status = BookingRequest.RequestStatus.Cancelled;
            _context.Update(bookingRequest);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<BookingRequestGetDTO>(bookingRequest));
        }


        /// <summary>
        /// Changes <see cref="BookingRequest.Status"/> to Accepted if status was Created
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "user")]
        [HttpPost("{id}/[Action]")]
        [Produces(typeof(BookingRequestGetDTO))]
        public async Task<IActionResult> Accept(int id)
        {
            string? accountId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (accountId == null)
                return Forbid();

            var bookingRequest = await _context.BookingRequests
                .FirstOrDefaultAsync(item => item.Id == id);

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


        /// <summary>
        /// Changes <see cref="BookingRequest.Status"/> to Rejected if status was Created
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "user")]
        [HttpPost("{id}/[Action]")]
        [Produces(typeof(BookingRequestGetDTO))]
        public async Task<IActionResult> Reject(int id)
        {
            string? accountId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (accountId == null)
                return Forbid();

            var bookingRequest = await _context.BookingRequests
                .FirstOrDefaultAsync(item => item.Id == id);

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

        #endregion

        /// <summary>
        /// Calculate and validate service items
        /// </summary>
        /// <param name="serviceItems"></param>
        /// <returns>Total service items prices sum</returns>
        /// <exception cref="Exception"></exception>
        private async Task<decimal> CalculateAndValidateOrderPrice(IEnumerable<BookingRequestServiceItem> serviceItems)
        {
            var itemQuantityDict = serviceItems
                .GroupBy(item => item.ServiceItemId)
                .ToDictionary(t => t.Key, t => t.Select(item => item.Quantity).Sum());

            foreach (var item in itemQuantityDict)
            {
                var serviceItem = await _context.ServiceItems.FirstOrDefaultAsync(si => si.Id == item.Key);
                if (serviceItem == null)
                    throw new Exception($"Услуга с Id = {item.Key} не существует");
                if (item.Value > serviceItem.MaxQuantity)
                    throw new Exception($"Для услуги с Id = {item.Key} максимальное количество шт = {serviceItem.MaxQuantity}");
            }

            try
            {
                IEnumerable<int>? productIds = itemQuantityDict.Select(item => item.Key);
                var productPriceDict = await _context
                    .ServiceItems
                    .Where(item => productIds.Contains(item.Id) == true)
                    .ToDictionaryAsync(item => item.Id, item => item.Price);

                decimal totalPrice = productPriceDict.Select(item => item.Value * itemQuantityDict[item.Key]).Sum();

                return totalPrice;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Checks whether the user is the owner of the place
        /// </summary>
        /// <param name="placeId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task<bool> UserIsOwner(int placeId, string userId)
        {
            var result = await _context.Places.AnyAsync(place => place.Id == placeId && place.UserId == userId);
            return result;
        }

        [HttpGet("[Action]/{placeId}")]
        public async Task<IEnumerable<BookingRequestGetDTO>> GetBlockingBookingRequests(int placeId)
        {
            var query = _context.BookingRequests.Where(item => item.PlaceId == placeId)
                .Where(item => item.Status == BookingRequest.RequestStatus.Created && (DateTime.UtcNow - item.CreationDateTime).TotalHours <= 3
                ||
                item.Status == BookingRequest.RequestStatus.Accepted);

            var result = await query.ToListAsync();

            return result.Select(item => _mapper.Map<BookingRequestGetDTO>(item));
        }

        [HttpPost("[Action]")]
        public async Task<IActionResult> GetPrice([FromBody] BookingRequestCreateDTO request)
        {
            var place = await _context.Places
                .Include(item => item.ServiceItems)
                .Include(item => item.Charges)
                .Include(item => item.Discounts)
                .FirstOrDefaultAsync(item => item.Id == request.PlaceId);

            if (place == null)
                return NotFound();

            if (place.BaseRate == null || place.IsBlocked || !place.IsActive)
                return BadRequest();

            //Обнуление минут и секунд
            request.StartDateTime = Normalize(request.StartDateTime);

            request.EndDateTime = Normalize(request.EndDateTime);

            //Меняем местами, если диапозон перевернут
            if (request.StartDateTime > request.EndDateTime)
                (request.StartDateTime, request.EndDateTime) = (request.EndDateTime, request.StartDateTime);

            var requestTimeRange = new TimeRange(request.StartDateTime, request.EndDateTime);
            int duration = (int)requestTimeRange.Duration.TotalHours;

            var newBookingRequest = _mapper.Map<BookingRequest>(request);

            decimal serviceItemsTotalPrice = 0;
            try
            {
                serviceItemsTotalPrice += await CalculateAndValidateOrderPrice(newBookingRequest.ServiceItems);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            decimal totalDiscount = place.Discounts
                .Where(item => item.FromHoursQuantity <= duration)
                .Select(item => item.Procents)
                .Sum();
            decimal totalCharge = place.Charges
                .Where(item => item.FromGuestsQuantity <= request.GuestsQuantity)
                .Select(item => item.Procents)
                .Sum();

            // Просчитываем конечную сумму 
            decimal totalPrice = (decimal)place.BaseRate * duration * (1 - totalDiscount / 100) * (1 + totalCharge / 100);
            totalPrice += serviceItemsTotalPrice;

            return Ok(new { price = totalPrice });
        }

        private static DateTime Normalize(DateTime dateTime)
        {
            dateTime = new DateTime(
                year: dateTime.Year,
                month: dateTime.Month,
                day: dateTime.Day,
                hour: dateTime.Hour,
                minute: 0,
                second: 0)
                .ToUniversalTime();

            return dateTime;
        }

        /// <summary>
        /// Creates new <see cref="BookingRequest"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = "user")]
        [HttpPost]
        [Produces(typeof(BookingRequestGetDTO))]
        public async Task<IActionResult> CreateBookingRequest([FromBody] BookingRequestCreateDTO request)
        {
            string? accountId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (accountId == null)
                return Forbid();

            var place = await _context.Places
                .Include(item => item.ServiceItems)
                .Include(item => item.Charges)
                .Include(item => item.Discounts)
                .FirstOrDefaultAsync(item => item.Id == request.PlaceId);

            if (place == null)
                return NotFound();

            if (place.BaseRate == null || place.IsBlocked || !place.IsActive)
                return BadRequest();

            if (place.UserId == accountId)
                return BadRequest("Вы являетесь владельцем площадки");

            if (place.Capacity != null && request.GuestsQuantity > place.Capacity)
                return BadRequest($"Максимальное количество гостей = {place.Capacity}");

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
            if (request.StartDateTime > request.EndDateTime)
                (request.StartDateTime, request.EndDateTime) = (request.EndDateTime, request.StartDateTime);

            var requestTimeRange = new TimeRange(request.StartDateTime, request.EndDateTime);
            int duration = (int)requestTimeRange.Duration.TotalHours;

            if (duration < 1)
            {
                return BadRequest("Минимальное время бронирования = 1 час");
            }
            if (requestTimeRange.Start < DateTime.UtcNow.AddHours(3))
            {
                return BadRequest("Начало должно быть не раньше чем за 3 часа");
            }
            if (place.BookingHorizonInDays != null)
            {
                if (requestTimeRange.Start > DateTime.UtcNow.AddDays((double)place.BookingHorizonInDays))
                    return BadRequest($"Начало должно быть не позже {DateTime.Now.AddDays((double)place.BookingHorizonInDays).ToString("f")}");
            }

            // Получаем массив с возможными пересекающимися бронированиями
            var bookedTimeIntervals = (await _context.BookingRequests
                .Where(item =>
                // У которых начало или конец после сегодняшней даты
                (item.StartDateTime >= DateTime.UtcNow || item.EndDateTime >= DateTime.UtcNow) &&

                //
                (item.StartDateTime <= requestTimeRange.Start ||
                item.EndDateTime <= requestTimeRange.End ||
                item.EndDateTime <= requestTimeRange.Start ||
                item.StartDateTime <= requestTimeRange.End
                )
                )
                .ToListAsync())
                .Where(item => item.Status == BookingRequest.RequestStatus.Created &&
                (DateTime.UtcNow - item.CreationDateTime).TotalHours <= 3
                ||
                item.Status == BookingRequest.RequestStatus.Accepted)
                // Которые приняты или созданы менее 4 часов назад
                .Select(item => new TimeRange(item.StartDateTime, item.EndDateTime))
                .ToList();

            // проверка на пересечение с другими датами
            if (bookedTimeIntervals.Any())
            {
                foreach (var timeRange in bookedTimeIntervals)
                {
                    if (requestTimeRange.OverlapsWith(timeRange))
                        return BadRequest("Время уже занято");
                }
            }
            // Суммируем одинаковые услуги
            request.ServiceItems = request.ServiceItems?
                .GroupBy(item => item.ServiceItemId)
                .Select(item => new BookingRequestServiceItemDTO()
                {
                    ServiceItemId = item.Key,
                    Quantity = item.Select(item => item.Quantity).Sum()
                })
                .ToArray();

            // получаем услуги которые есть у площадки
            var serviceItemIds = request.ServiceItems?.Select(item => item.ServiceItemId);
            var validServiceItemIds = place.ServiceItems
                .Where(item => serviceItemIds
                .Contains(item.Id))
                .Select(item => item.Id);

            if (serviceItemIds?.Count() != request.ServiceItems?.Length)
                return BadRequest("Запись невалидных услуг");

            // записываем только валидные услуги
            request.ServiceItems = request.ServiceItems?
                .Where(item => validServiceItemIds
                .Contains(item.ServiceItemId))
                .ToArray();


            var newBookingRequest = _mapper.Map<BookingRequest>(request);
            newBookingRequest.Status = BookingRequest.RequestStatus.Created;
            newBookingRequest.CreationDateTime = DateTime.UtcNow;
            newBookingRequest.UserId = accountId;

            decimal serviceItemsTotalPrice = 0;
            try
            {
                serviceItemsTotalPrice += await CalculateAndValidateOrderPrice(newBookingRequest.ServiceItems);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            decimal totalDiscount = place.Discounts
                .Where(item => item.FromHoursQuantity <= duration)
                .Select(item => item.Procents)
                .Sum();
            decimal totalCharge = place.Charges
                .Where(item => item.FromGuestsQuantity <= request.GuestsQuantity)
                .Select(item => item.Procents)
                .Sum();

            // Просчитываем конечную сумму 
            decimal totalPrice = (decimal)place.BaseRate * duration * (1 - totalDiscount / 100) * (1 + totalCharge / 100);
            totalPrice += serviceItemsTotalPrice;

            newBookingRequest.TotalPrice = totalPrice;

            var result = await _context.BookingRequests.AddAsync(newBookingRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateBookingRequest),
                _mapper.Map<BookingRequestGetDTO>(result.Entity));
        }
    }
}

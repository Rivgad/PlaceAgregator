using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlaceAgregator.EntityFramework;
using PlaceAgregator.Shared.DTOs.Places;
using PlaceAgregator.Shared.Extensions;
using PlaceAgregator.Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;

namespace PlaceAgregator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly int pageSize = 20;

        public PlacesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Charges

        [Authorize(Roles = "user")]
        [HttpPost("{id?}/Charges")]
        [Produces(typeof(ChargeGetDTO))]
        public async Task<IActionResult> AddCharge(int id, [FromBody] ChargeCreateDTO charge)
        {
            var place = await _context.Places.FirstOrDefaultAsync(item => item.Id == id && item.IsBlocked == false);
            if (place == null)
                return NotFound();

            string? accountId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (accountId == null)
                return Forbid();

            if (place.UserId != accountId)
                return Forbid();

            Charge newCharge = _mapper.Map<Charge>(charge);
            newCharge.PlaceId = place.Id;

            newCharge = _context.Charges.Add(newCharge).Entity;
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<ChargeGetDTO>(newCharge));
        }

        [Authorize(Roles = "user")]
        [HttpDelete("{id?}/Charges/{chargeId?}")]
        public async Task<IActionResult> DeleteCharge(int id, int chargeId)
        {
            var place = await _context.Places
                .Include(item => item.Charges)
                .FirstOrDefaultAsync(item => item.Id == id && item.IsBlocked == false);

            if (place == null)
                return NotFound();

            string? accountId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (accountId == null || place.UserId != accountId)
                return Forbid();

            var charge = place.Charges?.FirstOrDefault(item => item.Id == chargeId);
            if (charge == null)
                return BadRequest();

            _context.Charges.Remove(charge);
            await _context.SaveChangesAsync();

            return Ok(new { id = chargeId });
        }

        #endregion

        #region Discounts

        [Authorize(Roles = "user")]
        [HttpPost("{id?}/Discounts")]
        [Produces(typeof(DiscountGetDTO))]
        public async Task<IActionResult> AddDiscount(int id, [FromBody] DiscountCreateDTO discount)
        {
            var place = await _context.Places.FirstOrDefaultAsync(item => item.Id == id && item.IsBlocked == false);
            if (place == null)
                return NotFound();

            string? accountId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (accountId == null)
                return Forbid();

            if (place.UserId != accountId)
                return Forbid();

            Discount newDiscount = _mapper.Map<Discount>(discount);
            newDiscount.PlaceId = place.Id;

            newDiscount = _context.Discounts.Add(newDiscount).Entity;
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<DiscountGetDTO>(newDiscount));
        }

        [Authorize(Roles = "user")]
        [HttpDelete("{id?}/Discounts/{discountId?}")]
        public async Task<IActionResult> DeleteDiscount(int id, int discountId)
        {
            var place = await _context.Places
                .Include(item => item.Discounts)
                .FirstOrDefaultAsync(item => item.Id == id && item.IsBlocked == false);

            if (place == null)
                return NotFound();

            string? accountId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (accountId == null || place.UserId != accountId)
                return Forbid();

            var discount = place.Discounts?.FirstOrDefault(item => item.Id == discountId);
            if (discount == null)
                return BadRequest();

            _context.Discounts.Remove(discount);
            await _context.SaveChangesAsync();

            return Ok(new { id = discountId });
        }

        #endregion

        #region Photo

        [Authorize(Roles = "user")]
        [HttpPost("{id?}/Photos")]
        public async Task<IActionResult> AddPhoto(int id, [FromBody] byte[] base64Code)
        {
            var place = await _context.Places.FirstOrDefaultAsync(item => item.Id == id && item.IsBlocked == false);
            if (place == null)
                return NotFound();

            string? accountId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (accountId == null)
                return Forbid();

            if (place.UserId != accountId)
                return Forbid();

            var photo = _context.PlacePhotos.Add(new PlacePhoto()
            {
                PlaceId = place.Id,
                Value = base64Code
            });
            await _context.SaveChangesAsync();

            return Ok(photo.Entity);
        }

        [Authorize(Roles = "user")]
        [HttpDelete("{id?}/Photos/{photoId?}")]
        public async Task<IActionResult> DeletePhoto(int id, int photoId)
        {
            var place = await _context.Places
                .Include(item => item.Photos)
                .FirstOrDefaultAsync(item => item.Id == id && item.IsBlocked == false);
            if (place == null)
                return NotFound();

            string? accountId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (accountId == null)
                return Forbid();

            if (place.UserId != accountId)
                return Forbid();

            var photo = place.Photos?.FirstOrDefault(item => item.Id == photoId);
            if (photo == null)
                return BadRequest();

            _context.PlacePhotos.Remove(photo);

            await _context.SaveChangesAsync();

            return Ok(new { id = photoId });
        }

        #endregion

        #region Blocking

        [Authorize(Roles = "manager, admin")]
        [HttpPost("{id?}/[Action]")]
        public async Task<IActionResult> BlockPlace(int id)
        {
            var place = await _context.Places.FirstOrDefaultAsync(item => item.Id == id);

            if (place == null)
                return NotFound();

            place.IsBlocked = true;
            _context.Update(place);
            await _context.SaveChangesAsync();

            return Ok(new { id = id });
        }

        [Authorize(Roles = "manager, admin")]
        [HttpPost("{id?}/[Action]")]
        public async Task<IActionResult> UnblockPlace(int id)
        {
            var place = await _context.Places.FirstOrDefaultAsync(item => item.Id == id);

            if (place == null)
                return NotFound();

            place.IsBlocked = false;
            _context.Update(place);
            await _context.SaveChangesAsync();

            return Ok(new { id = id });
        }

        #endregion

        #region PlacesCrud

        [HttpGet("[Action]")]
        [Produces(typeof(PagesQuantityResponse))]
        public async Task<IActionResult> PagesQuantity()
        {
            decimal placesCount = await _context.Places.CountAsync(item => item.IsBlocked == false);
            var pagesCount = Math.Ceiling(placesCount / (decimal)pageSize);

            return Ok(new PagesQuantityResponse((int)pagesCount));
        }

        [HttpGet]
        [Produces(typeof(PlaceCardInfo[]))]
        public async Task<IEnumerable<PlaceCardInfo>> GetAllAsync([FromQuery] PlaceFilterDTO filter)
        {
            var query = _context.Places
                .Include(item => item.BookingRequests)
                .AsQueryable();

            query = query.Where(item => item.IsBlocked == false && item.IsActive == true);

            if (filter.MinCapacity != null)
                query = query.Where(item => item.Capacity >= filter.MinCapacity);

            if (filter.MinArea != null)
                query = query.Where(item => item.Area >= filter.MinArea);

            if (filter.MinRating != null)
                query = query.Where(item => item.Rating >= filter.MinRating);

            if (filter.Search != null)
                query = query.Where(item => 
                item.City.ToLower().Contains(filter.Search.ToLower()) ||
                item.Address.ToLower().Contains(filter.Search.ToLower()) ||
                item.Title.ToLower().Contains(filter.Search.ToLower()) ||
                item.Description.ToLower().Contains(filter.Search.ToLower()));

            if (filter.MaxBaseRate != null)
                query = query.Where(item => item.BaseRate <= filter.MaxBaseRate);

            return await query.Select(item => _mapper.Map<PlaceCardInfo>(item)).ToListAsync();
        }

        [Authorize(Roles = "user")]
        [HttpPost("{id}/ToggleIsActive")]
        [Produces(typeof(PlaceGetTableRowDTO))]
        public async Task<IActionResult> TogglePlaceIsActive(int id)
        {
            string? accountId = User.FindFirst(ClaimTypes.Sid)?.Value;

            var place = await _context.Places
                .FirstOrDefaultAsync(item => item.Id == id && item.IsBlocked == false);

            if (place == null)
                return NotFound();

            if (accountId != null && place.UserId == accountId)
            {
                place.IsActive = !place.IsActive;
                _context.Update(place);
                await _context.SaveChangesAsync();
                return Ok(_mapper.Map<PlaceGetTableRowDTO>(place));
            }

            return BadRequest("Вы не являетесь владельцем площадки");
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(PlaceGetDTO))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetPlaceAsync(int id)
        {
            var place = await _context.Places
                .Include(item => item.User)
                .Include(item => item.Charges)
                .Include(item => item.Discounts)
                .Include(item => item.Photos)
                .Include(item => item.EventTypes)
                .Include(item => item.Prohibitions)
                .FirstOrDefaultAsync(item => item.Id == id && item.IsBlocked == false);

            if (place == null)
                return NotFound();

            string? accountId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (place.IsActive == false && accountId != null && place.UserId != accountId)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PlaceGetDTO>(place));
        }

        [Authorize(Roles = "user")]
        [HttpGet("myPlaces")]
        [Produces(typeof(PlaceGetTableRowDTO[]))]
        public async Task<IActionResult> GetUserPlaces()
        {
            string? accountId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (accountId == null)
                return BadRequest();

            var places = await _context.Places.Where(item => item.UserId == accountId).ToListAsync();

            return Ok(places.Select(item => _mapper.Map<PlaceGetTableRowDTO>(item)));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var place = await _context.Places.FirstOrDefaultAsync(item => item.Id == id);
            if (place == null)
                return BadRequest();

            string? accountId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (accountId == null)
                return Forbid();

            if (place.UserId != accountId)
                return Forbid();

            _context.Places.Remove(place);
            await _context.SaveChangesAsync();

            return Ok(new { id = id });
        }

        [Authorize(Roles = "user")]
        [HttpPost]
        [Produces(typeof(PlaceGetDTO))]
        public async Task<IActionResult> CreatePlaceAsync([FromBody] PlaceCreateDTO place)
        {
            string? accountId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (accountId == null)
                return BadRequest();

            Place newPlace = new Place()
            {
                UserId = accountId,
                Title = place.Title,
                City = place.City,
                Address = place.Address,
            };

            var result = await _context.Places.AddAsync(newPlace);
            await _context.SaveChangesAsync();
            newPlace = result.Entity;

            return Ok(_mapper.Map<PlaceGetDTO>(newPlace));
        }

        [Authorize(Roles = "user")]
        [HttpPut("{id}")]
        [Produces(typeof(PlaceGetDTO))]
        public async Task<IActionResult> UpdatePlaceAsync(int id, [FromBody] PlaceUpdateDTO placeDTO)
        {
            var place = await _context.Places.FirstOrDefaultAsync(item => item.Id == id && item.IsBlocked == false);
            if (place == null)
                return NotFound();

            string? accountId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (accountId == null)
                return Forbid();

            if (place.UserId != accountId)
                return Forbid();

            _mapper.Map(placeDTO, place);

            _context.Places.Update(place);
            if (place.EventTypes != null)
                _context.AttachRange(place.EventTypes);

            if (place.Prohibitions != null)
                _context.AttachRange(place.Prohibitions);

            await _context.SaveChangesAsync();

            place = await _context.Places.FirstOrDefaultAsync(item => item.Id == id);

            if (place == null)
                return BadRequest();

            return Ok(_mapper.Map<PlaceGetDTO>(place));
        }

        #endregion
    }
}

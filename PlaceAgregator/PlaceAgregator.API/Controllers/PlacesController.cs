using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlaceAgregator.API.Services.Interfaces;
using PlaceAgregator.EntityFramework;
using PlaceAgregator.Shared.DTOs.Places;
using PlaceAgregator.Shared.Extensions;
using PlaceAgregator.Shared.Models;
using System.ComponentModel.DataAnnotations;
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

        
        #region ServiceItems

        [Authorize(Roles = "user")]
        [HttpPost("{placeId}/ServiceItems")]
        [Produces(typeof(ServiceItemGetDTO))]
        public async Task<IActionResult> CreateServiceItem(int placeId, [FromForm] ServiceItemCreateDTO serviceItem)
        {
            var place = await _context.Places.FirstOrDefaultAsync(item => item.Id == placeId && item.IsBlocked == false);
            if (place == null)
                return NotFound();

            string? accountId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (accountId == null)
                return Forbid();

            if (place.UserId != accountId)
                return Forbid();

            var newServiceItem = _mapper.Map<ServiceItem>(serviceItem);
            newServiceItem.PlaceId = place.Id;

            newServiceItem = _context.ServiceItems.Add(newServiceItem).Entity;
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateServiceItem), _mapper.Map<ServiceItemGetDTO>(newServiceItem));
        }

        [Authorize(Roles = "user")]
        [HttpPut("{placeId}/ServiceItems/{itemId}")]
        [Produces(typeof(ServiceItemGetDTO))]
        public async Task<IActionResult> UpdateServiceItem(int placeId, int itemId, [FromForm] ServiceItemUpdateDTO serviceItem)
        {
            var place = await _context.Places
                .Include(item => item.ServiceItems)
                .FirstOrDefaultAsync(item => item.Id == placeId && item.IsBlocked == false);
            if (place == null)
                return NotFound();

            string? accountId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (accountId == null)
                return Forbid();

            if (place.UserId != accountId)
                return Forbid();

            var existedServiceItem = place.ServiceItems?.FirstOrDefault(item => item.Id == itemId);
            if (existedServiceItem == null)
                return NotFound();

            _mapper.Map(serviceItem, existedServiceItem);

            existedServiceItem = _context.ServiceItems.Update(existedServiceItem).Entity;
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<ServiceItemGetDTO>(existedServiceItem));
        }

        [Authorize(Roles = "user")]
        [HttpDelete("{placeId}/ServiceItems/{itemId}")]
        public async Task<IActionResult> DeleteServiceItem(int placeId, int itemId)
        {
            var place = await _context.Places
                .Include(item => item.ServiceItems)
                .FirstOrDefaultAsync(item => item.Id == placeId && item.IsBlocked == false);
            if (place == null)
                return NotFound();

            string? accountId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (accountId == null)
                return Forbid();

            if (place.UserId != accountId)
                return Forbid();

            var serviceItem = place.ServiceItems?.FirstOrDefault(item => item.Id == itemId);
            if (serviceItem == null)
                return NotFound();

            _context.ServiceItems.Remove(serviceItem);
            await _context.SaveChangesAsync();

            return Ok(new { id = serviceItem.Id });
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
                .Include(item=>item.Photos)
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
        public async Task<IEnumerable<PlaceCardInfo>> GetAllAsync([Range(1, int.MaxValue)] int page)
        {
            page = page - 1;
            var result = _context.Places
                .Where(item => item.IsBlocked == false)
                .OrderBy(item => item.Id)
                .Skip(page * pageSize)
                .Take(pageSize);

            return await result.Select(item => _mapper.Map<PlaceCardInfo>(item)).ToListAsync();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(GetPlaceDTO))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetPlaceAsync(int id)
        {
            var place = await _context.Places
                .Include(item => item.Charges)
                .Include(item => item.Discounts)
                .Include(item => item.Photos)
                .Include(item => item.EventTypes)
                .Include(item => item.Prohibitions)
                .Include(item => item.ServiceItems)
                .FirstOrDefaultAsync(item => item.Id == id && item.IsBlocked == false);

            if (place == null)
                return NotFound();

            return Ok(_mapper.Map<GetPlaceDTO>(place));
        }

        [Authorize(Roles = "user")]
        [HttpGet("myPlaces")]
        public async Task<IActionResult> GetUserPlaces()
        {
            string? accountId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (accountId == null)
                return BadRequest();

            var places = await _context.Places.Where(item => item.UserId == accountId).ToListAsync();

            return Ok(places.Select(item => new { id = item.Id, title = item.Title, rating = item.Rating, isBlocked = item.IsBlocked }));
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
        [Produces(typeof(GetPlaceDTO))]
        public async Task<IActionResult> CreatePlaceAsync([FromForm]PlaceCreateDTO place)
        {
            string? accountId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (accountId == null)
                return BadRequest();

            Place newPlace = _mapper.Map<Place>(place);
            newPlace.UserId = accountId;

            var result = await _context.Places.AddAsync(newPlace);
            await _context.SaveChangesAsync();
            newPlace = result.Entity;

            return Ok(_mapper.Map<GetPlaceDTO>(newPlace));
        }

        [Authorize(Roles = "user")]
        [HttpPut("{id}")]
        [Produces(typeof(GetPlaceDTO))]
        public async Task<IActionResult> UpdatePlaceAsync(int id, [FromForm] PlaceUpdateDTO placeDTO)
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

            if (place.BuildingTypeId != null)
                _context.Attach(place.BuildingTypeId);
            if (place.ParkingTypeId != null)
                _context.AttachRange(place.ParkingTypeId);
            if (place.WaterTypeId != null)
                _context.AttachRange(place.WaterTypeId);
            await _context.SaveChangesAsync();

            return Ok(placeDTO);
        } 

        #endregion
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlaceAgregator.API.Services.Interfaces;
using PlaceAgregator.EntityFramework;
using PlaceAgregator.Shared.DTOs.Places;
using PlaceAgregator.Shared.Extensions;
using PlaceAgregator.Shared.Models;
using PlaceAgregator.Shared.Models.Enums;
using PlaceAgregator.Shared.Models.Types;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace PlaceAgregator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly IPlacesService _placesService;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly int pageSize = 20;

        public PlacesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("[Action]")]
        [Produces(typeof(PagesQuantityResponse))]
        public async Task<IActionResult> PagesQuantity()
        {
            decimal placesCount = await _context.Places.CountAsync();
            var pagesCount = Math.Ceiling(placesCount / (decimal)pageSize);

            return Ok(new PagesQuantityResponse((int)pagesCount));
        }

        [HttpGet]
        [Produces(typeof(PlaceCardInfo[]))]
        public async Task<IEnumerable<PlaceCardInfo>> GetAllAsync([Range(1, int.MaxValue)] int page)
        {
            page = page - 1;
            var result = _context.Places
                .Skip(page * pageSize)
                .Take(pageSize);

            return await result.Select(item => _mapper.Map<PlaceCardInfo>(item)).ToListAsync();
        }

        [HttpGet("{id?}")]
        [ProducesResponseType(200, Type = typeof(GetPlaceDTO))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetPlaceAsync(int id)
        {
            var place = await _context.Places.FirstOrDefaultAsync(item => item.Id == id);
            if (place == null)
                return BadRequest();

            return Ok(_mapper.Map<GetPlaceDTO>(place));
        }

        [HttpGet("myPlaces")]
        public async Task<IActionResult> GetUserPlaces()
        {
            string? accountId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (accountId == null)
                return BadRequest();

            var places = await _context.Places.Where(item=>item.UserId == accountId).ToListAsync();

            return Ok(places.Select(item=> new { id=item.Id, title=item.Title, rating=item.Rating }));
        }

        [HttpDelete("{id?}")]
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

            return Ok(new { id=id });
        }

        [Authorize(Roles = "user")]
        [HttpPost]
        [Produces(typeof(GetPlaceDTO))]
        public async Task<IActionResult> CreatePlaceAsync(PlaceCreateDTO place)
        {
            string? accountId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (accountId == null)
                return BadRequest();

            Place newPlace = new Place()
            {
                UserId = accountId,
                City = place.City,
                Title = place.Title,
                Address = place.Address
            };
            var result = await _context.Places.AddAsync(newPlace);
            await _context.SaveChangesAsync();
            newPlace = result.Entity;
            
            GetPlaceDTO dto = _mapper.Map<GetPlaceDTO>(newPlace);
            return Ok(dto);
        }

        [Authorize(Roles = "user")]
        [HttpPatch]
        [Produces(typeof(GetPlaceDTO))]
        public async Task<IActionResult> UpdatePlaceAsync(int placeId, [FromForm] PlaceUpdateDTO placeDTO)
        {
            var place = await _context.Places.FirstOrDefaultAsync(item => item.Id == placeId);
            if (place == null)
                return BadRequest();

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

            if (place.Rules != null)
                _context.AttachRange(place.Rules);

            if (place.BuildingTypeId != null)
                _context.Attach(place.BuildingTypeId);
            if (place.ParkingTypeId != null)
                _context.AttachRange(place.ParkingTypeId);
            if (place.WaterTypeId != null)
                _context.AttachRange(place.WaterTypeId);
            await _context.SaveChangesAsync();

            return Ok(placeDTO);
        }
    }
}

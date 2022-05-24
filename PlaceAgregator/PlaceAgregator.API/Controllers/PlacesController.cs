﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlaceAgregator.API.Services.Interfaces;
using PlaceAgregator.EntityFramework;
using PlaceAgregator.Shared.DTOs.Places;
using PlaceAgregator.Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;

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
            decimal placesCount = await _context.Places.CountAsync(item => item.IsBlocked == false);
            var pagesCount = Math.Ceiling(placesCount / (decimal)pageSize);

            return Ok(new PagesQuantityResponse((int)pagesCount));
        }

        [HttpPost("{id?}/Photos")]
        public async Task<IActionResult> AddPhoto(int id, [FromBody] byte[] base64Code)
        {
            var place = await _context.Places.FirstOrDefaultAsync(item => item.Id == id && item.IsBlocked == false);

            if (place == null)
                return NotFound();

            var photo = _context.PlacePhotos.Add(new PlacePhoto()
            {
                PlaceId = place.Id,
                Value = base64Code
            });
            await _context.SaveChangesAsync();

            return Ok(photo.Entity);
        }

        [HttpDelete("{id?}/Photos/{photoId?}")]
        public async Task<IActionResult> DeletePhoto(int id, int photoId)
        {
            var place = await _context.Places
                .Include(item=>item.Photos)
                .FirstOrDefaultAsync(item => item.Id == id && item.IsBlocked == false);

            if (place == null)
                return NotFound();

            var photo = place.Photos?.FirstOrDefault(item => item.Id == photoId);
            if (photo == null)
                return BadRequest();

            _context.PlacePhotos.Remove(photo);

            await _context.SaveChangesAsync();

            return Ok(new { id = photoId });
        }

        [Authorize(Roles ="manager, admin")]
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

        [HttpGet]
        [Produces(typeof(PlaceCardInfo[]))]
        public async Task<IEnumerable<PlaceCardInfo>> GetAllAsync([Range(1, int.MaxValue)] int page)
        {
            page = page - 1;
            var result = _context.Places
                .Where(item => item.IsBlocked == false)
                .Skip(page * pageSize)
                .Take(pageSize);

            return await result.Select(item => _mapper.Map<PlaceCardInfo>(item)).ToListAsync();
        }

        [HttpGet("{id?}")]
        [ProducesResponseType(200, Type = typeof(GetPlaceDTO))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetPlaceAsync(int id)
        {
            var place = await _context.Places
                .Include(item=>item.Photos)
                .Include(item=>item.EventTypes)
                .Include(item=>item.Prohibitions)
                .Include(item=>item.Rules)
                .Include(item=>item.ServiceItems)
                .FirstOrDefaultAsync(item => item.Id == id && item.IsBlocked == false);

            if (place == null)
                return NotFound();

            return Ok(_mapper.Map<GetPlaceDTO>(place));
        }

        [HttpGet("myPlaces")]
        public async Task<IActionResult> GetUserPlaces()
        {
            string? accountId = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (accountId == null)
                return BadRequest();

            var places = await _context.Places.Where(item => item.UserId == accountId).ToListAsync();

            return Ok(places.Select(item => new { id = item.Id, title = item.Title, rating = item.Rating, isBlocked = item.IsBlocked }));
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

            return Ok(new { id = id });
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
            var place = await _context.Places.FirstOrDefaultAsync(item => item.Id == placeId && item.IsBlocked == false);
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

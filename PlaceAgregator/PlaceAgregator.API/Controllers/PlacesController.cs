using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlaceAgregator.Entities;
using PlaceAgregator.EntityFramework;

namespace PlaceAgregator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public PlacesController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet("[Action]")]
        public IActionResult GetAllPlaces()
        {
            var places = _context.Places
                .Where(item => item.IsActive == true)
                .Select(item => new 
                { 
                    item.Id, 
                    item.Title, 
                    item.Area,
                    item.Capacity, 
                    item.BaseRate, 
                    item.Rating, 
                    item.Photo,
                    item.Address
                });

            return new JsonResult(places);
        }

        [HttpGet]
        public async Task<IActionResult> GetPlace(int id)
        {
            var place = await _context.Places
                .Include(item => item.Comments)
                .ThenInclude(item => item.User)
                .ThenInclude(item=>item.Account)
                .Include(item => item.User)
                .ThenInclude(item => item.Account)
                .Include(item => item.ServiceItems)
                .Include(item => item.BuildingType)
                .FirstOrDefaultAsync(item => item.Id == id);

            if (place == null)
            {
                return NotFound();
            }
            var serviceItems = place.ServiceItems?
                .Where(item => item.IsActive == true)
                .Select(item => new { title = item.Title, item.Price, item.Comment, item.Id });

            var comments = place.Comments?
                .Select(item => new { item.Id, item.PublicationDate, item.Rating, login = item.User.Account.Login, item.Text });

            return new JsonResult(new
            {
                place = new
                {
                    city = place.City,
                    title = place.Title,
                    photo = place.Photo,
                    address = place.Address,
                    commentsCount = place.Comments.Count(),
                    rating = place.Rating,
                    parameters = new object[]
                    {
                        new { title = "Тип здания", value = place.BuildingType?.Title, type = "buildingType" },
                        new { title = "Площадь", value = place.Area, type = "area" },
                        new { title = "Вместимость", value = place.Capacity, type = "capacity" },
                        new { title = "Высота потолков", value = place.CellingHeight, type = "height" },
                        new { title = "Парковка", value = place.ParkingType?.ToString(), type = "parkingType" },
                        new { title = "Количество туалетов", value = place.SharedToiletsQuantity, type = "toiletsCount" },
                        new { title = "Курение", value = place.SmokingRule?.ToString(), type = "smokingRule" },
                        new { title = "Присутствие администратора", value = place.AdministratorRule?.ToString(), type = "administratorRule" },
                        new { title = "Вода", value = place.WaterType?.ToString(), type = "water" }
                    },
                    serviceItems = serviceItems,
                    description = place.Description,

                    baseRate = place.BaseRate,

                    comments = comments,
                },
                landlord = new
                {
                    login = place.User.Account.Login,
                    firstName = place.User?.FirstName,
                    familyName = place.User?.LastName,
                }
            });
        }

        [Authorize]
        [HttpPost("[Action]")]
        public async Task<IActionResult> Update([FromBody] Place place)
        { 
            try
            {
                _context.Update(place);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public struct PlaceCreateData
        {
            public string Title { get; set; }
            public string City { get; set; }
            public string Address { get; set; }
        }

        [Authorize]
        [HttpPost("[Action]")]
        public async Task<IActionResult> Create([FromBody] PlaceCreateData data)
        {
            if (string.IsNullOrEmpty(data.Title) || string.IsNullOrEmpty(data.City) || string.IsNullOrEmpty(data.Address))
            {
                return BadRequest();
            }
            

            var account = await _context.Accounts
                .Include(item=>item.User)
                .FirstOrDefaultAsync(item => item.Login == User.Identity.Name);

            if(account == null)
            {
                return BadRequest();
            }

            var place = new Place() { UserId= account.User.Id, Address = data.Address, Title = data.Title, City = data.City };


            try
            {
                var res = await _context.AddAsync(place);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(Create), res.Entity.Id);
                //return Ok(new { placeId = res.Entity.Id });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        [Authorize]
        [HttpPost("[Action]")]
        public async Task<IActionResult> Delete(int id)
        {
            var place = await _context.Places.FirstOrDefaultAsync(item=>item.Id == id);

            if(place == null)
            {
                return NotFound();
            }
            try
            {
                _context.Places.Remove(place);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

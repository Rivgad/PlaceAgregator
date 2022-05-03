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
        [HttpGet]
        public IActionResult GetAllPlaces()
        {
            using var context = new ApplicationContext();
            var places = context.Places.Where(item=>item.IsActive==true).Select(item => new { item.Id,item.Title, item.Area, item.Capacity, item.BaseRate, item.Rating, item.Photo });

            return new JsonResult(places);
        }

        [HttpPost]
        public IActionResult GetPlace(int id)
        {
            using var context = new ApplicationContext();
            var place = context.Places
                .Include(item => item.Comments)
                .ThenInclude(item=>item.User)
                .Include(item=>item.User)
                .FirstOrDefault(item => item.Id == id);

            if(place == null)
            {
                return NotFound();
            }


            return new JsonResult(new 
            { 
                place = new 
                { 
                    place.Title, 
                    place.BaseRate, 
                    place.Description,

                }, 
                comments = place.Comments,
                landlord = new
                {
                    
                }
            });
        }
    }
}

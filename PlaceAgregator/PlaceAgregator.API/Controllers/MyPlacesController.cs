using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlaceAgregator.EntityFramework;

namespace PlaceAgregator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyPlacesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public MyPlacesController(ApplicationContext context)
        {
            _context = context;
        }

        [Authorize(Roles ="User")]
        [HttpGet]
        public async Task<IActionResult> GetAllUserPlaces()
        {
            var login = User.Identity.Name;
            var account = await _context.Accounts
                .Include(item=>item.User)
                .ThenInclude(item=>item.Places)
                .FirstOrDefaultAsync(item => item.Login == login);
            if(account == null || account.User == null)
            {
                return BadRequest();
            }

            var places = account.User.Places.Select(item => new { item.Id, item.Title, item.Rating });
            
            return new JsonResult(places);
        }
    }
}

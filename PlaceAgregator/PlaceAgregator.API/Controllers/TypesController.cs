using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlaceAgregator.EntityFramework;

namespace PlaceAgregator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var eventTypes = await _context.EventTypes.ToListAsync();
            var prohibitions = await _context.Prohibitions.ToListAsync();

            return Ok(new
            {
                eventTypes = eventTypes,
                prohibitions = prohibitions
            });
        }
    }
}

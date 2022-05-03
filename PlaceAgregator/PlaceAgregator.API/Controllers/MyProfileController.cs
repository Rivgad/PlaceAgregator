using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PlaceAgregator.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class MyProfileController : ControllerBase
    {
        [Authorize(Roles ="User")]
        [HttpGet("[action]")]
        public IActionResult GetNumber()
        {
            var result = User;
            return Ok(5);
        }
    }
}

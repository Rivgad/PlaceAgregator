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
    public class MyProfileController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public MyProfileController(ApplicationContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult Index(int id)
        {
            var account = _context.Accounts
                .Include(acc => acc.User)
                .FirstOrDefault(acc => acc.Id == id);

            if (account == null || account.User == null)
            {
                return BadRequest();
            }
            var login = account.Login;
            var firstName = account.User.FirstName;
            var familyName = account.User.LastName;
            var email = account.User.Email;
            var phone = account.User.PhoneNumber;

            return new JsonResult(new { login, firstName, familyName, email, phone });
        }

        [Authorize(Roles = "User")]
        [HttpPost("[Action]")]
        public async Task<IActionResult> Update(int id, string login, string firstName, string familyName, string email, string phone)
        {



            var account = await _context.Accounts
                .Include(acc => acc.User)
                .FirstOrDefaultAsync(acc => acc.Id == id);

            if (account == null || account.User == null)
            {
                return BadRequest();
            }

            account.Login = login;
            account.User.FirstName = firstName;
            account.User.LastName = familyName;
            account.User.Email = email;
            account.User.PhoneNumber = phone;

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}

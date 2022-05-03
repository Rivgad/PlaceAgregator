using PlaceAgregator.Entities;
using System.Security.Claims;

namespace PlaceAgregator.API.Services.Interfaces
{
    public interface IAuthService
    {
        string HashPassword(Account account, string password);
        bool VerifyPassword(Account account, string actualPassword, string hashedPassword);
        string GetToken(int id, ClaimsIdentity claimsIdentity);
    }
}

using System.Security.Claims;

namespace PlaceAgregator.API.Services.Interfaces
{
    public interface IAuthService
    {
        string GetToken(int id, ClaimsIdentity claimsIdentity);
    }
}

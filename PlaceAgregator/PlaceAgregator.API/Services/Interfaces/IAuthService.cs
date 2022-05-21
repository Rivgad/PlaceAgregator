using System.Security.Claims;

namespace PlaceAgregator.API.Services.Interfaces
{
    public interface IAuthService
    {
        string GetToken(IEnumerable<Claim> claims);
    }
}

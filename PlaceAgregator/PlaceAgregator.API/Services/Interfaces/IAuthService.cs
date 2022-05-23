using PlaceAgregator.Shared.DTOs;
using PlaceAgregator.Shared.DTOs.Authentication;
using System.Security.Claims;

namespace PlaceAgregator.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Response<LoginResponse>> LoginAsync(string userNameOrEmail, string password);
        Task<Response> RegistrationAsync(string email, string userName, string password);
    }
}

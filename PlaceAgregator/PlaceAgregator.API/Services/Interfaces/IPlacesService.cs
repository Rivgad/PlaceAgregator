using PlaceAgregator.Shared.DTOs.Places;
using PlaceAgregator.Shared.Models;

namespace PlaceAgregator.API.Services.Interfaces
{
    public interface IPlacesService
    {
        Task<GetPlaceDTO> CreatePlaceAsync(string City, string Title, string Address);
        Task DeletePlaceAsync(int id);
        Task<GetPlaceDTO> UpdatePlaceAsync(int id, PlaceUpdateDTO place);
        Task<GetPlaceDTO> GetUserPlacesIdAsync(int userId);
        Task<IEnumerable<PlaceCardInfo>> GetAllPlacesAsync();
        Task<IEnumerable<GetPlaceDTO>> GetPlaceById(int id);

        Task BlockPlaceAsync(int id);
        Task UnblockPlaceAsync(int id);

        Task<int> AddPhotoToPlaceAsync(int placeId, byte[] photo);
        Task DeletePhotoAsync(int id);

        Task AddServiceItemAsync(int placeId, ServiceItem serviceItem);
        Task UpdateServiceItemAsync(int id, ServiceItem serviceItem);
        Task DeleteServiceItemAsync(int id);

        Task AddRateAsync(int placeId, Rate rate);
        Task DeleteRateAsync(int id);

        Task AddDiscount(int placeId, Discount discount);
        Task DeleteDiscountAsync(int id);

        Task AddChargeAsync(int id, Charge charge);
        Task DeleteChargeAsync(int id);
    }
}

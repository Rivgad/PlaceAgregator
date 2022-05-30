using PlaceAgregator.API.Services.Interfaces;
using PlaceAgregator.Shared.DTOs.Places;
using PlaceAgregator.Shared.Models;

namespace PlaceAgregator.API.Services
{
    public class PlaceService : IPlacesService
    {
        public Task AddChargeAsync(int id, Charge charge)
        {
            throw new NotImplementedException();
        }

        public Task AddDiscount(int placeId, Discount discount)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddPhotoToPlaceAsync(int placeId, byte[] photo)
        {
            throw new NotImplementedException();
        }

        public Task AddServiceItemAsync(int placeId, ServiceItem serviceItem)
        {
            throw new NotImplementedException();
        }

        public Task BlockPlaceAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GetPlaceDTO> CreatePlaceAsync(string City, string Title, string Address)
        {
            throw new NotImplementedException();
        }

        public Task DeleteChargeAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteDiscountAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeletePhotoAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeletePlaceAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteServiceItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PlaceCardInfo>> GetAllPlacesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GetPlaceDTO>> GetPlaceById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GetPlaceDTO> GetUserPlacesIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task UnblockPlaceAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GetPlaceDTO> UpdatePlaceAsync(int id, PlaceUpdateDTO place)
        {
            throw new NotImplementedException();
        }

        public Task UpdateServiceItemAsync(int id, ServiceItem serviceItem)
        {
            throw new NotImplementedException();
        }
    }
}
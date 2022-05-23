using PlaceAgregator.Shared.Models;

namespace PlaceAgregator.API.Services.Interfaces
{
    public interface IPlacesService
    {
        Task CreatePlace(string City, string Title, string Address);
        Task DeletePlace(int id);
        Task UpdatePlace(int id, Place place);
    }
}

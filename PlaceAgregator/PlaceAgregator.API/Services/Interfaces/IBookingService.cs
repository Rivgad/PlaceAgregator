using PlaceAgregator.Shared.Models;

namespace PlaceAgregator.API.Services.Interfaces
{
    public interface IBookingService
    {
        Task<BookingRequest> CreateBookingRequest(BookingRequest bookingRequest);
        Task<BookingRequest> UpdateBookingRequestStatus(int id, BookingRequest.RequestStatus status);
        Task<BookingRequest> UpdateBookingRequest(int id, BookingRequest bookingRequest);
    }
}

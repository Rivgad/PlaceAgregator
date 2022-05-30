using PlaceAgregator.Shared.Models;
using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.DTOs.Booking
{
    public class BookingRequestFilterDTO : BaseFilterDTO
    {
        [JsonPropertyName("status")]
        public BookingRequest.RequestStatus? Status { get; set; }

        [JsonPropertyName("orderBy")]
        public string? OrderBy { get; set; } = $"{nameof(BookingRequest.CreationDateTime)}";
    }
}

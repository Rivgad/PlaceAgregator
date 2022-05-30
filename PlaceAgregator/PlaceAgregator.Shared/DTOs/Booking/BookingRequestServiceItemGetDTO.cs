using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.DTOs.Booking
{
    public class BookingRequestServiceItemGetDTO
    {
        [JsonPropertyName("serviceItemId")]
        public int ServiceItemId { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("quantity")]
        public int? Quantity { get; set; }
    }
}

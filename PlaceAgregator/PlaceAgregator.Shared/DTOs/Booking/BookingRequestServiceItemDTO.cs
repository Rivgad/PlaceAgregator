using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.DTOs.Booking
{
    public class BookingRequestServiceItemDTO
    {
        [Required]
        [JsonPropertyName("serviceItemId")]
        public int ServiceItemId { get; set; }

        [Required]
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }
}

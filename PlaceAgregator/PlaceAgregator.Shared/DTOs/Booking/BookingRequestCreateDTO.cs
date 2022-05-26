using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.DTOs.Booking
{
    public class BookingRequestCreateDTO
    {
        [Required]
        [JsonPropertyName("placeId")]
        public int PlaceId { get; set; }

        [Required]
        [JsonPropertyName("startDateTime")]
        public DateTime StartDateTime { get; set; }

        [Required]
        [JsonPropertyName("endDateTime")]
        public DateTime EndDateTime { get; set; }

        [JsonPropertyName("enrollDateTime")]
        public DateTime? EnrollDateTime { get; set; }

        [Required]
        [JsonPropertyName("guestsQuantity")]
        public int GuestsQuantity { get; set; }

        [JsonPropertyName("comment")]
        public string? Comment { get; set; }

        [JsonPropertyName("serviceItems")]
        public BookingRequestServiceItemDTO[]? ServiceItems { get; set; }
    }
}

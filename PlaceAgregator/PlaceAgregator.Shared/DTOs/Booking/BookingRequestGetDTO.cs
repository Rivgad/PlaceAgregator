using PlaceAgregator.Shared.Models;
using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.DTOs.Booking
{
    public class BookingRequestGetDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("placeId")]
        public int? PlaceId { get; set; }

        [JsonPropertyName("userId")]
        public string? UserId { get; set; }

        [JsonPropertyName("status")]
        public BookingRequest.RequestStatus Status { get; set; }

        [JsonPropertyName("creationDateTime")]
        public DateTime CreationDateTime { get; set; }

        [JsonPropertyName("startDateTime")]
        public DateTime StartDateTime { get; set; }

        [JsonPropertyName("endDateTime")]
        public DateTime EndDateTime { get; set; }

        [JsonPropertyName("guestsQuantity")]
        public int GuestsQuantity { get; set; }

        [JsonPropertyName("totalPrice")]
        public decimal? TotalPrice { get; set; }

        [JsonPropertyName("comment")]
        public string? Comment { get; set; }

        [JsonPropertyName("serviceItems")]
        public BookingRequestServiceItemGetDTO[]? ServiceItems { get; set; }
    }
}

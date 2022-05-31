using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.DTOs.Places
{
    public class PlaceUpdateDTO
    {
        [JsonPropertyName("prohibitions")]
        public int[]? ProhibitionIds { get; set; }

        [JsonPropertyName("eventTypeIds")]
        public int[]? EventTypeIds { get; set; }

        [JsonPropertyName("shedule")]
        public SheduleDTO? Shedule { get; set; }

        [JsonPropertyName("title")]
        [Required]
        public string? Title { get; set; }

        [JsonPropertyName("baseRate")]
        public decimal? BaseRate { get; set; }

        [JsonPropertyName("photo")]
        public byte[]? Photo { get; set; }

        [JsonPropertyName("address")]
        [Required]
        public string? Address { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("capacity")]
        [Range(0, int.MaxValue)]
        public int? Capacity { get; set; }

        [JsonPropertyName("area")]
        [Range(0, float.PositiveInfinity)]
        public decimal? Area { get; set; }

        [JsonPropertyName("bookingHorizonInDays")]
        [Range(0, int.MaxValue)]
        public int? BookingHorizonInDays { get; set; }
    }
}

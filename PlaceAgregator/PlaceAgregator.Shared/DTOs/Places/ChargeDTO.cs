using PlaceAgregator.Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.DTOs.Places
{
    public class ChargeGetDTO : ChargeDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
    public class ChargeDTO
    {
        [Required]
        [JsonPropertyName("placeId")]
        public int PlaceId { get; set; }

        [Required]
        [JsonPropertyName("procents")]
        public decimal Procents { get; set; }

        [JsonPropertyName("rateId")]
        public int? RateId { get; set; }

        [JsonPropertyName("timeInterval")]
        public TimeInterval? TimeInterval { get; set; }

        [JsonPropertyName("fromGuestsQuantity")]
        public int? FromGuestsQuantity { get; set; }

        [JsonPropertyName("comment")]
        public string? Comment { get; set; }
    }
}

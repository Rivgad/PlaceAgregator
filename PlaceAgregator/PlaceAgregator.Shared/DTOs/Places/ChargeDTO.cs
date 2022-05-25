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
    public class ChargeCreateDTO
    {
        [Required]
        [Range(1, float.PositiveInfinity)]
        [JsonPropertyName("procents")]
        public decimal Procents { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [JsonPropertyName("fromGuestsQuantity")]
        public int? FromGuestsQuantity { get; set; }

        [JsonPropertyName("comment")]
        public string? Comment { get; set; }

    }
    public class ChargeDTO
    {
        [JsonPropertyName("placeId")]
        public int PlaceId { get; set; }

        [Required]
        [Range(1, float.PositiveInfinity)]
        [JsonPropertyName("procents")]
        public decimal Procents { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [JsonPropertyName("fromGuestsQuantity")]
        public int? FromGuestsQuantity { get; set; }

        [JsonPropertyName("comment")]
        public string? Comment { get; set; }
    }
}

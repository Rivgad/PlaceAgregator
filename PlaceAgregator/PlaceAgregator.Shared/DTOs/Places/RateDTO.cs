using PlaceAgregator.Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.DTOs.Places
{
    public class RateGetDTO : RateDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
    public class CreateRateDTO
    {
        [Required]
        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("timeInterval")]
        public TimeInterval TimeInterval { get; set; }
    }
    public class RateDTO
    {
        [Required]
        [JsonPropertyName("placeId")]
        public int PlaceId { get; set; }

        [Required]
        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("timeInterval")]
        public TimeInterval? TimeInterval { get; set; }
    }
}

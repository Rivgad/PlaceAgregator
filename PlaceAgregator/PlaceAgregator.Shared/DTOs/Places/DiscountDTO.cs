using PlaceAgregator.Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.DTOs.Places
{
    public class DiscountGetDTO : DiscountDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
    public class DiscountDTO
    {
        [Required]
        [JsonPropertyName("placeId")]
        public int PlaceId { get; set; }

        [Required]
        [JsonPropertyName("procents")]
        public decimal Procents { get; set; }

        [JsonPropertyName("rateId")]
        public int? RateId { get; set; }

        [JsonPropertyName("fromHoursQuantity")]
        public int? FromHoursQuantity { get; set; }

        [JsonPropertyName("timeInterval")]
        public TimeInterval? TimeInterval { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.DTOs.Places
{
    public class DiscountGetDTO : DiscountDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
    public class DiscountCreateDTO
    {
        [Required]
        [JsonPropertyName("procents")]
        [Range(1, float.PositiveInfinity)]
        public decimal Procents { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [JsonPropertyName("fromHoursQuantity")]
        public int FromHoursQuantity { get; set; }
    }
    public class DiscountDTO
    {
        [Required]
        [JsonPropertyName("placeId")]
        public int PlaceId { get; set; }

        [Required]
        [Range(1, float.PositiveInfinity)]
        [JsonPropertyName("procents")]
        public decimal Procents { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [JsonPropertyName("fromHoursQuantity")]
        public int FromHoursQuantity { get; set; }
    }
}

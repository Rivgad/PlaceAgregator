using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static PlaceAgregator.Shared.Models.ServiceItem;

namespace PlaceAgregator.Shared.DTOs.Places
{
    public class ServiceItemGetDTO : ServiceItemDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
    public class ServiceItemDTO
    {
        [JsonPropertyName("placeId")]
        public int PlaceId { get; set; }

        [Required]
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [Required]
        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [Required]
        [JsonPropertyName("isCountable")]
        public bool IsCountable { get; set; } = true;

        [JsonPropertyName("maxQuantity")]
        public int? MaxQuantity { get; set; }

        [Required]
        [JsonPropertyName("per")]
        public PerValueType Per { get; set; } = PerValueType.Piece;

        [JsonPropertyName("comment")]
        public string? Comment { get; set; }
    }
}

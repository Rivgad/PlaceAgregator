using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.DTOs.Places
{
    public class PlaceFilterDTO : BaseFilterDTO
    {
        [JsonPropertyName("search")]
        public string? Search { get; set; }

        [JsonPropertyName("minCapacity")]
        public int? MinCapacity { get; set; }

        [JsonPropertyName("minArea")]
        public decimal? MinArea { get; set; }

        [JsonPropertyName("minRating")]
        public int? MinRating { get; set; }

        [JsonPropertyName("maxBaseRate")]
        public decimal? MaxBaseRate { get; set; }
    }
}

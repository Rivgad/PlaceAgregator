using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.DTOs
{
    public class FilterWithSearchDTO : BaseFilterDTO
    {
        [JsonPropertyName("search")]
        public string? Search { get; set; }
    }
}

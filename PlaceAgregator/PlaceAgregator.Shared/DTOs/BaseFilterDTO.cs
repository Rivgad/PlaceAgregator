using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.DTOs
{
    public class BaseFilterDTO
    {
        [JsonPropertyName("page")]
        [Range(1, int.MaxValue)]
        public int? Page { get; set; }

        [JsonPropertyName("pageSize")]
        [Range(10, 50)]
        public int? PageSize { get; set; }

        [JsonPropertyName("orderBy")]
        public string? OrderBy { get; set; }

        [JsonPropertyName("desc")]
        public bool? Desc { get; set; } = true;
    }
}

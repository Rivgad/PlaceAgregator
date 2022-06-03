using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.DTOs.Users
{
    public class UserFilterDTO : BaseFilterDTO
    {
        [JsonPropertyName("search")]
        public string? Search { get; set; }

        [JsonPropertyName("role")]
        public string? Role { get; set; }
    }
}

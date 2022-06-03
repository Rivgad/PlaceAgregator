using PlaceAgregator.Shared.DTOs.Users;
using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.DTOs.Places
{
    public class PlaceMinimalInfoDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("owner")]
        public AppUserGetDTO User { get; set; }

        [JsonPropertyName("isBlocked")]
        public bool IsBlocked { get; set; }
    }
}

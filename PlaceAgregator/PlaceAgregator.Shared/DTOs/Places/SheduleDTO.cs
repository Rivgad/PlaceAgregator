using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.DTOs.Places
{
    public class SheduleDTO
    {
        [JsonPropertyName("monday")]
        public bool? Monday { get; set; } = false;

        [JsonPropertyName("thuesday")]
        public bool? Thuesday { get; set; } = false;

        [JsonPropertyName("wednesday")]
        public bool? Wednesday { get; set; } = false;

        [JsonPropertyName("thursday")]
        public bool? Thursday { get; set; } = false;

        [JsonPropertyName("friday")]
        public bool? Friday { get; set; } = false;

        [JsonPropertyName("saturday")]
        public bool? Saturday { get; set; } = false;

        [JsonPropertyName("sunday")]
        public bool? Sunday { get; set; } = false;
    }
}

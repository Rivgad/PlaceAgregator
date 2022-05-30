using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.DTOs.Places
{
    public class PlaceCardInfo
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("photo")]
        public byte[]? Photo { get; set; }

        [JsonPropertyName("baseRate")]
        public int? BaseRate { get; set; }

        [JsonPropertyName("rating")]
        public decimal Rating { get; set; } = 0;

        [JsonPropertyName("area")]
        public decimal? Area { get; set; }

        [JsonPropertyName("capacity")]
        public int? Capacity { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }
    }
}

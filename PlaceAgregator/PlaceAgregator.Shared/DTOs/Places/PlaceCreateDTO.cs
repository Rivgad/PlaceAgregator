using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.DTOs.Places
{
    public class PlaceCreateDTO
    {
        [JsonPropertyName("city")]
        [Required]
        public string City { get; set; }

        [JsonPropertyName("address")]
        [Required]
        public string Address { get; set; }

        [JsonPropertyName("title")]
        [Required]
        public string Title { get; set; }
    }
}

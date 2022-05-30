using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.DTOs
{
    public class EntityDTO
    {
        [Required]
        [JsonPropertyName("Id")]
        public int Id { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.Models
{
    public class Charge : Entity
    {
        [Required]
        public int PlaceId { get; set; }

        [JsonIgnore]
        public Place Place { get; set; }

        [Required]
        [Range(1, float.PositiveInfinity)]
        public decimal Procents { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int FromGuestsQuantity { get; set; }

        public string? Comment { get; set; }
    }
}

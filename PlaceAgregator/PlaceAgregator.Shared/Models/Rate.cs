using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.Models
{
    public class Rate : Entity
    {
        public int PlaceId { get; set; }

        [JsonIgnore]
        public Place Place { get; set; }

        [Required]
        public decimal Price { get; set; }

        public TimeInterval? TimeInterval { get; set; }

        [JsonIgnore]
        public IEnumerable<Discount> Discounts { get; set; }

        [JsonIgnore]
        public IEnumerable<Charge> Charges { get; set; }
    }
}

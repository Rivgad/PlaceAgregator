using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.Models
{
    public class Discount : Entity
    {
        public int? RateId { get; set; }
        
        [JsonIgnore]
        public Rate? Rate { get; set; }

        public int PlaceId { get; set; }
        
        [JsonIgnore]
        public Place Place { get; set; }

        [Required]
        public decimal Procents { get; set; }
        public int? FromHoursQuantity { get; set; }

        public TimeInterval? TimeInterval { get; set; }
    }
}

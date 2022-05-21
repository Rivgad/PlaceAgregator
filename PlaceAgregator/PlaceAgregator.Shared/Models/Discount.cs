using System.ComponentModel.DataAnnotations;

namespace PlaceAgregator.Shared.Models
{
    public class Discount : Entity
    {
        public int? RateId { get; set; }
        public Rate? Rate { get; set; }

        public int PlaceId { get; set; }
        public Place Place { get; set; }

        [Required]
        public decimal Procents { get; set; }
        public int? FromHoursQuantity { get; set; }

        public TimeInterval? TimeInterval { get; set; }
    }
}

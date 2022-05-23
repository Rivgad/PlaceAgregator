using System.ComponentModel.DataAnnotations;

namespace PlaceAgregator.Shared.Models
{
    public class Charge : Entity
    {
        public int? RateId { get; set; }
        public Rate? Rate { get; set; }

        public int PlaceId { get; set; }
        public Place Place { get; set; }

        [Required]
        public decimal Procents { get; set; }
        public int? FromGuestsQuantity { get; set; }
        public string? Comment { get; set; }

        public TimeInterval? TimeInterval { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace PlaceAgregator.Shared.Models
{
    public class Rate : Entity
    {
        public int PlaceId { get; set; }
        public Place Place { get; set; }

        [Required]
        public decimal Price { get; set; }

        public TimeInterval? TimeInterval { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace PlaceAgregator.Shared.Models
{
    public class PlacePhoto : Entity
    {
        [Required]
        public int PlaceId { get; set; }
        public Place Place { get; set; }

        [Required]
        public byte[] Value { get; set; }

        [Required]
        public int Order { get; set; } = 0;
    }
}

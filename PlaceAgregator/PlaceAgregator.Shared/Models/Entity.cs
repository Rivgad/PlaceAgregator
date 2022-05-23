using System.ComponentModel.DataAnnotations;

namespace PlaceAgregator.Shared.Models
{
    public class Entity
    {
        [Required]
        public int? Id { get; set; }
    }
}

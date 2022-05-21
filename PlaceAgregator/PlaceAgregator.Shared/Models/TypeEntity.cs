using System.ComponentModel.DataAnnotations;

namespace PlaceAgregator.Shared.Models
{
    public class TypeEntity : Entity
    {
        [Required]
        public string Title { get; set; }
    }
}

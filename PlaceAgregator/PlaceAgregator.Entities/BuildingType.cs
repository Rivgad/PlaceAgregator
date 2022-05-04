using System.ComponentModel.DataAnnotations;

namespace PlaceAgregator.Entities
{
    public class BuildingType : Entity
    {
        [Required]
        public string Title { get; private set; }
    }
}

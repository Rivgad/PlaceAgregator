using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PlaceAgregator.Shared.Models
{
    public class ServiceItem : Entity
    {
        public enum PerValueType
        {
            [Description("Штука")]
            Piece = 1,

            [Description("Час")]
            Hour = 2,

            [Description("День")]
            Day = 3,

            [Description("Человек")]
            Person = 4
        }
        public int PlaceId { get; set; }
        public Place Place { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public bool IsCountable { get; set; } = true;
        public int? MaxCount { get; set; }

        [Required]
        public PerValueType Per { get; set; } = PerValueType.Piece;
        public string? Comment { get; set; }

        [Required]
        public bool IsActive { get; set; } = false;
    }
}

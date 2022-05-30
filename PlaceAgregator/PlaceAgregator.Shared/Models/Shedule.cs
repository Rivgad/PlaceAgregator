using System.ComponentModel.DataAnnotations;

namespace PlaceAgregator.Shared.Models
{
    public class Shedule
    {
        [Required]
        public bool? Monday { get; set; }

        [Required]
        public bool? Thuesday { get; set; }

        [Required]
        public bool? Wednesday { get; set; }

        [Required]
        public bool? Thursday { get; set; }

        [Required]
        public bool? Friday { get; set; }

        [Required]
        public bool? Saturday { get; set; }

        [Required]
        public bool? Sunday { get; set; }
    }
}

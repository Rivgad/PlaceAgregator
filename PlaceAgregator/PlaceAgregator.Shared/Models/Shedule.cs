using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PlaceAgregator.Shared.Models
{
    public class Shedule
    {
        [Required]
        [DefaultValue(false)]
        public bool? Monday { get; set; } = false;

        [Required]
        [DefaultValue(false)] 
        public bool? Thuesday { get; set; } = false;

        [Required]
        [DefaultValue(false)] 
        public bool? Wednesday { get; set; } = false;

        [Required]
        [DefaultValue(false)] 
        public bool? Thursday { get; set; } = false;

        [Required]
        [DefaultValue(false)] 
        public bool? Friday { get; set; } = false;

        [Required]
        [DefaultValue(false)] 
        public bool? Saturday { get; set; } = false;

        [Required]
        [DefaultValue(false)] 
        public bool? Sunday { get; set; } = false;
    }
}

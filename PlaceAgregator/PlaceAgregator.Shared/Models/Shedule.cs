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

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (GetType() != obj.GetType())
            {
                return false;
            }
            if (obj == null)
                return false;

            if (obj is not Shedule other)
                return false;

            if (Monday != other.Monday)
                return false;
            if (Thuesday != other.Thuesday)
                return false;
            if (Wednesday != other.Wednesday)
                return false;
            if (Thursday != other.Thursday)
                return false;
            if(Friday != other.Friday)
                return false;
            if (Saturday != other.Saturday)
                return false;
            if (Sunday != other.Sunday)
                return false;
            
            return false;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Monday.GetHashCode() ^ 
                Thuesday.GetHashCode() ^ 
                Wednesday.GetHashCode() ^
                Thuesday.GetHashCode() ^
                Friday.GetHashCode() ^
                Saturday.GetHashCode() ^
                Sunday.GetHashCode();
        }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.Models
{
    public class BookingRequestServiceItem
    {
        [Required]
        public int BookingRequestId { get; set; }

        [JsonIgnore]
        public BookingRequest BookingRequest { get; set; }

        [Required]
        public int ServiceItemId { get; set; }

        [JsonIgnore]
        public ServiceItem ServiceItem { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}

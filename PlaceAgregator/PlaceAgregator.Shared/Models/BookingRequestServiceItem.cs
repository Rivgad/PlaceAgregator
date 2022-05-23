using System.ComponentModel.DataAnnotations;

namespace PlaceAgregator.Shared.Models
{
    public class BookingRequestServiceItem
    {
        [Required]
        public int BookingRequestId { get; set; }
        public BookingRequest BookingRequest { get; set; }

        [Required]
        public int ServiceItemId { get; set; }
        public ServiceItem ServiceItem { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}

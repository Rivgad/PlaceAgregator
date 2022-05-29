using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.Models
{
    public class BookingRequest : Entity
    {
        public enum RequestStatus
        {
            [Description("Создана")]
            Created = 0,

            [Description("Принята")]
            Accepted = 1,

            [Description("Отклонена")]
            Rejected = 2,

            [Description("Отменена")]
            Cancelled = 3
        }

        [Required]
        public string UserId { get; set; }

        [JsonIgnore]
        public AppUser User { get; set; }

        [Required]
        public int PlaceId { get; set; }

        [JsonIgnore]
        public Place? Place { get; set; }

        [Required]
        public RequestStatus Status { get; set; } = RequestStatus.Created;

        [Required]
        public DateTime CreationDateTime { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }

        [Required]
        public DateTime EndDateTime { get; set; }

        public DateTime? EnrollDateTime { get; set; }

        [Required]
        public int GuestsQuantity { get; set; }
        public string? Comment { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        public IEnumerable<BookingRequestServiceItem>? ServiceItems { get; set; }
    }
}

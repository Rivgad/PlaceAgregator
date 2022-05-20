using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PlaceAgregator.Entities
{
    public class BookingRequest : Entity
    {
        public enum RequestStatus
        {
            [Description("Requested")]
            Requested = 0,

            [Description("Accepted")]
            Accepted = 1,

            [Description("Rejected")]
            Rejected = 2
        }

        public int UserId { get; private set; }
        public User User { get; set; }

        public int PlaceId { get; private set; }
        public Place Place { get; private set; }

        public RequestStatus Status { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime StartDateTime { get; private set; }
        public DateTime EndDateTime { get; private set; }
        public DateTime? EnrollDateTime { get; private set; }
        public int GuestsQuantity { get; private set; }
        public string? Comment { get; private set; }

        public IEnumerable<BookingRequestServiceItem>? ServiceItems { get; private set; }
    }
}

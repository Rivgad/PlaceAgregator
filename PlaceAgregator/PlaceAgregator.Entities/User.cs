using System.Collections.Generic;

namespace PlaceAgregator.Entities
{
    public class User : Entity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FamilyName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }

        public int AccountId { get; private set; }
        public Account Account { get; private set; }
        public IEnumerable<Place>? Places { get; private set; }
        public IEnumerable<Comment>? Comments { get; private set; }
        public IEnumerable<BookingRequest>? BookingRequests { get; private set; }
    }
}

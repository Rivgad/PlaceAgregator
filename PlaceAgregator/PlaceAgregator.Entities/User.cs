using System.Collections.Generic;

namespace PlaceAgregator.Entities
{
    public class User : Entity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FamilyName { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }

        public int AccountId { get; private set; }
        public Account Account { get; private set; }
        public IEnumerable<Place> Places { get; private set; }
        public IEnumerable<Comment> Comments { get; private set; }
        public IEnumerable<BookingRequest> BookingRequests { get; private set; }
    }
}

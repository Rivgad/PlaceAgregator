using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaceAgregator.Shared.Models
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Patronimyc { get; set; }

        public IEnumerable<Comment>? Comments { get; set; }
        public IEnumerable<Place>? Places { get; set; }
        public IEnumerable<BookingRequest>? BookingRequests { get; set; }
        public virtual IEnumerable<IdentityUserRole<string>> Roles { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.Models
{
    public class Comment
    {
        [Required]
        public string UserId { get; set; }

        [JsonIgnore]
        public AppUser User { get; set; }

        [Required]
        public int PlaceId { get; set; }

        [JsonIgnore]
        public Place Place { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime LastEditTime { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        public bool IsBlocked { get; set; }
    }
}

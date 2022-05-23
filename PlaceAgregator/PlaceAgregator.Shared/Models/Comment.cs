using System.ComponentModel.DataAnnotations;

namespace PlaceAgregator.Shared.Models
{
    public class Comment
    {
        [Required]
        public string UserId { get; set; }
        public AppUser User { get; set; }

        [Required]
        public int PlaceId { get; set; }
        public Place Place { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime LastEditTime { get; set; }

        [Required]
        public int Rating { get; set; } = 1;
    }
}

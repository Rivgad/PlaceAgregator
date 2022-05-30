using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.DTOs.Comments
{
    public class CommentDTO
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.DTOs.Authentication
{
    public class PasswordUpdateDTO
    {
        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; }

        [Required]
        [JsonPropertyName("newPassword")]
        public string NewPassword { get; set; }
    }
}

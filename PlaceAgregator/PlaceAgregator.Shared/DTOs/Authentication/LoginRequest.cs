using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.DTOs.Authentication
{
    public class LoginRequest
    {
        [JsonPropertyName("login")]
        [Required]
        public string UserNameOrEmail { get; set; }

        [JsonPropertyName("password")]
        [Required]
        public string Password { get; set; }
    }
}

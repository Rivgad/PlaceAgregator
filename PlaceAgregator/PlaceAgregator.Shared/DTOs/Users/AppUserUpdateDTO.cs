using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.DTOs.Users
{
    public class AppUserUpdateDTO
    {
        [MinLength(6)]
        [JsonPropertyName("userName")]
        public string? UserName { get; set; }

        [MinLength(3)]
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("firstName")]
        public string? FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string? LastName { get; set; }

        [JsonPropertyName("patronimyc")]
        public string? Patronimyc { get; set; }
    }
}

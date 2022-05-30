using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.DTOs.Users
{
    public class AppUserGetDTO
    {
        [JsonPropertyName("userId")]
        public string Id { get; set; }

        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("firstName")]
        public string? FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string? LastName { get; set; }
        
        [JsonPropertyName("patronimyc")]
        public string? Patronimyc { get; set; }
    }
}

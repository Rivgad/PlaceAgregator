using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.DTOs.Authentication
{

    public class LoginResponse
    {
        [Required]
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }

        [JsonPropertyName("roles")]
        public string[] Roles { get; set; }

        public LoginResponse(string accessToken, string[] roles)
        {
            AccessToken = accessToken;
            Roles = roles;
        }
    }
}

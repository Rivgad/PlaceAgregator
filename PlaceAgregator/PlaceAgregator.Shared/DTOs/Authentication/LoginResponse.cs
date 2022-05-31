using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.DTOs.Authentication
{

    public class LoginResponse
    {
        [Required]
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }

        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        [JsonPropertyName("roles")]
        public string[] Roles { get; set; }

        public LoginResponse(string accessToken, string userName, string[] roles)
        {
            AccessToken = accessToken;
            Roles = roles;
            UserName = userName;
        }
    }
}

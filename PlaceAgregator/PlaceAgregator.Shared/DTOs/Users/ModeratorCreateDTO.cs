using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.DTOs.Users
{
    public class ModeratorCreateDTO
    {
        [Required]
        [JsonPropertyName("email")]
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }

        [Required]
        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
        [JsonPropertyName("confirmPassword")]
        public string ConfirmPassword { get; set; }
    }
}

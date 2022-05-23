using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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

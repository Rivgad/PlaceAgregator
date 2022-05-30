using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.DTOs
{
    public class ResponseError
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        public ResponseError(string code, string description)
        {
            Code = code;
            Description = description;
        }
    }
    public class Response<T>
    {
        [JsonIgnore]
        public bool Succeeded { get; set; }

        [JsonPropertyName("errors")]
        public ResponseError[]? Errors { get; set; }

        [JsonPropertyName("message")]
        public T? Message { get; set; }
    }

    public class Response
    {
        [JsonIgnore]
        public bool Succeeded { get; set; }

        [JsonPropertyName("errors")]
        public ResponseError[]? Errors { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
}

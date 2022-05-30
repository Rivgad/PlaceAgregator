using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.DTOs.Places
{
    public class PagesQuantityResponse
    {
        [JsonPropertyName("pagesCount")]
        public int PagesCount { get; set; }

        public PagesQuantityResponse(int pagesCount)
        {
            PagesCount = pagesCount;
        }
    }
}

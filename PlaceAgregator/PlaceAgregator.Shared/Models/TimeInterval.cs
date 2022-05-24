using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.Models
{
    public class TimeInterval
    {
        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("endDate")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("startTo,e")]
        public TimeOnly StartTime { get; set; }

        [JsonPropertyName("endTime")]
        public TimeOnly EndTime { get; set; }

        [JsonPropertyName("shedule")]
        public Shedule Shedule { get; set; }
    }
}

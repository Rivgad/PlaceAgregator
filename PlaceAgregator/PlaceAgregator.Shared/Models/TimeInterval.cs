using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.Models
{
    public class TimeInterval
    {
        [JsonPropertyName("startDate")]
        public DateTime StartDateTime { get; set; }

        [JsonPropertyName("endDate")]
        public DateTime EndDateTime { get; set; }

        [JsonPropertyName("shedule")]
        public Shedule? Shedule { get; set; }

        public void Normalize()
        {
            if (StartDateTime > EndDateTime)
            {
                (StartDateTime, EndDateTime) = (EndDateTime, StartDateTime);
            }
        }
    }
}

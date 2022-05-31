using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.Models.Types
{
    public class EventType : TypeEntity
    {
        [JsonIgnore]
        public IEnumerable<Place> Places { get; set; }
    }
}

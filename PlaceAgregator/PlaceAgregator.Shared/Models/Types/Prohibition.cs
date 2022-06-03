using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.Models.Types
{
    public class Prohibition : TypeEntity
    {
        [JsonIgnore]
        public IEnumerable<Place> Places { get; set; }
    }
}

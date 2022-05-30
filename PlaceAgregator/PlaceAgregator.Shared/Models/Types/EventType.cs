namespace PlaceAgregator.Shared.Models.Types
{
    public class EventType : TypeEntity
    {
        public IEnumerable<Place> Places { get; set; }
    }
}

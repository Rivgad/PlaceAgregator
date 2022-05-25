namespace PlaceAgregator.Shared.Models.Types
{
    public class WaterType : TypeEntity
    {
        public IEnumerable<Place> Places { get; set; }
    }
}

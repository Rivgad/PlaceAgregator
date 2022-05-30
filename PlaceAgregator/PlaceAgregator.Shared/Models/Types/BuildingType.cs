namespace PlaceAgregator.Shared.Models.Types
{
    public class BuildingType : TypeEntity
    {
        public IEnumerable<Place> Places { get; set; }
    }
}

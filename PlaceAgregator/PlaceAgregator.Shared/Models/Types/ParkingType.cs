namespace PlaceAgregator.Shared.Models.Types
{
    public class ParkingType : TypeEntity
    {
        public IEnumerable<Place> Places { get; set; }
    }
}

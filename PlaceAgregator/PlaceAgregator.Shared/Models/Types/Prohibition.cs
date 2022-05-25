namespace PlaceAgregator.Shared.Models.Types
{
    public class Prohibition : TypeEntity
    {
        public IEnumerable<Place> Places { get; set; }
    }
}

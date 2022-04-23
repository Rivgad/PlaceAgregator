namespace PlaceAgregator.Entities
{
    public class Rate : Entity
    {
        public long PlaceId { get; private set; }
        public Place Place { get; private set; }

        public TimeInterval TimeInterval { get; private set; }
    }
}

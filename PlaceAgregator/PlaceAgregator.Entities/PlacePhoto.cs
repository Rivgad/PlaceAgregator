namespace PlaceAgregator.Entities
{
    public class PlacePhoto
    {
        public long PlaceId { get; private set; }
        public long PhotoId { get; private set; }
        public Place Place { get; private set; }
        public Photo Photo { get; private set; }
        public int Position { get; private set; }
    }
}

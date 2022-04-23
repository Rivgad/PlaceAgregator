namespace PlaceAgregator.Entities
{
    public class BookingRequestServiceItem
    {
        public long BookingRequestId { get; private set; }
        public long ServiceItemId { get; private set; }
        public int Quantity { get; private set; }

        public BookingRequest BookingRequest { get; private set; }
        public ServiceItem ServiceItem { get; private set; }
    }
}

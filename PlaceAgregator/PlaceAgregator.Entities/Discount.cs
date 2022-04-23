using System.ComponentModel;

namespace PlaceAgregator.Entities
{
    public class Discount : Entity
    {
        public enum PerValueType
        {
            [Description("%")]
            Procent = 1,

            [Description("руб/час")]
            MoneyHour = 2,

            [Description("руб/день")]
            MoneyDay = 3
        }
        public long PlaceId { get; private set; }
        public Place Place { get; private set; }
        public long? RateId { get; private set; }
        public Rate? Rate { get; private set; }

        public TimeInterval TimeInterval { get; private set; }
        public float Value { get; private set; }
        public PerValueType Per { get; private set; }
        public int? FromGuestQuantity { get; private set; }
        public string? Comment { get; private set; }
    }
}

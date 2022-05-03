using System.ComponentModel;

namespace PlaceAgregator.Entities
{
    public class Charge : Entity
    {
        public enum PerValueType
        {
            [Description("%")]
            Procent = 1,

            [Description("руб/час")]
            MoneyHour = 2
        }
        public int PlaceId { get; private set; }
        public Place Place { get; private set; }

        public int? RateId { get; private set; }
        public Rate? Rate { get; private set; }
        public TimeInterval TimeInterval { get; private set; }
        public float Value { get; private set; }
        public PerValueType Per { get; private set; }
        public int? FromHoursQuantity { get; private set; }
        public string? Comment { get; private set; }
    }
}

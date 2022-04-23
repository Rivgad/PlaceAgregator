using System.ComponentModel;

namespace PlaceAgregator.Entities
{
    public class ServiceItem : Entity
    {
        public enum PerValueType
        {
            [Description("Штука")]
            Piece = 1,

            [Description("Час")]
            Hour = 2,

            [Description("День")]
            Day = 3,

            [Description("Человек")]
            Person = 4
        }
        public long PlaceId { get; private set; }
        public Place Place { get; private set; }

        public string Title { get; private set; }
        public decimal Price { get; private set; }
        public bool IsCountable { get; private set; }
        public int? MaxCount { get; private set; }
        public PerValueType Per { get; private set; }
        public string? Comment { get; private set; }
        public bool IsActive { get; private set; }
    }
}

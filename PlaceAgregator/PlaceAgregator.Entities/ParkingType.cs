using System.ComponentModel;

namespace PlaceAgregator.Entities
{
    public enum ParkingType
    {
        [Description("Парковка далее чем 100 метров")]
        IsFurtherThan100Meters = 0,

        [Description("Парковка рядом со зданием")]
        Nearby = 1,

        [Description("Своя парковка")]
        Own = 2
    }
}

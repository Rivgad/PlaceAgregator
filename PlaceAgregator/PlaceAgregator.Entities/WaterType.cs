using System.ComponentModel;

namespace PlaceAgregator.Entities
{
    public enum WaterType
    {
        [Description("Кулер")]
        Cooler,
        [Description("Кран с водой")]
        WaterTap,
        [Description("Кран с фильтрованной водой")]
        FilteredWaterTap
    }
}

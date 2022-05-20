using System.ComponentModel;

namespace PlaceAgregator.Entities
{
    public enum SmokingRuleType
    {
        [Description("Можно курить в выделенном месте")]
        OnDisignatedSpace = 0,
        [Description("Можно курить в помещении")]
        Inside = 1,
        [Description("Можно курить на улице")]
        OnStreet = 2,
        [Description("Можно курить рядом с помещением")]
        AroundPlace = 3
    }
}
using System.ComponentModel;

namespace PlaceAgregator.Entities
{
    public enum AdministratorRuleType
    {
        [Description("Администратор не присутствует на мероприятиях")]
        NotOnAny = 0,
        [Description("Администратор присутствует на опасных видах мероприятий")]
        DangerousEvents = 1,
        [Description("Администратор присутствует на всех мероприятиях")]
        AllEvents = 2,
        [Description("Можно заказать администратора")]
        OnOrder = 3
    }
}
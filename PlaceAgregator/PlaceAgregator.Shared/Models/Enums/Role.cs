using System.ComponentModel;

namespace PlaceAgregator.Shared.Models.Enums
{
    public enum Role
    {
        [Description("user")]
        User = 0,

        [Description("manager")]
        Manager = 1,

        [Description("admin")]
        Admin = 2
    }
}

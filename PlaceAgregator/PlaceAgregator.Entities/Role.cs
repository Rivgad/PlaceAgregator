using System.ComponentModel;

namespace PlaceAgregator.Entities
{
    public enum Role
    {
        [Description("Admin")]
        Admin = 1,

        [Description("Manager")]
        Manager = 2,

        [Description("User")]
        User = 3
    }
}

using System.ComponentModel;

namespace PlaceAgregator.Shared.Models.Enums
{
    public enum Role
    {
        [Description(RoleConstants.User)]
        User = 0,

        [Description(RoleConstants.Moderator)]
        Moderator = 1,

        [Description(RoleConstants.Admin)]
        Admin = 2
    }

    public static class RoleConstants
    {
        public const string User = "user";
        public const string Moderator = "moderator";
        public const string Admin = "admin";
    }
}

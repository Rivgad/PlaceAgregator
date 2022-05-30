namespace PlaceAgregator.Shared.DTOs.Users
{
    public class UserFilterDTO : BaseFilterDTO
    {
        public string? UserName { get; set; }
        public string? UserId { get; set; }
        public string? Email { get; set; }
    }
}

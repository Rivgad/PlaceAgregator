namespace PlaceAgregator.Shared.DTOs.Comments
{
    public class CommentGetDTO
    {
        public string UserId { get; set; }
        public int PlaceId { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public DateTime LastEditTime { get; set; }
        public int Rating { get; set; }
        public bool IsBlocked { get; set; }
    }
}

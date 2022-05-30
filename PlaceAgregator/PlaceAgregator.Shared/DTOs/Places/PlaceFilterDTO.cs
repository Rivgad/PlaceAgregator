namespace PlaceAgregator.Shared.DTOs.Places
{
    public class PlaceFilterDTO : BaseFilterDTO
    {
        public string? City { get; set; }
        public int? MinCapacity { get; set; }
        public decimal? MinArea { get; set; }
        public string? Address { get; set; }
        public int? MinRating { get; set; }
        public decimal? MaxRate { get; set; }
    }
}

namespace PlaceAgregator.Shared.Models
{
    public class TimeInterval
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public Shedule Shedule { get; set; }
    }
}

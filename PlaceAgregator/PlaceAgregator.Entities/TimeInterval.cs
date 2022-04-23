using System;

namespace PlaceAgregator.Entities
{
    public class TimeInterval
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public bool Monday { get; private set; }
        public bool Tuesday { get; private set; }
        public bool Wednesday { get; private set; }
        public bool Thusday { get; private set; }
        public bool Friday { get; private set; }
        public bool Saturday { get; private set; }
        public bool Sunday { get; private set; }

    }
}

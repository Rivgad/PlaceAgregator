using PlaceAgregator.Shared.Models;

namespace PlaceAgregator.Shared.Extensions
{
    public static class TimeIntervalExtensions
    {
        public static bool IsOutOfRange(this TimeInterval timeInterval, 
            IEnumerable<TimeInterval> timeIntervals)
        {
            bool result = timeIntervals.Any(item =>
            {
                bool intersectedDate =
                     timeInterval.StartDateTime >= item.StartDateTime &&
                     timeInterval.StartDateTime <= item.EndDateTime
                     ||
                     timeInterval.EndDateTime >= item.StartDateTime &&
                     timeInterval.EndDateTime <= item.EndDateTime;

                bool shedulesAreEqual = false;
                if(timeInterval.Shedule != null && item.Shedule != null)
                    shedulesAreEqual = timeInterval.Shedule.Equals(item.Shedule);

                return intersectedDate && !shedulesAreEqual;
            });

            return !result;
        }

        public static bool IsOutOfRange(this IEnumerable<TimeInterval?> timeIntervals, 
            TimeInterval timeInterval)
        {
            bool result = timeIntervals.Any(item =>
            {
                if (item == null)
                    return false;

                bool intersectedDate =
                     timeInterval.StartDateTime >= item.StartDateTime &&
                     timeInterval.StartDateTime <= item.EndDateTime
                     ||
                     timeInterval.EndDateTime >= item.StartDateTime &&
                     timeInterval.EndDateTime <= item.EndDateTime;

                bool shedulesAreEqual = false;
                if (timeInterval.Shedule != null && item.Shedule != null)
                    shedulesAreEqual = timeInterval.Shedule.Equals(item.Shedule);

                return intersectedDate && !shedulesAreEqual;
            });

            return !result;
        }

        public static IEnumerable<TimeInterval>? GetIntersections(this IEnumerable<TimeInterval?> timeIntervals,
            TimeInterval timeInterval)
        {
            foreach (var item in timeIntervals)
            {
                if (item != null)
                {
                    bool intersectedDate =
                     timeInterval.StartDateTime >= item.StartDateTime &&
                     timeInterval.StartDateTime <= item.EndDateTime
                     ||
                     timeInterval.EndDateTime >= item.StartDateTime &&
                     timeInterval.EndDateTime <= item.EndDateTime;

                    if (intersectedDate)
                        yield return item;
                }
            }
        }
    }
}

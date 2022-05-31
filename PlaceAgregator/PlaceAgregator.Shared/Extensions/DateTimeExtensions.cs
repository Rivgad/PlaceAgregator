namespace PlaceAgregator.Shared.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime DropMinutesAndSeconds(this DateTime dateTime)
        {
            dateTime = new DateTime(
                year: dateTime.Year,
                month: dateTime.Month,
                day: dateTime.Day,
                hour: dateTime.Hour,
                minute: 0,
                second: 0);

            return dateTime;
        }
    }
}

using PlaceAgregator.Shared.Extensions;
using System;
using Xunit;

namespace PlaceAgregator.UnitTests
{
    public class DateTimeExtensionsTests
    {
        [Fact]
        public void DropMinutesAndSeconds_InputDateTimeNow_MinutesEqualsZero()
        {
            int expectedMinutes = 0;

            DateTime dateTime = new DateTime(2022, 06, 25, 12, 37, 40);
            dateTime = dateTime.DropMinutesAndSeconds();

            int actualMinutes = dateTime.Minute;

            Assert.Equal(expectedMinutes, actualMinutes);
        }

        [Fact]
        public void DropMinutesAndSeconds_InputDateTimeNow_SecondsEqualsZero()
        {
            int expectedSeconds = 0;

            DateTime dateTime = new DateTime(2050, 03, 1, 23, 21, 32);
            dateTime = dateTime.DropMinutesAndSeconds();

            int actualSeconds = dateTime.Second;

            Assert.Equal(expectedSeconds, actualSeconds);
        }

        [Fact]
        public void DropMinutesAndSeconds_InputDateTimeNow_MinutesAndSecondsEqualsZero()
        {
            int expectedSeconds = 0;
            int exptectedMinutes = 0;

            DateTime dateTime = new DateTime(2023, 12, 5, 20, 59, 01);
            dateTime = dateTime.DropMinutesAndSeconds();

            int actualSeconds = dateTime.Second;
            int actualMinutes = dateTime.Minute;

            Assert.Equal(expectedSeconds, actualSeconds);
            Assert.Equal(exptectedMinutes, actualMinutes);
        }
    }
}

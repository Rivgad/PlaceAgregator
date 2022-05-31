using PlaceAgregator.Shared.Extensions;
using System;
using Xunit;

namespace PlaceAgregator.UnitTests
{
    public class DateTimeExtensionsTests
    {
        [Fact]
        public void DropMinutesAndSeconds_InputDateTimeNow_ReturnWithoutMinutes()
        {
            int expectedMinutes = 0;

            DateTime dateTime = DateTime.Now;
            dateTime = dateTime.DropMinutesAndSeconds();
            int actualMinutes = dateTime.Minute;

            Assert.Equal(expectedMinutes, actualMinutes);
        }

        [Fact]
        public void DropMinutesAndSeconds_InputDateTimeNow_ReturnWithoutSeconds()
        {
            int expectedSeconds = 0;

            DateTime dateTime = DateTime.Now;
            dateTime = dateTime.DropMinutesAndSeconds();
            int seconds = dateTime.Second;

            Assert.Equal(expectedSeconds, seconds);
        }
    }
}

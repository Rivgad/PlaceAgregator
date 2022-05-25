using PlaceAgregator.Shared.Extensions;
using PlaceAgregator.Shared.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace PlaceAgregator.UnitTests
{
    public class TimeIntervalExtensionsTest
    {
        /// <summary>
        /// Array of dates
        /// Available date ranges:
        /// <code>
        ///            (-inf ; 01.05.2022T12:00) U 
        /// (10.05.2022T15:00; 15.05.2022T09:00) U 
        /// (20.05.2022T18:00; 25.05.2022T21:00) U 
        /// (30.05.2022T01:00; inf+)
        /// </code>
        /// </summary>
        IEnumerable<TimeInterval> _timeIntervals = new List<TimeInterval>()
        {
            new TimeInterval()
            {
                StartDateTime = new DateTime(2022, 05, 1, 12,00, 00),
                EndDateTime = new DateTime(2022, 05, 10, 15, 00, 00)
            },
            new TimeInterval()
            {
                StartDateTime = new DateTime(2022, 05, 15, 09, 00, 00),
                EndDateTime = new DateTime(2022, 05, 20, 18, 00, 00)
            },
            new TimeInterval()
            {
                StartDateTime = new DateTime(2022, 05, 25, 21, 00, 00),
                EndDateTime = new DateTime(2022, 05, 30, 01, 00, 00)
            }
        };

        [Fact]
        public void IsOutOfRange_returnsTrue()
        {
            var timeIntervals = _timeIntervals;

            TimeInterval timeInterval = new TimeInterval()
            {
                StartDateTime = new DateTime(2022, 05, 01, 01, 00, 00),
                EndDateTime = new DateTime(2022, 05, 01, 11, 59, 00),
            };
            var result = timeInterval.IsOutOfRange(timeIntervals);
            Assert.True(result);
            result = timeIntervals.IsOutOfRange(timeInterval);
            Assert.True(result);

            timeInterval = new TimeInterval()
            {
                StartDateTime = new DateTime(2022, 05, 10, 15, 01, 00),
                EndDateTime = new DateTime(2022, 05, 15, 08, 59, 00)
            };
            result = timeInterval.IsOutOfRange(timeIntervals);
            Assert.True(result);
            result = timeIntervals.IsOutOfRange(timeInterval);
            Assert.True(result);

            timeInterval = new TimeInterval()
            {
                StartDateTime = new DateTime(2022, 5, 30, 01, 01, 00),
                EndDateTime = new DateTime(2022, 6, 30, 15, 15, 00)
            };
            result = timeInterval.IsOutOfRange(timeIntervals);
            Assert.True(result);
            result = timeIntervals.IsOutOfRange(timeInterval);
            Assert.True(result);
        }

        [Fact]
        public void IsOutOfRange_returnsFalse()
        {
            TimeInterval timeInterval = new TimeInterval()
            {
                StartDateTime = new DateTime(2022, 05, 01, 01, 00,00),
                EndDateTime = new DateTime(2022, 05, 01, 12, 00, 00)
            };
            var result = timeInterval.IsOutOfRange(_timeIntervals);
            Assert.False(result);
            result = _timeIntervals.IsOutOfRange(timeInterval);
            Assert.False(result);

            timeInterval = new TimeInterval()
            {
                StartDateTime = new DateTime(2022, 05, 10, 15, 00, 00),
                EndDateTime = new DateTime(2022, 05, 15, 09, 00, 00)
            };
            result = timeInterval.IsOutOfRange(_timeIntervals);
            Assert.False(result);
            result = _timeIntervals.IsOutOfRange(timeInterval);
            Assert.False(result);

            timeInterval = new TimeInterval()
            {
                StartDateTime = new DateTime(2022, 5, 30),
                EndDateTime = new DateTime(2022, 6, 30, 15, 15 ,00)
            };
            result = timeInterval.IsOutOfRange(_timeIntervals);
            Assert.False(result);
            result = _timeIntervals.IsOutOfRange(timeInterval);
            Assert.False(result);
        }

        [Fact]
        public void IsOutOfRange_withShedule_returnsTrue()
        {
            var timeIntervals = new List<TimeInterval>()
            {
                new TimeInterval()
                {
                    StartDateTime = new DateTime(2022, 05, 1, 12,00, 00),
                    EndDateTime = new DateTime(2022, 05, 10, 15, 00, 00),
                    Shedule = new Shedule()
                    {
                        Monday = true,
                        Thuesday = true
                    }
                },
                new TimeInterval()
                {
                    StartDateTime = new DateTime(2022, 05, 15, 09, 00, 00),
                    EndDateTime = new DateTime(2022, 05, 20, 18, 00, 00)
                },
                new TimeInterval()
                {
                    StartDateTime = new DateTime(2022, 05, 25, 21, 00, 00),
                    EndDateTime = new DateTime(2022, 05, 30, 01, 00, 00)
                }
            };

            TimeInterval timeInterval = new TimeInterval()
            {
                StartDateTime = new DateTime(2022, 05, 1, 12, 00, 00),
                EndDateTime = new DateTime(2022, 05, 10, 15, 00, 00),
                Shedule = new Shedule()
                {
                    Friday = true,
                    Saturday = true
                }
            };
            var result = timeInterval.IsOutOfRange(timeIntervals);
            Assert.False(result);
            result = timeIntervals.IsOutOfRange(timeInterval);
            Assert.False(result);
        }

        [Fact]
        public void IsOutOfRange_withShedule_returnsFalse()
        {
            var timeIntervals = new List<TimeInterval>()
            {
                new TimeInterval()
                {
                    StartDateTime = new DateTime(2022, 05, 1, 12,00, 00),
                    EndDateTime = new DateTime(2022, 05, 10, 15, 00, 00),
                    Shedule = new Shedule()
                    {
                        Monday = true,
                        Thuesday = true
                    }
                },
                new TimeInterval()
                {
                    StartDateTime = new DateTime(2022, 05, 15, 09, 00, 00),
                    EndDateTime = new DateTime(2022, 05, 20, 18, 00, 00)
                },
                new TimeInterval()
                {
                    StartDateTime = new DateTime(2022, 05, 25, 21, 00, 00),
                    EndDateTime = new DateTime(2022, 05, 30, 01, 00, 00)
                }
            };

            TimeInterval timeInterval = new TimeInterval()
            {
                StartDateTime = new DateTime(2022, 05, 1, 12, 00, 00),
                EndDateTime = new DateTime(2022, 05, 10, 15, 00, 00),
                Shedule = new Shedule()
                {
                    Monday = true,
                    Thuesday = true
                }
            };
            var result = timeInterval.IsOutOfRange(timeIntervals);
            Assert.False(result);
            result = timeIntervals.IsOutOfRange(timeInterval);
            Assert.False(result);
        }
    }
}
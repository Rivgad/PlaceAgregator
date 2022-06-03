using PlaceAgregator.Shared.Extensions;
using PlaceAgregator.Shared.Models;
using System.Collections.Generic;
using Xunit;

namespace PlaceAgregator.UnitTests
{
    public class PlaceExtensionsTests
    {
        [Theory]
        [InlineData(232, 3, 10)]
        [InlineData(300, 1, 1)]
        [InlineData(500, 10, 10)]
        public void GetPrice_Discount20Charge20_ReturnsBaseRateMultiplyByHours(decimal baseRate, int hours, int guestsQuantity)
        {
            Place place = new Place();
            place.BaseRate = baseRate;
            place.Discounts = new List<Discount>()
            {
                new Discount() { FromHoursQuantity=1, Procents=20 }
            };
            place.Charges = new List<Charge>()
            {
                new Charge() { FromGuestsQuantity=1, Procents=20 }
            };

            decimal expected = (decimal)(place.BaseRate * hours);
            decimal actual = place.GetPrice(hours, guestsQuantity);

            Assert.Equal(expected, actual);
        }
    }
}

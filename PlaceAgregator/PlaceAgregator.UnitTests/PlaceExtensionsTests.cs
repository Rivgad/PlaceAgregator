using PlaceAgregator.Shared.Extensions;
using PlaceAgregator.Shared.Models;
using System.Collections.Generic;
using Xunit;

namespace PlaceAgregator.UnitTests
{
    public class PlaceExtensionsTests
    {
        [Theory]
        [InlineData(3, 5)]
        [InlineData(1, 1)]
        [InlineData(10, 10)]
        public void GetPrice_Discount20Charge20BaseRate400_ReturnsBaseRateMultipliedByHours(int hours, int guestsQuantity)
        {
            Place place = new Place();
            place.BaseRate = 400;

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

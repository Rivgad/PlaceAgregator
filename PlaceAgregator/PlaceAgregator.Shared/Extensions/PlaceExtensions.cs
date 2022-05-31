using PlaceAgregator.Shared.Models;

namespace PlaceAgregator.Shared.Extensions
{
    public static class PlaceExtensions
    {
        public static decimal GetPrice(this Place place, int hours, int guestsQuantity)
        {
            if(place.BaseRate == null)
                return 0;

            decimal totalDiscount = place.Discounts
                .Where(item => item.FromHoursQuantity <= hours)
                .Select(item => item.Procents)
                .Sum();

            decimal totalCharge = place.Charges
                .Where(item => item.FromGuestsQuantity <= guestsQuantity)
                .Select(item => item.Procents)
                .Sum();

            decimal totalPrice = (decimal)place.BaseRate * hours * ((1 - totalDiscount / 100) + (1 + totalCharge / 100)) / 2;

            return totalPrice;
        }
    }
}

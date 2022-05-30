using PlaceAgregator.Shared.DTOs.Users;
using PlaceAgregator.Shared.Models;
using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.DTOs.Places
{
    public class GetPlaceDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("owner")]
        public AppUserGetDTO User { get; set; }

        [JsonPropertyName("waterTypeId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? WaterTypeId { get; set; }

        [JsonPropertyName("buildingTypeId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? BuildingTypeId { get; set; }

        [JsonPropertyName("parkingTypeId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? ParkingTypeId { get; set; }

        [JsonPropertyName("prohibitions")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int[]? ProhibitionIds { get; set; }

        [JsonPropertyName("eventTypeIds")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int[]? EventTypeIds { get; set; }


        [JsonPropertyName("serviceItems")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<ServiceItemGetDTO>? ServiceItems { get; set; }

        [JsonPropertyName("discounts")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<DiscountGetDTO>? Discounts { get; set; }

        [JsonPropertyName("charges")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<ChargeGetDTO>? Charges { get; set; }

        [JsonPropertyName("photos")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<PlacePhoto>? Photos { get; set; }

        [JsonPropertyName("shedule")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Shedule? Shedule { get; set; }

        [JsonPropertyName("title")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Title { get; set; }

        [JsonPropertyName("baseRate")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? BaseRate { get; set; }

        [JsonPropertyName("photo")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public byte[]? Photo { get; set; }

        [JsonPropertyName("city")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string City { get; set; }

        [JsonPropertyName("address")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Address { get; set; }

        [JsonPropertyName("description")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Description { get; set; }

        [JsonPropertyName("capacity")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Capacity { get; set; }

        [JsonPropertyName("area")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? Area { get; set; }

        [JsonPropertyName("cellingHeight")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? CellingHeight { get; set; }

        [JsonPropertyName("socketsQuantity")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? SocketsQuantity { get; set; }

        [JsonPropertyName("maleToiletsQuantity")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? MaleToiletsQuantity { get; set; }

        [JsonPropertyName("femaleToiletsQuantity")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? FemaleToiletsQuantity { get; set; }

        [JsonPropertyName("sharedToiletsQuantity")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? SharedToiletsQuantity { get; set; }

        [JsonPropertyName("parkingSpace")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? ParkingSpace { get; set; }

        [JsonPropertyName("floor")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Floor { get; set; }

        [JsonPropertyName("floorsQuantity")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? FloorsQuantity { get; set; }

        [JsonPropertyName("hasElevator")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? HasElevator { get; set; }

        [JsonPropertyName("hasFreightElevator")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? HasFreightElevator { get; set; }

        [JsonPropertyName("hasDisabledEntrance")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? HasDisabledEntrance { get; set; }

        [JsonPropertyName("bookingHorizonInDays")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? BookingHorizonInDays { get; set; }
    }
}

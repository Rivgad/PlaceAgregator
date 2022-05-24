using PlaceAgregator.Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.DTOs.Places
{
    public class PlaceUpdateDTO
    {
        [Range(0, int.MaxValue)]
        [JsonPropertyName("waterTypeId")]
        public int? WaterTypeId { get; set; }

        [Range(0, int.MaxValue)]
        [JsonPropertyName("buildingTypeId")]
        public int? BuildingTypeId { get; set; }

        [Range(0, int.MaxValue)]
        [JsonPropertyName("parkingTypeId")]
        public int? ParkingTypeId { get; set; }

        [JsonPropertyName("rules")]
        public int[]? RuleIds { get; set; }

        [JsonPropertyName("prohibitions")]
        public int[]? ProhibitionIds { get; set; }

        [JsonPropertyName("eventTypeIds")]
        public int[]? EventTypeIds { get; set; }


        [JsonPropertyName("shedule")]
        public SheduleDTO? Shedule { get; set; }

        [JsonPropertyName("title")]
        [Required]
        public string? Title { get; set; }

        [JsonPropertyName("baseRate")]
        public decimal? BaseRate { get; set; }

        [JsonPropertyName("photo")]
        public byte[]? Photo { get; set; }

        [JsonPropertyName("address")]
        [Required]
        public string? Address { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("capacity")]

        public int? Capacity { get; set; }

        [JsonPropertyName("area")]
        [Range(0, float.PositiveInfinity)]
        public decimal? Area { get; set; }

        [JsonPropertyName("cellingHeight")]
        [Range(0, float.PositiveInfinity)]
        public decimal? CellingHeight { get; set; }

        [JsonPropertyName("socketsQuantity")]
        [Range(0, int.MaxValue)]
        public int? SocketsQuantity { get; set; }

        [JsonPropertyName("maleToiletsQuantity")]
        [Range(0, int.MaxValue)]
        public int? MaleToiletsQuantity { get; set; }

        [JsonPropertyName("femaleToiletsQuantity")]
        [Range(0, int.MaxValue)]
        public int? FemaleToiletsQuantity { get; set; }

        [JsonPropertyName("sharedToiletsQuantity")]
        [Range(0, int.MaxValue)]
        public int? SharedToiletsQuantity { get; set; }

        [JsonPropertyName("parkingSpace")]
        [Range(0, int.MaxValue)]
        public int? ParkingSpace { get; set; }

        [JsonPropertyName("floor")]
        [Range(0, int.MaxValue)]
        public int? Floor { get; set; }

        [JsonPropertyName("floorsQuantity")]
        [Range(0, int.MaxValue)]
        public int? FloorsQuantity { get; set; }

        [JsonPropertyName("hasElevator")]
        public bool? HasElevator { get; set; }

        [JsonPropertyName("hasFreightElevator")]
        public bool? HasFreightElevator { get; set; }

        [JsonPropertyName("hasDisabledEntrance")]
        public bool? HasDisabledEntrance { get; set; }

        [JsonPropertyName("bookingHorizonInDays")]
        [Range(0, int.MaxValue)]
        public int? BookingHorizonInDays { get; set; }
    }
}

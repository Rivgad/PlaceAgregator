﻿using PlaceAgregator.Shared.Models.Types;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlaceAgregator.Shared.Models
{
    public class Place : Entity
    {
        [JsonIgnore]
        public bool IsBlocked { get; set; } = false;

        #region FK

        public string UserId { get; set; }

        [JsonIgnore]
        public AppUser User { get; set; }

        #endregion

        #region Relations

        public IEnumerable<BookingRequest>? BookingRequests { get; set; }
        public IEnumerable<ServiceItem>? ServiceItems { get; set; }
        public IEnumerable<Comment>? Comments { get; set; }
        public IEnumerable<PlacePhoto>? Photos { get; set; }
        public IEnumerable<Prohibition>? Prohibitions { get; set; }
        public IEnumerable<EventType>? EventTypes { get; set; }
        public IEnumerable<Discount>? Discounts { get; set; }
        public IEnumerable<Charge>? Charges { get; set; }

        #endregion

        #region Properties

        public Shedule? Shedule { get; set; }

        public string Title { get; set; }

        public decimal? BaseRate { get; set; }

        public bool IsActive { get; set; } = false;

        public decimal Rating { get; set; } = 0;

        public byte[]? Photo { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string? Description { get; set; }

        public int? Capacity { get; set; }

        public decimal? Area { get; set; }

        public decimal? CellingHeight { get; set; }

        public int? SocketsQuantity { get; set; }

        public int? MaleToiletsQuantity { get; set; }

        public int? FemaleToiletsQuantity { get; set; }

        public int? SharedToiletsQuantity { get; set; }

        public int? ParkingSpace { get; set; }

        public int? Floor { get; set; }

        public int? FloorsQuantity { get; set; }

        [Required]
        public bool HasElevator { get; set; } = false;

        [Required]
        public bool HasFreightElevator { get; set; } = false;

        [Required]
        public bool HasDisabledEntrance { get; set; } = false;

        public int? BookingHorizonInDays { get; set; }

        #endregion

    }
}

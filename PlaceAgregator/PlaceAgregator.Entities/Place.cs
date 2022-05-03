using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlaceAgregator.Entities
{
    /// <summary>
    /// Class <see cref="Place"/>
    /// </summary>
    public class Place : Entity
    {
        /// <summary>
        /// Id of the <see cref="Entities.User"/> who owns the <see cref="Place"/>
        /// </summary>
        [Required]
        public int UserId { get;  set; }

        [Required]
        public User User { get;  set; }

        public bool IsActive { get;  set; } = false;

        [Required]
        [StringLength(maximumLength: 250, MinimumLength = 20)]
        public string? Title { get;  set; }

        [Required]
        [Range(minimum: 0.0, maximum: double.PositiveInfinity)]
        public decimal? BaseRate { get;  set; } = decimal.Zero;

        [Required]
        [Range(minimum: 0, maximum: 5)]
        public float? Rating { get;  set; } = 0;

        public byte[]? Photo { get;  set; }

        #region Place location

        [Required]
        [MinLength(1)]
        public string City { get;  set; }

        [MinLength(3)]
        public string Adress { get;  set; }

        [MinLength(3)]
        public string? MailIndex { get;  set; }

        #endregion

        #region Building description

        [MinLength(2)]
        public string? Description { get;  set; }

        [Range(minimum: 0, maximum: int.MaxValue)]
        public int? Capacity { get;  set; }

        [Range(minimum: 0, maximum: float.PositiveInfinity)]
        public float? Area { get;  set; }

        [Range(minimum: 0, maximum: float.PositiveInfinity)]
        public float? CellingHeight { get;  set; }

        public WaterType? WaterType { get;  set; }


        [Range(minimum: 0, maximum: ushort.MaxValue)]
        public ushort SocketsQuantity { get;  set; } = 0;


        [Range(minimum: 0, maximum: ushort.MaxValue)]
        public ushort MaleToiletsQuantity { get;  set; } = 0;


        [Range(minimum: 0, maximum: ushort.MaxValue)]
        public ushort FemaleToiletsQuantity { get;  set; } = 0;


        [Range(minimum: 0, maximum: ushort.MaxValue)]
        public ushort SharedToiletsQuantity { get;  set; } = 0;

        public int? BuildingTypeId { get;  set; }
        public BuildingType? BuildingType { get;  set; }


        public ParkingType? ParkingType { get;  set; }


        [Range(minimum: 0, maximum: ushort.MaxValue)]
        public ushort? ParkingSpace { get;  set; }


        [Range(minimum: 0, maximum: ushort.MaxValue)]
        public ushort? Floor { get;  set; }


        [Range(minimum: 0, maximum: ushort.MaxValue)]
        public ushort? FloorsQuantity { get;  set; }


        [Required]
        public bool HasElevator { get;  set; } = false;

        [Required]
        public bool HasFreightElevator { get;  set; } = false;

        [Required]
        public bool HasDisabledEntrance { get;  set; } = false;

        #endregion

        public int BookingHorizonInDays { get;  set; }

        public IEnumerable<ServiceItem>? ServiceItems { get;  set; } = new List<ServiceItem>();
        public IEnumerable<BookingRequest>? BookingRequests { get;  set; } = new List<BookingRequest>();
        public IEnumerable<PlacePhoto>? Photos { get;  set; } = new List<PlacePhoto>();
        public IEnumerable<EventType>? AvailableEvents { get; set; } = new List<EventType>();

        public IEnumerable<Prohibition>? Prohibitions { get; set; } = new List<Prohibition>();
        public IEnumerable<Permission>? Permissions { get;  set; } = new List<Permission>();
        public IEnumerable<Rule>? Rules { get; set; } = new List<Rule>();

        public IEnumerable<Rate>? Rates { get; set; } = new List<Rate>();
        public IEnumerable<Charge>? Charges { get; set; } = new List<Charge>();
        public IEnumerable<Discount>? Discounts { get; set; } = new List<Discount>();
        public IEnumerable<Comment> Comments { get; set; } = new List<Comment>();
    }
}

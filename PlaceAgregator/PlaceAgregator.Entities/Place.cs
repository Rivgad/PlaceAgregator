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
        public long UserId { get; private set; }

        [Required]
        public User User { get; private set; }

        public bool IsActive { get; private set; }

        [Required]
        [StringLength(maximumLength: 250, MinimumLength = 20)]
        public string Title { get; private set; }

        [Required]
        [Range(minimum: 0.0, maximum: double.PositiveInfinity)]
        public decimal BaseRate { get; private set; } = decimal.Zero;

        [Required]
        [Range(minimum: 0, maximum: 5)]
        public float Rating { get; private set; } = 0;


        #region Place location

        [Required]
        [MinLength(1)]
        public string City { get; private set; }

        [MinLength(3)]
        public string Adress { get; private set; }

        [MinLength(3)]
        public string? MailIndex { get; private set; }

        #endregion

        #region Building description

        [MinLength(2)]
        public string Description { get; private set; }

        [Range(minimum: 0, maximum: int.MaxValue)]
        public int Capacity { get; private set; }

        [Range(minimum: 0, maximum: float.PositiveInfinity)]
        public float Area { get; private set; }

        [Range(minimum: 0, maximum: float.PositiveInfinity)]
        public float CellingHeight { get; private set; }

        public WaterType? WaterType { get; private set; }


        [Range(minimum: 0, maximum: ushort.MaxValue)]
        public ushort SocketsQuantity { get; private set; }


        [Range(minimum: 0, maximum: ushort.MaxValue)]
        public ushort MaleToiletsQuantity { get; private set; }


        [Range(minimum: 0, maximum: ushort.MaxValue)]
        public ushort FemaleToiletsQuantity { get; private set; }


        [Range(minimum: 0, maximum: ushort.MaxValue)]
        public ushort SharedToiletsQuantity { get; private set; }

        public long BuildingTypeId { get; private set; }
        public BuildingType BuildingType { get; private set; }


        public ParkingType ParkingType { get; private set; }


        [Range(minimum: 0, maximum: ushort.MaxValue)]
        public ushort ParkingSpace { get; private set; }


        [Range(minimum: 0, maximum: ushort.MaxValue)]
        public ushort Floor { get; private set; }


        [Range(minimum: 0, maximum: ushort.MaxValue)]
        public ushort FloorsQuantity { get; private set; }


        [Required]
        public bool HasElevator { get; private set; } = false;

        [Required]
        public bool HasFreightElevator { get; private set; } = false;

        [Required]
        public bool HasDisabledEntrance { get; private set; } = false;

        #endregion

        public int BookingHorizonInDays { get; private set; }

        public IEnumerable<ServiceItem>? ServiceItems { get; private set; }
        public IEnumerable<BookingRequest>? BookingRequests { get; private set; }
        public IEnumerable<PlacePhoto>? Photos { get; private set; }
        public IEnumerable<EventType>? AvailableEvents { get; private set; }

        public IEnumerable<Prohibition>? Prohibitions { get; private set; }
        public IEnumerable<Permission>? Permissions { get; private set; }
        public IEnumerable<Rule>? Rules { get; private set; }

        public IEnumerable<Rate>? Rates { get; private set; }
        public IEnumerable<Charge>? Charges { get; private set; }
        public IEnumerable<Discount>? Discounts { get; private set; }
        public IEnumerable<Comment>? Comments { get; private set; }
    }
}

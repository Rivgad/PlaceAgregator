using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlaceAgregator.Shared.Extensions;
using PlaceAgregator.Shared.Models;
using PlaceAgregator.Shared.Models.Types;

namespace PlaceAgregator.EntityFramework
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole, string>
    {
        public DbSet<BookingRequest> BookingRequests { get; set; }
        public DbSet<BookingRequestServiceItem> BookingRequestServiceItems { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<ServiceItem> ServiceItems { get; set; }
        public DbSet<Charge> Charges { get; set; }
        public DbSet<Discount> Discounts { get; set; }

        public DbSet<Prohibition> Prohibitions { get; set; }
        public DbSet<BuildingType> BuildingTypes { get; set; }
        public DbSet<ParkingType> ParkingTypes { get; set; }
        public DbSet<WaterType> WaterTypes { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<PlacePhoto> PlacePhotos { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyUtcDateTimeConverter();

            modelBuilder.Entity<Place>()
                .OwnsOne(item => item.Shedule);

            modelBuilder.Entity<Place>()
                .HasMany(item => item.BookingRequests)
                .WithOne(item => item.Place)
                .HasForeignKey(item => item.PlaceId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<BookingRequest>()
                .HasOne(item => item.Place)
                .WithMany(item => item.BookingRequests)
                .HasForeignKey(item => item.PlaceId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<BookingRequest>()
                .HasOne(item => item.User)
                .WithMany(item => item.BookingRequests)
                .HasForeignKey(item => item.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookingRequestServiceItem>(
                j =>
                {
                    j.HasKey(item => new { item.ServiceItemId, item.BookingRequestId });
                    j.HasOne(item => item.ServiceItem)
                        .WithMany(item => item.BookingRequestServiceItems)
                        .HasForeignKey(item => item.ServiceItemId)
                        .OnDelete(DeleteBehavior.Cascade);
                    j.HasOne(item => item.BookingRequest)
                        .WithMany(item => item.ServiceItems)
                        .HasForeignKey(item => item.BookingRequestId)
                        .OnDelete(DeleteBehavior.Cascade);
                    j.Property(item => item.Quantity).IsRequired();
                });

            modelBuilder.Entity<Comment>(
                j =>
                {
                    j.HasKey(item => new { item.PlaceId, item.UserId });
                    j.Property(item => item.IsBlocked).HasDefaultValue(false);
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
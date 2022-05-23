using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Charge> Charges { get; set; }
        public DbSet<Discount> Discounts { get; set; }

        public DbSet<Prohibition> Prohibitions { get; set; }
        public DbSet<BuildingType> BuildingTypes { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<PlacePhoto> PlacePhotos { get; set; }
        public DbSet<Rule> Rules { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rate>()
                .OwnsOne(item => item.TimeInterval, ti =>
                {
                    ti.OwnsOne(item => item.Shedule);
                });

            modelBuilder.Entity<Charge>()
                .OwnsOne(item => item.TimeInterval, ti =>
                {
                    ti.OwnsOne(item => item.Shedule);
                });

            modelBuilder.Entity<Discount>()
                .OwnsOne(item => item.TimeInterval, ti =>
                {
                    ti.OwnsOne(item => item.Shedule);
                });

            modelBuilder.Entity<Place>()
                .OwnsOne(item => item.Shedule);


            modelBuilder.Entity<BookingRequestServiceItem>()
                .HasKey(item => new { item.ServiceItemId, item.BookingRequestId });

            modelBuilder.Entity<Comment>()
                .HasKey(item => new { item.PlaceId, item.UserId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
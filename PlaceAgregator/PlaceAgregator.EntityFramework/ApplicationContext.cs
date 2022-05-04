using Microsoft.EntityFrameworkCore;
using PlaceAgregator.Entities;

namespace PlaceAgregator.EntityFramework
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<BookingRequest> BookingRequests{ get; set; }
        public DbSet<BookingRequestServiceItem> BookingRequestServiceItems { get; set; }
        public DbSet<BuildingType> BuildingTypes { get; set; }
        public DbSet<Charge> Charges{ get; set; }
        public DbSet<Comment>Comments{ get; set; }
        public DbSet<Discount> Discounts{ get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Photo> Photos{ get; set; }
        public DbSet<Place> Places{ get; set; }
        public DbSet<PlacePhoto> PlacePhotos { get; set; }
        public DbSet<Prohibition> Prohibitions{ get; set; }
        public DbSet<Rate> Rates{ get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<ServiceItem> ServiceItems{ get; set; }
        public DbSet<User> Users{ get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=PlaceAgregator;Username=postgres;Password=13lu@if3r08");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rate>()
                .OwnsOne(item => item.TimeInterval);

            modelBuilder.Entity<Charge>()
                .OwnsOne(item => item.TimeInterval);

            modelBuilder.Entity<Discount>()
                .OwnsOne(item => item.TimeInterval);

            modelBuilder.Entity<Admin>()
                .HasOne(item => item.Account)
                .WithOne(item => item.Admin)
                .HasForeignKey<Admin>(item => item.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Manager>()
                .HasOne(item => item.Account)
                .WithOne(item => item.Manager)
                .HasForeignKey<Manager>(item => item.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Account>()
                .HasOne(item => item.User)
                .WithOne(item => item.Account)
                .HasForeignKey<User>(item => item.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookingRequestServiceItem>(
                j =>
                {
                    j.HasKey(item => new { item.ServiceItemId, item.BookingRequestId });
                    j.Property(item => item.Quantity);
                }
                );
            modelBuilder.Entity<PlacePhoto>(
                j =>
                {
                    j.HasKey(item => new { item.PlaceId, item.PhotoId });
                    j.Property(item => item.Position);
                }
                );
        }
    }
}
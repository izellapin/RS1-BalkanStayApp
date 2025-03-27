using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RS1_2024_25.API.Data.Models;
using RS1_2024_25.API.Data.Models.Auth;

namespace RS1_2024_25.API.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<MyAuthenticationToken> MyAuthenticationTokens { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Administrator> Administrators { get; set; }

        public DbSet<TwoFactorAuth> TwoFactorAuths { get; set; }

        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<OwnerReview> OwnerReviews { get; set; }
        public DbSet<ApartmentImage> ApartmentImages { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<ApartmentRule> ApartmentRules { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<ApartmentAmenity> ApartmentAmenities { get; set; }
        public DbSet<Toiletry> Toiletries { get; set; }
        public DbSet<ApartmentToiletry> ApartmentToiletries { get; set; }



        public DbSet<ContactMessage> ContactMessages { get; set; }

        public DbSet<UserImage> UserImages { get; set; }
        public DbSet<OwnerImage> OwnerImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfigurišite TPT nasleđivanje
            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Owner>().ToTable("Owner");
            modelBuilder.Entity<Administrator>().ToTable("Administrator");


            // Sprečavanje kaskadnog brisanja
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }

            ApplicationDbContextSeed.Seed(modelBuilder);

            // Dodatne konfiguracije
            OnModelCreatingPartial(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
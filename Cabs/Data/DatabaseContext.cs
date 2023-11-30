using Cabs.Areas.Website.ModelDtos;
using Cabs.Areas.Website.Models;
using Cabs.Areas.Website.Models.Authenication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cabs.Data
{
    public class DatabaseContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("Users").HasKey(u => u.Id);
            modelBuilder.Entity<RoleModel>()
                .HasKey(r => r.Id);

            // Thiết lập mối quan hệ one-to-one giữa User và Company
            modelBuilder.Entity<User>()
               .HasOne(u => u.Company)
               .WithOne(c => c.User)
               .HasForeignKey<CompanyModel>(c => c.UserFkId)
               .OnDelete(DeleteBehavior.NoAction);

            // Thiết lập mối quan hệ one-to-many giữa User và Driver
            modelBuilder.Entity<User>()
            .HasMany(c => c.Drivers)
            .WithOne(f => f.User)
            .HasForeignKey(f => f.UserFkId)
            .OnDelete(DeleteBehavior.NoAction);

            //// Thiết lập mối quan hệ one-to-many giữa RoleUser và User
            //modelBuilder.Entity<User>()
            //    .HasOne(u => u.RoleUser)
            //    .WithMany(ru => ru.Users)
            //    .HasForeignKey(u => u.RoleUserFkId)
            //    .OnDelete(DeleteBehavior.NoAction);

            // Thiết lập mối quan hệ one-to-many giữa Driver và Image
            modelBuilder.Entity<ImageModel>()
                .HasOne(u => u.Driver)
                .WithMany(ru => ru.Images)
                .HasForeignKey(u => u.DriverFkId)
                .OnDelete(DeleteBehavior.NoAction);

            // Thiết lập mối quan hệ one-to-many giữa Company và Image
            modelBuilder.Entity<ImageModel>()
                .HasOne(u => u.Company)
                .WithMany(ru => ru.Images)
                .HasForeignKey(u => u.CompanyFkId)
                .OnDelete(DeleteBehavior.NoAction);

            // Thiết lập mối quan hệ many-to-one giữa Driver và Company
            modelBuilder.Entity<CompanyModel>()
            .HasMany(c => c.Drivers)
            .WithOne(d => d.Company)
            .HasForeignKey(d => d.CompanyFkId)
            .OnDelete(DeleteBehavior.NoAction);

            // Thiết lập mối quan hệ many-to-one giữa Feedback và User
            modelBuilder.Entity<User>()
            .HasMany(c => c.Feedbacks)
            .WithOne(f => f.User)
            .HasForeignKey(f => f.CustomerFkId)
            .OnDelete(DeleteBehavior.NoAction);

            // Thiết lập mối quan hệ many-to-one giữa Booking và User
            modelBuilder.Entity<User>()
            .HasMany(c => c.Bookings)
            .WithOne(b => b.User)
            .HasForeignKey(b => b.CustomerFkId)
            .OnDelete(DeleteBehavior.NoAction);

            // Thiết lập mối quan hệ many-to-one giữa Advertise và Company
            modelBuilder.Entity<CompanyModel>()
            .HasMany(c => c.Advertises)
            .WithOne(a => a.Company)
            .HasForeignKey(a => a.CompanyFkId)
            .OnDelete(DeleteBehavior.NoAction);

            // Thiết lập mối quan hệ many-to-one giữa Advertise và Driver
            modelBuilder.Entity<DriverModel>()
            .HasMany(c => c.Advertises)
            .WithOne(a => a.Driver)
            .HasForeignKey(a => a.DriverFkId)
            .OnDelete(DeleteBehavior.NoAction);

            // Thiết lập mối quan hệ many-to-many giữa Booking và Company
            modelBuilder.Entity<BookingCompany>()
            .HasKey(bc => new { bc.BookingFkId, bc.CompanyFkId });

            modelBuilder.Entity<BookingCompany>()
                .HasOne(bc => bc.Booking)
                .WithMany(b => b.BookingCompanies)
                .HasForeignKey(bc => bc.BookingFkId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BookingCompany>()
                .HasOne(bc => bc.Company)
                .WithMany(c => c.BookingCompanies)
                .HasForeignKey(bc => bc.CompanyFkId)
                .OnDelete(DeleteBehavior.NoAction);

            // Thiết lập mối quan hệ many-to-many giữa Image và Advertise
            modelBuilder.Entity<AdvertiseImageModels>()
                .HasKey(ia => new { ia.ImageId, ia.AdvertiseId });

            modelBuilder.Entity<AdvertiseImageModels>()
                .HasOne(ia => ia.Image)
                .WithMany(image => image.ImageAdvertises)
                .HasForeignKey(ia => ia.ImageId);

            modelBuilder.Entity<AdvertiseImageModels>()
                .HasOne(ia => ia.Advertise)
                .WithMany(advertise => advertise.AdvertiseImages)
                .HasForeignKey(ia => ia.AdvertiseId);

            // Thiết lập mối quan hệ many-to-many giữa Booking và Driver
            modelBuilder.Entity<BookingDriver>()
            .HasKey(bd => new { bd.BookingFkId, bd.DriverFkId });

            modelBuilder.Entity<BookingDriver>()
                .HasOne(bd => bd.Booking)
                .WithMany(b => b.BookingDrivers)
                .HasForeignKey(bd => bd.BookingFkId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BookingDriver>()
                .HasOne(bd => bd.Driver)
                .WithMany(d => d.BookingDrivers)
                .HasForeignKey(bd => bd.DriverFkId)
                .OnDelete(DeleteBehavior.NoAction);

            // Thiết lập mối quan hệ một-một giữa Booking và Payment
            modelBuilder.Entity<BookingModel>()
                .HasOne(b => b.Payment)
                .WithOne(p => p.Booking)
                .HasForeignKey<PaymentModel>(p => p.BookingFkId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public DbSet<AdvertiseModel>? Advertises { get; set; }
        public DbSet<BookingModel>? Bookings { get; set; }
        public DbSet<CompanyModel>? Companies { get; set; }
        public DbSet<CompanyDto>? CompanyDtos { get; set; }
        public DbSet<DriverModel>? Drivers { get; set; }
        public DbSet<FeedbackModel>? Feedbacks { get; set; }
        public DbSet<PaymentModel>? Payments { get; set; }
        public DbSet<ImageModel>? Images { get; set; }
        public DbSet<AdvertiseImageModels>? AdvertiseImages { get; set; }
        public DbSet<BookingCompany>? BookingCompanies { get; set; }

        public DbSet<BookingDriver>? BookingDrivers { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<RoleModel>? Roles { get; set; }
    }
}



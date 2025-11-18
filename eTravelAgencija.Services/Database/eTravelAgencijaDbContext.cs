using System;
using System.Collections.Generic;
using System.Linq;
using eTravelAgencija.Services.Database;
using eTravelAgencija.Services.Database.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class eTravelAgencijaDbContext
    : IdentityDbContext<User, Role, int,
        IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    public eTravelAgencijaDbContext(DbContextOptions<eTravelAgencijaDbContext> options)
        : base(options)
    {
    }

    public DbSet<Offer> Offers { get; set; }
    public DbSet<OfferDetails> OfferDetails { get; set; }
    public DbSet<OfferImage> OfferImages { get; set; }
    public DbSet<OfferPlanDay> OfferPlanDays { get; set; }
    public DbSet<OfferCategory> OfferCategories { get; set; }
    public DbSet<OfferSubCategory> OfferSubCategories { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<OfferHotels> OfferHotels { get; set; }
    public DbSet<HotelImages> HotelImages { get; set; }
    public DbSet<HotelRooms> HotelRooms { get; set; }
    public DbSet<Rooms> Rooms { get; set; }
    public DbSet<Voucher> Vouchers { get; set; }
    public DbSet<UserVoucher> UserVouchers { get; set; }
    public DbSet<UserToken> UserTokens { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<WorkApplication> WorkApplications { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserRole>(ur =>
        {
            ur.ToTable("AspNetUserRoles");

            ur.HasKey(x => new { x.UserId, x.RoleId });

            ur.HasOne<User>()
                .WithMany(u => u.UserRoles)
                .HasForeignKey(x => x.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            ur.HasOne<Role>()
                .WithMany(r => r.UserRoles)
                .HasForeignKey(x => x.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<OfferHotels>()
        .HasKey(oh => new { oh.OfferDetailsId, oh.HotelId });

        builder.Entity<OfferPlanDay>()
    .HasKey(opd => new { opd.OfferDetailsId, opd.DayNumber });


        builder.Entity<HotelRooms>()
        .HasKey(hr => new { hr.HotelId, hr.RoomId });

        builder.Entity<HotelRooms>()
            .HasOne(hr => hr.Hotel)
            .WithMany(h => h.HotelRooms)
            .HasForeignKey(hr => hr.HotelId);

        builder.Entity<HotelRooms>()
            .HasOne(hr => hr.Rooms)
            .WithMany()
            .HasForeignKey(hr => hr.RoomId);

        builder.Entity<UserVoucher>()
        .HasKey(uv => new { uv.UserId, uv.VoucherId });

        builder.Entity<Payment>()
        .HasKey(p => new { p.ReservationId, p.RateId });

        SeedData.Seed(builder);

    }
}





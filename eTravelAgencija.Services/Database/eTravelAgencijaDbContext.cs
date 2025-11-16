using System;
using System.Collections.Generic;
using System.Linq;
using eTravelAgencija.Services.Database;
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
    public DbSet<WorkApplication> WorkApplications{ get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var hasher = new PasswordHasher<User>();

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

        builder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "Korisnik", NormalizedName = "KORISNIK", Description = "Osnovna korisnička rola" },
            new Role { Id = 2, Name = "Radnik", NormalizedName = "RADNIK", Description = "Zaposleni koji upravlja ponudama i rezervacijama" },
            new Role { Id = 3, Name = "Direktor", NormalizedName = "DIREKTOR", Description = "Administrator sistema" }
        );

        builder.Entity<Rate>().HasData(
        new Rate
        {
            Id = 1,
            Name = "Prva rata",
            OrderNumber = 1
        },
        new Rate
        {
            Id = 2,
            Name = "Druga rata",
            OrderNumber = 2
        },
        new Rate
        {
            Id = 3,
            Name = "Treća rata",
            OrderNumber = 3
        },
        new Rate
        {
            Id = 4,
            Name = "Puni iznos",
            OrderNumber = 0
        }
    );


        builder.Entity<User>().HasData(
    new User
    {
        Id = 1,
        UserName = "radnik",
        NormalizedUserName = "RADNIK",
        Email = "radnik@etravel.com",
        NormalizedEmail = "RADNIK@ETRAVEL.COM",
        EmailConfirmed = true,
        FirstName = "Marko",
        LastName = "Radnik",
        PhoneNumber = "+38761111111",
        isBlocked = false,
        DateBirth = new DateTime(1990, 5, 10, 0, 0, 0, DateTimeKind.Utc),
        MainImage = "test",
        CreatedAt = DateTime.UtcNow,
        PasswordHash = hasher.HashPassword(null, "Radnik123!")
    },
    new User
    {
        Id = 2,
        UserName = "direktor",
        NormalizedUserName = "DIREKTOR",
        Email = "direktor@etravel.com",
        NormalizedEmail = "DIREKTOR@ETRAVEL.COM",
        EmailConfirmed = true,
        FirstName = "Amir",
        LastName = "Direktor",
        PhoneNumber = "+38762222222",
        isBlocked = false,
        DateBirth = new DateTime(1985, 3, 20, 0, 0, 0, DateTimeKind.Utc),
        MainImage = "test",
        CreatedAt = DateTime.UtcNow,
        PasswordHash = hasher.HashPassword(null, "Direktor123!")
    },
    new User
    {
        Id = 4,
        UserName = "korisnik",
        NormalizedUserName = "KORISNIK",
        Email = "korisnik@etravel.com",
        NormalizedEmail = "KORISNIK@ETRAVEL.COM",
        EmailConfirmed = true,
        FirstName = "Ajdin",
        LastName = "Korisnik",
        PhoneNumber = "+38763333333",
        isBlocked = false,
        DateBirth = new DateTime(2002, 11, 5, 0, 0, 0, DateTimeKind.Utc),
        MainImage = "test",
        CreatedAt = DateTime.UtcNow,
        PasswordHash = hasher.HashPassword(null, "Korisnik123!")
    }
);

        // --------------------
        // Generate 40 fake users
        // --------------------

        var fakeUsers = new List<User>();
        var fakeUserRoles = new List<UserRole>();

        var rand = new Random();
        int nextUserId = 10;

        for (int u = 0; u < 40; u++)
        {
            int id = nextUserId++;

            var dob = new DateTime(
                rand.Next(1960, 2008),
                rand.Next(1, 13),
                rand.Next(1, 28),
                0, 0, 0,
                DateTimeKind.Utc
            );

            var created = DateTime.SpecifyKind(
                DateTime.UtcNow.AddDays(-rand.Next(1, 300)),
                DateTimeKind.Utc
            );

            fakeUsers.Add(new User
            {
                Id = id,
                UserName = $"user{id}",
                NormalizedUserName = $"USER{id}",
                Email = $"user{id}@mail.com",
                NormalizedEmail = $"USER{id}@MAIL.COM",
                EmailConfirmed = true,
                FirstName = $"User{id}",
                LastName = "Test",
                PhoneNumber = $"+3876{rand.Next(1000000, 9999999)}",
                isBlocked = false,
                DateBirth = dob,
                MainImage = "test",
                CreatedAt = created,
                PasswordHash = hasher.HashPassword(null, $"User{id}123!")
            });

            fakeUserRoles.Add(new UserRole
            {
                UserId = id,
                RoleId = 1  // Korisnik
            });
        }

        builder.Entity<User>().HasData(fakeUsers);
        fakeUserRoles.Add(new UserRole { UserId = 1, RoleId = 2 });
        fakeUserRoles.Add(new UserRole { UserId = 2, RoleId = 3 });
        fakeUserRoles.Add(new UserRole { UserId = 4, RoleId = 1 });

        builder.Entity<UserRole>().HasData(fakeUserRoles);



        builder.Entity<UserToken>().HasData(
            new UserToken
            {
                UserId = 4,
                Equity = 80
            }
        );


        builder.Entity<Voucher>().HasData(
            new Voucher
            {
                Id = 1,
                VoucherCode = "WELCOME20",
                Discount = 0.20m,
                priceInTokens = 40
            },
            new Voucher
            {
                Id = 2,
                VoucherCode = "SUMMER50",
                Discount = 0.50m,
                priceInTokens = 80
            },
            new Voucher
            {
                Id = 3,
                VoucherCode = "VIP70",
                Discount = 0.70m,
                priceInTokens = 40
            }
        );


        builder.Entity<OfferCategory>().HasData(
        new OfferCategory { Id = 1, Name = "Praznična putovanja" },
        new OfferCategory { Id = 2, Name = "Specijalna putovanja" },
        new OfferCategory { Id = 3, Name = "Osjetite mjesec" }
        );

        builder.Entity<OfferSubCategory>().HasData(
    // Specijalna putovanja 
    new OfferSubCategory { Id = -1, Name = "Bez podkategorije", CategoryId = 2 },
    // Praznična putovanja (CategoryId = 1)
    new OfferSubCategory { Id = 1, Name = "Božić", CategoryId = 1 },
    new OfferSubCategory { Id = 2, Name = "Bajram", CategoryId = 1 },
    new OfferSubCategory { Id = 3, Name = "Prvi maj", CategoryId = 1 },

    // Osjetite mjesec (CategoryId = 3)
    new OfferSubCategory { Id = 4, Name = "Januar", CategoryId = 3 },
    new OfferSubCategory { Id = 5, Name = "Februar", CategoryId = 3 },
    new OfferSubCategory { Id = 6, Name = "Mart", CategoryId = 3 },
    new OfferSubCategory { Id = 7, Name = "April", CategoryId = 3 },
    new OfferSubCategory { Id = 8, Name = "Maj", CategoryId = 3 },
    new OfferSubCategory { Id = 9, Name = "Juni", CategoryId = 3 },
    new OfferSubCategory { Id = 10, Name = "Juli", CategoryId = 3 },
    new OfferSubCategory { Id = 11, Name = "August", CategoryId = 3 },
    new OfferSubCategory { Id = 12, Name = "Septembar", CategoryId = 3 },
    new OfferSubCategory { Id = 13, Name = "Oktobar", CategoryId = 3 },
    new OfferSubCategory { Id = 14, Name = "Novembar", CategoryId = 3 },
    new OfferSubCategory { Id = 15, Name = "Decembar", CategoryId = 3 }
);

        // OFFER SEED DATA (without Price)
        builder.Entity<Offer>().HasData(
            new Offer { Id = 1, Title = "Putovanje u Pariz", DaysInTotal = 7, WayOfTravel = "Avion", SubCategoryId = 1 },
            new Offer { Id = 2, Title = "Putovanje u Rim", DaysInTotal = 6, WayOfTravel = "Autobus", SubCategoryId = 2 },
            new Offer { Id = 3, Title = "Putovanje u Madrid", DaysInTotal = 8, WayOfTravel = "Avion", SubCategoryId = 3 },
            new Offer { Id = 4, Title = "Putovanje u Beč", DaysInTotal = 4, WayOfTravel = "Autobus", SubCategoryId = 4 },
            new Offer { Id = 5, Title = "Putovanje u Atina", DaysInTotal = 9, WayOfTravel = "Avion", SubCategoryId = 5 },
            new Offer { Id = 6, Title = "Putovanje u Amsterdam", DaysInTotal = 6, WayOfTravel = "Avion", SubCategoryId = 6 },
            new Offer { Id = 7, Title = "Putovanje u Lisabon", DaysInTotal = 7, WayOfTravel = "Autobus", SubCategoryId = 7 },
            new Offer { Id = 8, Title = "Putovanje u Berlin", DaysInTotal = 5, WayOfTravel = "Avion", SubCategoryId = 8 },
            new Offer { Id = 9, Title = "Putovanje u Prag", DaysInTotal = 6, WayOfTravel = "Avion", SubCategoryId = 9 },
            new Offer { Id = 10, Title = "Putovanje u Kopenhagen", DaysInTotal = 10, WayOfTravel = "Avion", SubCategoryId = 10 },
            new Offer { Id = 11, Title = "Putovanje u Oslo", DaysInTotal = 9, WayOfTravel = "Avion", SubCategoryId = 11 },
            new Offer { Id = 12, Title = "Putovanje u Stockholm", DaysInTotal = 8, WayOfTravel = "Avion", SubCategoryId = 12 },
            new Offer { Id = 13, Title = "Putovanje u Ženeva", DaysInTotal = 6, WayOfTravel = "Autobus", SubCategoryId = 1 },
            new Offer { Id = 14, Title = "Putovanje u Cirih", DaysInTotal = 7, WayOfTravel = "Avion", SubCategoryId = 2 },
            new Offer { Id = 15, Title = "Putovanje u Istanbul", DaysInTotal = 5, WayOfTravel = "Autobus", SubCategoryId = 3 },
            new Offer { Id = 16, Title = "Putovanje u Sarajevo", DaysInTotal = 3, WayOfTravel = "Autobus", SubCategoryId = 4 },
            new Offer { Id = 17, Title = "Putovanje u Zagreb", DaysInTotal = 4, WayOfTravel = "Autobus", SubCategoryId = 5 },
            new Offer { Id = 18, Title = "Putovanje u Beograd", DaysInTotal = 4, WayOfTravel = "Autobus", SubCategoryId = 6 },
            new Offer { Id = 19, Title = "Putovanje u Dubrovnik", DaysInTotal = 6, WayOfTravel = "Avion", SubCategoryId = 7 },
            new Offer { Id = 20, Title = "Putovanje u Split", DaysInTotal = 6, WayOfTravel = "Autobus", SubCategoryId = 8 },
            new Offer { Id = 21, Title = "Putovanje u Ljubljana", DaysInTotal = 5, WayOfTravel = "Autobus", SubCategoryId = 9 },
            new Offer { Id = 22, Title = "Putovanje u Podgorica", DaysInTotal = 5, WayOfTravel = "Avion", SubCategoryId = 10 },
            new Offer { Id = 23, Title = "Putovanje u Tirana", DaysInTotal = 5, WayOfTravel = "Avion", SubCategoryId = 11 },
            new Offer { Id = 24, Title = "Putovanje u Skoplje", DaysInTotal = 5, WayOfTravel = "Autobus", SubCategoryId = 12 },
            new Offer { Id = 25, Title = "Putovanje u Budimpešta", DaysInTotal = 6, WayOfTravel = "Avion", SubCategoryId = 1 },
            new Offer { Id = 26, Title = "Putovanje u Brisel", DaysInTotal = 7, WayOfTravel = "Avion", SubCategoryId = 2 },
            new Offer { Id = 27, Title = "Putovanje u Varšava", DaysInTotal = 6, WayOfTravel = "Autobus", SubCategoryId = 3 },
            new Offer { Id = 28, Title = "Putovanje u Krakov", DaysInTotal = 6, WayOfTravel = "Autobus", SubCategoryId = 4 },
            new Offer { Id = 29, Title = "Putovanje u Sofija", DaysInTotal = 6, WayOfTravel = "Autobus", SubCategoryId = 5 },
            new Offer { Id = 30, Title = "Putovanje u Bukurešt", DaysInTotal = 7, WayOfTravel = "Avion", SubCategoryId = 6 }
        );

        // OFFER DETAILS SEED DATA (with MinimalPrice)
        builder.Entity<OfferDetails>().HasData(
    new OfferDetails { OfferId = 1, MinimalPrice = 850m, Description = "Uživajte u opuštajućem putovanju kroz Pariz sa vrhunskim vodičima i nezaboravnim doživljajima.", City = "Pariz", Country = "Francuska", ResidenceTaxPerDay = 4.89m, ResidenceTotal = 34.23m, TravelInsuranceTotal = 48.90m },
    new OfferDetails { OfferId = 2, MinimalPrice = 760m, Description = "Iskusite čari Rima, njegove istorijske znamenitosti i autentičnu kuhinju.", City = "Rim", Country = "Italija", ResidenceTaxPerDay = 5.87m, ResidenceTotal = 35.22m, TravelInsuranceTotal = 48.90m },
    new OfferDetails { OfferId = 3, MinimalPrice = 920m, Description = "Otkrijte Madrid, grad prepun kulture, umetnosti i sjajnih pejzaža.", City = "Madrid", Country = "Španija", ResidenceTaxPerDay = 6.85m, ResidenceTotal = 54.80m, TravelInsuranceTotal = 39.12m },
    new OfferDetails { OfferId = 4, MinimalPrice = 540m, Description = "Posetite Beč, grad muzike, istorije i predivne arhitekture.", City = "Beč", Country = "Austrija", ResidenceTaxPerDay = 3.91m, ResidenceTotal = 15.64m, TravelInsuranceTotal = 35.20m },
    new OfferDetails { OfferId = 5, MinimalPrice = 970m, Description = "Istražite Atinu i uživajte u drevnoj grčkoj istoriji i prelepim plažama.", City = "Atina", Country = "Grčka", ResidenceTaxPerDay = 3.91m, ResidenceTotal = 35.19m, TravelInsuranceTotal = 43.03m },
    new OfferDetails { OfferId = 6, MinimalPrice = 1010m, Description = "Amsterdam vas poziva svojim kanalima, muzejima i opuštenom atmosferom.", City = "Amsterdam", Country = "Nizozemska", ResidenceTaxPerDay = 5.87m, ResidenceTotal = 35.22m, TravelInsuranceTotal = 48.90m },
    new OfferDetails { OfferId = 7, MinimalPrice = 890m, Description = "Doživite čari Lisabona, njegovu arhitekturu i ukusnu hranu.", City = "Lisabon", Country = "Portugal", ResidenceTaxPerDay = 3.91m, ResidenceTotal = 27.37m, TravelInsuranceTotal = 39.12m },
    new OfferDetails { OfferId = 8, MinimalPrice = 730m, Description = "Berlin, dinamični grad sa bogatom istorijom i živahnom kulturom.", City = "Berlin", Country = "Njemačka", ResidenceTaxPerDay = 2.93m, ResidenceTotal = 14.65m, TravelInsuranceTotal = 35.20m },
    new OfferDetails { OfferId = 9, MinimalPrice = 810m, Description = "Prag, grad bajki, mostova i nezaboravnih večeri.", City = "Prag", Country = "Češka", ResidenceTaxPerDay = 2.93m, ResidenceTotal = 17.58m, TravelInsuranceTotal = 33.25m },
    new OfferDetails { OfferId = 10, MinimalPrice = 1340m, Description = "Kopenhagen – skandinavska oaza sa modernim i tradicionalnim sadržajima.", City = "Kopenhagen", Country = "Danska", ResidenceTaxPerDay = 4.89m, ResidenceTotal = 48.90m, TravelInsuranceTotal = 48.90m },
    new OfferDetails { OfferId = 11, MinimalPrice = 1230m, Description = "Oslo, prirodna lepota i urbani život na dohvat ruke.", City = "Oslo", Country = "Norveška", ResidenceTaxPerDay = 4.30m, ResidenceTotal = 38.70m, TravelInsuranceTotal = 50.85m },
    new OfferDetails { OfferId = 12, MinimalPrice = 1110m, Description = "Stockholm, grad na vodi sa bogatom istorijom i prelepim pejzažima.", City = "Stockholm", Country = "Švedska", ResidenceTaxPerDay = 4.50m, ResidenceTotal = 36.00m, TravelInsuranceTotal = 48.90m },
    new OfferDetails { OfferId = 13, MinimalPrice = 990m, Description = "Ženeva, srce švajcarskih Alpa i međunarodna prestonica.", City = "Ženeva", Country = "Švicarska", ResidenceTaxPerDay = 5.48m, ResidenceTotal = 32.88m, TravelInsuranceTotal = 54.76m },
    new OfferDetails { OfferId = 14, MinimalPrice = 970m, Description = "Cirih, finansijski centar i kulturni dragulj Švajcarske.", City = "Cirih", Country = "Švicarska", ResidenceTaxPerDay = 5.48m, ResidenceTotal = 38.36m, TravelInsuranceTotal = 54.76m },
    new OfferDetails { OfferId = 15, MinimalPrice = 700m, Description = "Istanbul, grad na dva kontinenta sa jedinstvenom atmosferom.", City = "Istanbul", Country = "Turska", ResidenceTaxPerDay = 2.35m, ResidenceTotal = 11.75m, TravelInsuranceTotal = 29.34m },
    new OfferDetails { OfferId = 16, MinimalPrice = 430m, Description = "Sarajevo, mesto susreta kultura i istorije.", City = "Sarajevo", Country = "Bosna i Hercegovina", ResidenceTaxPerDay = 1.96m, ResidenceTotal = 5.88m, TravelInsuranceTotal = 23.47m },
    new OfferDetails { OfferId = 17, MinimalPrice = 500m, Description = "Zagreb, moderna metropola sa bogatom tradicijom.", City = "Zagreb", Country = "Hrvatska", ResidenceTaxPerDay = 2.60m, ResidenceTotal = 10.40m, TravelInsuranceTotal = 29.34m },
    new OfferDetails { OfferId = 18, MinimalPrice = 520m, Description = "Beograd, grad sa živahnim noćnim životom i bogatom istorijom.", City = "Beograd", Country = "Srbija", ResidenceTaxPerDay = 2.15m, ResidenceTotal = 8.60m, TravelInsuranceTotal = 27.38m },
    new OfferDetails { OfferId = 19, MinimalPrice = 840m, Description = "Dubrovnik, biser Jadrana i mediteranske lepote.", City = "Dubrovnik", Country = "Hrvatska", ResidenceTaxPerDay = 2.60m, ResidenceTotal = 15.60m, TravelInsuranceTotal = 29.34m },
    new OfferDetails { OfferId = 20, MinimalPrice = 790m, Description = "Split, spoj istorije i modernog šarma uz prelepe plaže.", City = "Split", Country = "Hrvatska", ResidenceTaxPerDay = 2.60m, ResidenceTotal = 15.60m, TravelInsuranceTotal = 29.34m },
    new OfferDetails { OfferId = 21, MinimalPrice = 680m, Description = "Ljubljana, zeleno srce Evrope sa opuštajućom atmosferom.", City = "Ljubljana", Country = "Slovenija", ResidenceTaxPerDay = 4.89m, ResidenceTotal = 24.45m, TravelInsuranceTotal = 35.20m },
    new OfferDetails { OfferId = 22, MinimalPrice = 620m, Description = "Podgorica, nova evropska destinacija puna iznenađenja.", City = "Podgorica", Country = "Crna Gora", ResidenceTaxPerDay = 2.35m, ResidenceTotal = 11.75m, TravelInsuranceTotal = 25.43m },
    new OfferDetails { OfferId = 23, MinimalPrice = 640m, Description = "Tirana, šarmantan grad sa bogatom kulturom i prijateljskom atmosferom.", City = "Tirana", Country = "Albanija", ResidenceTaxPerDay = 1.96m, ResidenceTotal = 9.80m, TravelInsuranceTotal = 23.47m },
    new OfferDetails { OfferId = 24, MinimalPrice = 600m, Description = "Skoplje, spoj starog i novog u srcu Balkana.", City = "Skoplje", Country = "Sjeverna Makedonija", ResidenceTaxPerDay = 1.96m, ResidenceTotal = 9.80m, TravelInsuranceTotal = 23.47m },
    new OfferDetails { OfferId = 25, MinimalPrice = 900m, Description = "Budimpešta, grad termalnih kupališta i veličanstvene arhitekture.", City = "Budimpešta", Country = "Mađarska", ResidenceTaxPerDay = 3.52m, ResidenceTotal = 21.12m, TravelInsuranceTotal = 33.25m },
    new OfferDetails { OfferId = 26, MinimalPrice = 1050m, Description = "Brisel, prestonica Evrope sa bogatom istorijom i gastronomijom.", City = "Brisel", Country = "Belgija", ResidenceTaxPerDay = 4.89m, ResidenceTotal = 34.23m, TravelInsuranceTotal = 43.03m },
    new OfferDetails { OfferId = 27, MinimalPrice = 970m, Description = "Varšava, grad koji uspešno spaja istoriju i moderni duh.", City = "Varšava", Country = "Poljska", ResidenceTaxPerDay = 2.93m, ResidenceTotal = 17.58m, TravelInsuranceTotal = 35.20m },
    new OfferDetails { OfferId = 28, MinimalPrice = 960m, Description = "Krakov, biser Poljske sa bogatom kulturnom scenom.", City = "Krakov", Country = "Poljska", ResidenceTaxPerDay = 2.93m, ResidenceTotal = 17.58m, TravelInsuranceTotal = 35.20m },
    new OfferDetails { OfferId = 29, MinimalPrice = 880m, Description = "Sofija, srce Bugarske sa prelepim planinama i istorijom.", City = "Sofija", Country = "Bugarska", ResidenceTaxPerDay = 2.35m, ResidenceTotal = 14.10m, TravelInsuranceTotal = 29.34m },
    new OfferDetails { OfferId = 30, MinimalPrice = 910m, Description = "Bukurešt, grad kontrasta i dinamične kulture.", City = "Bukurešt", Country = "Rumunija", ResidenceTaxPerDay = 2.54m, ResidenceTotal = 17.78m, TravelInsuranceTotal = 29.34m }
);


        builder.Entity<OfferPlanDay>().HasData(
    new OfferPlanDay { OfferDetailsId = 1, DayNumber = 1, DayTitle = "Dolazak u Pariz", DayDescription = "Polazak u ranim jutarnjim satima i dolazak u Pariz. Smještaj u hotel i kraće upoznavanje sa okolinom." },
    new OfferPlanDay { OfferDetailsId = 1, DayNumber = 2, DayTitle = "Obilazak centra grada", DayDescription = "Obilazak Ajfelovog tornja, Šanzelizea i Trijumfalne kapije." },
    new OfferPlanDay { OfferDetailsId = 1, DayNumber = 3, DayTitle = "Luvr i Sjena", DayDescription = "Posjeta muzeju Luvr i vožnja brodom po rijeci Seni." },
    new OfferPlanDay { OfferDetailsId = 1, DayNumber = 4, DayTitle = "Versaj", DayDescription = "Izlet do prelijepog dvorca Versaj i uživanje u kraljevskim vrtovima." },
    new OfferPlanDay { OfferDetailsId = 1, DayNumber = 5, DayTitle = "Slobodno vrijeme", DayDescription = "Dan rezervisan za šoping, šetnju i lične aktivnosti." },
    new OfferPlanDay { OfferDetailsId = 1, DayNumber = 6, DayTitle = "Kulturni doživljaji", DayDescription = "Uživanje u francuskoj kuhinji i večernja šetnja Monmartrom." },
    new OfferPlanDay { OfferDetailsId = 1, DayNumber = 7, DayTitle = "Povratak", DayDescription = "Odjava iz hotela, transfer do aerodroma i povratak kući." },

    // 🇮🇹 Rim – 6 dana
    new OfferPlanDay { OfferDetailsId = 2, DayNumber = 1, DayTitle = "Dolazak u Rim", DayDescription = "Dolazak i smještaj u hotelu. Upoznavanje sa gradom i lagana večernja šetnja." },
    new OfferPlanDay { OfferDetailsId = 2, DayNumber = 2, DayTitle = "Antički Rim", DayDescription = "Posjeta Koloseumu, Forum Romanumu i Panteonu." },
    new OfferPlanDay { OfferDetailsId = 2, DayNumber = 3, DayTitle = "Vatikan", DayDescription = "Obilazak Vatikana, Sikstinske kapele i bazilike Svetog Petra." },
    new OfferPlanDay { OfferDetailsId = 2, DayNumber = 4, DayTitle = "Trg Navona i Fontana di Trevi", DayDescription = "Obilazak najpoznatijih trgova i fontana Rima." },
    new OfferPlanDay { OfferDetailsId = 2, DayNumber = 5, DayTitle = "Slobodno vrijeme", DayDescription = "Dan za odmor, šoping i uživanje u italijanskoj kuhinji." },
    new OfferPlanDay { OfferDetailsId = 2, DayNumber = 6, DayTitle = "Povratak", DayDescription = "Pakovanje i odlazak prema aerodromu." },

    // 🇪🇸 Madrid – 8 dana
    new OfferPlanDay { OfferDetailsId = 3, DayNumber = 1, DayTitle = "Dolazak u Madrid", DayDescription = "Dolazak i smještaj u hotel. Kratko upoznavanje sa centrom grada." },
    new OfferPlanDay { OfferDetailsId = 3, DayNumber = 2, DayTitle = "Kraljevska palata", DayDescription = "Posjeta Kraljevskoj palati i trgu Plaza Mayor." },
    new OfferPlanDay { OfferDetailsId = 3, DayNumber = 3, DayTitle = "Muzej Prado", DayDescription = "Posjeta najpoznatijem muzeju u Španiji – Pradu." },
    new OfferPlanDay { OfferDetailsId = 3, DayNumber = 4, DayTitle = "Park Retiro", DayDescription = "Šetnja prelijepim parkom Retiro i slobodno popodne." },
    new OfferPlanDay { OfferDetailsId = 3, DayNumber = 5, DayTitle = "Izlet u Toledo", DayDescription = "Fakultativni izlet u srednjovjekovni grad Toledo." },
    new OfferPlanDay { OfferDetailsId = 3, DayNumber = 6, DayTitle = "Gastronomski dan", DayDescription = "Uživanje u tradicionalnoj španskoj hrani i vinu." },
    new OfferPlanDay { OfferDetailsId = 3, DayNumber = 7, DayTitle = "Slobodan dan", DayDescription = "Dan za individualne aktivnosti i šoping." },
    new OfferPlanDay { OfferDetailsId = 3, DayNumber = 8, DayTitle = "Povratak", DayDescription = "Odjava iz hotela i let prema kući." }
);





        builder.Entity<OfferImage>().HasData(
        Enumerable.Range(1, 30).Select(id => new OfferImage
        {
            Id = id,
            OfferId = id,
            ImageUrl = "/images/offer/pariz.jpg",
            isMain = true
        }).ToArray()
        );

        builder.Entity<Hotel>().HasData(
    new Hotel { Id = 1, Name = "Hotel Kovačević", Address = "Blaževićeva 404", Stars = 4 },
    new Hotel { Id = 2, Name = "Hotel Vuković", Address = "Potočna 520", Stars = 3 },
    new Hotel { Id = 3, Name = "Hotel Petrović", Address = "Milice Todorović 102", Stars = 5 },
    new Hotel { Id = 4, Name = "Hotel Ilić", Address = "Cara Dušana 77", Stars = 4 },
    new Hotel { Id = 5, Name = "Hotel Stojanović", Address = "Bulevar Kralja Petra 15", Stars = 4 },
    new Hotel { Id = 6, Name = "Hotel Marković", Address = "Svetog Save 88", Stars = 3 },
    new Hotel { Id = 7, Name = "Hotel Jovanović", Address = "Narodnog fronta 25", Stars = 5 },
    new Hotel { Id = 8, Name = "Hotel Nikolić", Address = "Kralja Milana 12", Stars = 4 },
    new Hotel { Id = 9, Name = "Hotel Milošević", Address = "Bulevar Oslobođenja 33", Stars = 3 },
    new Hotel { Id = 10, Name = "Hotel Ristić", Address = "Žarka Zrenjanina 8", Stars = 4 },
    new Hotel { Id = 11, Name = "Hotel Lukić", Address = "Kosovska 40", Stars = 3 },
    new Hotel { Id = 12, Name = "Hotel Savić", Address = "Cara Lazara 77", Stars = 4 },
    new Hotel { Id = 13, Name = "Hotel Milenković", Address = "Ulica Kralja Aleksandra 58", Stars = 5 },
    new Hotel { Id = 14, Name = "Hotel Janković", Address = "Makedonska 91", Stars = 4 },
    new Hotel { Id = 15, Name = "Hotel Pavlović", Address = "Narodnog heroja 120", Stars = 3 },
    new Hotel { Id = 16, Name = "Hotel Todorović", Address = "Bulevar Kralja Aleksandra 19", Stars = 4 },
    new Hotel { Id = 17, Name = "Hotel Božić", Address = "Njegoševa 7", Stars = 5 },
    new Hotel { Id = 18, Name = "Hotel Živanović", Address = "Braće Jerković 14", Stars = 3 },
    new Hotel { Id = 19, Name = "Hotel Miladinović", Address = "Svetozara Markovića 22", Stars = 4 },
    new Hotel { Id = 20, Name = "Hotel Radosavljević", Address = "Kneza Miloša 50", Stars = 4 },
    new Hotel { Id = 21, Name = "Hotel Ćosić", Address = "Bulevar revolucije 65", Stars = 3 },
    new Hotel { Id = 22, Name = "Hotel Stanković", Address = "Vojvode Stepe 33", Stars = 4 },
    new Hotel { Id = 23, Name = "Hotel Perić", Address = "Mileve Marić 45", Stars = 5 },
    new Hotel { Id = 24, Name = "Hotel Radovanović", Address = "Bulevar despota Stefana 14", Stars = 4 },
    new Hotel { Id = 25, Name = "Hotel Novaković", Address = "Gavrila Principa 18", Stars = 3 },
    new Hotel { Id = 26, Name = "Hotel Vasić", Address = "Resavska 12", Stars = 4 },
    new Hotel { Id = 27, Name = "Hotel Tadić", Address = "Njegoševa 90", Stars = 5 },
    new Hotel { Id = 28, Name = "Hotel Milović", Address = "Ulica kralja Petra 66", Stars = 4 },
    new Hotel { Id = 29, Name = "Hotel Rakić", Address = "Kraljice Marije 30", Stars = 3 },
    new Hotel { Id = 30, Name = "Hotel Jović", Address = "Terazije 55", Stars = 4 },
    new Hotel { Id = 31, Name = "Hotel Milić", Address = "Kneza Ljubomira 14", Stars = 3 },
    new Hotel { Id = 32, Name = "Hotel Đorđević", Address = "Bulevar kralja Aleksandra 11", Stars = 4 },
    new Hotel { Id = 33, Name = "Hotel Karanović", Address = "Cara Dušana 99", Stars = 5 },
    new Hotel { Id = 34, Name = "Hotel Radulović", Address = "Ulica Marije Bursać 40", Stars = 4 },
    new Hotel { Id = 35, Name = "Hotel Filipović", Address = "Nikole Pašića 12", Stars = 3 },
    new Hotel { Id = 36, Name = "Hotel Stankov", Address = "Bulevar oslobođenja 32", Stars = 4 },
    new Hotel { Id = 37, Name = "Hotel Sokolović", Address = "Kralja Petra 44", Stars = 5 },
    new Hotel { Id = 38, Name = "Hotel Popović", Address = "Ulica kralja Milana 8", Stars = 4 },
    new Hotel { Id = 39, Name = "Hotel Vučić", Address = "Bulevar Kralja Petra 6", Stars = 3 },
    new Hotel { Id = 40, Name = "Hotel Jankov", Address = "Nikole Tesle 15", Stars = 4 },
    new Hotel { Id = 41, Name = "Hotel Zorić", Address = "Kneza Mihaila 19", Stars = 5 },
    new Hotel { Id = 42, Name = "Hotel Dragić", Address = "Mileve Marić 27", Stars = 4 },
    new Hotel { Id = 43, Name = "Hotel Tomašević", Address = "Bulevar oslobođenja 50", Stars = 3 },
    new Hotel { Id = 44, Name = "Hotel Mijatović", Address = "Kralja Aleksandra 22", Stars = 4 },
    new Hotel { Id = 45, Name = "Hotel Filipović", Address = "Ulica Kralja Petra 31", Stars = 5 },
    new Hotel { Id = 46, Name = "Hotel Radović", Address = "Narodnog fronta 18", Stars = 4 },
    new Hotel { Id = 47, Name = "Hotel Đukić", Address = "Bulevar Kralja Petra 14", Stars = 3 },
    new Hotel { Id = 48, Name = "Hotel Popović", Address = "Cara Dušana 7", Stars = 4 },
    new Hotel { Id = 49, Name = "Hotel Marinković", Address = "Kneza Miloša 5", Stars = 5 },
    new Hotel { Id = 50, Name = "Hotel Kostić", Address = "Bulevar Oslobođenja 16", Stars = 4 },
    new Hotel { Id = 51, Name = "Hotel Milutinović", Address = "Resavska 2", Stars = 3 },
    new Hotel { Id = 52, Name = "Hotel Radosavljević", Address = "Narodnog heroja 38", Stars = 4 },
    new Hotel { Id = 53, Name = "Hotel Ilić", Address = "Ulica Vuka Karadžića 14", Stars = 5 },
    new Hotel { Id = 54, Name = "Hotel Novak", Address = "Bulevar Oslobođenja 50", Stars = 4 },
    new Hotel { Id = 55, Name = "Hotel Đorđević", Address = "Kralja Petra 66", Stars = 3 },
    new Hotel { Id = 56, Name = "Hotel Jović", Address = "Njegoševa 11", Stars = 4 },
    new Hotel { Id = 57, Name = "Hotel Stevanović", Address = "Bulevar kralja Aleksandra 88", Stars = 5 },
    new Hotel { Id = 58, Name = "Hotel Mandić", Address = "Ulica Kralja Petra 3", Stars = 4 },
    new Hotel { Id = 59, Name = "Hotel Bošnjak", Address = "Narodnog fronta 17", Stars = 3 },
    new Hotel { Id = 60, Name = "Hotel Radovanović", Address = "Bulevar oslobođenja 43", Stars = 4 },
    new Hotel { Id = 61, Name = "Hotel Pavlović", Address = "Kneza Miloša 18", Stars = 5 },
    new Hotel { Id = 62, Name = "Hotel Ilić", Address = "Ulica Kralja Petra 7", Stars = 4 },
    new Hotel { Id = 63, Name = "Hotel Živković", Address = "Bulevar Kralja Petra 40", Stars = 3 },
    new Hotel { Id = 64, Name = "Hotel Janković", Address = "Narodnog heroja 23", Stars = 4 },
    new Hotel { Id = 65, Name = "Hotel Marković", Address = "Kralja Milana 50", Stars = 5 },
    new Hotel { Id = 66, Name = "Hotel Savić", Address = "Ulica Kralja Petra 28", Stars = 4 },
    new Hotel { Id = 67, Name = "Hotel Stojanović", Address = "Bulevar oslobođenja 29", Stars = 3 },
    new Hotel { Id = 68, Name = "Hotel Milosević", Address = "Narodnog fronta 11", Stars = 4 },
    new Hotel { Id = 69, Name = "Hotel Ristić", Address = "Kneza Miloša 22", Stars = 5 },
    new Hotel { Id = 70, Name = "Hotel Ilić", Address = "Ulica Kralja Petra 1", Stars = 4 }
);


        builder.Entity<HotelImages>().HasData(
    new HotelImages { Id = 1, HotelId = 1, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 2, HotelId = 2, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 3, HotelId = 3, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 4, HotelId = 4, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 5, HotelId = 5, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 6, HotelId = 6, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 7, HotelId = 7, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 8, HotelId = 8, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 9, HotelId = 9, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 10, HotelId = 10, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 11, HotelId = 11, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 12, HotelId = 12, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 13, HotelId = 13, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 14, HotelId = 14, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 15, HotelId = 15, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 16, HotelId = 16, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 17, HotelId = 17, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 18, HotelId = 18, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 19, HotelId = 19, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 20, HotelId = 20, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 21, HotelId = 21, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 22, HotelId = 22, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 23, HotelId = 23, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 24, HotelId = 24, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 25, HotelId = 25, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 26, HotelId = 26, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 27, HotelId = 27, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 28, HotelId = 28, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 29, HotelId = 29, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 30, HotelId = 30, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 31, HotelId = 31, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 32, HotelId = 32, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 33, HotelId = 33, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 34, HotelId = 34, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 35, HotelId = 35, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 36, HotelId = 36, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 37, HotelId = 37, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 38, HotelId = 38, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 39, HotelId = 39, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 40, HotelId = 40, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 41, HotelId = 41, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 42, HotelId = 42, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 43, HotelId = 43, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 44, HotelId = 44, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 45, HotelId = 45, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 46, HotelId = 46, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 47, HotelId = 47, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 48, HotelId = 48, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 49, HotelId = 49, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 50, HotelId = 50, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 51, HotelId = 51, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 52, HotelId = 52, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 53, HotelId = 53, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 54, HotelId = 54, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 55, HotelId = 55, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 56, HotelId = 56, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 57, HotelId = 57, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 58, HotelId = 58, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 59, HotelId = 59, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 60, HotelId = 60, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 61, HotelId = 61, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 62, HotelId = 62, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 63, HotelId = 63, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 64, HotelId = 64, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 65, HotelId = 65, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 66, HotelId = 66, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 67, HotelId = 67, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 68, HotelId = 68, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 69, HotelId = 69, ImageUrl = "/images/hotels/room.jpg", IsMain = true },
    new HotelImages { Id = 70, HotelId = 70, ImageUrl = "/images/hotels/room.jpg", IsMain = true }
);


        builder.Entity<OfferHotels>().HasData(
    new OfferHotels { OfferDetailsId = 1, HotelId = 1, DepartureDate = new DateTime(2025, 1, 10, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2025, 1, 15, 0, 0, 0, DateTimeKind.Utc) },
    new OfferHotels { OfferDetailsId = 1, HotelId = 2, DepartureDate = new DateTime(2025, 1, 10, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2025, 1, 15, 0, 0, 0, DateTimeKind.Utc) },
    new OfferHotels { OfferDetailsId = 2, HotelId = 3, DepartureDate = new DateTime(2025, 2, 5, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2025, 2, 11, 0, 0, 0, DateTimeKind.Utc) },
    new OfferHotels { OfferDetailsId = 2, HotelId = 4, DepartureDate = new DateTime(2025, 2, 5, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2025, 2, 11, 0, 0, 0, DateTimeKind.Utc) },
    new OfferHotels { OfferDetailsId = 3, HotelId = 5, DepartureDate = new DateTime(2025, 3, 12, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2025, 3, 18, 0, 0, 0, DateTimeKind.Utc) },
    new OfferHotels { OfferDetailsId = 3, HotelId = 6, DepartureDate = new DateTime(2025, 3, 12, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2025, 3, 18, 0, 0, 0, DateTimeKind.Utc) },
    new OfferHotels { OfferDetailsId = 4, HotelId = 7, DepartureDate = new DateTime(2025, 4, 2, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2025, 4, 8, 0, 0, 0, DateTimeKind.Utc) },
    new OfferHotels { OfferDetailsId = 5, HotelId = 8, DepartureDate = new DateTime(2025, 5, 16, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2025, 5, 23, 0, 0, 0, DateTimeKind.Utc) },
    new OfferHotels { OfferDetailsId = 5, HotelId = 9, DepartureDate = new DateTime(2025, 5, 16, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2025, 5, 23, 0, 0, 0, DateTimeKind.Utc) },
    new OfferHotels { OfferDetailsId = 6, HotelId = 10, DepartureDate = new DateTime(2025, 6, 9, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2025, 6, 15, 0, 0, 0, DateTimeKind.Utc) },
    new OfferHotels { OfferDetailsId = 7, HotelId = 11, DepartureDate = new DateTime(2025, 7, 4, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2025, 7, 10, 0, 0, 0, DateTimeKind.Utc) },
    new OfferHotels { OfferDetailsId = 8, HotelId = 12, DepartureDate = new DateTime(2025, 8, 1, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2025, 8, 8, 0, 0, 0, DateTimeKind.Utc) },
    new OfferHotels { OfferDetailsId = 9, HotelId = 13, DepartureDate = new DateTime(2025, 9, 6, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2025, 9, 13, 0, 0, 0, DateTimeKind.Utc) },
    new OfferHotels { OfferDetailsId = 10, HotelId = 14, DepartureDate = new DateTime(2025, 10, 11, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2025, 10, 18, 0, 0, 0, DateTimeKind.Utc) },
    new OfferHotels { OfferDetailsId = 11, HotelId = 15, DepartureDate = new DateTime(2025, 11, 3, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2025, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
    new OfferHotels { OfferDetailsId = 12, HotelId = 16, DepartureDate = new DateTime(2025, 12, 14, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2025, 12, 21, 0, 0, 0, DateTimeKind.Utc) },
    new OfferHotels { OfferDetailsId = 13, HotelId = 17, DepartureDate = new DateTime(2026, 1, 20, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2026, 1, 28, 0, 0, 0, DateTimeKind.Utc) },
    new OfferHotels { OfferDetailsId = 14, HotelId = 18, DepartureDate = new DateTime(2026, 2, 15, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2026, 2, 22, 0, 0, 0, DateTimeKind.Utc) },
    new OfferHotels { OfferDetailsId = 15, HotelId = 19, DepartureDate = new DateTime(2026, 3, 10, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2026, 3, 17, 0, 0, 0, DateTimeKind.Utc) },
    new OfferHotels { OfferDetailsId = 16, HotelId = 20, DepartureDate = new DateTime(2026, 4, 5, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2026, 4, 12, 0, 0, 0, DateTimeKind.Utc) }
);



        builder.Entity<Rooms>().HasData(
            new Rooms { Id = 1, RoomType = "Dvokrevetna", RoomCount = 2 },
            new Rooms { Id = 2, RoomType = "Trokrevetna", RoomCount = 3 },
            new Rooms { Id = 3, RoomType = "Jednokrevetna", RoomCount = 2 },
            new Rooms { Id = 4, RoomType = "Cetverokrevetna", RoomCount = 4 }
        );


        builder.Entity<HotelRooms>().HasData(
    new HotelRooms { HotelId = 1, RoomId = 1, RoomsLeft = 10 },
    new HotelRooms { HotelId = 1, RoomId = 2, RoomsLeft = 5 },
    new HotelRooms { HotelId = 2, RoomId = 1, RoomsLeft = 8 },
    new HotelRooms { HotelId = 2, RoomId = 3, RoomsLeft = 4 },
    new HotelRooms { HotelId = 3, RoomId = 2, RoomsLeft = 6 },
    new HotelRooms { HotelId = 3, RoomId = 3, RoomsLeft = 3 },
    new HotelRooms { HotelId = 4, RoomId = 1, RoomsLeft = 7 },
    new HotelRooms { HotelId = 4, RoomId = 2, RoomsLeft = 2 }
   );
    }
}





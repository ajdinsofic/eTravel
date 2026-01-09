using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace eTravelAgencija.Services.Database.Seed
{

    public static class SeedData
    {
        private static string OfferImage(int offerId, int index)
        => $"https://picsum.photos/seed/offer-{offerId}-{index}/900/600";

        private static string HotelImage(int hotelId, int index)
        => $"https://picsum.photos/seed/hotel-{hotelId}-{index}/800/500";


        public static void Seed(ModelBuilder builder)
        {
            // =========================================================================
            // 1. ROLE SISTEM
            // =========================================================================
            builder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Korisnik", NormalizedName = "KORISNIK", Description = "Osnovna korisnička rola" },
                new Role { Id = 2, Name = "Radnik", NormalizedName = "RADNIK", Description = "Zaposleni koji upravlja ponudama i rezervacijama" },
                new Role { Id = 3, Name = "Direktor", NormalizedName = "DIREKTOR", Description = "Administrator sistema" }
                //new Role { Id = 4, Name = "Anonymus", NormalizedName = "ANONYMUS", Description = "Neprijavljeni korisnik mobilne aplikacije" }

            );

            // =========================================================================
            // 2. RATE (RATNI SISTEM)
            // =========================================================================
            builder.Entity<Rate>().HasData(
                new Rate { Id = 1, Name = "Prva rata", OrderNumber = 1 },
                new Rate { Id = 2, Name = "Druga rata", OrderNumber = 2 },
                new Rate { Id = 3, Name = "Treća rata", OrderNumber = 3 },
                new Rate { Id = 4, Name = "Puni iznos", OrderNumber = 0 },
                new Rate { Id = 5, Name = "Preostali iznos", OrderNumber = 0 }
            );

            // =========================================================================
            // 3. KATEGORIJE PONUDA
            // =========================================================================
            builder.Entity<OfferCategory>().HasData(
                new OfferCategory { Id = 1, Name = "Praznična putovanja" },
                new OfferCategory { Id = 2, Name = "Specijalna putovanja" },
                new OfferCategory { Id = 3, Name = "Osjetite mjesec" }
            );

            // =========================================================================
            // 4. PODKATEGORIJE
            // =========================================================================
            builder.Entity<OfferSubCategory>().HasData(
                new OfferSubCategory { Id = -1, Name = "Bez podkategorije", CategoryId = 2 },

                new OfferSubCategory { Id = 1, Name = "Božić", CategoryId = 1 },
                new OfferSubCategory { Id = 2, Name = "Bajram", CategoryId = 1 },
                new OfferSubCategory { Id = 3, Name = "Prvi maj", CategoryId = 1 },

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

            // =========================================================================
            // 5. DEFAULT KORISNICI
            // =========================================================================
            var hasher = new PasswordHasher<User>();

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
                    ImageUrl = "test.jpg",
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
                    ImageUrl = "test.jpg",
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
                    ImageUrl = "test.jpg",
                    CreatedAt = DateTime.UtcNow,
                    PasswordHash = hasher.HashPassword(null, "Korisnik123!")
                },

                new User
                {
                    Id = 5,
                    UserName = "maja.petrovic",
                    NormalizedUserName = "MAJA.PETROVIC",
                    Email = "maja.petrovic@etravel.com",
                    NormalizedEmail = "MAJA.PETROVIC@ETRAVEL.COM",
                    EmailConfirmed = true,
                    FirstName = "Maja",
                    LastName = "Petrović",
                    PhoneNumber = "+38761555111",
                    isBlocked = false,
                    DateBirth = new DateTime(1998, 4, 12, 0, 0, 0, DateTimeKind.Utc),
                    ImageUrl = "test.jpg",
                    CreatedAt = DateTime.UtcNow,
                    PasswordHash = hasher.HashPassword(null, "Korisnik123!")
                },

                new User
                {
                    Id = 6,
                    UserName = "edin.mesic",
                    NormalizedUserName = "EDIN.MESIC",
                    Email = "edin.mesic@etravel.com",
                    NormalizedEmail = "EDIN.MESIC@ETRAVEL.COM",
                    EmailConfirmed = true,
                    FirstName = "Edin",
                    LastName = "Mešić",
                    PhoneNumber = "+38761666123",
                    isBlocked = false,
                    DateBirth = new DateTime(1995, 9, 8, 0, 0, 0, DateTimeKind.Utc),
                    ImageUrl = "test.jpg",
                    CreatedAt = DateTime.UtcNow,
                    PasswordHash = hasher.HashPassword(null, "Korisnik123!")
                },

                new User
                {
                    Id = 7,
                    UserName = "lana.kovac",
                    NormalizedUserName = "LANA.KOVAC",
                    Email = "lana.kovac@etravel.com",
                    NormalizedEmail = "LANA.KOVAC@ETRAVEL.COM",
                    EmailConfirmed = true,
                    FirstName = "Lana",
                    LastName = "Kovač",
                    PhoneNumber = "+38761777141",
                    isBlocked = false,
                    DateBirth = new DateTime(2000, 1, 22, 0, 0, 0, DateTimeKind.Utc),
                    ImageUrl = "test.jpg",
                    CreatedAt = DateTime.UtcNow,
                    PasswordHash = hasher.HashPassword(null, "Korisnik123!")
                },

                new User
                {
                    Id = 8,
                    UserName = "haris.becirovic",
                    NormalizedUserName = "HARIS.BECIROVIC",
                    Email = "haris.becirovic@etravel.com",
                    NormalizedEmail = "HARIS.BECIROVIC@ETRAVEL.COM",
                    EmailConfirmed = true,
                    FirstName = "Haris",
                    LastName = "Bećirović",
                    PhoneNumber = "+38761888222",
                    isBlocked = false,
                    DateBirth = new DateTime(1993, 6, 30, 0, 0, 0, DateTimeKind.Utc),
                    ImageUrl = "test.jpg",
                    CreatedAt = DateTime.UtcNow,
                    PasswordHash = hasher.HashPassword(null, "Korisnik123!")
                },

                new User
                {
                    Id = 9,
                    UserName = "amira.karic",
                    NormalizedUserName = "AMIRA.KARIC",
                    Email = "amira.karic@etravel.com",
                    NormalizedEmail = "AMIRA.KARIC@ETRAVEL.COM",
                    EmailConfirmed = true,
                    FirstName = "Amira",
                    LastName = "Karić",
                    PhoneNumber = "+38761999444",
                    isBlocked = false,
                    DateBirth = new DateTime(1999, 2, 14, 0, 0, 0, DateTimeKind.Utc),
                    ImageUrl = "test.jpg",
                    CreatedAt = DateTime.UtcNow,
                    PasswordHash = hasher.HashPassword(null, "Korisnik123!")
                },

                new User
                {
                    Id = 10,
                    UserName = "tarik.suljic",
                    NormalizedUserName = "TARIK.SULJIC",
                    Email = "tarik.suljic@etravel.com",
                    NormalizedEmail = "TARIK.SULJIC@ETRAVEL.COM",
                    EmailConfirmed = true,
                    FirstName = "Tarik",
                    LastName = "Suljić",
                    PhoneNumber = "+38762011223",
                    isBlocked = false,
                    DateBirth = new DateTime(1997, 5, 19, 0, 0, 0, DateTimeKind.Utc),
                    ImageUrl = "test.jpg",
                    CreatedAt = DateTime.UtcNow,
                    PasswordHash = hasher.HashPassword(null, "Korisnik123!")
                },

                new User
                {
                    Id = 11,
                    UserName = "selma.babic",
                    NormalizedUserName = "SELMA.BABIC",
                    Email = "selma.babic@etravel.com",
                    NormalizedEmail = "SELMA.BABIC@ETRAVEL.COM",
                    EmailConfirmed = true,
                    FirstName = "Selma",
                    LastName = "Babić",
                    PhoneNumber = "+38762044321",
                    isBlocked = false,
                    DateBirth = new DateTime(2001, 10, 11, 0, 0, 0, DateTimeKind.Utc),
                    ImageUrl = "test.jpg",
                    CreatedAt = DateTime.UtcNow,
                    PasswordHash = hasher.HashPassword(null, "Korisnik123!")
                },

                new User
                {
                    Id = 12,
                    UserName = "nedim.ceric",
                    NormalizedUserName = "NEDIM.CERIC",
                    Email = "nedim.ceric@etravel.com",
                    NormalizedEmail = "NEDIM.CERIC@ETRAVEL.COM",
                    EmailConfirmed = true,
                    FirstName = "Nedim",
                    LastName = "Ćerić",
                    PhoneNumber = "+38762077311",
                    isBlocked = false,
                    DateBirth = new DateTime(1994, 12, 2, 0, 0, 0, DateTimeKind.Utc),
                    ImageUrl = "test.jpg",
                    CreatedAt = DateTime.UtcNow,
                    PasswordHash = hasher.HashPassword(null, "Korisnik123!")
                },

                new User
                {
                    Id = 13,
                    UserName = "alma.vujic",
                    NormalizedUserName = "ALMA.VUJIC",
                    Email = "alma.vujic@etravel.com",
                    NormalizedEmail = "ALMA.VUJIC@ETRAVEL.COM",
                    EmailConfirmed = true,
                    FirstName = "Alma",
                    LastName = "Vujić",
                    PhoneNumber = "+38762111333",
                    isBlocked = false,
                    DateBirth = new DateTime(1996, 11, 9, 0, 0, 0, DateTimeKind.Utc),
                    ImageUrl = "test.jpg",
                    CreatedAt = DateTime.UtcNow,
                    PasswordHash = hasher.HashPassword(null, "Korisnik123!")
                },

                new User
                {
                    Id = 14,
                    UserName = "mirza.drace",
                    NormalizedUserName = "MIRZA.DRACE",
                    Email = "mirza.drace@etravel.com",
                    NormalizedEmail = "MIRZA.DRACE@ETRAVEL.COM",
                    EmailConfirmed = true,
                    FirstName = "Mirza",
                    LastName = "DračE",
                    PhoneNumber = "+38762144555",
                    isBlocked = false,
                    DateBirth = new DateTime(1992, 7, 4, 0, 0, 0, DateTimeKind.Utc),
                    ImageUrl = "test.jpg",
                    CreatedAt = DateTime.UtcNow,
                    PasswordHash = hasher.HashPassword(null, "Korisnik123!")
                },

                new User
                {
                    Id = 15,
                    UserName = "melisa.nuhanovic",
                    NormalizedUserName = "MELISA.NUHANOVIC",
                    Email = "melisa.nuhanovic@etravel.com",
                    NormalizedEmail = "MELISA.NUHANOVIC@ETRAVEL.COM",
                    EmailConfirmed = true,
                    FirstName = "Melisa",
                    LastName = "Nuhanović",
                    PhoneNumber = "+38762200333",
                    isBlocked = false,
                    DateBirth = new DateTime(2000, 6, 17, 0, 0, 0, DateTimeKind.Utc),
                    ImageUrl = "test.jpg",
                    CreatedAt = DateTime.UtcNow,
                    PasswordHash = hasher.HashPassword(null, "Korisnik123!")
                },

                new User
                {
                    Id = 16,
                    UserName = "almin.kosuta",
                    NormalizedUserName = "ALMIN.KOSUTA",
                    Email = "almin.kosuta@etravel.com",
                    NormalizedEmail = "ALMIN.KOSUTA@ETRAVEL.COM",
                    EmailConfirmed = true,
                    FirstName = "Almin",
                    LastName = "Košuta",
                    PhoneNumber = "+38762255677",
                    isBlocked = false,
                    DateBirth = new DateTime(1991, 3, 29, 0, 0, 0, DateTimeKind.Utc),
                    ImageUrl = "test.jpg",
                    CreatedAt = DateTime.UtcNow,
                    PasswordHash = hasher.HashPassword(null, "Korisnik123!")
                },

                new User
                {
                    Id = 17,
                    UserName = "dina.hodzic",
                    NormalizedUserName = "DINA.HODZIC",
                    Email = "dina.hodzic@etravel.com",
                    NormalizedEmail = "DINA.HODZIC@ETRAVEL.COM",
                    EmailConfirmed = true,
                    FirstName = "Dina",
                    LastName = "Hodžić",
                    PhoneNumber = "+38762277991",
                    isBlocked = false,
                    DateBirth = new DateTime(1998, 8, 27, 0, 0, 0, DateTimeKind.Utc),
                    ImageUrl = "test.jpg",
                    CreatedAt = DateTime.UtcNow,
                    PasswordHash = hasher.HashPassword(null, "Korisnik123!")
                },

                new User
                {
                    Id = 18,
                    UserName = "alem.celik",
                    NormalizedUserName = "ALEM.CELIK",
                    Email = "alem.celik@etravel.com",
                    NormalizedEmail = "ALEM.CELIK@ETRAVEL.COM",
                    EmailConfirmed = true,
                    FirstName = "Alem",
                    LastName = "Čelik",
                    PhoneNumber = "+38762300990",
                    isBlocked = false,
                    DateBirth = new DateTime(1997, 2, 8, 0, 0, 0, DateTimeKind.Utc),
                    ImageUrl = "test.jpg",
                    CreatedAt = DateTime.UtcNow,
                    PasswordHash = hasher.HashPassword(null, "Korisnik123!")
                },

                new User
                {
                    Id = 19,
                    UserName = "lejla.avdic",
                    NormalizedUserName = "LEJLA.AVDIC",
                    Email = "lejla.avdic@etravel.com",
                    NormalizedEmail = "LEJLA.AVDIC@ETRAVEL.COM",
                    EmailConfirmed = true,
                    FirstName = "Lejla",
                    LastName = "Avdić",
                    PhoneNumber = "+38762355123",
                    isBlocked = false,
                    DateBirth = new DateTime(2001, 1, 5, 0, 0, 0, DateTimeKind.Utc),
                    ImageUrl = "test.jpg",
                    CreatedAt = DateTime.UtcNow,
                    PasswordHash = hasher.HashPassword(null, "Korisnik123!")
                },

                new User
                {
                    Id = 20,
                    UserName = "adnan.pasalic",
                    NormalizedUserName = "ADNAN.PASALIC",
                    Email = "adnan.pasalic@etravel.com",
                    NormalizedEmail = "ADNAN.PASALIC@ETRAVEL.COM",
                    EmailConfirmed = true,
                    FirstName = "Adnan",
                    LastName = "Pašalić",
                    PhoneNumber = "+38762388321",
                    isBlocked = false,
                    DateBirth = new DateTime(1999, 9, 3, 0, 0, 0, DateTimeKind.Utc),
                    ImageUrl = "test.jpg",
                    CreatedAt = DateTime.UtcNow,
                    PasswordHash = hasher.HashPassword(null, "Korisnik123!")
                },

                new User
                {
                    Id = 21,
                    UserName = "inez.kantic",
                    NormalizedUserName = "INEZ.KANTIC",
                    Email = "inez.kantic@etravel.com",
                    NormalizedEmail = "INEZ.KANTIC@ETRAVEL.COM",
                    EmailConfirmed = true,
                    FirstName = "Inez",
                    LastName = "Kantić",
                    PhoneNumber = "+38762444123",
                    isBlocked = false,
                    DateBirth = new DateTime(1996, 4, 14, 0, 0, 0, DateTimeKind.Utc),
                    ImageUrl = "test.jpg",
                    CreatedAt = DateTime.UtcNow,
                    PasswordHash = hasher.HashPassword(null, "Korisnik123!")
                },

                new User
                {
                    Id = 22,
                    UserName = "amir.halilovic",
                    NormalizedUserName = "AMIR.HALILOVIC",
                    Email = "amir.halilovic@etravel.com",
                    NormalizedEmail = "AMIR.HALILOVIC@ETRAVEL.COM",
                    EmailConfirmed = true,
                    FirstName = "Amir",
                    LastName = "Halilović",
                    PhoneNumber = "+38762477331",
                    isBlocked = false,
                    DateBirth = new DateTime(1993, 11, 19, 0, 0, 0, DateTimeKind.Utc),
                    ImageUrl = "test.jpg",
                    CreatedAt = DateTime.UtcNow,
                    PasswordHash = hasher.HashPassword(null, "Korisnik123!")
                },

                new User
                {
                    Id = 23,
                    UserName = "lamija.kreso",
                    NormalizedUserName = "LAMIJA.KRESO",
                    Email = "lamija.kreso@etravel.com",
                    NormalizedEmail = "LAMIJA.KRESO@ETRAVEL.COM",
                    EmailConfirmed = true,
                    FirstName = "Lamija",
                    LastName = "Krešo",
                    PhoneNumber = "+38762555991",
                    isBlocked = false,
                    DateBirth = new DateTime(2002, 12, 21, 0, 0, 0, DateTimeKind.Utc),
                    ImageUrl = "test.jpg",
                    CreatedAt = DateTime.UtcNow,
                    PasswordHash = hasher.HashPassword(null, "Korisnik123!")
                },

                new User
                {
                    Id = 24,
                    UserName = "omer.smajic",
                    NormalizedUserName = "OMER.SMAJIC",
                    Email = "omer.smajic@etravel.com",
                    NormalizedEmail = "OMER.SMAJIC@ETRAVEL.COM",
                    EmailConfirmed = true,
                    FirstName = "Omer",
                    LastName = "Smajić",
                    PhoneNumber = "+38762666112",
                    isBlocked = false,
                    DateBirth = new DateTime(1998, 7, 1, 0, 0, 0, DateTimeKind.Utc),
                    ImageUrl = "test.jpg",
                    CreatedAt = DateTime.UtcNow,
                    PasswordHash = hasher.HashPassword(null, "Korisnik123!")
                }

            );

            // =========================================================================
            // 6. USER ROLES (Veza korisnika i rola)
            // =========================================================================
            builder.Entity<UserRole>().HasData(
                new UserRole { UserId = 1, RoleId = 2 }, // radnik → Radnik
                new UserRole { UserId = 2, RoleId = 3 }, // direktor → Direktor       // KORISNICI (Svi ostali)
                new UserRole { UserId = 4, RoleId = 1 },
                new UserRole { UserId = 5, RoleId = 1 },
                new UserRole { UserId = 6, RoleId = 1 },
                new UserRole { UserId = 7, RoleId = 1 },
                new UserRole { UserId = 8, RoleId = 1 },
                new UserRole { UserId = 9, RoleId = 1 },
                new UserRole { UserId = 10, RoleId = 1 },
                new UserRole { UserId = 11, RoleId = 1 },
                new UserRole { UserId = 12, RoleId = 1 },
                new UserRole { UserId = 13, RoleId = 1 },
                new UserRole { UserId = 14, RoleId = 1 },
                new UserRole { UserId = 15, RoleId = 1 },
                new UserRole { UserId = 16, RoleId = 1 },
                new UserRole { UserId = 17, RoleId = 1 },
                new UserRole { UserId = 18, RoleId = 1 },
                new UserRole { UserId = 19, RoleId = 1 },
                new UserRole { UserId = 20, RoleId = 1 },
                new UserRole { UserId = 21, RoleId = 1 },
                new UserRole { UserId = 22, RoleId = 1 },
                new UserRole { UserId = 23, RoleId = 1 },
                new UserRole { UserId = 24, RoleId = 1 }
            );

            // =========================================================================
            // 7. OFFER (ponude)
            // =========================================================================
            builder.Entity<Offer>().HasData(
                new Offer { Id = 1, Title = "Firenca", DaysInTotal = 5, WayOfTravel = "Autobus", SubCategoryId = 1 },
                new Offer { Id = 2, Title = "Santorini", DaysInTotal = 7, WayOfTravel = "Avion", SubCategoryId = 1 },
                new Offer { Id = 3, Title = "Istanbul", DaysInTotal = 4, WayOfTravel = "Autobus", SubCategoryId = 1 },
                new Offer { Id = 4, Title = "Barcelona", DaysInTotal = 6, WayOfTravel = "Avion", SubCategoryId = 7 },
                new Offer { Id = 5, Title = "Pariz", DaysInTotal = 5, WayOfTravel = "Avion", SubCategoryId = 3 },
                new Offer { Id = 6, Title = "Prag", DaysInTotal = 4, WayOfTravel = "Autobus", SubCategoryId = 12 },
                new Offer { Id = 7, Title = "Beč", DaysInTotal = 3, WayOfTravel = "Autobus", SubCategoryId = 9 },
                new Offer { Id = 8, Title = "Amsterdam", DaysInTotal = 5, WayOfTravel = "Avion", SubCategoryId = 2 },
                new Offer { Id = 9, Title = "London", DaysInTotal = 6, WayOfTravel = "Avion", SubCategoryId = 15 },
                new Offer { Id = 10, Title = "Dubai", DaysInTotal = 7, WayOfTravel = "Avion", SubCategoryId = 10 },
                new Offer { Id = 11, Title = "Kairo", DaysInTotal = 5, WayOfTravel = "Avion", SubCategoryId = 6 },
                new Offer { Id = 12, Title = "Budimpešta", DaysInTotal = 3, WayOfTravel = "Autobus", SubCategoryId = 4 },
                new Offer { Id = 13, Title = "Krakow", DaysInTotal = 4, WayOfTravel = "Autobus", SubCategoryId = 11 },
                new Offer { Id = 14, Title = "Zanzibar", DaysInTotal = 8, WayOfTravel = "Avion", SubCategoryId = 1 },
                new Offer { Id = 15, Title = "Hurgada", DaysInTotal = 7, WayOfTravel = "Avion", SubCategoryId = 13 },
                new Offer { Id = 16, Title = "Lisabon", DaysInTotal = 6, WayOfTravel = "Avion", SubCategoryId = 8 },
                new Offer { Id = 17, Title = "Atina", DaysInTotal = 5, WayOfTravel = "Avion", SubCategoryId = 5 },
                new Offer { Id = 18, Title = "Split", DaysInTotal = 3, WayOfTravel = "Autobus", SubCategoryId = 14 }

            );

            // =========================================================================
            // 8. OFFER DETAILS
            // =========================================================================
            builder.Entity<OfferDetails>().HasData(
                new OfferDetails
                {
                    OfferId = 1,
                    MinimalPrice = 450,
                    Description = "Putovanje u prekrasnu Firencu.",
                    City = "Firenca",
                    Country = "Italija",
                    ResidenceTaxPerDay = 2.00m,
                    ResidenceTotal = 10m,
                    TravelInsuranceTotal = 15m,
                    TotalCountOfReservations = 98
                },
                new OfferDetails
                {
                    OfferId = 2,
                    MinimalPrice = 900,
                    Description = "Santorini – raj na zemlji.",
                    City = "Santorini",
                    Country = "Grčka",
                    ResidenceTaxPerDay = 3.00m,
                    ResidenceTotal = 21m,
                    TravelInsuranceTotal = 25m,
                    TotalCountOfReservations = 65
                },
                new OfferDetails
                {
                    OfferId = 3,
                    MinimalPrice = 350,
                    Description = "Istanbul – čarolija dva kontinenta.",
                    City = "Istanbul",
                    Country = "Turska",
                    ResidenceTaxPerDay = 1.50m,
                    ResidenceTotal = 6m,
                    TravelInsuranceTotal = 12m,
                    TotalCountOfReservations = 53
                },
                new OfferDetails
                {
                    OfferId = 4,
                    MinimalPrice = 750,
                    Description = "Barcelona – vibrantan grad umjetnosti i mora.",
                    City = "Barcelona",
                    Country = "Španija",
                    ResidenceTaxPerDay = 3.50m,
                    ResidenceTotal = 21m,
                    TravelInsuranceTotal = 20m,
                    TotalCountOfReservations = 91
                },
                new OfferDetails
                {
                    OfferId = 5,
                    MinimalPrice = 820,
                    Description = "Pariz – grad svjetlosti i romantike.",
                    City = "Pariz",
                    Country = "Francuska",
                    ResidenceTaxPerDay = 2.50m,
                    ResidenceTotal = 12.5m,
                    TravelInsuranceTotal = 18m,
                    TotalCountOfReservations = 140
                },
                new OfferDetails
                {
                    OfferId = 6,
                    MinimalPrice = 390,
                    Description = "Prag – grad stotinu tornjeva.",
                    City = "Prag",
                    Country = "Češka",
                    ResidenceTaxPerDay = 1.20m,
                    ResidenceTotal = 4.8m,
                    TravelInsuranceTotal = 10m,
                    TotalCountOfReservations = 67
                },
                new OfferDetails
                {
                    OfferId = 7,
                    MinimalPrice = 250,
                    Description = "Beč – istorija, umjetnost i kraljevska arhitektura.",
                    City = "Beč",
                    Country = "Austrija",
                    ResidenceTaxPerDay = 1.80m,
                    ResidenceTotal = 5.4m,
                    TravelInsuranceTotal = 10m,
                    TotalCountOfReservations = 102
                },
                new OfferDetails
                {
                    OfferId = 8,
                    MinimalPrice = 860,
                    Description = "Amsterdam – kanali, muzeji i jedinstvena atmosfera.",
                    City = "Amsterdam",
                    Country = "Nizozemska",
                    ResidenceTaxPerDay = 3.00m,
                    ResidenceTotal = 15m,
                    TravelInsuranceTotal = 22m,
                    TotalCountOfReservations = 119
                },
                new OfferDetails
                {
                    OfferId = 9,
                    MinimalPrice = 950,
                    Description = "London – tradicija i moderna kultura.",
                    City = "London",
                    Country = "Ujedinjeno Kraljevstvo",
                    ResidenceTaxPerDay = 3.20m,
                    ResidenceTotal = 19.2m,
                    TravelInsuranceTotal = 24m,
                    TotalCountOfReservations = 155
                },
                new OfferDetails
                {
                    OfferId = 10,
                    MinimalPrice = 1300,
                    Description = "Dubai – luksuz, pustinja i avantura.",
                    City = "Dubai",
                    Country = "UAE",
                    ResidenceTaxPerDay = 5.00m,
                    ResidenceTotal = 35m,
                    TravelInsuranceTotal = 30m,
                    TotalCountOfReservations = 174
                },
                new OfferDetails
                {
                    OfferId = 11,
                    MinimalPrice = 780,
                    Description = "Kairo – drevne piramide i Nil.",
                    City = "Kairo",
                    Country = "Egipat",
                    ResidenceTaxPerDay = 2.20m,
                    ResidenceTotal = 11m,
                    TravelInsuranceTotal = 20m,
                    TotalCountOfReservations = 94
                },
                new OfferDetails
                {
                    OfferId = 12,
                    MinimalPrice = 230,
                    Description = "Budimpešta – čuvena termalna oaza.",
                    City = "Budimpešta",
                    Country = "Mađarska",
                    ResidenceTaxPerDay = 1.50m,
                    ResidenceTotal = 4.5m,
                    TravelInsuranceTotal = 9m,
                    TotalCountOfReservations = 88
                },
                new OfferDetails
                {
                    OfferId = 13,
                    MinimalPrice = 310,
                    Description = "Krakow – historijski dragulj Poljske.",
                    City = "Krakow",
                    Country = "Poljska",
                    ResidenceTaxPerDay = 1.40m,
                    ResidenceTotal = 5.6m,
                    TravelInsuranceTotal = 11m,
                    TotalCountOfReservations = 73
                },
                new OfferDetails
                {
                    OfferId = 14,
                    MinimalPrice = 1600,
                    Description = "Zanzibar – egzotični raj u Indijskom okeanu.",
                    City = "Zanzibar",
                    Country = "Tanzanija",
                    ResidenceTaxPerDay = 4.00m,
                    ResidenceTotal = 32m,
                    TravelInsuranceTotal = 35m,
                    TotalCountOfReservations = 61
                },
                new OfferDetails
                {
                    OfferId = 15,
                    MinimalPrice = 1100,
                    Description = "Hurgada – idealan izbor za all inclusive odmor.",
                    City = "Hurgada",
                    Country = "Egipat",
                    ResidenceTaxPerDay = 3.00m,
                    ResidenceTotal = 21m,
                    TravelInsuranceTotal = 25m,
                    TotalCountOfReservations = 108
                },
                new OfferDetails
                {
                    OfferId = 16,
                    MinimalPrice = 780,
                    Description = "Lisabon – šarmantni grad na obali Atlantika.",
                    City = "Lisabon",
                    Country = "Portugal",
                    ResidenceTaxPerDay = 2.50m,
                    ResidenceTotal = 15m,
                    TravelInsuranceTotal = 22m,
                    TotalCountOfReservations = 97
                },
                new OfferDetails
                {
                    OfferId = 17,
                    MinimalPrice = 520,
                    Description = "Atina – kolijevka civilizacije.",
                    City = "Atina",
                    Country = "Grčka",
                    ResidenceTaxPerDay = 2.00m,
                    ResidenceTotal = 10m,
                    TravelInsuranceTotal = 16m,
                    TotalCountOfReservations = 121
                },
                new OfferDetails
                {
                    OfferId = 18,
                    MinimalPrice = 180,
                    Description = "Split – uživanje na jadranskoj obali.",
                    City = "Split",
                    Country = "Hrvatska",
                    ResidenceTaxPerDay = 1.00m,
                    ResidenceTotal = 3m,
                    TravelInsuranceTotal = 8m,
                    TotalCountOfReservations = 112
                }
            );

            builder.Entity<OfferImage>().HasData(
    // OFFER 1
    new OfferImage { Id = 1, OfferId = 1, ImageUrl = OfferImage(1, 1), isMain = true },
    new OfferImage { Id = 2, OfferId = 1, ImageUrl = OfferImage(1, 2), isMain = false },
    new OfferImage { Id = 3, OfferId = 1, ImageUrl = OfferImage(1, 3), isMain = false },
    new OfferImage { Id = 4, OfferId = 1, ImageUrl = OfferImage(1, 4), isMain = false },

    // OFFER 2
    new OfferImage { Id = 5, OfferId = 2, ImageUrl = OfferImage(2, 1), isMain = true },
    new OfferImage { Id = 6, OfferId = 2, ImageUrl = OfferImage(2, 2), isMain = false },
    new OfferImage { Id = 7, OfferId = 2, ImageUrl = OfferImage(2, 3), isMain = false },
    new OfferImage { Id = 8, OfferId = 2, ImageUrl = OfferImage(2, 4), isMain = false },

    // OFFER 3
    new OfferImage { Id = 9, OfferId = 3, ImageUrl = OfferImage(3, 1), isMain = true },
    new OfferImage { Id = 10, OfferId = 3, ImageUrl = OfferImage(3, 2), isMain = false },
    new OfferImage { Id = 11, OfferId = 3, ImageUrl = OfferImage(3, 3), isMain = false },
    new OfferImage { Id = 12, OfferId = 3, ImageUrl = OfferImage(3, 4), isMain = false },

    // OFFER 4
    new OfferImage { Id = 13, OfferId = 4, ImageUrl = OfferImage(4, 1), isMain = true },
    new OfferImage { Id = 14, OfferId = 4, ImageUrl = OfferImage(4, 2), isMain = false },
    new OfferImage { Id = 15, OfferId = 4, ImageUrl = OfferImage(4, 3), isMain = false },
    new OfferImage { Id = 16, OfferId = 4, ImageUrl = OfferImage(4, 4), isMain = false },

    // OFFER 5
    new OfferImage { Id = 17, OfferId = 5, ImageUrl = OfferImage(5, 1), isMain = true },
    new OfferImage { Id = 18, OfferId = 5, ImageUrl = OfferImage(5, 2), isMain = false },
    new OfferImage { Id = 19, OfferId = 5, ImageUrl = OfferImage(5, 3), isMain = false },
    new OfferImage { Id = 20, OfferId = 5, ImageUrl = OfferImage(5, 4), isMain = false },

    // OFFER 6
    new OfferImage { Id = 21, OfferId = 6, ImageUrl = OfferImage(6, 1), isMain = true },
    new OfferImage { Id = 22, OfferId = 6, ImageUrl = OfferImage(6, 2), isMain = false },
    new OfferImage { Id = 23, OfferId = 6, ImageUrl = OfferImage(6, 3), isMain = false },
    new OfferImage { Id = 24, OfferId = 6, ImageUrl = OfferImage(6, 4), isMain = false },

    // OFFER 7
    new OfferImage { Id = 25, OfferId = 7, ImageUrl = OfferImage(7, 1), isMain = true },
    new OfferImage { Id = 26, OfferId = 7, ImageUrl = OfferImage(7, 2), isMain = false },
    new OfferImage { Id = 27, OfferId = 7, ImageUrl = OfferImage(7, 3), isMain = false },
    new OfferImage { Id = 28, OfferId = 7, ImageUrl = OfferImage(7, 4), isMain = false },

    // OFFER 8
    new OfferImage { Id = 29, OfferId = 8, ImageUrl = OfferImage(8, 1), isMain = true },
    new OfferImage { Id = 30, OfferId = 8, ImageUrl = OfferImage(8, 2), isMain = false },
    new OfferImage { Id = 31, OfferId = 8, ImageUrl = OfferImage(8, 3), isMain = false },
    new OfferImage { Id = 32, OfferId = 8, ImageUrl = OfferImage(8, 4), isMain = false },

    // OFFER 9
    new OfferImage { Id = 33, OfferId = 9, ImageUrl = OfferImage(9, 1), isMain = true },
    new OfferImage { Id = 34, OfferId = 9, ImageUrl = OfferImage(9, 2), isMain = false },
    new OfferImage { Id = 35, OfferId = 9, ImageUrl = OfferImage(9, 3), isMain = false },
    new OfferImage { Id = 36, OfferId = 9, ImageUrl = OfferImage(9, 4), isMain = false },

    // OFFER 10
    new OfferImage { Id = 37, OfferId = 10, ImageUrl = OfferImage(10, 1), isMain = true },
    new OfferImage { Id = 38, OfferId = 10, ImageUrl = OfferImage(10, 2), isMain = false },
    new OfferImage { Id = 39, OfferId = 10, ImageUrl = OfferImage(10, 3), isMain = false },
    new OfferImage { Id = 40, OfferId = 10, ImageUrl = OfferImage(10, 4), isMain = false },

    // OFFER 11
    new OfferImage { Id = 41, OfferId = 11, ImageUrl = OfferImage(11, 1), isMain = true },
    new OfferImage { Id = 42, OfferId = 11, ImageUrl = OfferImage(11, 2), isMain = false },
    new OfferImage { Id = 43, OfferId = 11, ImageUrl = OfferImage(11, 3), isMain = false },
    new OfferImage { Id = 44, OfferId = 11, ImageUrl = OfferImage(11, 4), isMain = false },

    // OFFER 12
    new OfferImage { Id = 45, OfferId = 12, ImageUrl = OfferImage(12, 1), isMain = true },
    new OfferImage { Id = 46, OfferId = 12, ImageUrl = OfferImage(12, 2), isMain = false },
    new OfferImage { Id = 47, OfferId = 12, ImageUrl = OfferImage(12, 3), isMain = false },
    new OfferImage { Id = 48, OfferId = 12, ImageUrl = OfferImage(12, 4), isMain = false },

    // OFFER 13
    new OfferImage { Id = 49, OfferId = 13, ImageUrl = OfferImage(13, 1), isMain = true },
    new OfferImage { Id = 50, OfferId = 13, ImageUrl = OfferImage(13, 2), isMain = false },
    new OfferImage { Id = 51, OfferId = 13, ImageUrl = OfferImage(13, 3), isMain = false },
    new OfferImage { Id = 52, OfferId = 13, ImageUrl = OfferImage(13, 4), isMain = false },

    // OFFER 14
    new OfferImage { Id = 53, OfferId = 14, ImageUrl = OfferImage(14, 1), isMain = true },
    new OfferImage { Id = 54, OfferId = 14, ImageUrl = OfferImage(14, 2), isMain = false },
    new OfferImage { Id = 55, OfferId = 14, ImageUrl = OfferImage(14, 3), isMain = false },
    new OfferImage { Id = 56, OfferId = 14, ImageUrl = OfferImage(14, 4), isMain = false },

    // OFFER 15
    new OfferImage { Id = 57, OfferId = 15, ImageUrl = OfferImage(15, 1), isMain = true },
    new OfferImage { Id = 58, OfferId = 15, ImageUrl = OfferImage(15, 2), isMain = false },
    new OfferImage { Id = 59, OfferId = 15, ImageUrl = OfferImage(15, 3), isMain = false },
    new OfferImage { Id = 60, OfferId = 15, ImageUrl = OfferImage(15, 4), isMain = false },

    // OFFER 16
    new OfferImage { Id = 61, OfferId = 16, ImageUrl = OfferImage(16, 1), isMain = true },
    new OfferImage { Id = 62, OfferId = 16, ImageUrl = OfferImage(16, 2), isMain = false },
    new OfferImage { Id = 63, OfferId = 16, ImageUrl = OfferImage(16, 3), isMain = false },
    new OfferImage { Id = 64, OfferId = 16, ImageUrl = OfferImage(16, 4), isMain = false },

    // OFFER 17
    new OfferImage { Id = 65, OfferId = 17, ImageUrl = OfferImage(17, 1), isMain = true },
    new OfferImage { Id = 66, OfferId = 17, ImageUrl = OfferImage(17, 2), isMain = false },
    new OfferImage { Id = 67, OfferId = 17, ImageUrl = OfferImage(17, 3), isMain = false },
    new OfferImage { Id = 68, OfferId = 17, ImageUrl = OfferImage(17, 4), isMain = false },

    // OFFER 18
    new OfferImage { Id = 69, OfferId = 18, ImageUrl = OfferImage(18, 1), isMain = true },
    new OfferImage { Id = 70, OfferId = 18, ImageUrl = OfferImage(18, 2), isMain = false },
    new OfferImage { Id = 71, OfferId = 18, ImageUrl = OfferImage(18, 3), isMain = false },
    new OfferImage { Id = 72, OfferId = 18, ImageUrl = OfferImage(18, 4), isMain = false }
);




            // =========================================================================
            // 9.1. OFFER PLAN DAYS 
            // =========================================================================

            // ======================================================================
            // OFFER PLAN DAYS
            // ======================================================================

            // ----------------------
            // OFFER 1 – FIRENCA (5)
            // ----------------------
            builder.Entity<OfferPlanDay>().HasData(
                new OfferPlanDay
                {
                    OfferDetailsId = 1,
                    DayNumber = 1,
                    DayTitle = "Polazak i dolazak u Firencu",
                    DayDescription = "Polazak u ranim jutarnjim satima. Pauze tokom puta. Dolazak u Firencu u poslijepodnevnim satima. Smještaj u hotel i slobodno vrijeme."
                },
                new OfferPlanDay
                {
                    OfferDetailsId = 1,
                    DayNumber = 2,
                    DayTitle = "Upoznavanje sa starim gradom",
                    DayDescription = "Razgledanje: Katedrala Santa Maria del Fiore, Piazza della Signoria, Palazzo Vecchio i Ponte Vecchio."
                },
                new OfferPlanDay
                {
                    OfferDetailsId = 1,
                    DayNumber = 3,
                    DayTitle = "Galerija Uffizi",
                    DayDescription = "Posjeta galeriji Uffizi i slobodno vrijeme za individualne aktivnosti."
                },
                new OfferPlanDay
                {
                    OfferDetailsId = 1,
                    DayNumber = 4,
                    DayTitle = "Izlet u Pisu ili slobodan dan",
                    DayDescription = "Fakultativni izlet u Pisu ili slobodno vrijeme u Firenci."
                },
                new OfferPlanDay
                {
                    OfferDetailsId = 1,
                    DayNumber = 5,
                    DayTitle = "Povratak kući",
                    DayDescription = "Odjava iz hotela i povratak kući."
                }
            );

            // ----------------------
            // OFFER 2 – SANTORINI (7)
            // ----------------------
            builder.Entity<OfferPlanDay>().HasData(
                new OfferPlanDay { OfferDetailsId = 2, DayNumber = 1, DayTitle = "Dolazak na Santorini", DayDescription = "Dolazak i smještaj u hotel." },
                new OfferPlanDay { OfferDetailsId = 2, DayNumber = 2, DayTitle = "Fira – glavni grad ostrva", DayDescription = "Obilazak Fire i slobodno vrijeme." },
                new OfferPlanDay { OfferDetailsId = 2, DayNumber = 3, DayTitle = "Oia – zalazak sunca", DayDescription = "Posjeta Oiji i uživanje u zalasku sunca." },
                new OfferPlanDay { OfferDetailsId = 2, DayNumber = 4, DayTitle = "Vulkanske plaže", DayDescription = "Obilazak crne i crvene plaže." },
                new OfferPlanDay { OfferDetailsId = 2, DayNumber = 5, DayTitle = "Vulkansko ostrvo", DayDescription = "Izlet brodom i termalni izvori." },
                new OfferPlanDay { OfferDetailsId = 2, DayNumber = 6, DayTitle = "Slobodan dan", DayDescription = "Odmor ili fakultativne aktivnosti." },
                new OfferPlanDay { OfferDetailsId = 2, DayNumber = 7, DayTitle = "Povratak kući", DayDescription = "Odjava iz hotela i povratak kući." }
            );

            // ----------------------
            // OFFER 3 – ISTANBUL (4)
            // ----------------------
            builder.Entity<OfferPlanDay>().HasData(
                new OfferPlanDay { OfferDetailsId = 3, DayNumber = 1, DayTitle = "Dolazak u Istanbul", DayDescription = "Dolazak i smještaj u hotel." },
                new OfferPlanDay { OfferDetailsId = 3, DayNumber = 2, DayTitle = "Sultanahmet", DayDescription = "Aja Sofija, Plava džamija i Topkapi palata." },
                new OfferPlanDay { OfferDetailsId = 3, DayNumber = 3, DayTitle = "Bosfor i Taksim", DayDescription = "Krstarenje Bosforom i šetnja Istiklal ulicom." },
                new OfferPlanDay { OfferDetailsId = 3, DayNumber = 4, DayTitle = "Povratak kući", DayDescription = "Odjava iz hotela i povratak kući." }
            );

            // ======================================================================
            // AUTOMATSKI PLANOVI ZA OSTALE OFFERE
            // ======================================================================

            void AddGenericPlanDays(int offerId, string city, int days)
            {
                for (int day = 1; day <= days; day++)
                {
                    builder.Entity<OfferPlanDay>().HasData(
                        new OfferPlanDay
                        {
                            OfferDetailsId = offerId,
                            DayNumber = day,
                            DayTitle = day switch
                            {
                                1 => $"Dolazak u {city}",
                                var d when d == days => "Povratak kući",
                                _ => $"Boravak u {city}"
                            },
                            DayDescription = day switch
                            {
                                1 => $"Dolazak u {city}, smještaj u hotel i slobodno vrijeme.",
                                var d when d == days => "Odjava iz hotela i povratak kući prema planu putovanja.",
                                _ => $"Slobodno vrijeme za razgledanje, izlete i uživanje u gradu {city}."
                            }
                        }
                    );
                }
            }

            AddGenericPlanDays(4, "Barcelona", 8);
            AddGenericPlanDays(5, "Paris", 6);
            AddGenericPlanDays(6, "Prague", 11);
            AddGenericPlanDays(7, "Vienna", 9);
            AddGenericPlanDays(8, "Amsterdam", 9);
            AddGenericPlanDays(9, "London", 4);
            AddGenericPlanDays(10, "Dubai", 7);
            AddGenericPlanDays(11, "Cairo", 10);
            AddGenericPlanDays(12, "Budapest", 7);
            AddGenericPlanDays(13, "Krakow", 6);
            AddGenericPlanDays(14, "Zanzibar", 4);
            AddGenericPlanDays(15, "Hurghada", 6);
            AddGenericPlanDays(16, "Lisbon", 8);
            AddGenericPlanDays(17, "Athens", 4);
            AddGenericPlanDays(18, "Split", 6);



            // =========================================================================
            // 10. HOTELS (3 po ponudi)
            // =========================================================================
            builder.Entity<Hotel>().HasData(
    // =========================
    // FIRENCA (3)
    // =========================
    new Hotel { Id = 100, Name = "Hotel Medici", Address = "Via Roma 12", Stars = 4 },
    new Hotel { Id = 101, Name = "Hotel Firenze Centro", Address = "Piazza Duomo 2", Stars = 3 },
    new Hotel { Id = 102, Name = "Hotel Ponte Vecchio", Address = "Ponte Vecchio 5", Stars = 5 },

    // =========================
    // SANTORINI (3)
    // =========================
    new Hotel { Id = 110, Name = "Blue Dome Resort", Address = "Santorini Beach 9", Stars = 5 },
    new Hotel { Id = 111, Name = "Aegean View", Address = "Oia Street 44", Stars = 4 },
    new Hotel { Id = 112, Name = "White Cave Hotel", Address = "Fira 21", Stars = 3 },

    // =========================
    // ISTANBUL (3)
    // =========================
    new Hotel { Id = 120, Name = "Hotel Sultanahmet", Address = "Sultanahmet 1", Stars = 4 },
    new Hotel { Id = 121, Name = "Galata Inn", Address = "Galata 5", Stars = 3 },
    new Hotel { Id = 122, Name = "Bosfor Palace Hotel", Address = "Bosfor Blvd 7", Stars = 5 },

    // =========================
    // BARCELONA (3)
    // =========================
    new Hotel { Id = 200, Name = "Hotel Condal Barcelona", Address = "La Rambla 12", Stars = 3 },
    new Hotel { Id = 215, Name = "Barcelona Central Plaza", Address = "Carrer de Pelai 45", Stars = 4 },
    new Hotel { Id = 216, Name = "Sagrada Familia View Hotel", Address = "Carrer de Mallorca 401", Stars = 5 },

    // =========================
    // PARIS (3)
    // =========================
    new Hotel { Id = 201, Name = "Hotel Louvre Rivoli", Address = "Rue de Rivoli 99", Stars = 4 },
    new Hotel { Id = 217, Name = "Eiffel Tower Boutique Hotel", Address = "Avenue de Suffren 22", Stars = 5 },
    new Hotel { Id = 218, Name = "Montmartre Comfort Inn", Address = "Rue Lepic 88", Stars = 3 },

    // =========================
    // PRAGUE (3)
    // =========================
    new Hotel { Id = 202, Name = "Hotel Charles Bridge Inn", Address = "Mostecká 7", Stars = 4 },
    new Hotel { Id = 219, Name = "Old Town Square Hotel", Address = "Staroměstské náměstí 15", Stars = 5 },
    new Hotel { Id = 220, Name = "Prague City Stay", Address = "Vodičkova 12", Stars = 3 },

    // =========================
    // VIENNA (3)
    // =========================
    new Hotel { Id = 203, Name = "Hotel Kaiserhof Wien", Address = "Frankenberggasse 10", Stars = 4 },
    new Hotel { Id = 221, Name = "Vienna Opera House Hotel", Address = "Opernring 5", Stars = 5 },
    new Hotel { Id = 222, Name = "Danube Riverside Hotel", Address = "Handelskai 94", Stars = 3 },

    // =========================
    // AMSTERDAM (3)
    // =========================
    new Hotel { Id = 204, Name = "Hotel Amsterdam Canal View", Address = "Keizersgracht 84", Stars = 5 },
    new Hotel { Id = 223, Name = "Dam Square Boutique Hotel", Address = "Damrak 50", Stars = 4 },
    new Hotel { Id = 224, Name = "Amsterdam City Budget Hotel", Address = "Leidsekade 70", Stars = 3 },

    // =========================
    // LONDON (3)
    // =========================
    new Hotel { Id = 205, Name = "Hotel Westminster London", Address = "Victoria Street 22", Stars = 4 },
    new Hotel { Id = 225, Name = "Piccadilly Central Hotel", Address = "Piccadilly 33", Stars = 5 },
    new Hotel { Id = 226, Name = "London Bridge Inn", Address = "Tooley Street 18", Stars = 3 },

    // =========================
    // DUBAI (3)
    // =========================
    new Hotel { Id = 206, Name = "Dubai Marina Pearl Hotel", Address = "Dubai Marina Walk 5", Stars = 5 },
    new Hotel { Id = 227, Name = "Burj Khalifa View Hotel", Address = "Downtown Dubai 1", Stars = 5 },
    new Hotel { Id = 228, Name = "Dubai Creek City Hotel", Address = "Al Seef Street 9", Stars = 4 },

    // =========================
    // CAIRO (3)
    // =========================
    new Hotel { Id = 207, Name = "Cairo Pyramids View Hotel", Address = "Pyramid Street 18", Stars = 4 },
    new Hotel { Id = 229, Name = "Nile Riverside Palace", Address = "Corniche El Nil 101", Stars = 5 },
    new Hotel { Id = 230, Name = "Downtown Cairo Hotel", Address = "Tahrir Square 6", Stars = 3 },

    // =========================
    // BUDAPEST (3)
    // =========================
    new Hotel { Id = 208, Name = "Budapest Royal Center Hotel", Address = "Váci Utca 33", Stars = 3 },
    new Hotel { Id = 231, Name = "Danube Panorama Hotel", Address = "Bem Rakpart 15", Stars = 4 },
    new Hotel { Id = 232, Name = "Thermal Spa Boutique Hotel", Address = "Andrássy út 45", Stars = 5 },

    // =========================
    // KRAKOW (3)
    // =========================
    new Hotel { Id = 209, Name = "Krakow Old Town Plaza Hotel", Address = "Floriańska 15", Stars = 4 },
    new Hotel { Id = 233, Name = "Wawel Castle View Hotel", Address = "Kanonicza 8", Stars = 5 },
    new Hotel { Id = 234, Name = "Krakow City Budget Stay", Address = "Dietla 60", Stars = 3 },

    // =========================
    // ZANZIBAR (3)
    // =========================
    new Hotel { Id = 210, Name = "Zanzibar Blue Lagoon Resort", Address = "Kendwa Beach 1", Stars = 5 },
    new Hotel { Id = 235, Name = "Stone Town Heritage Hotel", Address = "Mkunazini Street 14", Stars = 4 },
    new Hotel { Id = 236, Name = "Zanzibar Beach Paradise", Address = "Nungwi Coast 7", Stars = 5 },

    // =========================
    // HURGHADA (3)
    // =========================
    new Hotel { Id = 211, Name = "Hurghada Golden Sand Resort", Address = "Sheraton Road 55", Stars = 5 },
    new Hotel { Id = 237, Name = "Red Sea Coral Resort", Address = "El Mamsha 22", Stars = 4 },
    new Hotel { Id = 238, Name = "Hurghada Sunlight Hotel", Address = "Village Road 9", Stars = 3 },

    // =========================
    // LISBON (3)
    // =========================
    new Hotel { Id = 212, Name = "Lisbon Alfama Boutique Hotel", Address = "Rua dos Remédios 21", Stars = 4 },
    new Hotel { Id = 239, Name = "Belem Riverside Hotel", Address = "Avenida Brasília 78", Stars = 5 },
    new Hotel { Id = 240, Name = "Lisbon Downtown Stay", Address = "Rua Augusta 120", Stars = 3 },

    // =========================
    // ATHENS (3)
    // =========================
    new Hotel { Id = 213, Name = "Acropolis View Hotel Athens", Address = "Dionysiou Areopagitou 8", Stars = 3 },
    new Hotel { Id = 241, Name = "Plaka Heritage Hotel", Address = "Adrianou 30", Stars = 4 },
    new Hotel { Id = 242, Name = "Athens Central Palace", Address = "Syntagma Square 3", Stars = 5 },

    // =========================
    // SPLIT (3)
    // =========================
    new Hotel { Id = 214, Name = "Hotel Adriatic Split", Address = "Obala Hrvatskog Narodnog 17", Stars = 4 },
    new Hotel { Id = 243, Name = "Diocletian Palace Boutique Hotel", Address = "Peristil 2", Stars = 5 },
    new Hotel { Id = 244, Name = "Split City Beach Hotel", Address = "Bačvice 14", Stars = 3 }
);


            // =========================================================================
            // 11. HOTEL IMAGES (1 main + 3 dodatne)
            // =========================================================================
            int hid = 1;

            void AddHotelImages(int hotelId)
            {
                builder.Entity<HotelImages>().HasData(
                    new HotelImages
                    {
                        Id = hid++,
                        HotelId = hotelId,
                        ImageUrl = HotelImage(hotelId, 1),
                        IsMain = true
                    },
                    new HotelImages
                    {
                        Id = hid++,
                        HotelId = hotelId,
                        ImageUrl = HotelImage(hotelId, 2),
                        IsMain = false
                    },
                    new HotelImages
                    {
                        Id = hid++,
                        HotelId = hotelId,
                        ImageUrl = HotelImage(hotelId, 3),
                        IsMain = false
                    },
                    new HotelImages
                    {
                        Id = hid++,
                        HotelId = hotelId,
                        ImageUrl = HotelImage(hotelId, 4),
                        IsMain = false
                    }
                );
            }


            foreach (var hotelId in new[]
            {
    // Firenca
    100, 101, 102,
    // Santorini
    110, 111, 112,
    // Istanbul
    120, 121, 122,

    // Barcelona
    200, 215, 216,
    // Paris
    201, 217, 218,
    // Prague
    202, 219, 220,
    // Vienna
    203, 221, 222,
    // Amsterdam
    204, 223, 224,
    // London
    205, 225, 226,
    // Dubai
    206, 227, 228,
    // Cairo
    207, 229, 230,
    // Budapest
    208, 231, 232,
    // Krakow
    209, 233, 234,
    // Zanzibar
    210, 235, 236,
    // Hurghada
    211, 237, 238,
    // Lisbon
    212, 239, 240,
    // Athens
    213, 241, 242,
    // Split
    214, 243, 244
})
            {
                AddHotelImages(hotelId);
            }




            // =========================================================================
            // 12. OFFER → HOTELS (UTC)
            // =========================================================================
            var departures = new[]
{
    new DateTime(2026, 3, 2, 0, 0, 0, DateTimeKind.Utc),
    new DateTime(2026, 5, 15, 0, 0, 0, DateTimeKind.Utc),
    new DateTime(2026, 8, 27, 0, 0, 0, DateTimeKind.Utc)
};


            var offerHotelMap = new[]
            {
    // Firenca (5 dana)
    new { OfferDetailsId = 1, Days = 5, Hotels = new[] { 100, 101, 102 } },

    // Santorini (7 dana)
    new { OfferDetailsId = 2, Days = 7, Hotels = new[] { 110, 111, 112 } },

    // Istanbul (4 dana)
    new { OfferDetailsId = 3, Days = 4, Hotels = new[] { 120, 121, 122 } },

    // Ostali – po 3 hotela (pretpostavka koju si ranije definisao)
    new { OfferDetailsId = 4, Days = 6, Hotels = new[] { 200, 215, 216 } },
    new { OfferDetailsId = 5, Days = 5, Hotels = new[] { 201, 217, 218 } },
    new { OfferDetailsId = 6, Days = 4, Hotels = new[] { 202, 219, 220 } },
    new { OfferDetailsId = 7, Days = 3, Hotels = new[] { 203, 221, 222 } },
    new { OfferDetailsId = 8, Days = 5, Hotels = new[] { 204, 223, 224 } },
    new { OfferDetailsId = 9, Days = 6, Hotels = new[] { 205, 225, 226 } },
    new { OfferDetailsId = 10, Days = 7, Hotels = new[] { 206, 227, 228 } },
    new { OfferDetailsId = 11, Days = 5, Hotels = new[] { 207, 229, 230 } },
    new { OfferDetailsId = 12, Days = 3, Hotels = new[] { 208, 231, 232 } },
    new { OfferDetailsId = 13, Days = 4, Hotels = new[] { 209, 233, 234 } },
    new { OfferDetailsId = 14, Days = 8, Hotels = new[] { 210, 235, 236 } },
    new { OfferDetailsId = 15, Days = 7, Hotels = new[] { 211, 237, 238 } },
    new { OfferDetailsId = 16, Days = 6, Hotels = new[] { 212, 239, 240 } },
    new { OfferDetailsId = 17, Days = 5, Hotels = new[] { 213, 241, 242 } },
    new { OfferDetailsId = 18, Days = 3, Hotels = new[] { 214, 243, 244 } }
};

            foreach (var offer in offerHotelMap)
            {
                foreach (var hotelId in offer.Hotels)
                {
                    foreach (var dep in departures)
                    {
                        builder.Entity<OfferHotels>().HasData(
                            new OfferHotels
                            {
                                OfferDetailsId = offer.OfferDetailsId,
                                HotelId = hotelId,
                                DepartureDate = dep,
                                ReturnDate = dep.AddDays(offer.Days)
                            }
                        );
                    }
                }
            }




            // =========================================================================
            // 13. ROOMS
            // =========================================================================

            builder.Entity<Rooms>().HasData(
            new Rooms { Id = 1, RoomType = "Dvokrevetna" },
            new Rooms { Id = 2, RoomType = "Trokrevetna" },
            new Rooms { Id = 3, RoomType = "Petokrevetna" },
            new Rooms { Id = 4, RoomType = "Cetverokrevetna" }
            );

            // =========================================================================
            // 14. HOTEL ROOMS
            // =========================================================================

            var roomIds = new[] { 1, 2, 3, 4 };

            var hotels = new[]
            {
    // FIRENCA
    100, 101, 102,
    // SANTORINI
    110, 111, 112,
    // ISTANBUL
    120, 121, 122,

    // BARCELONA
    200, 215, 216,
    // PARIS
    201, 217, 218,
    // PRAGUE
    202, 219, 220,
    // VIENNA
    203, 221, 222,
    // AMSTERDAM
    204, 223, 224,
    // LONDON
    205, 225, 226,
    // DUBAI
    206, 227, 228,
    // CAIRO
    207, 229, 230,
    // BUDAPEST
    208, 231, 232,
    // KRAKOW
    209, 233, 234,
    // ZANZIBAR
    210, 235, 236,
    // HURGHADA
    211, 237, 238,
    // LISBON
    212, 239, 240,
    // ATHENS
    213, 241, 242,
    // SPLIT
    214, 243, 244
};

            // 🔒 FIXED SEED → uvijek isti “random”
            var rng = new Random(2026);

            foreach (var hotelId in hotels)
            {
                foreach (var roomId in roomIds)
                {
                    // Random 5–15 (5 uključeno, 16 nije)
                    var roomsLeft = rng.Next(5, 16);

                    builder.Entity<HotelRooms>().HasData(
                        new HotelRooms
                        {
                            HotelId = hotelId,
                            RoomId = roomId,
                            RoomsLeft = roomsLeft
                        }
                    );
                }
            }



            // =========================================================================
            // 15. RESERVATIONS (UserId 5–24)
            // =========================================================================

            builder.Entity<Reservation>().HasData(


        new Reservation
        {
            Id = 2000,
            UserId = 5,
            OfferId = 1,
            HotelId = 100,
            RoomId = 1,
            IsActive = true,
            IncludeInsurance = true,
            isFirstRatePaid = true,
            isFullPaid = false,
            isDiscountUsed = false,
            DiscountValue = 0,
            TotalPrice = 450,
            PriceLeftToPay = 150,
            CreatedAt = new DateTime(2025, 12, 1, 0, 0, 0, DateTimeKind.Utc),
            addedNeeds = "Vegetarijanski meni"
        },

        new Reservation
        {
            Id = 2001,
            UserId = 6,
            OfferId = 2,
            HotelId = 110,
            RoomId = 2,
            IsActive = true,
            IncludeInsurance = false,
            isFirstRatePaid = false,
            isFullPaid = true,
            isDiscountUsed = false,
            DiscountValue = 0,
            TotalPrice = 900,
            PriceLeftToPay = 900,
            CreatedAt = new DateTime(2025, 12, 1, 0, 0, 0, DateTimeKind.Utc),
            addedNeeds = ""
        },

        new Reservation
        {
            Id = 2002,
            UserId = 7,
            OfferId = 3,
            HotelId = 120,
            RoomId = 1,
            IsActive = true,
            IncludeInsurance = true,
            isFirstRatePaid = true,
            isFullPaid = false,
            isDiscountUsed = false,
            DiscountValue = 0,
            TotalPrice = 350,
            PriceLeftToPay = 50,
            CreatedAt = new DateTime(2025, 12, 1, 0, 0, 0, DateTimeKind.Utc),
            addedNeeds = ""
        },

        new Reservation
        {
            Id = 2003,
            UserId = 8,
            OfferId = 2,
            HotelId = 111,
            RoomId = 3,
            IsActive = true,
            IncludeInsurance = false,
            isFirstRatePaid = false,
            isFullPaid = true,
            isDiscountUsed = false,
            DiscountValue = 0,
            TotalPrice = 900,
            PriceLeftToPay = 0,
            CreatedAt = new DateTime(2025, 12, 1, 0, 0, 0, DateTimeKind.Utc),
            addedNeeds = ""
        },

        new Reservation
        {
            Id = 2004,
            UserId = 9,
            OfferId = 1,
            HotelId = 101,
            RoomId = 2,
            IsActive = true,
            IncludeInsurance = true,
            isFirstRatePaid = true,
            isFullPaid = false,
            isDiscountUsed = false,
            DiscountValue = 0,
            TotalPrice = 450,
            PriceLeftToPay = 150,
            CreatedAt = new DateTime(2025, 12, 1, 0, 0, 0, DateTimeKind.Utc),
            addedNeeds = ""
        },

        new Reservation
        {
            Id = 2005,
            UserId = 10,
            OfferId = 3,
            HotelId = 122,
            RoomId = 1,
            IsActive = true,
            IncludeInsurance = false,
            isFirstRatePaid = true,
            isFullPaid = false,
            isDiscountUsed = false,
            DiscountValue = 0,
            TotalPrice = 350,
            PriceLeftToPay = 50,
            CreatedAt = new DateTime(2025, 12, 1, 0, 0, 0, DateTimeKind.Utc),
            addedNeeds = ""
        },

        new Reservation
        {
            Id = 2006,
            UserId = 11,
            OfferId = 1,
            HotelId = 102,
            RoomId = 4,
            IsActive = true,
            IncludeInsurance = true,
            isFirstRatePaid = false,
            isFullPaid = true,
            isDiscountUsed = false,
            DiscountValue = 0,
            TotalPrice = 450,
            PriceLeftToPay = 0,
            CreatedAt = new DateTime(2025, 12, 1, 0, 0, 0, DateTimeKind.Utc),
            addedNeeds = ""
        },

        new Reservation
        {
            Id = 2007,
            UserId = 12,
            OfferId = 2,
            HotelId = 112,
            RoomId = 1,
            IsActive = true,
            IncludeInsurance = true,
            isFirstRatePaid = true,
            isFullPaid = false,
            isDiscountUsed = false,
            DiscountValue = 0,
            TotalPrice = 900,
            PriceLeftToPay = 600,
            CreatedAt = new DateTime(2025, 12, 1, 0, 0, 0, DateTimeKind.Utc),
            addedNeeds = "Pristup teretani"
        },

        new Reservation
        {
            Id = 2008,
            UserId = 13,
            OfferId = 3,
            HotelId = 120,
            RoomId = 2,
            IsActive = true,
            IncludeInsurance = false,
            isFirstRatePaid = false,
            isFullPaid = true,
            isDiscountUsed = false,
            DiscountValue = 0,
            TotalPrice = 350,
            PriceLeftToPay = 0,
            CreatedAt = new DateTime(2025, 12, 1, 0, 0, 0, DateTimeKind.Utc),
            addedNeeds = ""
        },

        new Reservation
        {
            Id = 2009,
            UserId = 14,
            OfferId = 1,
            HotelId = 101,
            RoomId = 3,
            IsActive = true,
            IncludeInsurance = true,
            isFirstRatePaid = true,
            isFullPaid = false,
            isDiscountUsed = false,
            DiscountValue = 0,
            TotalPrice = 450,
            PriceLeftToPay = 150,
            CreatedAt = new DateTime(2025, 12, 1, 0, 0, 0, DateTimeKind.Utc),
            addedNeeds = ""
        },

        new Reservation
        {
            Id = 2010,
            UserId = 15,
            OfferId = 2,
            HotelId = 110,
            RoomId = 4,
            IsActive = true,
            IncludeInsurance = false,
            isFirstRatePaid = true,
            isFullPaid = false,
            isDiscountUsed = false,
            DiscountValue = 0,
            TotalPrice = 900,
            PriceLeftToPay = 600,
            CreatedAt = new DateTime(2025, 12, 1, 0, 0, 0, DateTimeKind.Utc),
            addedNeeds = ""
        },

new Reservation
{
    Id = 2011,
    UserId = 16,
    OfferId = 3,
    HotelId = 121,
    RoomId = 2,
    IsActive = true,
    IncludeInsurance = true,
    isFirstRatePaid = true,
    isFullPaid = false,
    isDiscountUsed = false,
    DiscountValue = 0,
    TotalPrice = 350,
    PriceLeftToPay = 50,
    CreatedAt = new DateTime(2025, 12, 1, 0, 0, 0, DateTimeKind.Utc),
    addedNeeds = ""
},

new Reservation
{
    Id = 2012,
    UserId = 17,
    OfferId = 1,
    HotelId = 102,
    RoomId = 2,
    IsActive = true,
    IncludeInsurance = false,
    isFirstRatePaid = true,
    isFullPaid = false,
    isDiscountUsed = false,
    DiscountValue = 0,
    TotalPrice = 450,
    PriceLeftToPay = 150,
    CreatedAt = new DateTime(2025, 12, 1, 0, 0, 0, DateTimeKind.Utc),
    addedNeeds = ""
},

new Reservation
{
    Id = 2013,
    UserId = 18,
    OfferId = 2,
    HotelId = 111,
    RoomId = 2,
    IsActive = true,
    IncludeInsurance = true,
    isFirstRatePaid = false,
    isFullPaid = true,
    isDiscountUsed = false,
    DiscountValue = 0,
    TotalPrice = 900,
    PriceLeftToPay = 0,
    CreatedAt = new DateTime(2025, 12, 1, 0, 0, 0, DateTimeKind.Utc),
    addedNeeds = "Bez glutena"
},

new Reservation
{
    Id = 2014,
    UserId = 19,
    OfferId = 3,
    HotelId = 120,
    RoomId = 3,
    IsActive = true,
    IncludeInsurance = true,
    isFirstRatePaid = true,
    isFullPaid = false,
    isDiscountUsed = false,
    DiscountValue = 0,
    TotalPrice = 350,
    PriceLeftToPay = 50,
    CreatedAt = new DateTime(2025, 12, 1, 0, 0, 0, DateTimeKind.Utc),
    addedNeeds = ""
},

new Reservation
{
    Id = 2015,
    UserId = 20,
    OfferId = 1,
    HotelId = 101,
    RoomId = 1,
    IsActive = true,
    IncludeInsurance = true,
    isFirstRatePaid = false,
    isFullPaid = true,
    isDiscountUsed = false,
    DiscountValue = 0,
    TotalPrice = 450,
    PriceLeftToPay = 0,
    CreatedAt = new DateTime(2025, 12, 1, 0, 0, 0, DateTimeKind.Utc),
    addedNeeds = ""
},

new Reservation
{
    Id = 2016,
    UserId = 21,
    OfferId = 2,
    HotelId = 112,
    RoomId = 1,
    IsActive = true,
    IncludeInsurance = true,
    isFirstRatePaid = true,
    isFullPaid = false,
    isDiscountUsed = false,
    DiscountValue = 0,
    TotalPrice = 900,
    PriceLeftToPay = 600,
    CreatedAt = new DateTime(2025, 12, 1, 0, 0, 0, DateTimeKind.Utc),
    addedNeeds = ""
},

new Reservation
{
    Id = 2017,
    UserId = 22,
    OfferId = 3,
    HotelId = 122,
    RoomId = 1,
    IsActive = true,
    IncludeInsurance = true,
    isFirstRatePaid = true,
    isFullPaid = false,
    isDiscountUsed = false,
    DiscountValue = 0,
    TotalPrice = 350,
    PriceLeftToPay = 50,
    CreatedAt = new DateTime(2025, 12, 1, 0, 0, 0, DateTimeKind.Utc),
    addedNeeds = ""
},

new Reservation
{
    Id = 2018,
    UserId = 23,
    OfferId = 1,
    HotelId = 100,
    RoomId = 1,
    IsActive = true,
    IncludeInsurance = false,
    isFirstRatePaid = true,
    isFullPaid = false,
    isDiscountUsed = false,
    DiscountValue = 0,
    TotalPrice = 450,
    PriceLeftToPay = 150,
    CreatedAt = new DateTime(2025, 12, 1, 0, 0, 0, DateTimeKind.Utc),
    addedNeeds = ""
},

new Reservation
{
    Id = 2019,
    UserId = 24,
    OfferId = 2,
    HotelId = 111,
    RoomId = 3,
    IsActive = true,
    IncludeInsurance = true,
    isFirstRatePaid = false,
    isFullPaid = true,
    isDiscountUsed = false,
    DiscountValue = 0,
    TotalPrice = 900,
    PriceLeftToPay = 0,
    CreatedAt = new DateTime(2025, 12, 1, 0, 0, 0, DateTimeKind.Utc),
    addedNeeds = ""
},


        new Reservation
        {
            Id = 3000,
            UserId = 6,
            OfferId = 1,
            HotelId = 100,
            RoomId = 1,
            IsActive = true,
            IncludeInsurance = false,
            isFirstRatePaid = true,
            isFullPaid = true,
            isDiscountUsed = false,
            DiscountValue = 0,
            TotalPrice = 0,
            PriceLeftToPay = 0,
            CreatedAt = new DateTime(2026, 3, 2, 0, 0, 0, DateTimeKind.Utc),
            addedNeeds = ""
        },

        new Reservation
        {
            Id = 3001,
            UserId = 6,
            OfferId = 2,
            HotelId = 110,
            RoomId = 2,
            IsActive = true,
            IncludeInsurance = false,
            isFirstRatePaid = true,
            isFullPaid = true,
            isDiscountUsed = false,
            DiscountValue = 0,
            TotalPrice = 0,
            PriceLeftToPay = 0,
            CreatedAt = new DateTime(2026, 3, 2, 0, 0, 0, DateTimeKind.Utc),
            addedNeeds = ""
        },

        new Reservation
        {
            Id = 3002,
            UserId = 6,
            OfferId = 3,
            HotelId = 120,
            RoomId = 1,
            IsActive = true,
            IncludeInsurance = false,
            isFirstRatePaid = true,
            isFullPaid = true,
            isDiscountUsed = false,
            DiscountValue = 0,
            TotalPrice = 0,
            PriceLeftToPay = 0,
            CreatedAt = new DateTime(2026, 3, 2, 0, 0, 0, DateTimeKind.Utc),
            addedNeeds = ""
        }
);


            builder.Entity<Payment>().HasData(

// =====================================================
// RES 2000 – 1 + 2 + 5 (FULL PAID)
// =====================================================
new Payment { ReservationId = 2000, RateId = 1, Amount = 100, PaymentMethod = "kartica", IsConfirmed = true },
new Payment { ReservationId = 2000, RateId = 2, Amount = 200, PaymentMethod = "kartica", IsConfirmed = true },
new Payment { ReservationId = 2000, RateId = 5, Amount = 150, PaymentMethod = "kartica", IsConfirmed = true },

// =====================================================
// RES 2001 – 1 + 2 + 5 (FULL PAID)
// =====================================================
new Payment { ReservationId = 2001, RateId = 1, Amount = 100, PaymentMethod = "kartica", IsConfirmed = true },
new Payment { ReservationId = 2001, RateId = 2, Amount = 200, PaymentMethod = "kartica", IsConfirmed = true },
new Payment { ReservationId = 2001, RateId = 5, Amount = 600, PaymentMethod = "kartica", IsConfirmed = true },

// =====================================================
// RES 2002 – 1 CONFIRMED, 2 PENDING
// =====================================================
new Payment { ReservationId = 2002, RateId = 1, Amount = 100, PaymentMethod = "kartica", IsConfirmed = true },
new Payment { ReservationId = 2002, RateId = 2, Amount = 200, PaymentMethod = "uplatnica", IsConfirmed = false },

// =====================================================
// RES 2003 – 1 + 2 CONFIRMED, 5 PENDING
// =====================================================
new Payment { ReservationId = 2003, RateId = 1, Amount = 100, PaymentMethod = "kartica", IsConfirmed = true },
new Payment { ReservationId = 2003, RateId = 2, Amount = 200, PaymentMethod = "kartica", IsConfirmed = true },
new Payment { ReservationId = 2003, RateId = 5, Amount = 600, PaymentMethod = "uplatnica", IsConfirmed = false },

// =====================================================
// RES 2004 – FULL AMOUNT (PENDING)
// =====================================================
new Payment
{
    ReservationId = 2004,
    RateId = 4,
    Amount = 450,
    PaymentMethod = "uplatnica",
    IsConfirmed = false
},

// =====================================================
// RES 2005 – 1 CONFIRMED + 5 PENDING
// =====================================================
new Payment { ReservationId = 2005, RateId = 1, Amount = 100, PaymentMethod = "kartica", IsConfirmed = true },
new Payment { ReservationId = 2005, RateId = 5, Amount = 400, PaymentMethod = "uplatnica", IsConfirmed = false },

// =====================================================
// RES 2006 – 1 + 2 + 5 (FULL PAID)
// =====================================================
new Payment { ReservationId = 2006, RateId = 1, Amount = 100, PaymentMethod = "kartica", IsConfirmed = true },
new Payment { ReservationId = 2006, RateId = 2, Amount = 200, PaymentMethod = "kartica", IsConfirmed = true },
new Payment { ReservationId = 2006, RateId = 5, Amount = 150, PaymentMethod = "kartica", IsConfirmed = true },

// =====================================================
// RES 2007 – 1 + 2 (čekanje 5)
// =====================================================
new Payment { ReservationId = 2007, RateId = 1, Amount = 100, PaymentMethod = "kartica", IsConfirmed = true },
new Payment { ReservationId = 2007, RateId = 2, Amount = 200, PaymentMethod = "kartica", IsConfirmed = true },

// =====================================================
// RES 2008 – 1 + 2 + 5 (FULL PAID)
// =====================================================
new Payment { ReservationId = 2008, RateId = 1, Amount = 100, PaymentMethod = "kartica", IsConfirmed = true },
new Payment { ReservationId = 2008, RateId = 2, Amount = 200, PaymentMethod = "kartica", IsConfirmed = true },
new Payment { ReservationId = 2008, RateId = 5, Amount = 50, PaymentMethod = "kartica", IsConfirmed = true },

// =====================================================
// RES 2009 – 1 + 2 + 5 (FULL PAID)
// =====================================================
new Payment { ReservationId = 2009, RateId = 1, Amount = 100, PaymentMethod = "kartica", IsConfirmed = true },
new Payment { ReservationId = 2009, RateId = 2, Amount = 200, PaymentMethod = "kartica", IsConfirmed = true },
new Payment { ReservationId = 2009, RateId = 5, Amount = 150, PaymentMethod = "kartica", IsConfirmed = true },

// =====================================================
// RES 2010 – FULL AMOUNT CONFIRMED
// =====================================================
new Payment
{
    ReservationId = 2010,
    RateId = 4,
    Amount = 900,
    PaymentMethod = "kartica",
    IsConfirmed = true
}
);


builder.Entity<Comment>().HasData(

    // =====================================================
    // OFFER 1 – FIRENCA
    // =====================================================
    new Comment { Id = 1, userId = 4, offerId = 1, comment = "Predivno putovanje! Organizacija odlična, vodič fenomenalan.", starRate = 5 },
    new Comment { Id = 2, userId = 5, offerId = 1, comment = "Sve super osim hotela koji je mogao biti bolji.", starRate = 4 },
    new Comment { Id = 3, userId = 6, offerId = 1, comment = "Firenca je čarobna! Hrana, arhitektura i atmosfera su nevjerovatni.", starRate = 5 },

    // =====================================================
    // OFFER 2 – SANTORINI
    // =====================================================
    new Comment { Id = 4, userId = 7, offerId = 2, comment = "Santorini je san! Zalazak sunca u Oiji je nešto posebno.", starRate = 5 },
    new Comment { Id = 5, userId = 8, offerId = 2, comment = "Prelijepo ostrvo, ali dosta turista. Organizacija korektna.", starRate = 4 },

    // =====================================================
    // OFFER 3 – ISTANBUL
    // =====================================================
    new Comment { Id = 6, userId = 9, offerId = 3, comment = "Istanbul nudi spoj istoka i zapada. Tura je bila vrlo zanimljiva.", starRate = 5 },
    new Comment { Id = 7, userId = 10, offerId = 3, comment = "Dobra cijena za ono što se dobije. Posebno mi se dopao Bosfor.", starRate = 4 },

    // =====================================================
    // OFFER 4 – BARCELONA
    // =====================================================
    new Comment { Id = 8, userId = 11, offerId = 4, comment = "Barcelona je bila fantastična! Hotel blizu centra.", starRate = 5 },
    new Comment { Id = 9, userId = 12, offerId = 4, comment = "Grad pun energije. Raspored obilazaka dobro isplaniran.", starRate = 4 },

    // =====================================================
    // OFFER 5 – PARIS
    // =====================================================
    new Comment { Id = 10, userId = 13, offerId = 5, comment = "Grad svjetlosti je ispunio sva očekivanja!", starRate = 5 },
    new Comment { Id = 11, userId = 14, offerId = 5, comment = "Skupo, ali vrijedilo je svakog dinara.", starRate = 5 },

    // =====================================================
    // OFFER 6 – PRAGUE
    // =====================================================
    new Comment { Id = 12, userId = 15, offerId = 6, comment = "Prelijep stari grad i romantična atmosfera.", starRate = 5 },
    new Comment { Id = 13, userId = 16, offerId = 6, comment = "Odličan omjer cijene i kvaliteta.", starRate = 4 },

    // =====================================================
    // OFFER 7 – VIENNA
    // =====================================================
    new Comment { Id = 14, userId = 17, offerId = 7, comment = "Mirno i ugodno putovanje, savršeno za vikend bijeg.", starRate = 4 },
    new Comment { Id = 15, userId = 18, offerId = 7, comment = "Beč je elegantan i kulturno bogat grad.", starRate = 5 },

    // =====================================================
    // OFFER 8 – AMSTERDAM
    // =====================================================
    new Comment { Id = 16, userId = 19, offerId = 8, comment = "Kanali i arhitektura su predivni. Preporučujem!", starRate = 5 },
    new Comment { Id = 17, userId = 20, offerId = 8, comment = "Grad je zanimljiv, ali dosta gužve.", starRate = 4 },

    // =====================================================
    // OFFER 9 – LONDON
    // =====================================================
    new Comment { Id = 18, userId = 21, offerId = 9, comment = "London je impresivan, puno znamenitosti.", starRate = 5 },
    new Comment { Id = 19, userId = 22, offerId = 9, comment = "Kratko putovanje, ali vrlo sadržajno.", starRate = 4 },

    // =====================================================
    // OFFER 10 – DUBAI
    // =====================================================
    new Comment { Id = 20, userId = 23, offerId = 10, comment = "Dubai je nevjerovatno iskustvo! Top organizacija.", starRate = 5 },
    new Comment { Id = 21, userId = 24, offerId = 10, comment = "Luksuz na svakom koraku. Hotel vrhunski.", starRate = 5 },

    // =====================================================
    // OFFER 11 – CAIRO
    // =====================================================
    new Comment { Id = 22, userId = 5, offerId = 11, comment = "Piramide su fascinantne. Putovanje vrijedno iskustva.", starRate = 5 },
    new Comment { Id = 23, userId = 6, offerId = 11, comment = "Zanimljivo, ali dosta naporno zbog vrućine.", starRate = 4 },

    // =====================================================
    // OFFER 12 – BUDAPEST
    // =====================================================
    new Comment { Id = 24, userId = 7, offerId = 12, comment = "Lijep grad i odlična atmosfera.", starRate = 4 },
    new Comment { Id = 25, userId = 8, offerId = 12, comment = "Termalne banje su pun pogodak.", starRate = 5 },

    // =====================================================
    // OFFER 13 – KRAKOW
    // =====================================================
    new Comment { Id = 26, userId = 9, offerId = 13, comment = "Historijski vrlo zanimljiv grad.", starRate = 5 },
    new Comment { Id = 27, userId = 10, offerId = 13, comment = "Dobra organizacija i povoljne cijene.", starRate = 4 },

    // =====================================================
    // OFFER 14 – ZANZIBAR
    // =====================================================
    new Comment { Id = 28, userId = 11, offerId = 14, comment = "Rajska destinacija! Plaže su nestvarne.", starRate = 5 },
    new Comment { Id = 29, userId = 12, offerId = 14, comment = "Savršeno za odmor i opuštanje.", starRate = 5 },

    // =====================================================
    // OFFER 15 – HURGHADA
    // =====================================================
    new Comment { Id = 30, userId = 13, offerId = 15, comment = "More i resort su bili odlični.", starRate = 4 },
    new Comment { Id = 31, userId = 14, offerId = 15, comment = "Idealan izbor za porodični odmor.", starRate = 5 },

    // =====================================================
    // OFFER 16 – LISBON
    // =====================================================
    new Comment { Id = 32, userId = 15, offerId = 16, comment = "Lisabon je šarmantan i topao grad.", starRate = 5 },
    new Comment { Id = 33, userId = 16, offerId = 16, comment = "Preporučujem svima koji vole opuštena putovanja.", starRate = 4 },

    // =====================================================
    // OFFER 17 – ATHENS
    // =====================================================
    new Comment { Id = 34, userId = 17, offerId = 17, comment = "Akropolj je nešto što se mora vidjeti.", starRate = 5 },
    new Comment { Id = 35, userId = 18, offerId = 17, comment = "Dobar balans između obilazaka i slobodnog vremena.", starRate = 4 },

    // =====================================================
    // OFFER 18 – SPLIT
    // =====================================================
    new Comment { Id = 36, userId = 19, offerId = 18, comment = "Predivan grad i odlična atmosfera.", starRate = 5 },
    new Comment { Id = 37, userId = 20, offerId = 18, comment = "Idealno ljetno putovanje.", starRate = 5 }
);

            builder.Entity<Voucher>().HasData(
                new Voucher
                {
                    Id = 1,
                    VoucherCode = "ETRAVEL20",
                    Discount = 0.20m,          // 20%
                    priceInTokens = 40
                },
                new Voucher
                {
                    Id = 2,
                    VoucherCode = "ETRAVEL50",
                    Discount = 0.50m,          // 50%
                    priceInTokens = 80
                },
                new Voucher
                {
                    Id = 3,
                    VoucherCode = "ETRAVEL70",
                    Discount = 0.70m,          // 70%
                    priceInTokens = 120
                }
            );

            builder.Entity<UserVoucher>().HasData(
                // Maja (5)
                new UserVoucher { UserId = 5, VoucherId = 1, isUsed = false },
                new UserVoucher { UserId = 5, VoucherId = 2, isUsed = true },

                // Edin (6)
                new UserVoucher { UserId = 6, VoucherId = 1, isUsed = false },

                // Lana (7)
                new UserVoucher { UserId = 7, VoucherId = 3, isUsed = false },

                // Haris (8)
                new UserVoucher { UserId = 8, VoucherId = 2, isUsed = true },

                // Amira (9)
                new UserVoucher { UserId = 9, VoucherId = 1, isUsed = false },
                new UserVoucher { UserId = 9, VoucherId = 3, isUsed = true },

                // Tarik (10)
                new UserVoucher { UserId = 10, VoucherId = 2, isUsed = false },

                // Selma (11)
                new UserVoucher { UserId = 11, VoucherId = 1, isUsed = false },

                // Nedim (12)
                new UserVoucher { UserId = 12, VoucherId = 3, isUsed = false }
            );




            builder.Entity<UserToken>().HasData(
                new UserToken { UserId = 4, Equity = 120 },
                new UserToken { UserId = 5, Equity = 340 },
                new UserToken { UserId = 6, Equity = 75 },
                new UserToken { UserId = 7, Equity = 210 },
                new UserToken { UserId = 8, Equity = 500 },
                new UserToken { UserId = 9, Equity = 160 },
                new UserToken { UserId = 10, Equity = 90 },
                new UserToken { UserId = 11, Equity = 430 },
                new UserToken { UserId = 12, Equity = 60 },
                new UserToken { UserId = 13, Equity = 280 },
                new UserToken { UserId = 14, Equity = 150 },
                new UserToken { UserId = 15, Equity = 390 },
                new UserToken { UserId = 16, Equity = 45 },
                new UserToken { UserId = 17, Equity = 310 },
                new UserToken { UserId = 18, Equity = 200 },
                new UserToken { UserId = 19, Equity = 470 },
                new UserToken { UserId = 20, Equity = 130 },
                new UserToken { UserId = 21, Equity = 260 },
                new UserToken { UserId = 22, Equity = 80 },
                new UserToken { UserId = 23, Equity = 350 },
                new UserToken { UserId = 24, Equity = 170 }
            );


            builder.Entity<WorkApplication>().HasData(
    new WorkApplication
    {
        Id = 1,
        UserId = 4,
        CvFileName = "test.pdf",
        AppliedAt = DateTime.UtcNow.AddDays(-2),
        letter = "Smisleno me motiviše vaša moderna organizacija i želja da radim na projektima koji imaju stvarni utjecaj. Vjerujem da mogu doprinijeti svojim radnim navikama i voljom za učenjem."
    },
    new WorkApplication
    {
        Id = 2,
        UserId = 5,
        CvFileName = "test.pdf",
        AppliedAt = DateTime.UtcNow.AddDays(-4),
        letter = "Privukla me prilika da radim u dinamičnom okruženju gdje se cijeni timski rad i napredak. Želim biti dio profesionalne i pozitivne radne zajednice."
    },
    new WorkApplication
    {
        Id = 3,
        UserId = 6,
        CvFileName = "test.pdf",
        AppliedAt = DateTime.UtcNow.AddDays(-7),
        letter = "Vaša kompanija je prepoznata po kvalitetnom radu i inovacijama. Smatram da mogu mnogo naučiti i istovremeno doprinijeti svojim iskustvom i posvećenošću."
    },
    new WorkApplication
    {
        Id = 4,
        UserId = 7,
        CvFileName = "test.pdf",
        AppliedAt = DateTime.UtcNow.AddDays(-10),
        letter = "Želim raditi u sredini koja podstiče razvoj i podržava kreativnost. Vaša firma upravo to nudi, i zato bih voljela biti dio vašeg tima."
    },
    new WorkApplication
    {
        Id = 5,
        UserId = 8,
        CvFileName = "test.pdf",
        AppliedAt = DateTime.UtcNow.AddDays(-12),
        letter = "Vašu kompaniju vidim kao mjesto gdje se talenat i rad cijene. Motivisan sam da se usavršavam i doprinosim vašem rastu."
    },
    new WorkApplication
    {
        Id = 6,
        UserId = 9,
        CvFileName = "test.pdf",
        AppliedAt = DateTime.UtcNow.AddDays(-14),
        letter = "Tražim priliku da radim u profesionalnoj sredini gdje mogu napredovati. Posebno me privlači vaša organizacijska kultura i pristup radu."
    },
    new WorkApplication
    {
        Id = 7,
        UserId = 10,
        CvFileName = "test.pdf",
        AppliedAt = DateTime.UtcNow.AddDays(-17),
        letter = "Motiviše me želja da učim nove tehnologije i doprinesem timskim rezultatima. Vjerujem da bih se dobro uklopio u vaše okruženje."
    },
    new WorkApplication
    {
        Id = 8,
        UserId = 11,
        CvFileName = "test.pdf",
        AppliedAt = DateTime.UtcNow.AddDays(-18),
        letter = "Smatram da je vaša kompanija idealno mjesto za profesionalni rast. Cijenim vaš pristup organizaciji i inovativnosti."
    },
    new WorkApplication
    {
        Id = 9,
        UserId = 12,
        CvFileName = "test.pdf",
        AppliedAt = DateTime.UtcNow.AddDays(-20),
        letter = "Zainteresovana sam za rad kod vas jer nudite stabilno i ugodno radno okruženje u kojem se prepoznaje trud i zalaganje."
    },
    new WorkApplication
    {
        Id = 10,
        UserId = 13,
        CvFileName = "test.pdf",
        AppliedAt = DateTime.UtcNow.AddDays(-22),
        letter = "Želim biti dio tima koji teži kvaliteti i rastu. Vaša firma mi djeluje kao pravo mjesto za to."
    },
    new WorkApplication
    {
        Id = 11,
        UserId = 14,
        CvFileName = "test.pdf",
        AppliedAt = DateTime.UtcNow.AddDays(-23),
        letter = "Motivisan sam mogućnošću da radim u kompaniji koja cijeni profesionalizam i timski rad. Spreman sam da dam svoj maksimum."
    },
    new WorkApplication
    {
        Id = 12,
        UserId = 15,
        CvFileName = "test.pdf",
        AppliedAt = DateTime.UtcNow.AddDays(-24),
        letter = "Privlači me prilika da radim sa stručnim i kreativnim timom. Vaš način rada me posebno inspiriše."
    },
    new WorkApplication
    {
        Id = 13,
        UserId = 16,
        CvFileName = "test.pdf",
        AppliedAt = DateTime.UtcNow.AddDays(-25),
        letter = "Vaša kompanija nudi odlične mogućnosti za rast i razvoj, što je glavni razlog moje prijave."
    },
    new WorkApplication
    {
        Id = 14,
        UserId = 17,
        CvFileName = "test.pdf",
        AppliedAt = DateTime.UtcNow.AddDays(-26),
        letter = "Motivisan sam da učim, radim i napredujem. Vjerujem da bih bio odličan dodatak vašem timu."
    },
    new WorkApplication
    {
        Id = 15,
        UserId = 18,
        CvFileName = "test.pdf",
        AppliedAt = DateTime.UtcNow.AddDays(-27),
        letter = "Želim raditi u okruženju gdje se cijeni inicijativa, kreativnost i kvalitetan rad. Vaša firma ispunjava sve te kriterije."
    },
    new WorkApplication
    {
        Id = 16,
        UserId = 19,
        CvFileName = "test.pdf",
        AppliedAt = DateTime.UtcNow.AddDays(-28),
        letter = "Vidim veliku vrijednost u vašim projektima i načinu rada. Želim biti dio tima koji radi sa strašću."
    },
    new WorkApplication
    {
        Id = 17,
        UserId = 20,
        CvFileName = "test.pdf",
        AppliedAt = DateTime.UtcNow.AddDays(-29),
        letter = "Prijavljujem se jer vjerujem da bih u vašoj kompaniji mogao postići veliki profesionalni iskorak."
    },
    new WorkApplication
    {
        Id = 18,
        UserId = 21,
        CvFileName = "test.pdf",
        AppliedAt = DateTime.UtcNow.AddDays(-30),
        letter = "Motiviše me želja da radim u stabilnoj i ozbiljnoj organizaciji koja nudi perspektivu i razvoj."
    },
    new WorkApplication
    {
        Id = 19,
        UserId = 22,
        CvFileName = "test.pdf",
        AppliedAt = DateTime.UtcNow.AddDays(-31),
        letter = "Vaša kompanija mi djeluje kao pravo mjesto da pokažem svoje vještine i dodatno ih unaprijedim."
    },
    new WorkApplication
    {
        Id = 20,
        UserId = 23,
        CvFileName = "test.pdf",
        AppliedAt = DateTime.UtcNow.AddDays(-32),
        letter = "Privlači me vaš profesionalan pristup, moderna organizacija i atmosfera koja podstiče učenje."
    },
    new WorkApplication
    {
        Id = 21,
        UserId = 24,
        CvFileName = "test.pdf",
        AppliedAt = DateTime.UtcNow.AddDays(-33),
        letter = "Motivisana sam da radim u vašoj firmi jer cijenim vaše vrijednosti i način poslovanja. Vjerujem da bih se idealno uklopila."
    }
);

        }
    }
}

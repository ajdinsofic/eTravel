using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace eTravelAgencija.Services.Database.Seed
{
    public static class SeedData
    {
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
                new UserRole { UserId = 2, RoleId = 3 }, // direktor → Direktor
                                                         // KORISNICI (Svi ostali)
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

            // =========================================================================
            // 9. OFFER IMAGES (1 glavna + 3 dodatne)
            // =========================================================================
            builder.Entity<OfferImage>().HasData(
                new OfferImage { Id = 1, OfferId = 1, ImageUrl = "firenca_main.jpg", isMain = true },
                new OfferImage { Id = 2, OfferId = 1, ImageUrl = "firenca_1.jpg", isMain = false },
                new OfferImage { Id = 3, OfferId = 1, ImageUrl = "firenca_2.jpg", isMain = false },
                new OfferImage { Id = 4, OfferId = 1, ImageUrl = "firenca_3.jpg", isMain = false },

                new OfferImage { Id = 5, OfferId = 2, ImageUrl = "santorini_main.jpg", isMain = true },
                new OfferImage { Id = 6, OfferId = 2, ImageUrl = "santorini_1.jpg", isMain = false },
                new OfferImage { Id = 7, OfferId = 2, ImageUrl = "santorini_2.jpg", isMain = false },
                new OfferImage { Id = 8, OfferId = 2, ImageUrl = "santorini_3.jpg", isMain = false },

                new OfferImage { Id = 9, OfferId = 3, ImageUrl = "istanbul_main.jpg", isMain = true },
                new OfferImage { Id = 10, OfferId = 3, ImageUrl = "istanbul_1.jpg", isMain = false },
                new OfferImage { Id = 11, OfferId = 3, ImageUrl = "istanbul_2.jpg", isMain = false },
                new OfferImage { Id = 12, OfferId = 3, ImageUrl = "istanbul_3.jpg", isMain = false },

                new OfferImage { Id = 1000, OfferId = 4, ImageUrl = "primjer.jpg", isMain = true },
                new OfferImage { Id = 1001, OfferId = 5, ImageUrl = "primjer.jpg", isMain = true },
                new OfferImage { Id = 1002, OfferId = 6, ImageUrl = "primjer.jpg", isMain = true },
                new OfferImage { Id = 1003, OfferId = 7, ImageUrl = "primjer.jpg", isMain = true },
                new OfferImage { Id = 1004, OfferId = 8, ImageUrl = "primjer.jpg", isMain = true },
                new OfferImage { Id = 1005, OfferId = 9, ImageUrl = "primjer.jpg", isMain = true },
                new OfferImage { Id = 1006, OfferId = 10, ImageUrl = "primjer.jpg", isMain = true },
                new OfferImage { Id = 1007, OfferId = 11, ImageUrl = "primjer.jpg", isMain = true },
                new OfferImage { Id = 1008, OfferId = 12, ImageUrl = "primjer.jpg", isMain = true },
                new OfferImage { Id = 1009, OfferId = 13, ImageUrl = "primjer.jpg", isMain = true },
                new OfferImage { Id = 1010, OfferId = 14, ImageUrl = "primjer.jpg", isMain = true },
                new OfferImage { Id = 1011, OfferId = 15, ImageUrl = "primjer.jpg", isMain = true },
                new OfferImage { Id = 1012, OfferId = 16, ImageUrl = "primjer.jpg", isMain = true },
                new OfferImage { Id = 1013, OfferId = 17, ImageUrl = "primjer.jpg", isMain = true },
                new OfferImage { Id = 1014, OfferId = 18, ImageUrl = "primjer.jpg", isMain = true }

            );

            // =========================================================================
            // 9.1. OFFER PLAN DAYS 
            // =========================================================================

            // Firenca
            builder.Entity<OfferPlanDay>().HasData(
                new OfferPlanDay
                {
                    OfferDetailsId = 1,
                    DayNumber = 1,
                    DayTitle = "Polazak i dolazak u Firencu",
                    DayDescription = "Polazak u ranim jutarnjim satima. Pauze tokom puta. Dolazak u Firencu u poslijepodnevnim satima. Smještaj u hotel i slobodno vrijeme za odmor ili šetnju centrom grada."
                },
                new OfferPlanDay
                {
                    OfferDetailsId = 1,
                    DayNumber = 2,
                    DayTitle = "Upoznavanje sa starim gradom",
                    DayDescription = "Doručak. Organizovano razgledanje: Katedrala Santa Maria del Fiore, Piazza della Signoria, Palazzo Vecchio i Ponte Vecchio. Popodne slobodno vrijeme za individualno istraživanje i kupovinu suvenira."
                },
                new OfferPlanDay
                {
                    OfferDetailsId = 1,
                    DayNumber = 3,
                    DayTitle = "Galerija Uffizi i slobodno popodne",
                    DayDescription = "Posjeta čuvenoj galeriji Uffizi – remek-djela renesansnih umjetnika: Botticelli, Michelangelo, Da Vinci. Nakon obilaska, slobodno vrijeme za ručak u lokalnim restoranima i uživanje u talijanskoj kuhinji."
                },
                new OfferPlanDay
                {
                    OfferDetailsId = 1,
                    DayNumber = 4,
                    DayTitle = "Izlet u Pisu ili slobodan dan",
                    DayDescription = "Mogućnost fakultativnog izleta u Pisu i posjete Krivom tornju. Alternativno, slobodan dan u Firenci za šoping, obilazak muzeja, degustaciju vina ili šetnju slikovitim ulicama."
                },
                new OfferPlanDay
                {
                    OfferDetailsId = 1,
                    DayNumber = 5,
                    DayTitle = "Povratak kući",
                    DayDescription = "Check-out iz hotela i polazak prema kući. Pauze tokom puta. Dolazak u kasnim večernjim satima."
                }
            );

            // Santorini
            builder.Entity<OfferPlanDay>().HasData(
                new OfferPlanDay
                {
                    OfferDetailsId = 2,
                    DayNumber = 1,
                    DayTitle = "Dolazak na Santorini",
                    DayDescription = "Let ili transfer do Santorinija. Smještaj u hotel. Slobodno vrijeme za odmor, kupanje ili večernju šetnju rivom u Firi."
                },
                new OfferPlanDay
                {
                    OfferDetailsId = 2,
                    DayNumber = 2,
                    DayTitle = "Fira – glavni grad ostrva",
                    DayDescription = "Nakon doručka slijedi obilazak Fire: uske bijele ulice, crkve sa plavim kupolama i prekrasni vidikovci. Popodne slobodno vrijeme za kupovinu ili posjetu lokalnim tavernama."
                },
                new OfferPlanDay
                {
                    OfferDetailsId = 2,
                    DayNumber = 3,
                    DayTitle = "Oia – najljepši zalazak sunca",
                    DayDescription = "Prijepodnevno slobodno vrijeme za plažu. U poslijepodnevnim satima odlazak u Oiu – najpoznatije mjesto na Santoriniju. Uživanje u fantastičnom zalasku sunca."
                },
                new OfferPlanDay
                {
                    OfferDetailsId = 2,
                    DayNumber = 4,
                    DayTitle = "Crna i crvena plaža",
                    DayDescription = "Obilazak vulkanskih plaža: Red Beach i Perissa (Black Beach). Slobodno vrijeme za kupanje i sunčanje. Povratak u hotel u večernjim satima."
                },
                new OfferPlanDay
                {
                    OfferDetailsId = 2,
                    DayNumber = 5,
                    DayTitle = "Vulkansko ostrvo i termalni izvori",
                    DayDescription = "Izlet brodom do vulkanskog ostrva Nea Kameni, šetnja kraterom i kupanje u toplim termalnim izvorima. Povratak brodom u luku."
                },
                new OfferPlanDay
                {
                    OfferDetailsId = 2,
                    DayNumber = 6,
                    DayTitle = "Slobodan dan",
                    DayDescription = "Dan predviđen za odmor, kupanje ili fakultativne aktivnosti – iznajmljivanje quada, degustacije vina, panoramska vožnja ostrvom."
                },
                new OfferPlanDay
                {
                    OfferDetailsId = 2,
                    DayNumber = 7,
                    DayTitle = "Povratak kući",
                    DayDescription = "Odjava iz hotela i povratak kući prema planu putovanja."
                }
            );

            //Istambul
            builder.Entity<OfferPlanDay>().HasData(
                new OfferPlanDay
                {
                    OfferDetailsId = 3,
                    DayNumber = 1,
                    DayTitle = "Dolazak u Istanbul",
                    DayDescription = "Dolazak u Istanbul i smještaj u hotel. Slobodno vrijeme za odmor. Uveče mogućnost odlaska na večeru u tradicionalni turski restoran."
                },
                new OfferPlanDay
                {
                    OfferDetailsId = 3,
                    DayNumber = 2,
                    DayTitle = "Stari dio Istanbula – Sultanahmet",
                    DayDescription = "Obilazak najvećih atrakcija: Aja Sofija, Sultanahmet džamija, Hipodrom i Topkapi palata. Popodne slobodno vrijeme ili posjeta Grand Bazaru."
                },
                new OfferPlanDay
                {
                    OfferDetailsId = 3,
                    DayNumber = 3,
                    DayTitle = "Bosfor krstarenje i Taksim",
                    DayDescription = "Jutarnje krstarenje Bosforom – pogled na palače, mostove i obalu. U popodnevnim satima odlazak na Taksim trg i šetnja Istiklal ulicom."
                },
                new OfferPlanDay
                {
                    OfferDetailsId = 3,
                    DayNumber = 4,
                    DayTitle = "Povratak kući",
                    DayDescription = "Slobodno vrijeme do polaska. Odjava iz hotela i povratak kući."
                }
            );


            // =========================================================================
            // 10. HOTELS (3 po ponudi)
            // =========================================================================
            builder.Entity<Hotel>().HasData(
                // Firenca
                new Hotel { Id = 100, Name = "Hotel Medici", Address = "Via Roma 12", Stars = 4 },
                new Hotel { Id = 101, Name = "Hotel Firenze Centro", Address = "Piazza Duomo 2", Stars = 3 },
                new Hotel { Id = 102, Name = "Hotel Ponte Vecchio", Address = "Ponte Vecchio 5", Stars = 5 },

                // Santorini
                new Hotel { Id = 110, Name = "Blue Dome Resort", Address = "Santorini Beach 9", Stars = 5 },
                new Hotel { Id = 111, Name = "Aegean View", Address = "Oia Street 44", Stars = 4 },
                new Hotel { Id = 112, Name = "White Cave Hotel", Address = "Fira 21", Stars = 3 },

                // Istanbul
                new Hotel { Id = 120, Name = "Hotel Sultanahmet", Address = "Sultanahmet 1", Stars = 4 },
                new Hotel { Id = 121, Name = "Galata Inn", Address = "Galata 5", Stars = 3 },
                new Hotel { Id = 122, Name = "Bosfor Palace Hotel", Address = "Bosfor Blvd 7", Stars = 5 },
                new Hotel { Id = 200, Name = "Hotel Condal Barcelona", Address = "La Rambla 12", Stars = 3 },
                new Hotel { Id = 201, Name = "Hotel Louvre Rivoli", Address = "Rue de Rivoli 99", Stars = 4 },
                new Hotel { Id = 202, Name = "Hotel Charles Bridge Inn", Address = "Mostecká 7", Stars = 4 },
                new Hotel { Id = 203, Name = "Hotel Kaiserhof Wien", Address = "Frankenberggasse 10", Stars = 4 },
                new Hotel { Id = 204, Name = "Hotel Amsterdam Canal View", Address = "Keizersgracht 84", Stars = 5 },
                new Hotel { Id = 205, Name = "Hotel Westminster London", Address = "Victoria Street 22", Stars = 4 },
                new Hotel { Id = 206, Name = "Dubai Marina Pearl Hotel", Address = "Dubai Marina Walk 5", Stars = 5 },
                new Hotel { Id = 207, Name = "Cairo Pyramids View Hotel", Address = "Pyramid Street 18", Stars = 4 },
                new Hotel { Id = 208, Name = "Budapest Royal Center Hotel", Address = "Váci Utca 33", Stars = 3 },
                new Hotel { Id = 209, Name = "Krakow Old Town Plaza Hotel", Address = "Floriańska 15", Stars = 4 },
                new Hotel { Id = 210, Name = "Zanzibar Blue Lagoon Resort", Address = "Kendwa Beach 1", Stars = 5 },
                new Hotel { Id = 211, Name = "Hurghada Golden Sand Resort", Address = "Sheraton Road 55", Stars = 5 },
                new Hotel { Id = 212, Name = "Lisbon Alfama Boutique Hotel", Address = "Rua dos Remédios 21", Stars = 4 },
                new Hotel { Id = 213, Name = "Acropolis View Hotel Athens", Address = "Dionysiou Areopagitou 8", Stars = 3 },
                new Hotel { Id = 214, Name = "Hotel Adriatic Split", Address = "Obala Hrvatskog Narodnog 17", Stars = 4 }

            );

            // =========================================================================
            // 11. HOTEL IMAGES (1 main + 3 dodatne)
            // =========================================================================
            int hid = 1;
            void AddHotelImages(int hotelId)
            {
                builder.Entity<HotelImages>().HasData(
                    new HotelImages { Id = hid++, HotelId = hotelId, ImageUrl = $"hotel{hotelId}_main.jpg", IsMain = true },
                    new HotelImages { Id = hid++, HotelId = hotelId, ImageUrl = $"hotel{hotelId}_1.jpg", IsMain = false },
                    new HotelImages { Id = hid++, HotelId = hotelId, ImageUrl = $"hotel{hotelId}_2.jpg", IsMain = false },
                    new HotelImages { Id = hid++, HotelId = hotelId, ImageUrl = $"hotel{hotelId}_3.jpg", IsMain = false }


                );
            }

            foreach (var hotelId in new[] { 100, 101, 102, 110, 111, 112, 120, 121, 122 })
                AddHotelImages(hotelId);

            builder.Entity<HotelImages>().HasData(
                new HotelImages { Id = 6000, HotelId = 200, ImageUrl = "primjer.jpg", IsMain = true },
                new HotelImages { Id = 6001, HotelId = 201, ImageUrl = "primjer.jpg", IsMain = true },
                new HotelImages { Id = 6002, HotelId = 202, ImageUrl = "primjer.jpg", IsMain = true },
                new HotelImages { Id = 6003, HotelId = 203, ImageUrl = "primjer.jpg", IsMain = true },
                new HotelImages { Id = 6004, HotelId = 204, ImageUrl = "primjer.jpg", IsMain = true },
                new HotelImages { Id = 6005, HotelId = 205, ImageUrl = "primjer.jpg", IsMain = true },
                new HotelImages { Id = 6006, HotelId = 206, ImageUrl = "primjer.jpg", IsMain = true },
                new HotelImages { Id = 6007, HotelId = 207, ImageUrl = "primjer.jpg", IsMain = true },
                new HotelImages { Id = 6008, HotelId = 208, ImageUrl = "primjer.jpg", IsMain = true },
                new HotelImages { Id = 6009, HotelId = 209, ImageUrl = "primjer.jpg", IsMain = true },
                new HotelImages { Id = 6010, HotelId = 210, ImageUrl = "primjer.jpg", IsMain = true },
                new HotelImages { Id = 6011, HotelId = 211, ImageUrl = "primjer.jpg", IsMain = true },
                new HotelImages { Id = 6012, HotelId = 212, ImageUrl = "primjer.jpg", IsMain = true },
                new HotelImages { Id = 6013, HotelId = 213, ImageUrl = "primjer.jpg", IsMain = true },
                new HotelImages { Id = 6014, HotelId = 214, ImageUrl = "primjer.jpg", IsMain = true }
            );



            // =========================================================================
            // 12. OFFER → HOTELS (UTC)
            // =========================================================================
            builder.Entity<OfferHotels>().HasData(
                // FIRNCA — 5 dana
                new OfferHotels { OfferDetailsId = 1, HotelId = 100, DepartureDate = new DateTime(2026, 3, 2, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2026, 3, 7, 0, 0, 0, DateTimeKind.Utc) },
                new OfferHotels { OfferDetailsId = 1, HotelId = 101, DepartureDate = new DateTime(2026, 3, 2, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2026, 3, 7, 0, 0, 0, DateTimeKind.Utc) },
                new OfferHotels { OfferDetailsId = 1, HotelId = 102, DepartureDate = new DateTime(2026, 3, 2, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2026, 3, 7, 0, 0, 0, DateTimeKind.Utc) },

                // SANTORINI — 7 dana
                new OfferHotels { OfferDetailsId = 2, HotelId = 110, DepartureDate = new DateTime(2026, 3, 2, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2026, 3, 9, 0, 0, 0, DateTimeKind.Utc) },
                new OfferHotels { OfferDetailsId = 2, HotelId = 111, DepartureDate = new DateTime(2026, 3, 2, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2026, 3, 9, 0, 0, 0, DateTimeKind.Utc) },
                new OfferHotels { OfferDetailsId = 2, HotelId = 112, DepartureDate = new DateTime(2026, 3, 2, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2026, 3, 9, 0, 0, 0, DateTimeKind.Utc) },

                // ISTANBUL — 4 dana
                new OfferHotels { OfferDetailsId = 3, HotelId = 120, DepartureDate = new DateTime(2026, 3, 2, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2026, 3, 6, 0, 0, 0, DateTimeKind.Utc) },
                new OfferHotels { OfferDetailsId = 3, HotelId = 121, DepartureDate = new DateTime(2026, 3, 2, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2026, 3, 6, 0, 0, 0, DateTimeKind.Utc) },
                new OfferHotels { OfferDetailsId = 3, HotelId = 122, DepartureDate = new DateTime(2026, 3, 2, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2026, 3, 6, 0, 0, 0, DateTimeKind.Utc) },
                new OfferHotels { OfferDetailsId = 4, HotelId = 200, DepartureDate = new DateTime(2026, 3, 2, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2026, 3, 10, 0, 0, 0, DateTimeKind.Utc) }, // 8 dana
                new OfferHotels { OfferDetailsId = 5, HotelId = 201, DepartureDate = new DateTime(2026, 3, 2, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2026, 3, 8, 0, 0, 0, DateTimeKind.Utc) },  // 6 dana
                new OfferHotels { OfferDetailsId = 6, HotelId = 202, DepartureDate = new DateTime(2026, 3, 2, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2026, 3, 13, 0, 0, 0, DateTimeKind.Utc) }, // 11 dana
                new OfferHotels { OfferDetailsId = 7, HotelId = 203, DepartureDate = new DateTime(2026, 3, 2, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2026, 3, 11, 0, 0, 0, DateTimeKind.Utc) }, // 9 dana
                new OfferHotels { OfferDetailsId = 8, HotelId = 204, DepartureDate = new DateTime(2026, 3, 2, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2026, 3, 11, 0, 0, 0, DateTimeKind.Utc) }, // 9 dana
                new OfferHotels { OfferDetailsId = 9, HotelId = 205, DepartureDate = new DateTime(2026, 3, 2, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2026, 3, 6, 0, 0, 0, DateTimeKind.Utc) },  // 4 dana
                new OfferHotels { OfferDetailsId = 10, HotelId = 206, DepartureDate = new DateTime(2026, 3, 2, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2026, 3, 9, 0, 0, 0, DateTimeKind.Utc) },  // 7 dana
                new OfferHotels { OfferDetailsId = 11, HotelId = 207, DepartureDate = new DateTime(2026, 3, 2, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2026, 3, 12, 0, 0, 0, DateTimeKind.Utc) }, // 10 dana
                new OfferHotels { OfferDetailsId = 12, HotelId = 208, DepartureDate = new DateTime(2026, 3, 2, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2026, 3, 9, 0, 0, 0, DateTimeKind.Utc) },  // 7 dana
                new OfferHotels { OfferDetailsId = 13, HotelId = 209, DepartureDate = new DateTime(2026, 3, 2, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2026, 3, 8, 0, 0, 0, DateTimeKind.Utc) },  // 6 dana
                new OfferHotels { OfferDetailsId = 14, HotelId = 210, DepartureDate = new DateTime(2026, 3, 2, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2026, 3, 6, 0, 0, 0, DateTimeKind.Utc) },  // 4 dana
                new OfferHotels { OfferDetailsId = 15, HotelId = 211, DepartureDate = new DateTime(2026, 3, 2, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2026, 3, 8, 0, 0, 0, DateTimeKind.Utc) },  // 6 dana
                new OfferHotels { OfferDetailsId = 16, HotelId = 212, DepartureDate = new DateTime(2026, 3, 2, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2026, 3, 10, 0, 0, 0, DateTimeKind.Utc) }, // 8 dana
                new OfferHotels { OfferDetailsId = 17, HotelId = 213, DepartureDate = new DateTime(2026, 3, 2, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2026, 3, 6, 0, 0, 0, DateTimeKind.Utc) },  // 4 dana
                new OfferHotels { OfferDetailsId = 18, HotelId = 214, DepartureDate = new DateTime(2026, 3, 2, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2026, 3, 8, 0, 0, 0, DateTimeKind.Utc) } // 6 dana

            );


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

            builder.Entity<HotelRooms>().HasData(
                // Hotel 100
                new HotelRooms { HotelId = 100, RoomId = 1, RoomsLeft = 10 },
                new HotelRooms { HotelId = 100, RoomId = 2, RoomsLeft = 6 },
                new HotelRooms { HotelId = 100, RoomId = 3, RoomsLeft = 4 },
                new HotelRooms { HotelId = 100, RoomId = 4, RoomsLeft = 8 },

                // Hotel 101
                new HotelRooms { HotelId = 101, RoomId = 1, RoomsLeft = 10 },
                new HotelRooms { HotelId = 101, RoomId = 2, RoomsLeft = 6 },
                new HotelRooms { HotelId = 101, RoomId = 3, RoomsLeft = 4 },
                new HotelRooms { HotelId = 101, RoomId = 4, RoomsLeft = 8 },

                // Hotel 102
                new HotelRooms { HotelId = 102, RoomId = 1, RoomsLeft = 10 },
                new HotelRooms { HotelId = 102, RoomId = 2, RoomsLeft = 6 },
                new HotelRooms { HotelId = 102, RoomId = 3, RoomsLeft = 4 },
                new HotelRooms { HotelId = 102, RoomId = 4, RoomsLeft = 8 },

                // Hotel 110
                new HotelRooms { HotelId = 110, RoomId = 1, RoomsLeft = 10 },
                new HotelRooms { HotelId = 110, RoomId = 2, RoomsLeft = 6 },
                new HotelRooms { HotelId = 110, RoomId = 3, RoomsLeft = 4 },
                new HotelRooms { HotelId = 110, RoomId = 4, RoomsLeft = 8 },

                // Hotel 111
                new HotelRooms { HotelId = 111, RoomId = 1, RoomsLeft = 10 },
                new HotelRooms { HotelId = 111, RoomId = 2, RoomsLeft = 6 },
                new HotelRooms { HotelId = 111, RoomId = 3, RoomsLeft = 4 },
                new HotelRooms { HotelId = 111, RoomId = 4, RoomsLeft = 8 },

                // Hotel 112
                new HotelRooms { HotelId = 112, RoomId = 1, RoomsLeft = 10 },
                new HotelRooms { HotelId = 112, RoomId = 2, RoomsLeft = 6 },
                new HotelRooms { HotelId = 112, RoomId = 3, RoomsLeft = 4 },
                new HotelRooms { HotelId = 112, RoomId = 4, RoomsLeft = 8 },

                // Hotel 120
                new HotelRooms { HotelId = 120, RoomId = 1, RoomsLeft = 10 },
                new HotelRooms { HotelId = 120, RoomId = 2, RoomsLeft = 6 },
                new HotelRooms { HotelId = 120, RoomId = 3, RoomsLeft = 4 },
                new HotelRooms { HotelId = 120, RoomId = 4, RoomsLeft = 8 },

                // Hotel 121
                new HotelRooms { HotelId = 121, RoomId = 1, RoomsLeft = 10 },
                new HotelRooms { HotelId = 121, RoomId = 2, RoomsLeft = 6 },
                new HotelRooms { HotelId = 121, RoomId = 3, RoomsLeft = 4 },
                new HotelRooms { HotelId = 121, RoomId = 4, RoomsLeft = 8 },

                // Hotel 122
                new HotelRooms { HotelId = 122, RoomId = 1, RoomsLeft = 10 },
                new HotelRooms { HotelId = 122, RoomId = 2, RoomsLeft = 6 },
                new HotelRooms { HotelId = 122, RoomId = 3, RoomsLeft = 4 },
                new HotelRooms { HotelId = 122, RoomId = 4, RoomsLeft = 8 }
            );

            // =========================================================================
            // 15. RESERVATIONS (UserId 5–24)
            // =========================================================================

            builder.Entity<Reservation>().HasData(
    new Reservation { Id = 2000, UserId = 5, OfferId = 1, HotelId = 100, RoomId = 1, IsActive = true, IncludeInsurance = true, isFirstRatePaid = true, isFullPaid = false, TotalPrice = 450, PriceLeftToPay = 150, CreatedAt = new DateTime(2025, 12, 01, 0, 0, 0, DateTimeKind.Utc), addedNeeds = "Vegetarijanski meni" },
    new Reservation { Id = 2001, UserId = 6, OfferId = 2, HotelId = 110, RoomId = 2, IsActive = true, IncludeInsurance = false, isFirstRatePaid = false, isFullPaid = true, TotalPrice = 900, PriceLeftToPay = 900, CreatedAt = new DateTime(2025, 12, 01, 0, 0, 0, DateTimeKind.Utc), addedNeeds = "" },
    new Reservation { Id = 2002, UserId = 7, OfferId = 3, HotelId = 120, RoomId = 1, IsActive = true, IncludeInsurance = true, isFirstRatePaid = true, isFullPaid = false, TotalPrice = 350, PriceLeftToPay = 50, CreatedAt = new DateTime(2025, 12, 01, 0, 0, 0, DateTimeKind.Utc), addedNeeds = "" },
    new Reservation { Id = 2003, UserId = 8, OfferId = 2, HotelId = 111, RoomId = 3, IsActive = true, IncludeInsurance = false, isFirstRatePaid = false, isFullPaid = true, TotalPrice = 900, PriceLeftToPay = 0, CreatedAt = new DateTime(2025, 12, 01, 0, 0, 0, DateTimeKind.Utc), addedNeeds = "" },
    new Reservation { Id = 2004, UserId = 9, OfferId = 1, HotelId = 101, RoomId = 2, IsActive = true, IncludeInsurance = true, isFirstRatePaid = true, isFullPaid = false, TotalPrice = 450, PriceLeftToPay = 150, CreatedAt = new DateTime(2025, 12, 01, 0, 0, 0, DateTimeKind.Utc), addedNeeds = "" },
    new Reservation { Id = 2005, UserId = 10, OfferId = 3, HotelId = 122, RoomId = 1, IsActive = true, IncludeInsurance = false, isFirstRatePaid = true, isFullPaid = false, TotalPrice = 350, PriceLeftToPay = 50, CreatedAt = new DateTime(2025, 12, 01, 0, 0, 0, DateTimeKind.Utc), addedNeeds = "" },
    new Reservation { Id = 2006, UserId = 11, OfferId = 1, HotelId = 102, RoomId = 4, IsActive = true, IncludeInsurance = true, isFirstRatePaid = false, isFullPaid = true, TotalPrice = 450, PriceLeftToPay = 0, CreatedAt = new DateTime(2025, 12, 01, 0, 0, 0, DateTimeKind.Utc), addedNeeds = "" },
    new Reservation { Id = 2007, UserId = 12, OfferId = 2, HotelId = 112, RoomId = 1, IsActive = true, IncludeInsurance = true, isFirstRatePaid = true, isFullPaid = false, TotalPrice = 900, PriceLeftToPay = 600, CreatedAt = new DateTime(2025, 12, 01, 0, 0, 0, DateTimeKind.Utc), addedNeeds = "Pristup teretani" },
    new Reservation { Id = 2008, UserId = 13, OfferId = 3, HotelId = 120, RoomId = 2, IsActive = true, IncludeInsurance = false, isFirstRatePaid = false, isFullPaid = true, TotalPrice = 350, PriceLeftToPay = 0, CreatedAt = new DateTime(2025, 12, 01, 0, 0, 0, DateTimeKind.Utc), addedNeeds = "" },
    new Reservation { Id = 2009, UserId = 14, OfferId = 1, HotelId = 101, RoomId = 3, IsActive = true, IncludeInsurance = true, isFirstRatePaid = true, isFullPaid = false, TotalPrice = 450, PriceLeftToPay = 150, CreatedAt = new DateTime(2025, 12, 01, 0, 0, 0, DateTimeKind.Utc), addedNeeds = "" },

    new Reservation { Id = 2010, UserId = 15, OfferId = 2, HotelId = 110, RoomId = 4, IsActive = true, IncludeInsurance = false, isFirstRatePaid = true, isFullPaid = false, TotalPrice = 900, PriceLeftToPay = 600, CreatedAt = new DateTime(2025, 12, 01, 0, 0, 0, DateTimeKind.Utc), addedNeeds = "" },
    new Reservation { Id = 2011, UserId = 16, OfferId = 3, HotelId = 121, RoomId = 2, IsActive = true, IncludeInsurance = true, isFirstRatePaid = true, isFullPaid = false, TotalPrice = 350, PriceLeftToPay = 50, CreatedAt = new DateTime(2025, 12, 01, 0, 0, 0, DateTimeKind.Utc), addedNeeds = "" },
    new Reservation { Id = 2012, UserId = 17, OfferId = 1, HotelId = 102, RoomId = 2, IsActive = true, IncludeInsurance = false, isFirstRatePaid = true, isFullPaid = false, TotalPrice = 450, PriceLeftToPay = 150, CreatedAt = new DateTime(2025, 12, 01, 0, 0, 0, DateTimeKind.Utc), addedNeeds = "" },
    new Reservation { Id = 2013, UserId = 18, OfferId = 2, HotelId = 111, RoomId = 2, IsActive = true, IncludeInsurance = true, isFirstRatePaid = false, isFullPaid = true, TotalPrice = 900, PriceLeftToPay = 0, CreatedAt = new DateTime(2025, 12, 01, 0, 0, 0, DateTimeKind.Utc), addedNeeds = "Bez glutena" },
    new Reservation { Id = 2014, UserId = 19, OfferId = 3, HotelId = 120, RoomId = 3, IsActive = true, IncludeInsurance = true, isFirstRatePaid = true, isFullPaid = false, TotalPrice = 350, PriceLeftToPay = 50, CreatedAt = new DateTime(2025, 12, 01, 0, 0, 0, DateTimeKind.Utc), addedNeeds = "" },
    new Reservation { Id = 2015, UserId = 20, OfferId = 1, HotelId = 101, RoomId = 1, IsActive = true, IncludeInsurance = true, isFirstRatePaid = false, isFullPaid = true, TotalPrice = 450, PriceLeftToPay = 0, CreatedAt = new DateTime(2025, 12, 01, 0, 0, 0, DateTimeKind.Utc), addedNeeds = "" },
    new Reservation { Id = 2016, UserId = 21, OfferId = 2, HotelId = 112, RoomId = 1, IsActive = true, IncludeInsurance = true, isFirstRatePaid = true, isFullPaid = false, TotalPrice = 900, PriceLeftToPay = 600, CreatedAt = new DateTime(2025, 12, 01, 0, 0, 0, DateTimeKind.Utc), addedNeeds = "" },
    new Reservation { Id = 2017, UserId = 22, OfferId = 3, HotelId = 122, RoomId = 1, IsActive = true, IncludeInsurance = true, isFirstRatePaid = true, isFullPaid = false, TotalPrice = 350, PriceLeftToPay = 50, CreatedAt = new DateTime(2025, 12, 01, 0, 0, 0, DateTimeKind.Utc), addedNeeds = "" },
    new Reservation { Id = 2018, UserId = 23, OfferId = 1, HotelId = 100, RoomId = 1, IsActive = true, IncludeInsurance = false, isFirstRatePaid = true, isFullPaid = false, TotalPrice = 450, PriceLeftToPay = 150, CreatedAt = new DateTime(2025, 12, 01, 0, 0, 0, DateTimeKind.Utc), addedNeeds = "" },
    new Reservation { Id = 2019, UserId = 24, OfferId = 2, HotelId = 111, RoomId = 3, IsActive = true, IncludeInsurance = true, isFirstRatePaid = false, isFullPaid = true, TotalPrice = 900, PriceLeftToPay = 0, CreatedAt = new DateTime(2025, 12, 01, 0, 0, 0, DateTimeKind.Utc), addedNeeds = "" },
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
        TotalPrice = 0,
        PriceLeftToPay = 0,
        CreatedAt = new DateTime(2026, 3, 2, 0, 0, 0, DateTimeKind.Utc),
        addedNeeds = ""
    },

    // ============================
    // USER 6 — SANTORINI (Offer 2)
    // ============================
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
        TotalPrice = 0,
        PriceLeftToPay = 0,
        CreatedAt = new DateTime(2026, 3, 2, 0, 0, 0, DateTimeKind.Utc),
        addedNeeds = ""
    },

    // ============================
    // USER 6 — ISTANBUL (Offer 3)
    // ============================
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
            new Comment
            {
                Id = 1,
                userId = 4,      // Ajdin
                offerId = 1,     // Firenca
                comment = "Predivno putovanje! Organizacija odlična, vodič fenomenalan.",
                starRate = 5
            },
            new Comment
            {
                Id = 2,
                userId = 5,
                offerId = 1,
                comment = "Sve super osim hotela koji je mogao biti bolji.",
                starRate = 4
            },
            new Comment
            {
                Id = 3,
                userId = 6,
                offerId = 2,     // Santorini
                comment = "Santorini je san! Svaka preporuka, ponovo bih išao.",
                starRate = 5
            },
            new Comment
            {
                Id = 4,
                userId = 7,
                offerId = 2,
                comment = "Lijepo putovanje, ali dosta gužve.",
                starRate = 4
            },
            new Comment
            {
                Id = 5,
                userId = 8,
                offerId = 3,     // Istanbul
                comment = "Veoma interesantna tura, vodič je znao sve detalje.",
                starRate = 5
            },
            new Comment
            {
                Id = 6,
                userId = 9,
                offerId = 3,
                comment = "Odlična cijena za ono što se dobije.",
                starRate = 4
            },
            new Comment
            {
                Id = 7,
                userId = 10,
                offerId = 4,     // Barcelona
                comment = "Barcelona je bila fantastična! Hotel blizu centra.",
                starRate = 5
            },
            new Comment
            {
                Id = 8,
                userId = 11,
                offerId = 4,
                comment = "Predugo putovanje avionom, ali sve ostalo vrhunski.",
                starRate = 4
            },
            new Comment
            {
                Id = 9,
                userId = 12,
                offerId = 5,     // Pariz
                comment = "Grad svjetlosti je ispunio sva očekivanja!",
                starRate = 5
            },
            new Comment
            {
                Id = 10,
                userId = 13,
                offerId = 5,
                comment = "Skupo, ali vrijedilo je svakog dinara.",
                starRate = 5
            },
            new Comment
            {
                Id = 11,
                userId = 14,
                offerId = 7,     // Beč
                comment = "Mirno i ugodno putovanje, savršeno za vikend bijeg.",
                starRate = 4
            },
            new Comment
            {
                Id = 12,
                userId = 15,
                offerId = 7,
                comment = "Sve je bilo dobro organizovano.",
                starRate = 4
            },
            new Comment
            {
                Id = 13,
                userId = 16,
                offerId = 10,    // Dubai
                comment = "Dubai je nevjerovatno iskustvo! Top organizacija.",
                starRate = 5
            },
            new Comment
            {
                Id = 14,
                userId = 17,
                offerId = 10,
                comment = "Hotel vrhunski, vodič odličan. Preporučujem!",
                starRate = 5
            },
            new Comment
            {
                Id = 15,
                userId = 18,
                offerId = 12,    // Budimpešta
                comment = "Lijep grad, ali premalo vremena u slobodnoj zoni.",
                starRate = 3
            },

new Comment
{
    Id = 30,
    userId = 6, // Edin
    offerId = 1,
    comment = "Uživao sam u svakom trenutku. Posebno mi se dopala posjeta galeriji Uffizi. Preporučujem!",
    starRate = 5
},

new Comment
{
    Id = 40,
    userId = 7, // Lana
    offerId = 1,
    comment = "Prelijep grad, ali je bilo dosta gužve. Vodič je bio odličan i puno nam je olakšao obilazak.",
    starRate = 4
},

new Comment
{
    Id = 50,
    userId = 8, // Haris
    offerId = 1,
    comment = "Firenca je idealna destinacija za ljubitelje historije i arhitekture. Sve pohvale eTravel timu!",
    starRate = 5
},

new Comment
{
    Id = 60,
    userId = 9, // Amira
    offerId = 1,
    comment = "Putovanje je bilo divno, ali mislim da je moglo trajati dan duže. Grad nudi previše toga da se vidi.",
    starRate = 4
},

new Comment
{
    Id = 70,
    userId = 10, // Tarik
    offerId = 1,
    comment = "Odlična organizacija, lijep hotel i savršeno isplanirana tura. Definitivno ponovo putujem preko vas!",
    starRate = 5
},

new Comment
{
    Id = 80,
    userId = 11, // Selma
    offerId = 1,
    comment = "Predivna atmosfera u gradu. Najviše mi se dopao obilazak Duoma. Vrijedi svake marke!",
    starRate = 5
},

new Comment
{
    Id = 90,
    userId = 12, // Nedim
    offerId = 1,
    comment = "Vrlo profesionalna agencija. Sve je bilo ispoštovano prema planu. Putovanje bez brige!",
    starRate = 4
},

new Comment
{
    Id = 100,
    userId = 13, // Alma
    offerId = 1,
    comment = "Iznad očekivanja! Hrana u Firenci je savršena, a vodič izuzetno ljubazan i informisan.",
    starRate = 5
},

new Comment
{
    Id = 110,
    userId = 14, // Mirza
    offerId = 1,
    comment = "Sve je bilo odlično, ali raspored je bio malo pretrpan. Ipak, uživao sam u obilasku grada.",
    starRate = 3
},

new Comment
{
    Id = 120,
    userId = 15, // Melisa
    offerId = 1,
    comment = "Najbolje putovanje do sada! Preporučujem svakome ko želi spoj kulture, hrane i dobre organizacije.",
    starRate = 5
},

new Comment
{
    Id = 130,
    userId = 16, // Almin
    offerId = 1,
    comment = "Bilo je super, ali mislim da bi bilo bolje da smo imali više slobodnog vremena za samostalno istraživanje.",
    starRate = 4
},

new Comment
{
    Id = 140,
    userId = 17, // Dina
    offerId = 1,
    comment = "Savršeno iskustvo! Firenca je čarobna, sve preporuke za ovaj aranžman.",
    starRate = 5
},

new Comment
{
    Id = 150,
    userId = 5,
    offerId = 1,
    comment = "Putovanje je prošlo bez ikakvih problema. Sve na vrijeme, sve tačno onako kako je opisano.",
    starRate = 5
},

new Comment
{
    Id = 160,
    userId = 6,
    offerId = 1,
    comment = "Moja druga posjeta Firenci, ali prvi put preko ove agencije. Organizacija vrhunska!",
    starRate = 5
},

new Comment
{
    Id = 170,
    userId = 7,
    offerId = 1,
    comment = "Uživala sam! Preporučila bih ovo putovanje svima koji žele opuštajuće i edukativno iskustvo.",
    starRate = 5
},

new Comment
{
    Id = 180,
    userId = 8,
    offerId = 1,
    comment = "Sve preporuke! Jedino što bih promijenio jeste dužina pauza tokom obilaska.",
    starRate = 4
},

new Comment
{
    Id = 190,
    userId = 9,
    offerId = 1,
    comment = "Perfektno putovanje, odličan vodič i fenomenalne lokacije. Firenca je nevjerovatna!",
    starRate = 5
},

new Comment
{
    Id = 200,
    userId = 10,
    offerId = 1,
    comment = "Sve je bilo super, ali smještaj bi mogao biti malo bliže centru grada.",
    starRate = 4
},

new Comment
{
    Id = 210,
    userId = 11,
    offerId = 1,
    comment = "Najbolje organizovan aranžman na kojem sam bila. Svaka čast eTravel timu!",
    starRate = 5
}

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
    new UserToken { UserId = 4,  Equity = 120 },
    new UserToken { UserId = 5,  Equity = 340 },
    new UserToken { UserId = 6,  Equity = 75 },
    new UserToken { UserId = 7,  Equity = 210 },
    new UserToken { UserId = 8,  Equity = 500 },
    new UserToken { UserId = 9,  Equity = 160 },
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

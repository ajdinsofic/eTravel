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

            // =========================================================================
            // 6. USER ROLES (Veza korisnika i rola)
            // =========================================================================
            builder.Entity<UserRole>().HasData(
                new UserRole { UserId = 1, RoleId = 2 }, // radnik → Radnik
                new UserRole { UserId = 2, RoleId = 3 }, // direktor → Direktor
                new UserRole { UserId = 4, RoleId = 1 }  // korisnik → Korisnik
            );

            // =========================================================================
            // 7. OFFER (ponude)
            // =========================================================================
            builder.Entity<Offer>().HasData(
                new Offer { Id = 1, Title = "Firenca", DaysInTotal = 5, WayOfTravel = "Autobus", SubCategoryId = 1 },
                new Offer { Id = 2, Title = "Santorini", DaysInTotal = 7, WayOfTravel = "Avion", SubCategoryId = 1 },
                new Offer { Id = 3, Title = "Istanbul", DaysInTotal = 4, WayOfTravel = "Autobus", SubCategoryId = 1 }
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
                    TravelInsuranceTotal = 15m
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
                    TravelInsuranceTotal = 25m
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
                    TravelInsuranceTotal = 12m
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
                new OfferImage { Id = 12, OfferId = 3, ImageUrl = "istanbul_3.jpg", isMain = false }
            );

            // =========================================================================
            // 9.1. OFFER PLAN DAYS 
            // =========================================================================

           // Firenca
            builder.Entity<OfferPlanDay>().HasData(
                new OfferPlanDay {
                    OfferDetailsId = 1,
                    DayNumber = 1,
                    DayTitle = "Polazak i dolazak u Firencu",
                    DayDescription = "Polazak u ranim jutarnjim satima. Pauze tokom puta. Dolazak u Firencu u poslijepodnevnim satima. Smještaj u hotel i slobodno vrijeme za odmor ili šetnju centrom grada."
                },
                new OfferPlanDay {
                    OfferDetailsId = 1,
                    DayNumber = 2,
                    DayTitle = "Upoznavanje sa starim gradom",
                    DayDescription = "Doručak. Organizovano razgledanje: Katedrala Santa Maria del Fiore, Piazza della Signoria, Palazzo Vecchio i Ponte Vecchio. Popodne slobodno vrijeme za individualno istraživanje i kupovinu suvenira."
                },
                new OfferPlanDay {
                    OfferDetailsId = 1,
                    DayNumber = 3,
                    DayTitle = "Galerija Uffizi i slobodno popodne",
                    DayDescription = "Posjeta čuvenoj galeriji Uffizi – remek-djela renesansnih umjetnika: Botticelli, Michelangelo, Da Vinci. Nakon obilaska, slobodno vrijeme za ručak u lokalnim restoranima i uživanje u talijanskoj kuhinji."
                },
                new OfferPlanDay {
                    OfferDetailsId = 1,
                    DayNumber = 4,
                    DayTitle = "Izlet u Pisu ili slobodan dan",
                    DayDescription = "Mogućnost fakultativnog izleta u Pisu i posjete Krivom tornju. Alternativno, slobodan dan u Firenci za šoping, obilazak muzeja, degustaciju vina ili šetnju slikovitim ulicama."
                },
                new OfferPlanDay {
                    OfferDetailsId = 1,
                    DayNumber = 5,
                    DayTitle = "Povratak kući",
                    DayDescription = "Check-out iz hotela i polazak prema kući. Pauze tokom puta. Dolazak u kasnim večernjim satima."
                }
            );

            // Santorini
            builder.Entity<OfferPlanDay>().HasData(
                new OfferPlanDay {
                    OfferDetailsId = 2,
                    DayNumber = 1,
                    DayTitle = "Dolazak na Santorini",
                    DayDescription = "Let ili transfer do Santorinija. Smještaj u hotel. Slobodno vrijeme za odmor, kupanje ili večernju šetnju rivom u Firi."
                },
                new OfferPlanDay {
                    OfferDetailsId = 2,
                    DayNumber = 2,
                    DayTitle = "Fira – glavni grad ostrva",
                    DayDescription = "Nakon doručka slijedi obilazak Fire: uske bijele ulice, crkve sa plavim kupolama i prekrasni vidikovci. Popodne slobodno vrijeme za kupovinu ili posjetu lokalnim tavernama."
                },
                new OfferPlanDay {
                    OfferDetailsId = 2,
                    DayNumber = 3,
                    DayTitle = "Oia – najljepši zalazak sunca",
                    DayDescription = "Prijepodnevno slobodno vrijeme za plažu. U poslijepodnevnim satima odlazak u Oiu – najpoznatije mjesto na Santoriniju. Uživanje u fantastičnom zalasku sunca."
                },
                new OfferPlanDay {
                    OfferDetailsId = 2,
                    DayNumber = 4,
                    DayTitle = "Crna i crvena plaža",
                    DayDescription = "Obilazak vulkanskih plaža: Red Beach i Perissa (Black Beach). Slobodno vrijeme za kupanje i sunčanje. Povratak u hotel u večernjim satima."
                },
                new OfferPlanDay {
                    OfferDetailsId = 2,
                    DayNumber = 5,
                    DayTitle = "Vulkansko ostrvo i termalni izvori",
                    DayDescription = "Izlet brodom do vulkanskog ostrva Nea Kameni, šetnja kraterom i kupanje u toplim termalnim izvorima. Povratak brodom u luku."
                },
                new OfferPlanDay {
                    OfferDetailsId = 2,
                    DayNumber = 6,
                    DayTitle = "Slobodan dan",
                    DayDescription = "Dan predviđen za odmor, kupanje ili fakultativne aktivnosti – iznajmljivanje quada, degustacije vina, panoramska vožnja ostrvom."
                },
                new OfferPlanDay {
                    OfferDetailsId = 2,
                    DayNumber = 7,
                    DayTitle = "Povratak kući",
                    DayDescription = "Odjava iz hotela i povratak kući prema planu putovanja."
                }
            );

            //Istambul
            builder.Entity<OfferPlanDay>().HasData(
                new OfferPlanDay {
                    OfferDetailsId = 3,
                    DayNumber = 1,
                    DayTitle = "Dolazak u Istanbul",
                    DayDescription = "Dolazak u Istanbul i smještaj u hotel. Slobodno vrijeme za odmor. Uveče mogućnost odlaska na večeru u tradicionalni turski restoran."
                },
                new OfferPlanDay {
                    OfferDetailsId = 3,
                    DayNumber = 2,
                    DayTitle = "Stari dio Istanbula – Sultanahmet",
                    DayDescription = "Obilazak najvećih atrakcija: Aja Sofija, Sultanahmet džamija, Hipodrom i Topkapi palata. Popodne slobodno vrijeme ili posjeta Grand Bazaru."
                },
                new OfferPlanDay {
                    OfferDetailsId = 3,
                    DayNumber = 3,
                    DayTitle = "Bosfor krstarenje i Taksim",
                    DayDescription = "Jutarnje krstarenje Bosforom – pogled na palače, mostove i obalu. U popodnevnim satima odlazak na Taksim trg i šetnja Istiklal ulicom."
                },
                new OfferPlanDay {
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
                new Hotel { Id = 122, Name = "Bosfor Palace Hotel", Address = "Bosfor Blvd 7", Stars = 5 }
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


            // =========================================================================
            // 12. OFFER → HOTELS (UTC)
            // =========================================================================
            builder.Entity<OfferHotels>().HasData(
                // Firenca
                new OfferHotels { OfferDetailsId = 1, HotelId = 100, DepartureDate = new DateTime(2025, 5, 10, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2025, 5, 15, 0, 0, 0, DateTimeKind.Utc) },
                new OfferHotels { OfferDetailsId = 1, HotelId = 101, DepartureDate = new DateTime(2025, 5, 10, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2025, 5, 15, 0, 0, 0, DateTimeKind.Utc) },
                new OfferHotels { OfferDetailsId = 1, HotelId = 102, DepartureDate = new DateTime(2025, 5, 10, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2025, 5, 15, 0, 0, 0, DateTimeKind.Utc) },

                // Santorini
                new OfferHotels { OfferDetailsId = 2, HotelId = 110, DepartureDate = new DateTime(2025, 6, 1, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2025, 6, 8, 0, 0, 0, DateTimeKind.Utc) },
                new OfferHotels { OfferDetailsId = 2, HotelId = 111, DepartureDate = new DateTime(2025, 6, 1, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2025, 6, 8, 0, 0, 0, DateTimeKind.Utc) },
                new OfferHotels { OfferDetailsId = 2, HotelId = 112, DepartureDate = new DateTime(2025, 6, 1, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2025, 6, 8, 0, 0, 0, DateTimeKind.Utc) },

                // Istanbul
                new OfferHotels { OfferDetailsId = 3, HotelId = 120, DepartureDate = new DateTime(2025, 4, 15, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2025, 4, 19, 0, 0, 0, DateTimeKind.Utc) },
                new OfferHotels { OfferDetailsId = 3, HotelId = 121, DepartureDate = new DateTime(2025, 4, 15, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2025, 4, 19, 0, 0, 0, DateTimeKind.Utc) },
                new OfferHotels { OfferDetailsId = 3, HotelId = 122, DepartureDate = new DateTime(2025, 4, 15, 0, 0, 0, DateTimeKind.Utc), ReturnDate = new DateTime(2025, 4, 19, 0, 0, 0, DateTimeKind.Utc) }
            );

            // =========================================================================
            // 13. ROOMS
            // =========================================================================

            builder.Entity<Rooms>().HasData(
            new Rooms { Id = 1, RoomType = "Dvokrevetna" },
            new Rooms { Id = 2, RoomType = "Trokrevetna" },
            new Rooms { Id = 3, RoomType = "Petokrevetna" },
            new Rooms { Id = 4, RoomType = "Cetverokrevetna"}
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

        }
    }
}

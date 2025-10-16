using System.Linq;
using eTravelAgencija.Services.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class eTravelAgencijaDbContext
    : IdentityDbContext<User, Role, string,
        IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
{
    public eTravelAgencijaDbContext(DbContextOptions<eTravelAgencijaDbContext> options)
        : base(options)
    {
    }

    public DbSet<Offer> Offers { get; set; }
    public DbSet<OfferDetails> OfferDetails { get; set; }
    public DbSet<OfferImage> OfferImages { get; set; }
    public DbSet<OfferPlanDay> OfferPlanDays { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<OfferHotels> OfferHotels { get; set; }
    public DbSet<OfferCategory> OfferCategories { get; set; }
    public DbSet<OfferSubCategory> OfferSubCategories { get; set; }
    public DbSet<HotelImages> HotelImages { get; set; }
    public DbSet<HotelRooms> HotelRooms { get; set; }
    public DbSet<Rooms> Rooms { get; set; }
    public DbSet<OfferHotels> offerHotels { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserRole>(b =>
        {
            b.HasKey(ur => new { ur.UserId, ur.RoleId });
        });

        builder.Entity<OfferHotels>()
        .HasKey(oh => new { oh.OfferDetailsId, oh.HotelId });

        builder.Entity<OfferHotels>()
            .HasKey(oh => new { oh.OfferDetailsId, oh.HotelId, oh.DepartureDate, oh.ReturnDate });


        builder.Entity<OfferCategory>().HasData(
        new OfferCategory { Id = 1, Name = "Praznična putovanja" },
        new OfferCategory { Id = 2, Name = "Specijalna putovanja" },
        new OfferCategory { Id = 3, Name = "Osjetite mjesec" }
    );

        builder.Entity<OfferSubCategory>().HasData(
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

        builder.Entity<Offer>().HasData(
             new Offer { Id = 1, Title = "Putovanje u Pariz", Price = 850m, DaysInTotal = 7, WayOfTravel = "Avion", SubCategoryId = 1 },
             new Offer { Id = 2, Title = "Putovanje u Rim", Price = 760m, DaysInTotal = 6, WayOfTravel = "Autobus", SubCategoryId = 2 },
             new Offer { Id = 3, Title = "Putovanje u Madrid", Price = 920m, DaysInTotal = 8, WayOfTravel = "Avion", SubCategoryId = 3 },
             new Offer { Id = 4, Title = "Putovanje u Beč", Price = 540m, DaysInTotal = 4, WayOfTravel = "Autobus", SubCategoryId = 4 },
             new Offer { Id = 5, Title = "Putovanje u Atina", Price = 970m, DaysInTotal = 9, WayOfTravel = "Avion", SubCategoryId = 5 },
             new Offer { Id = 6, Title = "Putovanje u Amsterdam", Price = 1010m, DaysInTotal = 6, WayOfTravel = "Avion", SubCategoryId = 6 },
             new Offer { Id = 7, Title = "Putovanje u Lisabon", Price = 890m, DaysInTotal = 7, WayOfTravel = "Autobus", SubCategoryId = 7 },
             new Offer { Id = 8, Title = "Putovanje u Berlin", Price = 730m, DaysInTotal = 5, WayOfTravel = "Avion", SubCategoryId = 8 },
             new Offer { Id = 9, Title = "Putovanje u Prag", Price = 810m, DaysInTotal = 6, WayOfTravel = "Avion", SubCategoryId = 9 },
             new Offer { Id = 10, Title = "Putovanje u Kopenhagen", Price = 1340m, DaysInTotal = 10, WayOfTravel = "Avion", SubCategoryId = 10 },
             new Offer { Id = 11, Title = "Putovanje u Oslo", Price = 1230m, DaysInTotal = 9, WayOfTravel = "Avion", SubCategoryId = 11 },
             new Offer { Id = 12, Title = "Putovanje u Stockholm", Price = 1110m, DaysInTotal = 8, WayOfTravel = "Avion", SubCategoryId = 12 },
             new Offer { Id = 13, Title = "Putovanje u Ženeva", Price = 990m, DaysInTotal = 6, WayOfTravel = "Autobus", SubCategoryId = 1 },
             new Offer { Id = 14, Title = "Putovanje u Cirih", Price = 970m, DaysInTotal = 7, WayOfTravel = "Avion", SubCategoryId = 2 },
             new Offer { Id = 15, Title = "Putovanje u Istanbul", Price = 700m, DaysInTotal = 5, WayOfTravel = "Autobus", SubCategoryId = 3 },
             new Offer { Id = 16, Title = "Putovanje u Sarajevo", Price = 430m, DaysInTotal = 3, WayOfTravel = "Autobus", SubCategoryId = 4 },
             new Offer { Id = 17, Title = "Putovanje u Zagreb", Price = 500m, DaysInTotal = 4, WayOfTravel = "Autobus", SubCategoryId = 5 },
             new Offer { Id = 18, Title = "Putovanje u Beograd", Price = 520m, DaysInTotal = 4, WayOfTravel = "Autobus", SubCategoryId = 6 },
             new Offer { Id = 19, Title = "Putovanje u Dubrovnik", Price = 840m, DaysInTotal = 6, WayOfTravel = "Avion", SubCategoryId = 7 },
             new Offer { Id = 20, Title = "Putovanje u Split", Price = 790m, DaysInTotal = 6, WayOfTravel = "Autobus", SubCategoryId = 8 },
             new Offer { Id = 21, Title = "Putovanje u Ljubljana", Price = 680m, DaysInTotal = 5, WayOfTravel = "Autobus", SubCategoryId = 9 },
             new Offer { Id = 22, Title = "Putovanje u Podgorica", Price = 620m, DaysInTotal = 5, WayOfTravel = "Avion", SubCategoryId = 10 },
             new Offer { Id = 23, Title = "Putovanje u Tirana", Price = 640m, DaysInTotal = 5, WayOfTravel = "Avion", SubCategoryId = 11 },
             new Offer { Id = 24, Title = "Putovanje u Skoplje", Price = 600m, DaysInTotal = 5, WayOfTravel = "Autobus", SubCategoryId = 12 },
             new Offer { Id = 25, Title = "Putovanje u Budimpešta", Price = 900m, DaysInTotal = 6, WayOfTravel = "Avion", SubCategoryId = 1 },
             new Offer { Id = 26, Title = "Putovanje u Brisel", Price = 1050m, DaysInTotal = 7, WayOfTravel = "Avion", SubCategoryId = 2 },
             new Offer { Id = 27, Title = "Putovanje u Varšava", Price = 970m, DaysInTotal = 6, WayOfTravel = "Autobus", SubCategoryId = 3 },
             new Offer { Id = 28, Title = "Putovanje u Krakov", Price = 960m, DaysInTotal = 6, WayOfTravel = "Autobus", SubCategoryId = 4 },
             new Offer { Id = 29, Title = "Putovanje u Sofija", Price = 880m, DaysInTotal = 6, WayOfTravel = "Autobus", SubCategoryId = 5 },
             new Offer { Id = 30, Title = "Putovanje u Bukurešt", Price = 910m, DaysInTotal = 7, WayOfTravel = "Avion", SubCategoryId = 6 }
        );

        builder.Entity<OfferDetails>().HasData(
    new OfferDetails { OfferId = 1, Description = "Uživajte u opuštajućem putovanju kroz Pariz sa vrhunskim vodičima i nezaboravnim doživljajima.", City = "Pariz", Country = "Francuska" },
    new OfferDetails { OfferId = 2, Description = "Iskusite čari Rima, njegove istorijske znamenitosti i autentičnu kuhinju.", City = "Rim", Country = "Italija" },
    new OfferDetails { OfferId = 3, Description = "Otkrijte Madrid, grad prepun kulture, umetnosti i sjajnih pejzaža.", City = "Madrid", Country = "Španija" },
    new OfferDetails { OfferId = 4, Description = "Posetite Beč, grad muzike, istorije i predivne arhitekture.", City = "Beč", Country = "Austrija" },
    new OfferDetails { OfferId = 5, Description = "Istražite Atinu i uživajte u drevnoj grčkoj istoriji i prelepim plažama.", City = "Atina", Country = "Grčka" },
    new OfferDetails { OfferId = 6, Description = "Amsterdam vas poziva svojim kanalima, muzejima i opuštenom atmosferom.", City = "Amsterdam", Country = "Nizozemska" },
    new OfferDetails { OfferId = 7, Description = "Doživite čari Lisabona, njegovu arhitekturu i ukusnu hranu.", City = "Lisabon", Country = "Portugal" },
    new OfferDetails { OfferId = 8, Description = "Berlin, dinamični grad sa bogatom istorijom i živahnom kulturom.", City = "Berlin", Country = "Njemačka" },
    new OfferDetails { OfferId = 9, Description = "Prag, grad bajki, mostova i nezaboravnih večeri.", City = "Prag", Country = "Češka" },
    new OfferDetails { OfferId = 10, Description = "Kopenhagen – skandinavska oaza sa modernim i tradicionalnim sadržajima.", City = "Kopenhagen", Country = "Danska" },
    new OfferDetails { OfferId = 11, Description = "Oslo, prirodna lepota i urbani život na dohvat ruke.", City = "Oslo", Country = "Norveška" },
    new OfferDetails { OfferId = 12, Description = "Stockholm, grad na vodi sa bogatom istorijom i prelepim pejzažima.", City = "Stockholm", Country = "Švedska" },
    new OfferDetails { OfferId = 13, Description = "Ženeva, srce švajcarskih Alpa i međunarodna prestonica.", City = "Ženeva", Country = "Švicarska" },
    new OfferDetails { OfferId = 14, Description = "Cirih, finansijski centar i kulturni dragulj Švajcarske.", City = "Cirih", Country = "Švicarska" },
    new OfferDetails { OfferId = 15, Description = "Istanbul, grad na dva kontinenta sa jedinstvenom atmosferom.", City = "Istanbul", Country = "Turska" },
    new OfferDetails { OfferId = 16, Description = "Sarajevo, mesto susreta kultura i istorije.", City = "Sarajevo", Country = "Bosna i Hercegovina" },
    new OfferDetails { OfferId = 17, Description = "Zagreb, moderna metropola sa bogatom tradicijom.", City = "Zagreb", Country = "Hrvatska" },
    new OfferDetails { OfferId = 18, Description = "Beograd, grad sa živahnim noćnim životom i bogatom istorijom.", City = "Beograd", Country = "Srbija" },
    new OfferDetails { OfferId = 19, Description = "Dubrovnik, biser Jadrana i mediteranske lepote.", City = "Dubrovnik", Country = "Hrvatska" },
    new OfferDetails { OfferId = 20, Description = "Split, spoj istorije i modernog šarma uz prelepe plaže.", City = "Split", Country = "Hrvatska" },
    new OfferDetails { OfferId = 21, Description = "Ljubljana, zeleno srce Evrope sa opuštajućom atmosferom.", City = "Ljubljana", Country = "Slovenija" },
    new OfferDetails { OfferId = 22, Description = "Podgorica, nova evropska destinacija puna iznenađenja.", City = "Podgorica", Country = "Crna Gora" },
    new OfferDetails { OfferId = 23, Description = "Tirana, šarmantan grad sa bogatom kulturom i prijateljskom atmosferom.", City = "Tirana", Country = "Albanija" },
    new OfferDetails { OfferId = 24, Description = "Skoplje, spoj starog i novog u srcu Balkana.", City = "Skoplje", Country = "Sjeverna Makedonija" },
    new OfferDetails { OfferId = 25, Description = "Budimpešta, grad termalnih kupališta i veličanstvene arhitekture.", City = "Budimpešta", Country = "Mađarska" },
    new OfferDetails { OfferId = 26, Description = "Brisel, prestonica Evrope sa bogatom istorijom i gastronomijom.", City = "Brisel", Country = "Belgija" },
    new OfferDetails { OfferId = 27, Description = "Varšava, grad koji uspešno spaja istoriju i moderni duh.", City = "Varšava", Country = "Poljska" },
    new OfferDetails { OfferId = 28, Description = "Krakov, biser Poljske sa bogatom kulturnom scenom.", City = "Krakov", Country = "Poljska" },
    new OfferDetails { OfferId = 29, Description = "Sofija, srce Bugarske sa prelepim planinama i istorijom.", City = "Sofija", Country = "Bugarska" },
    new OfferDetails { OfferId = 30, Description = "Bukurešt, grad kontrasta i dinamične kulture.", City = "Bukurešt", Country = "Rumunija" }
);



        builder.Entity<OfferImage>().HasData(
        Enumerable.Range(1, 30).Select(id => new OfferImage
        {
            Id = id,
            OfferId = id,
            ImageUrl = "/images/offer/pariz.jpg"
        }).ToArray()
        );

        builder.Entity<Hotel>().HasData(
    new Hotel { Id = 1, Name = "Hotel Kovačević", Address = "Blaževićeva 404", City = "N/A", Country = "N/A", Stars = 4 },
    new Hotel { Id = 2, Name = "Hotel Vuković", Address = "Potočna 520", City = "N/A", Country = "N/A", Stars = 3 },
    new Hotel { Id = 3, Name = "Hotel Petrović", Address = "Milice Todorović 102", City = "N/A", Country = "N/A", Stars = 5 },
    new Hotel { Id = 4, Name = "Hotel Ilić", Address = "Cara Dušana 77", City = "N/A", Country = "N/A", Stars = 4 },
    new Hotel { Id = 5, Name = "Hotel Stojanović", Address = "Bulevar Kralja Petra 15", City = "N/A", Country = "N/A", Stars = 4 },
    new Hotel { Id = 6, Name = "Hotel Marković", Address = "Svetog Save 88", City = "N/A", Country = "N/A", Stars = 3 },
    new Hotel { Id = 7, Name = "Hotel Jovanović", Address = "Narodnog fronta 25", City = "N/A", Country = "N/A", Stars = 5 },
    new Hotel { Id = 8, Name = "Hotel Nikolić", Address = "Kralja Milana 12", City = "N/A", Country = "N/A", Stars = 4 },
    new Hotel { Id = 9, Name = "Hotel Milošević", Address = "Bulevar Oslobođenja 33", City = "N/A", Country = "N/A", Stars = 3 },
    new Hotel { Id = 10, Name = "Hotel Ristić", Address = "Žarka Zrenjanina 8", City = "N/A", Country = "N/A", Stars = 4 },
    new Hotel { Id = 11, Name = "Hotel Lukić", Address = "Kosovska 40", City = "N/A", Country = "N/A", Stars = 3 },
    new Hotel { Id = 12, Name = "Hotel Savić", Address = "Cara Lazara 77", City = "N/A", Country = "N/A", Stars = 4 },
    new Hotel { Id = 13, Name = "Hotel Milenković", Address = "Ulica Kralja Aleksandra 58", City = "N/A", Country = "N/A", Stars = 5 },
    new Hotel { Id = 14, Name = "Hotel Janković", Address = "Makedonska 91", City = "N/A", Country = "N/A", Stars = 4 },
    new Hotel { Id = 15, Name = "Hotel Pavlović", Address = "Narodnog heroja 120", City = "N/A", Country = "N/A", Stars = 3 },
    new Hotel { Id = 16, Name = "Hotel Todorović", Address = "Bulevar Kralja Aleksandra 19", City = "N/A", Country = "N/A", Stars = 4 },
    new Hotel { Id = 17, Name = "Hotel Božić", Address = "Njegoševa 7", City = "N/A", Country = "N/A", Stars = 5 },
    new Hotel { Id = 18, Name = "Hotel Živanović", Address = "Braće Jerković 14", City = "N/A", Country = "N/A", Stars = 3 },
    new Hotel { Id = 19, Name = "Hotel Miladinović", Address = "Svetozara Markovića 22", City = "N/A", Country = "N/A", Stars = 4 },
    new Hotel { Id = 20, Name = "Hotel Radosavljević", Address = "Kneza Miloša 50", City = "N/A", Country = "N/A", Stars = 4 },
    new Hotel { Id = 21, Name = "Hotel Ćosić", Address = "Bulevar revolucije 65", City = "N/A", Country = "N/A", Stars = 3 },
    new Hotel { Id = 22, Name = "Hotel Stanković", Address = "Vojvode Stepe 33", City = "N/A", Country = "N/A", Stars = 4 },
    new Hotel { Id = 23, Name = "Hotel Perić", Address = "Mileve Marić 45", City = "N/A", Country = "N/A", Stars = 5 },
    new Hotel { Id = 24, Name = "Hotel Radovanović", Address = "Bulevar despota Stefana 14", City = "N/A", Country = "N/A", Stars = 4 },
    new Hotel { Id = 25, Name = "Hotel Novaković", Address = "Gavrila Principa 18", City = "N/A", Country = "N/A", Stars = 3 },
    new Hotel { Id = 26, Name = "Hotel Vasić", Address = "Resavska 12", City = "N/A", Country = "N/A", Stars = 4 },
    new Hotel { Id = 27, Name = "Hotel Tadić", Address = "Njegoševa 90", City = "N/A", Country = "N/A", Stars = 5 },
    new Hotel { Id = 28, Name = "Hotel Milović", Address = "Ulica kralja Petra 66", City = "N/A", Country = "N/A", Stars = 4 },
    new Hotel { Id = 29, Name = "Hotel Rakić", Address = "Kraljice Marije 30", City = "N/A", Country = "N/A", Stars = 3 },
    new Hotel { Id = 30, Name = "Hotel Jović", Address = "Terazije 55", City = "N/A", Country = "N/A", Stars = 4 },
    new Hotel { Id = 31, Name = "Hotel Milić", Address = "Kneza Ljubomira 14", City = "N/A", Country = "N/A", Stars = 3 },
    new Hotel { Id = 32, Name = "Hotel Đorđević", Address = "Bulevar kralja Aleksandra 11", City = "N/A", Country = "N/A", Stars = 4 },
    new Hotel { Id = 33, Name = "Hotel Karanović", Address = "Cara Dušana 99", City = "N/A", Country = "N/A", Stars = 5 },
    new Hotel { Id = 34, Name = "Hotel Radulović", Address = "Ulica Marije Bursać 40", City = "N/A", Country = "N/A", Stars = 4 },
    new Hotel { Id = 35, Name = "Hotel Filipović", Address = "Nikole Pašića 12", City = "N/A", Country = "N/A", Stars = 3 },
    new Hotel { Id = 36, Name = "Hotel Stankov", Address = "Bulevar oslobođenja 32", City = "N/A", Country = "N/A", Stars = 4 },
    new Hotel { Id = 37, Name = "Hotel Sokolović", Address = "Kralja Petra 44", City = "N/A", Country = "N/A", Stars = 5 },
    new Hotel { Id = 38, Name = "Hotel Popović", Address = "Ulica kralja Milana 8", City = "N/A", Country = "N/A", Stars = 4 },
    new Hotel { Id = 39, Name = "Hotel Vučić", Address = "Bulevar Kralja Petra 6", City = "N/A", Country = "N/A", Stars = 3 },
    new Hotel { Id = 40, Name = "Hotel Jankov", Address = "Nikole Tesle 15", City = "N/A", Country = "N/A", Stars = 4 },
    new Hotel { Id = 41, Name = "Hotel Zorić", Address = "Kneza Mihaila 19", City = "N/A", Country = "N/A", Stars = 5 },
    new Hotel { Id = 42, Name = "Hotel Dragić", Address = "Mileve Marić 27", City = "N/A", Country = "N/A", Stars = 4 },
    new Hotel { Id = 43, Name = "Hotel Tomašević", Address = "Bulevar oslobođenja 50", City = "N/A", Country = "N/A", Stars = 3 },
    new Hotel { Id = 44, Name = "Hotel Mijatović", Address = "Kralja Aleksandra 22", City = "N/A", Country = "N/A", Stars = 4 },
    new Hotel { Id = 45, Name = "Hotel Filipović", Address = "Ulica Kralja Petra 31", City = "N/A", Country = "N/A", Stars = 5 },
    new Hotel { Id = 46, Name = "Hotel Radović", Address = "Narodnog fronta 18", City = "N/A", Country = "N/A", Stars = 4 },
    new Hotel { Id = 47, Name = "Hotel Đukić", Address = "Bulevar Kralja Petra 14", City = "N/A", Country = "N/A", Stars = 3 },
    new Hotel { Id = 48, Name = "Hotel Popović", Address = "Cara Dušana 7", City = "N/A", Country = "N/A", Stars = 4 },
    new Hotel { Id = 49, Name = "Hotel Marinković", Address = "Kneza Miloša 5", City = "N/A", Country = "N/A", Stars = 5 },
    new Hotel { Id = 50, Name = "Hotel Kostić", Address = "Bulevar Oslobođenja 16", City = "N/A", Country = "N/A", Stars = 4 },
    new Hotel { Id = 51, Name = "Hotel Milutinović", Address = "Resavska 2", City = "N/A", Country = "N/A", Stars = 3 },
    new Hotel { Id = 52, Name = "Hotel Radosavljević", Address = "Narodnog heroja 38", City = "N/A", Country = "N/A", Stars = 4 },
    new Hotel { Id = 53, Name = "Hotel Ilić", Address = "Ulica Vuka Karadžića 14", City = "N/A", Country = "N/A", Stars = 5 },
    new Hotel { Id = 54, Name = "Hotel Novak", Address = "Bulevar Oslobođenja 50", City = "N/A", Country = "N/A", Stars = 4 },
    new Hotel { Id = 55, Name = "Hotel Đorđević", Address = "Kralja Petra 66", City = "N/A", Country = "N/A", Stars = 3 },
    new Hotel { Id = 56, Name = "Hotel Jović", Address = "Njegoševa 11", City = "N/A", Country = "N/A", Stars = 4 },
    new Hotel { Id = 57, Name = "Hotel Stevanović", Address = "Bulevar kralja Aleksandra 88", City = "N/A", Country = "N/A", Stars = 5 },
    new Hotel { Id = 58, Name = "Hotel Mandić", Address = "Ulica Kralja Petra 3", City = "N/A", Country = "N/A", Stars = 4 },
    new Hotel { Id = 59, Name = "Hotel Bošnjak", Address = "Narodnog fronta 17", City = "N/A", Country = "N/A", Stars = 3 },
    new Hotel { Id = 60, Name = "Hotel Radovanović", Address = "Bulevar oslobođenja 43", City = "N/A", Country = "N/A", Stars = 4 },
    new Hotel { Id = 61, Name = "Hotel Pavlović", Address = "Kneza Miloša 18", City = "N/A", Country = "N/A", Stars = 5 },
    new Hotel { Id = 62, Name = "Hotel Ilić", Address = "Ulica Kralja Petra 7", City = "N/A", Country = "N/A", Stars = 4 },
    new Hotel { Id = 63, Name = "Hotel Živković", Address = "Bulevar Kralja Petra 40", City = "N/A", Country = "N/A", Stars = 3 },
    new Hotel { Id = 64, Name = "Hotel Janković", Address = "Narodnog heroja 23", City = "N/A", Country = "N/A", Stars = 4 },
    new Hotel { Id = 65, Name = "Hotel Marković", Address = "Kralja Milana 50", City = "N/A", Country = "N/A", Stars = 5 },
    new Hotel { Id = 66, Name = "Hotel Savić", Address = "Ulica Kralja Petra 28", City = "N/A", Country = "N/A", Stars = 4 },
    new Hotel { Id = 67, Name = "Hotel Stojanović", Address = "Bulevar oslobođenja 29", City = "N/A", Country = "N/A", Stars = 3 },
    new Hotel { Id = 68, Name = "Hotel Milosević", Address = "Narodnog fronta 11", City = "N/A", Country = "N/A", Stars = 4 },
    new Hotel { Id = 69, Name = "Hotel Ristić", Address = "Kneza Miloša 22", City = "N/A", Country = "N/A", Stars = 5 },
    new Hotel { Id = 70, Name = "Hotel Ilić", Address = "Ulica Kralja Petra 1", City = "N/A", Country = "N/A", Stars = 4 }
);

        builder.Entity<HotelImages>().HasData(
            new HotelImages { Id = 1, HotelId = 1, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 2, HotelId = 2, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 3, HotelId = 3, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 4, HotelId = 4, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 5, HotelId = 5, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 6, HotelId = 6, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 7, HotelId = 7, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 8, HotelId = 8, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 9, HotelId = 9, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 10, HotelId = 10, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 11, HotelId = 11, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 12, HotelId = 12, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 13, HotelId = 13, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 14, HotelId = 14, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 15, HotelId = 15, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 16, HotelId = 16, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 17, HotelId = 17, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 18, HotelId = 18, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 19, HotelId = 19, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 20, HotelId = 20, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 21, HotelId = 21, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 22, HotelId = 22, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 23, HotelId = 23, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 24, HotelId = 24, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 25, HotelId = 25, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 26, HotelId = 26, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 27, HotelId = 27, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 28, HotelId = 28, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 29, HotelId = 29, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 30, HotelId = 30, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 31, HotelId = 31, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 32, HotelId = 32, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 33, HotelId = 33, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 34, HotelId = 34, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 35, HotelId = 35, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 36, HotelId = 36, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 37, HotelId = 37, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 38, HotelId = 38, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 39, HotelId = 39, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 40, HotelId = 40, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 41, HotelId = 41, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 42, HotelId = 42, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 43, HotelId = 43, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 44, HotelId = 44, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 45, HotelId = 45, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 46, HotelId = 46, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 47, HotelId = 47, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 48, HotelId = 48, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 49, HotelId = 49, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 50, HotelId = 50, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 51, HotelId = 51, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 52, HotelId = 52, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 53, HotelId = 53, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 54, HotelId = 54, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 55, HotelId = 55, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 56, HotelId = 56, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 57, HotelId = 57, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 58, HotelId = 58, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 59, HotelId = 59, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 60, HotelId = 60, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 61, HotelId = 61, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 62, HotelId = 62, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 63, HotelId = 63, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 64, HotelId = 64, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 65, HotelId = 65, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 66, HotelId = 66, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 67, HotelId = 67, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 68, HotelId = 68, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 69, HotelId = 69, ImageUrl = "/images/hotels/room.jpg" },
            new HotelImages { Id = 70, HotelId = 70, ImageUrl = "/images/hotels/room.jpg" }
        );

        builder.Entity<OfferHotels>().HasData(
    new OfferHotels { OfferDetailsId = 1, HotelId = 1 },
    new OfferHotels { OfferDetailsId = 1, HotelId = 2 },
    new OfferHotels { OfferDetailsId = 2, HotelId = 3 },
    new OfferHotels { OfferDetailsId = 2, HotelId = 4 },
    new OfferHotels { OfferDetailsId = 3, HotelId = 5 },
    new OfferHotels { OfferDetailsId = 3, HotelId = 6 },
    new OfferHotels { OfferDetailsId = 4, HotelId = 7 },
    new OfferHotels { OfferDetailsId = 5, HotelId = 8 },
    new OfferHotels { OfferDetailsId = 5, HotelId = 9 },
    new OfferHotels { OfferDetailsId = 6, HotelId = 10 },
    new OfferHotels { OfferDetailsId = 7, HotelId = 11 },
    new OfferHotels { OfferDetailsId = 8, HotelId = 12 },
    new OfferHotels { OfferDetailsId = 9, HotelId = 13 },
    new OfferHotels { OfferDetailsId = 10, HotelId = 14 },
    new OfferHotels { OfferDetailsId = 11, HotelId = 15 },
    new OfferHotels { OfferDetailsId = 12, HotelId = 16 },
    new OfferHotels { OfferDetailsId = 13, HotelId = 17 },
    new OfferHotels { OfferDetailsId = 14, HotelId = 18 },
    new OfferHotels { OfferDetailsId = 15, HotelId = 19 },
    new OfferHotels { OfferDetailsId = 16, HotelId = 20 }
);


        builder.Entity<Rooms>().HasData(
            new Rooms { Id = 1, RoomType = "Dvokrevetna" },
            new Rooms { Id = 2, RoomType = "Trokrevetna" },
            new Rooms { Id = 3, RoomType = "Jednokrevetna" },
            new Rooms { Id = 4, RoomType = "Apartman" },
            new Rooms { Id = 5, RoomType = "Porodična soba" }
        );


        builder.Entity<HotelRooms>().HasData(
    new HotelRooms { Id = 1, HotelId = 1, RoomId = 1, RoomsLeft = 10 },
    new HotelRooms { Id = 2, HotelId = 1, RoomId = 2, RoomsLeft = 5 },
    new HotelRooms { Id = 3, HotelId = 2, RoomId = 1, RoomsLeft = 8 },
    new HotelRooms { Id = 4, HotelId = 2, RoomId = 3, RoomsLeft = 4 },
    new HotelRooms { Id = 5, HotelId = 3, RoomId = 2, RoomsLeft = 6 },
    new HotelRooms { Id = 6, HotelId = 3, RoomId = 3, RoomsLeft = 3 },
    new HotelRooms { Id = 7, HotelId = 4, RoomId = 1, RoomsLeft = 7 },
    new HotelRooms { Id = 8, HotelId = 4, RoomId = 2, RoomsLeft = 2 }
    // Dodaj dalje po isti princip do željenog broja hotela
    );
    }
}





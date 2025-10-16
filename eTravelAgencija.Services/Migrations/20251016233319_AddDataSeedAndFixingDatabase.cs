using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eTravelAgencija.Services.Migrations
{
    /// <inheritdoc />
    public partial class AddDataSeedAndFixingDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false),
                    Stars = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfferCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoomType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HotelImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HotelId = table.Column<int>(type: "integer", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HotelImages_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OfferSubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferSubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferSubCategories_OfferCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "OfferCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotelRooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HotelId = table.Column<int>(type: "integer", nullable: false),
                    RoomId = table.Column<int>(type: "integer", nullable: false),
                    RoomsLeft = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelRooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HotelRooms_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HotelRooms_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    DaysInTotal = table.Column<int>(type: "integer", nullable: false),
                    WayOfTravel = table.Column<string>(type: "text", nullable: false),
                    SubCategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offers_OfferSubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "OfferSubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OfferDetails",
                columns: table => new
                {
                    OfferId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferDetails", x => x.OfferId);
                    table.ForeignKey(
                        name: "FK_OfferDetails_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OfferHotels",
                columns: table => new
                {
                    OfferDetailsId = table.Column<int>(type: "integer", nullable: false),
                    HotelId = table.Column<int>(type: "integer", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferHotels", x => new { x.OfferDetailsId, x.HotelId, x.DepartureDate, x.ReturnDate });
                    table.ForeignKey(
                        name: "FK_OfferHotels_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferHotels_OfferDetails_OfferDetailsId",
                        column: x => x.OfferDetailsId,
                        principalTable: "OfferDetails",
                        principalColumn: "OfferId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OfferImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OfferId = table.Column<int>(type: "integer", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferImages_OfferDetails_OfferId",
                        column: x => x.OfferId,
                        principalTable: "OfferDetails",
                        principalColumn: "OfferId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OfferPlanDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OfferDetailsId = table.Column<int>(type: "integer", nullable: false),
                    DayNumber = table.Column<int>(type: "integer", nullable: false),
                    DayTitle = table.Column<string>(type: "text", nullable: false),
                    DayDescription = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferPlanDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferPlanDays_OfferDetails_OfferDetailsId",
                        column: x => x.OfferDetailsId,
                        principalTable: "OfferDetails",
                        principalColumn: "OfferId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "Address", "City", "Country", "Name", "Stars" },
                values: new object[,]
                {
                    { 1, "Blaževićeva 404", "N/A", "N/A", "Hotel Kovačević", 4 },
                    { 2, "Potočna 520", "N/A", "N/A", "Hotel Vuković", 3 },
                    { 3, "Milice Todorović 102", "N/A", "N/A", "Hotel Petrović", 5 },
                    { 4, "Cara Dušana 77", "N/A", "N/A", "Hotel Ilić", 4 },
                    { 5, "Bulevar Kralja Petra 15", "N/A", "N/A", "Hotel Stojanović", 4 },
                    { 6, "Svetog Save 88", "N/A", "N/A", "Hotel Marković", 3 },
                    { 7, "Narodnog fronta 25", "N/A", "N/A", "Hotel Jovanović", 5 },
                    { 8, "Kralja Milana 12", "N/A", "N/A", "Hotel Nikolić", 4 },
                    { 9, "Bulevar Oslobođenja 33", "N/A", "N/A", "Hotel Milošević", 3 },
                    { 10, "Žarka Zrenjanina 8", "N/A", "N/A", "Hotel Ristić", 4 },
                    { 11, "Kosovska 40", "N/A", "N/A", "Hotel Lukić", 3 },
                    { 12, "Cara Lazara 77", "N/A", "N/A", "Hotel Savić", 4 },
                    { 13, "Ulica Kralja Aleksandra 58", "N/A", "N/A", "Hotel Milenković", 5 },
                    { 14, "Makedonska 91", "N/A", "N/A", "Hotel Janković", 4 },
                    { 15, "Narodnog heroja 120", "N/A", "N/A", "Hotel Pavlović", 3 },
                    { 16, "Bulevar Kralja Aleksandra 19", "N/A", "N/A", "Hotel Todorović", 4 },
                    { 17, "Njegoševa 7", "N/A", "N/A", "Hotel Božić", 5 },
                    { 18, "Braće Jerković 14", "N/A", "N/A", "Hotel Živanović", 3 },
                    { 19, "Svetozara Markovića 22", "N/A", "N/A", "Hotel Miladinović", 4 },
                    { 20, "Kneza Miloša 50", "N/A", "N/A", "Hotel Radosavljević", 4 },
                    { 21, "Bulevar revolucije 65", "N/A", "N/A", "Hotel Ćosić", 3 },
                    { 22, "Vojvode Stepe 33", "N/A", "N/A", "Hotel Stanković", 4 },
                    { 23, "Mileve Marić 45", "N/A", "N/A", "Hotel Perić", 5 },
                    { 24, "Bulevar despota Stefana 14", "N/A", "N/A", "Hotel Radovanović", 4 },
                    { 25, "Gavrila Principa 18", "N/A", "N/A", "Hotel Novaković", 3 },
                    { 26, "Resavska 12", "N/A", "N/A", "Hotel Vasić", 4 },
                    { 27, "Njegoševa 90", "N/A", "N/A", "Hotel Tadić", 5 },
                    { 28, "Ulica kralja Petra 66", "N/A", "N/A", "Hotel Milović", 4 },
                    { 29, "Kraljice Marije 30", "N/A", "N/A", "Hotel Rakić", 3 },
                    { 30, "Terazije 55", "N/A", "N/A", "Hotel Jović", 4 },
                    { 31, "Kneza Ljubomira 14", "N/A", "N/A", "Hotel Milić", 3 },
                    { 32, "Bulevar kralja Aleksandra 11", "N/A", "N/A", "Hotel Đorđević", 4 },
                    { 33, "Cara Dušana 99", "N/A", "N/A", "Hotel Karanović", 5 },
                    { 34, "Ulica Marije Bursać 40", "N/A", "N/A", "Hotel Radulović", 4 },
                    { 35, "Nikole Pašića 12", "N/A", "N/A", "Hotel Filipović", 3 },
                    { 36, "Bulevar oslobođenja 32", "N/A", "N/A", "Hotel Stankov", 4 },
                    { 37, "Kralja Petra 44", "N/A", "N/A", "Hotel Sokolović", 5 },
                    { 38, "Ulica kralja Milana 8", "N/A", "N/A", "Hotel Popović", 4 },
                    { 39, "Bulevar Kralja Petra 6", "N/A", "N/A", "Hotel Vučić", 3 },
                    { 40, "Nikole Tesle 15", "N/A", "N/A", "Hotel Jankov", 4 },
                    { 41, "Kneza Mihaila 19", "N/A", "N/A", "Hotel Zorić", 5 },
                    { 42, "Mileve Marić 27", "N/A", "N/A", "Hotel Dragić", 4 },
                    { 43, "Bulevar oslobođenja 50", "N/A", "N/A", "Hotel Tomašević", 3 },
                    { 44, "Kralja Aleksandra 22", "N/A", "N/A", "Hotel Mijatović", 4 },
                    { 45, "Ulica Kralja Petra 31", "N/A", "N/A", "Hotel Filipović", 5 },
                    { 46, "Narodnog fronta 18", "N/A", "N/A", "Hotel Radović", 4 },
                    { 47, "Bulevar Kralja Petra 14", "N/A", "N/A", "Hotel Đukić", 3 },
                    { 48, "Cara Dušana 7", "N/A", "N/A", "Hotel Popović", 4 },
                    { 49, "Kneza Miloša 5", "N/A", "N/A", "Hotel Marinković", 5 },
                    { 50, "Bulevar Oslobođenja 16", "N/A", "N/A", "Hotel Kostić", 4 },
                    { 51, "Resavska 2", "N/A", "N/A", "Hotel Milutinović", 3 },
                    { 52, "Narodnog heroja 38", "N/A", "N/A", "Hotel Radosavljević", 4 },
                    { 53, "Ulica Vuka Karadžića 14", "N/A", "N/A", "Hotel Ilić", 5 },
                    { 54, "Bulevar Oslobođenja 50", "N/A", "N/A", "Hotel Novak", 4 },
                    { 55, "Kralja Petra 66", "N/A", "N/A", "Hotel Đorđević", 3 },
                    { 56, "Njegoševa 11", "N/A", "N/A", "Hotel Jović", 4 },
                    { 57, "Bulevar kralja Aleksandra 88", "N/A", "N/A", "Hotel Stevanović", 5 },
                    { 58, "Ulica Kralja Petra 3", "N/A", "N/A", "Hotel Mandić", 4 },
                    { 59, "Narodnog fronta 17", "N/A", "N/A", "Hotel Bošnjak", 3 },
                    { 60, "Bulevar oslobođenja 43", "N/A", "N/A", "Hotel Radovanović", 4 },
                    { 61, "Kneza Miloša 18", "N/A", "N/A", "Hotel Pavlović", 5 },
                    { 62, "Ulica Kralja Petra 7", "N/A", "N/A", "Hotel Ilić", 4 },
                    { 63, "Bulevar Kralja Petra 40", "N/A", "N/A", "Hotel Živković", 3 },
                    { 64, "Narodnog heroja 23", "N/A", "N/A", "Hotel Janković", 4 },
                    { 65, "Kralja Milana 50", "N/A", "N/A", "Hotel Marković", 5 },
                    { 66, "Ulica Kralja Petra 28", "N/A", "N/A", "Hotel Savić", 4 },
                    { 67, "Bulevar oslobođenja 29", "N/A", "N/A", "Hotel Stojanović", 3 },
                    { 68, "Narodnog fronta 11", "N/A", "N/A", "Hotel Milosević", 4 },
                    { 69, "Kneza Miloša 22", "N/A", "N/A", "Hotel Ristić", 5 },
                    { 70, "Ulica Kralja Petra 1", "N/A", "N/A", "Hotel Ilić", 4 }
                });

            migrationBuilder.InsertData(
                table: "OfferCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Praznična putovanja" },
                    { 2, "Specijalna putovanja" },
                    { 3, "Osjetite mjesec" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "RoomType" },
                values: new object[,]
                {
                    { 1, "Dvokrevetna" },
                    { 2, "Trokrevetna" },
                    { 3, "Jednokrevetna" },
                    { 4, "Apartman" },
                    { 5, "Porodična soba" }
                });

            migrationBuilder.InsertData(
                table: "HotelImages",
                columns: new[] { "Id", "HotelId", "ImageUrl" },
                values: new object[,]
                {
                    { 1, 1, "/images/hotels/room.jpg" },
                    { 2, 2, "/images/hotels/room.jpg" },
                    { 3, 3, "/images/hotels/room.jpg" },
                    { 4, 4, "/images/hotels/room.jpg" },
                    { 5, 5, "/images/hotels/room.jpg" },
                    { 6, 6, "/images/hotels/room.jpg" },
                    { 7, 7, "/images/hotels/room.jpg" },
                    { 8, 8, "/images/hotels/room.jpg" },
                    { 9, 9, "/images/hotels/room.jpg" },
                    { 10, 10, "/images/hotels/room.jpg" },
                    { 11, 11, "/images/hotels/room.jpg" },
                    { 12, 12, "/images/hotels/room.jpg" },
                    { 13, 13, "/images/hotels/room.jpg" },
                    { 14, 14, "/images/hotels/room.jpg" },
                    { 15, 15, "/images/hotels/room.jpg" },
                    { 16, 16, "/images/hotels/room.jpg" },
                    { 17, 17, "/images/hotels/room.jpg" },
                    { 18, 18, "/images/hotels/room.jpg" },
                    { 19, 19, "/images/hotels/room.jpg" },
                    { 20, 20, "/images/hotels/room.jpg" },
                    { 21, 21, "/images/hotels/room.jpg" },
                    { 22, 22, "/images/hotels/room.jpg" },
                    { 23, 23, "/images/hotels/room.jpg" },
                    { 24, 24, "/images/hotels/room.jpg" },
                    { 25, 25, "/images/hotels/room.jpg" },
                    { 26, 26, "/images/hotels/room.jpg" },
                    { 27, 27, "/images/hotels/room.jpg" },
                    { 28, 28, "/images/hotels/room.jpg" },
                    { 29, 29, "/images/hotels/room.jpg" },
                    { 30, 30, "/images/hotels/room.jpg" },
                    { 31, 31, "/images/hotels/room.jpg" },
                    { 32, 32, "/images/hotels/room.jpg" },
                    { 33, 33, "/images/hotels/room.jpg" },
                    { 34, 34, "/images/hotels/room.jpg" },
                    { 35, 35, "/images/hotels/room.jpg" },
                    { 36, 36, "/images/hotels/room.jpg" },
                    { 37, 37, "/images/hotels/room.jpg" },
                    { 38, 38, "/images/hotels/room.jpg" },
                    { 39, 39, "/images/hotels/room.jpg" },
                    { 40, 40, "/images/hotels/room.jpg" },
                    { 41, 41, "/images/hotels/room.jpg" },
                    { 42, 42, "/images/hotels/room.jpg" },
                    { 43, 43, "/images/hotels/room.jpg" },
                    { 44, 44, "/images/hotels/room.jpg" },
                    { 45, 45, "/images/hotels/room.jpg" },
                    { 46, 46, "/images/hotels/room.jpg" },
                    { 47, 47, "/images/hotels/room.jpg" },
                    { 48, 48, "/images/hotels/room.jpg" },
                    { 49, 49, "/images/hotels/room.jpg" },
                    { 50, 50, "/images/hotels/room.jpg" },
                    { 51, 51, "/images/hotels/room.jpg" },
                    { 52, 52, "/images/hotels/room.jpg" },
                    { 53, 53, "/images/hotels/room.jpg" },
                    { 54, 54, "/images/hotels/room.jpg" },
                    { 55, 55, "/images/hotels/room.jpg" },
                    { 56, 56, "/images/hotels/room.jpg" },
                    { 57, 57, "/images/hotels/room.jpg" },
                    { 58, 58, "/images/hotels/room.jpg" },
                    { 59, 59, "/images/hotels/room.jpg" },
                    { 60, 60, "/images/hotels/room.jpg" },
                    { 61, 61, "/images/hotels/room.jpg" },
                    { 62, 62, "/images/hotels/room.jpg" },
                    { 63, 63, "/images/hotels/room.jpg" },
                    { 64, 64, "/images/hotels/room.jpg" },
                    { 65, 65, "/images/hotels/room.jpg" },
                    { 66, 66, "/images/hotels/room.jpg" },
                    { 67, 67, "/images/hotels/room.jpg" },
                    { 68, 68, "/images/hotels/room.jpg" },
                    { 69, 69, "/images/hotels/room.jpg" },
                    { 70, 70, "/images/hotels/room.jpg" }
                });

            migrationBuilder.InsertData(
                table: "HotelRooms",
                columns: new[] { "Id", "HotelId", "RoomId", "RoomsLeft" },
                values: new object[,]
                {
                    { 1, 1, 1, 10 },
                    { 2, 1, 2, 5 },
                    { 3, 2, 1, 8 },
                    { 4, 2, 3, 4 },
                    { 5, 3, 2, 6 },
                    { 6, 3, 3, 3 },
                    { 7, 4, 1, 7 },
                    { 8, 4, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "OfferSubCategories",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Božić" },
                    { 2, 1, "Bajram" },
                    { 3, 1, "Prvi maj" },
                    { 4, 3, "Januar" },
                    { 5, 3, "Februar" },
                    { 6, 3, "Mart" },
                    { 7, 3, "April" },
                    { 8, 3, "Maj" },
                    { 9, 3, "Juni" },
                    { 10, 3, "Juli" },
                    { 11, 3, "August" },
                    { 12, 3, "Septembar" },
                    { 13, 3, "Oktobar" },
                    { 14, 3, "Novembar" },
                    { 15, 3, "Decembar" }
                });

            migrationBuilder.InsertData(
                table: "Offers",
                columns: new[] { "Id", "DaysInTotal", "Price", "SubCategoryId", "Title", "WayOfTravel" },
                values: new object[,]
                {
                    { 1, 7, 850m, 1, "Putovanje u Pariz", "Avion" },
                    { 2, 6, 760m, 2, "Putovanje u Rim", "Autobus" },
                    { 3, 8, 920m, 3, "Putovanje u Madrid", "Avion" },
                    { 4, 4, 540m, 4, "Putovanje u Beč", "Autobus" },
                    { 5, 9, 970m, 5, "Putovanje u Atina", "Avion" },
                    { 6, 6, 1010m, 6, "Putovanje u Amsterdam", "Avion" },
                    { 7, 7, 890m, 7, "Putovanje u Lisabon", "Autobus" },
                    { 8, 5, 730m, 8, "Putovanje u Berlin", "Avion" },
                    { 9, 6, 810m, 9, "Putovanje u Prag", "Avion" },
                    { 10, 10, 1340m, 10, "Putovanje u Kopenhagen", "Avion" },
                    { 11, 9, 1230m, 11, "Putovanje u Oslo", "Avion" },
                    { 12, 8, 1110m, 12, "Putovanje u Stockholm", "Avion" },
                    { 13, 6, 990m, 1, "Putovanje u Ženeva", "Autobus" },
                    { 14, 7, 970m, 2, "Putovanje u Cirih", "Avion" },
                    { 15, 5, 700m, 3, "Putovanje u Istanbul", "Autobus" },
                    { 16, 3, 430m, 4, "Putovanje u Sarajevo", "Autobus" },
                    { 17, 4, 500m, 5, "Putovanje u Zagreb", "Autobus" },
                    { 18, 4, 520m, 6, "Putovanje u Beograd", "Autobus" },
                    { 19, 6, 840m, 7, "Putovanje u Dubrovnik", "Avion" },
                    { 20, 6, 790m, 8, "Putovanje u Split", "Autobus" },
                    { 21, 5, 680m, 9, "Putovanje u Ljubljana", "Autobus" },
                    { 22, 5, 620m, 10, "Putovanje u Podgorica", "Avion" },
                    { 23, 5, 640m, 11, "Putovanje u Tirana", "Avion" },
                    { 24, 5, 600m, 12, "Putovanje u Skoplje", "Autobus" },
                    { 25, 6, 900m, 1, "Putovanje u Budimpešta", "Avion" },
                    { 26, 7, 1050m, 2, "Putovanje u Brisel", "Avion" },
                    { 27, 6, 970m, 3, "Putovanje u Varšava", "Autobus" },
                    { 28, 6, 960m, 4, "Putovanje u Krakov", "Autobus" },
                    { 29, 6, 880m, 5, "Putovanje u Sofija", "Autobus" },
                    { 30, 7, 910m, 6, "Putovanje u Bukurešt", "Avion" }
                });

            migrationBuilder.InsertData(
                table: "OfferDetails",
                columns: new[] { "OfferId", "City", "Country", "Description" },
                values: new object[,]
                {
                    { 1, "Pariz", "Francuska", "Uživajte u opuštajućem putovanju kroz Pariz sa vrhunskim vodičima i nezaboravnim doživljajima." },
                    { 2, "Rim", "Italija", "Iskusite čari Rima, njegove istorijske znamenitosti i autentičnu kuhinju." },
                    { 3, "Madrid", "Španija", "Otkrijte Madrid, grad prepun kulture, umetnosti i sjajnih pejzaža." },
                    { 4, "Beč", "Austrija", "Posetite Beč, grad muzike, istorije i predivne arhitekture." },
                    { 5, "Atina", "Grčka", "Istražite Atinu i uživajte u drevnoj grčkoj istoriji i prelepim plažama." },
                    { 6, "Amsterdam", "Nizozemska", "Amsterdam vas poziva svojim kanalima, muzejima i opuštenom atmosferom." },
                    { 7, "Lisabon", "Portugal", "Doživite čari Lisabona, njegovu arhitekturu i ukusnu hranu." },
                    { 8, "Berlin", "Njemačka", "Berlin, dinamični grad sa bogatom istorijom i živahnom kulturom." },
                    { 9, "Prag", "Češka", "Prag, grad bajki, mostova i nezaboravnih večeri." },
                    { 10, "Kopenhagen", "Danska", "Kopenhagen – skandinavska oaza sa modernim i tradicionalnim sadržajima." },
                    { 11, "Oslo", "Norveška", "Oslo, prirodna lepota i urbani život na dohvat ruke." },
                    { 12, "Stockholm", "Švedska", "Stockholm, grad na vodi sa bogatom istorijom i prelepim pejzažima." },
                    { 13, "Ženeva", "Švicarska", "Ženeva, srce švajcarskih Alpa i međunarodna prestonica." },
                    { 14, "Cirih", "Švicarska", "Cirih, finansijski centar i kulturni dragulj Švajcarske." },
                    { 15, "Istanbul", "Turska", "Istanbul, grad na dva kontinenta sa jedinstvenom atmosferom." },
                    { 16, "Sarajevo", "Bosna i Hercegovina", "Sarajevo, mesto susreta kultura i istorije." },
                    { 17, "Zagreb", "Hrvatska", "Zagreb, moderna metropola sa bogatom tradicijom." },
                    { 18, "Beograd", "Srbija", "Beograd, grad sa živahnim noćnim životom i bogatom istorijom." },
                    { 19, "Dubrovnik", "Hrvatska", "Dubrovnik, biser Jadrana i mediteranske lepote." },
                    { 20, "Split", "Hrvatska", "Split, spoj istorije i modernog šarma uz prelepe plaže." },
                    { 21, "Ljubljana", "Slovenija", "Ljubljana, zeleno srce Evrope sa opuštajućom atmosferom." },
                    { 22, "Podgorica", "Crna Gora", "Podgorica, nova evropska destinacija puna iznenađenja." },
                    { 23, "Tirana", "Albanija", "Tirana, šarmantan grad sa bogatom kulturom i prijateljskom atmosferom." },
                    { 24, "Skoplje", "Sjeverna Makedonija", "Skoplje, spoj starog i novog u srcu Balkana." },
                    { 25, "Budimpešta", "Mađarska", "Budimpešta, grad termalnih kupališta i veličanstvene arhitekture." },
                    { 26, "Brisel", "Belgija", "Brisel, prestonica Evrope sa bogatom istorijom i gastronomijom." },
                    { 27, "Varšava", "Poljska", "Varšava, grad koji uspešno spaja istoriju i moderni duh." },
                    { 28, "Krakov", "Poljska", "Krakov, biser Poljske sa bogatom kulturnom scenom." },
                    { 29, "Sofija", "Bugarska", "Sofija, srce Bugarske sa prelepim planinama i istorijom." },
                    { 30, "Bukurešt", "Rumunija", "Bukurešt, grad kontrasta i dinamične kulture." }
                });

            migrationBuilder.InsertData(
                table: "OfferHotels",
                columns: new[] { "DepartureDate", "HotelId", "OfferDetailsId", "ReturnDate" },
                values: new object[,]
                {
                    { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, 11, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, 12, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 17, 13, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, 14, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, 15, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, 16, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "OfferImages",
                columns: new[] { "Id", "ImageUrl", "OfferId" },
                values: new object[,]
                {
                    { 1, "/images/offer/pariz.jpg", 1 },
                    { 2, "/images/offer/pariz.jpg", 2 },
                    { 3, "/images/offer/pariz.jpg", 3 },
                    { 4, "/images/offer/pariz.jpg", 4 },
                    { 5, "/images/offer/pariz.jpg", 5 },
                    { 6, "/images/offer/pariz.jpg", 6 },
                    { 7, "/images/offer/pariz.jpg", 7 },
                    { 8, "/images/offer/pariz.jpg", 8 },
                    { 9, "/images/offer/pariz.jpg", 9 },
                    { 10, "/images/offer/pariz.jpg", 10 },
                    { 11, "/images/offer/pariz.jpg", 11 },
                    { 12, "/images/offer/pariz.jpg", 12 },
                    { 13, "/images/offer/pariz.jpg", 13 },
                    { 14, "/images/offer/pariz.jpg", 14 },
                    { 15, "/images/offer/pariz.jpg", 15 },
                    { 16, "/images/offer/pariz.jpg", 16 },
                    { 17, "/images/offer/pariz.jpg", 17 },
                    { 18, "/images/offer/pariz.jpg", 18 },
                    { 19, "/images/offer/pariz.jpg", 19 },
                    { 20, "/images/offer/pariz.jpg", 20 },
                    { 21, "/images/offer/pariz.jpg", 21 },
                    { 22, "/images/offer/pariz.jpg", 22 },
                    { 23, "/images/offer/pariz.jpg", 23 },
                    { 24, "/images/offer/pariz.jpg", 24 },
                    { 25, "/images/offer/pariz.jpg", 25 },
                    { 26, "/images/offer/pariz.jpg", 26 },
                    { 27, "/images/offer/pariz.jpg", 27 },
                    { 28, "/images/offer/pariz.jpg", 28 },
                    { 29, "/images/offer/pariz.jpg", 29 },
                    { 30, "/images/offer/pariz.jpg", 30 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotelImages_HotelId",
                table: "HotelImages",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelRooms_HotelId",
                table: "HotelRooms",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelRooms_RoomId",
                table: "HotelRooms",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferHotels_HotelId",
                table: "OfferHotels",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferImages_OfferId",
                table: "OfferImages",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferPlanDays_OfferDetailsId",
                table: "OfferPlanDays",
                column: "OfferDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_SubCategoryId",
                table: "Offers",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferSubCategories_CategoryId",
                table: "OfferSubCategories",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotelImages");

            migrationBuilder.DropTable(
                name: "HotelRooms");

            migrationBuilder.DropTable(
                name: "OfferHotels");

            migrationBuilder.DropTable(
                name: "OfferImages");

            migrationBuilder.DropTable(
                name: "OfferPlanDays");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "OfferDetails");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "OfferSubCategories");

            migrationBuilder.DropTable(
                name: "OfferCategories");
        }
    }
}

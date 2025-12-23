using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eTravelAgencija.Services.Migrations
{
    /// <inheritdoc />
    public partial class mainFixingDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    DateBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    isBlocked = table.Column<bool>(type: "boolean", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Stars = table.Column<int>(type: "integer", nullable: false),
                    CalculatedPrice = table.Column<decimal>(type: "numeric", nullable: false)
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
                name: "Rate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    OrderNumber = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rate", x => x.Id);
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
                name: "Vouchers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VoucherCode = table.Column<string>(type: "text", nullable: false),
                    Discount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    priceInTokens = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vouchers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Equity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkApplications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CvFileName = table.Column<string>(type: "text", nullable: false),
                    AppliedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    letter = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkApplications_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotelImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HotelId = table.Column<int>(type: "integer", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    IsMain = table.Column<bool>(type: "boolean", nullable: false)
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
                    HotelId = table.Column<int>(type: "integer", nullable: false),
                    RoomId = table.Column<int>(type: "integer", nullable: false),
                    RoomsLeft = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelRooms", x => new { x.HotelId, x.RoomId });
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
                name: "UserVouchers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    VoucherId = table.Column<int>(type: "integer", nullable: false),
                    isUsed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVouchers", x => new { x.UserId, x.VoucherId });
                    table.ForeignKey(
                        name: "FK_UserVouchers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserVouchers_Vouchers_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "Vouchers",
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
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userId = table.Column<int>(type: "integer", nullable: false),
                    offerId = table.Column<int>(type: "integer", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: false),
                    starRate = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Offers_offerId",
                        column: x => x.offerId,
                        principalTable: "Offers",
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
                    City = table.Column<string>(type: "text", nullable: false),
                    TotalCountOfReservations = table.Column<int>(type: "integer", nullable: false),
                    MinimalPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ResidenceTaxPerDay = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    TravelInsuranceTotal = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ResidenceTotal = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
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
                    table.PrimaryKey("PK_OfferHotels", x => new { x.OfferDetailsId, x.HotelId });
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
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    isMain = table.Column<bool>(type: "boolean", nullable: false)
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
                    OfferDetailsId = table.Column<int>(type: "integer", nullable: false),
                    DayNumber = table.Column<int>(type: "integer", nullable: false),
                    DayTitle = table.Column<string>(type: "text", nullable: false),
                    DayDescription = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferPlanDays", x => new { x.OfferDetailsId, x.DayNumber });
                    table.ForeignKey(
                        name: "FK_OfferPlanDays_OfferDetails_OfferDetailsId",
                        column: x => x.OfferDetailsId,
                        principalTable: "OfferDetails",
                        principalColumn: "OfferId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    OfferId = table.Column<int>(type: "integer", nullable: false),
                    HotelId = table.Column<int>(type: "integer", nullable: false),
                    RoomId = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IncludeInsurance = table.Column<bool>(type: "boolean", nullable: false),
                    isFirstRatePaid = table.Column<bool>(type: "boolean", nullable: false),
                    isFullPaid = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PriceLeftToPay = table.Column<decimal>(type: "numeric", nullable: false),
                    addedNeeds = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_OfferDetails_OfferId",
                        column: x => x.OfferId,
                        principalTable: "OfferDetails",
                        principalColumn: "OfferId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "integer", nullable: false),
                    RateId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PaymentMethod = table.Column<string>(type: "text", nullable: false),
                    PaymentDeadline = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeadlineExtended = table.Column<bool>(type: "boolean", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => new { x.ReservationId, x.RateId });
                    table.ForeignKey(
                        name: "FK_Payments_Rate_RateId",
                        column: x => x.RateId,
                        principalTable: "Rate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, null, "Osnovna korisnička rola", "Korisnik", "KORISNIK" },
                    { 2, null, "Zaposleni koji upravlja ponudama i rezervacijama", "Radnik", "RADNIK" },
                    { 3, null, "Administrator sistema", "Direktor", "DIREKTOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "DateBirth", "Email", "EmailConfirmed", "FirstName", "ImageUrl", "LastLoginAt", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "isBlocked" },
                values: new object[,]
                {
                    { 1, 0, "c069849e-f9dd-431d-86f5-05ab70beba8d", new DateTime(2025, 12, 22, 18, 29, 15, 508, DateTimeKind.Utc).AddTicks(9658), new DateTime(1990, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), "radnik@etravel.com", true, "Marko", "test.jpg", null, "Radnik", false, null, "RADNIK@ETRAVEL.COM", "RADNIK", "AQAAAAIAAYagAAAAEAfg4af6Q8D7B80KpNxJhEr27PRM9888PbO0P10RxEVMRpie1knJYMTqGC5AGY3kww==", "+38761111111", false, null, false, "radnik", false },
                    { 2, 0, "30899324-2d0c-4612-bc2a-a272c7392d5a", new DateTime(2025, 12, 22, 18, 29, 15, 594, DateTimeKind.Utc).AddTicks(9873), new DateTime(1985, 3, 20, 0, 0, 0, 0, DateTimeKind.Utc), "direktor@etravel.com", true, "Amir", "test.jpg", null, "Direktor", false, null, "DIREKTOR@ETRAVEL.COM", "DIREKTOR", "AQAAAAIAAYagAAAAEKTDCGYIDWKJ7ld5Gk5Oe3WGHXwr609gVtf7j99NzK69/fnpAKkN2lw13iT1nn8Ocg==", "+38762222222", false, null, false, "direktor", false },
                    { 4, 0, "0aa13be6-5333-4b5e-a983-2fac1746bdf6", new DateTime(2025, 12, 22, 18, 29, 15, 662, DateTimeKind.Utc).AddTicks(270), new DateTime(2002, 11, 5, 0, 0, 0, 0, DateTimeKind.Utc), "korisnik@etravel.com", true, "Ajdin", "test.jpg", null, "Korisnik", false, null, "KORISNIK@ETRAVEL.COM", "KORISNIK", "AQAAAAIAAYagAAAAEJ8pDlcEKosPltQagHcfB7NR73D2gK9YhjeQxQb9d0walVueViGW/PKejrPDPzAM8w==", "+38763333333", false, null, false, "korisnik", false },
                    { 5, 0, "e532d22d-d4c5-4132-92b3-c298dd9f8592", new DateTime(2025, 12, 22, 18, 29, 15, 729, DateTimeKind.Utc).AddTicks(7404), new DateTime(1998, 4, 12, 0, 0, 0, 0, DateTimeKind.Utc), "maja.petrovic@etravel.com", true, "Maja", "test.jpg", null, "Petrović", false, null, "MAJA.PETROVIC@ETRAVEL.COM", "MAJA.PETROVIC", "AQAAAAIAAYagAAAAEHQCul9wi3PBNWQlTdrNqCj6ty+IgAwhmDpgqBk/bGP39JjHNVrgw/60UUK+gVMzJQ==", "+38761555111", false, null, false, "maja.petrovic", false },
                    { 6, 0, "666d0569-ed04-43d8-9bfc-855adaa42aaa", new DateTime(2025, 12, 22, 18, 29, 15, 797, DateTimeKind.Utc).AddTicks(8918), new DateTime(1995, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "edin.mesic@etravel.com", true, "Edin", "test.jpg", null, "Mešić", false, null, "EDIN.MESIC@ETRAVEL.COM", "EDIN.MESIC", "AQAAAAIAAYagAAAAEEzMw2jVp+UrNAnh6J0OxQcRCFy3qxt/PL0neFNqH/+NB63Kt6YbI44ieZVi5RUpoA==", "+38761666123", false, null, false, "edin.mesic", false },
                    { 7, 0, "e7430115-865e-4c4f-ada6-9946eeb38845", new DateTime(2025, 12, 22, 18, 29, 15, 870, DateTimeKind.Utc).AddTicks(7148), new DateTime(2000, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "lana.kovac@etravel.com", true, "Lana", "test.jpg", null, "Kovač", false, null, "LANA.KOVAC@ETRAVEL.COM", "LANA.KOVAC", "AQAAAAIAAYagAAAAEC0WPJNpb2ncMJteZjZ2rdEsgQJ1wQZR2yb/mQezI0na5cE7U6BfDducVYgGpokI0A==", "+38761777141", false, null, false, "lana.kovac", false },
                    { 8, 0, "a52624b0-5a57-48fb-b08e-cfd26b0463d0", new DateTime(2025, 12, 22, 18, 29, 15, 951, DateTimeKind.Utc).AddTicks(3786), new DateTime(1993, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), "haris.becirovic@etravel.com", true, "Haris", "test.jpg", null, "Bećirović", false, null, "HARIS.BECIROVIC@ETRAVEL.COM", "HARIS.BECIROVIC", "AQAAAAIAAYagAAAAEO4iM3cvqDR4sevwraVHUrD4PZ3LURYDMy5QXHUNdP50/wquenjrE8bcVtLmlwyjBw==", "+38761888222", false, null, false, "haris.becirovic", false },
                    { 9, 0, "1c4e9f0a-f8e0-494a-ac2e-6296972a7072", new DateTime(2025, 12, 22, 18, 29, 16, 56, DateTimeKind.Utc).AddTicks(9221), new DateTime(1999, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "amira.karic@etravel.com", true, "Amira", "test.jpg", null, "Karić", false, null, "AMIRA.KARIC@ETRAVEL.COM", "AMIRA.KARIC", "AQAAAAIAAYagAAAAEAxozcirE9e5mlLBbxjAo62xubifPElX2mBUZNMFM0cBwiVfbftT3fg2dDTW0n4HSg==", "+38761999444", false, null, false, "amira.karic", false },
                    { 10, 0, "3dfdc0da-cfc5-4eca-99e5-f070d073e27d", new DateTime(2025, 12, 22, 18, 29, 16, 124, DateTimeKind.Utc).AddTicks(6593), new DateTime(1997, 5, 19, 0, 0, 0, 0, DateTimeKind.Utc), "tarik.suljic@etravel.com", true, "Tarik", "test.jpg", null, "Suljić", false, null, "TARIK.SULJIC@ETRAVEL.COM", "TARIK.SULJIC", "AQAAAAIAAYagAAAAEMgFqaShPqhZW3rXRNDXwyuncNyA1GHMf1vU9TlE3L/GYmKxq9zxv2KhP5NkOcSiMQ==", "+38762011223", false, null, false, "tarik.suljic", false },
                    { 11, 0, "94180b6a-1fdb-47c5-86c7-97f862fa2948", new DateTime(2025, 12, 22, 18, 29, 16, 194, DateTimeKind.Utc).AddTicks(5882), new DateTime(2001, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "selma.babic@etravel.com", true, "Selma", "test.jpg", null, "Babić", false, null, "SELMA.BABIC@ETRAVEL.COM", "SELMA.BABIC", "AQAAAAIAAYagAAAAEDTuKMsqLM0OAu/Ls8ht8MXNGHFkW3krRycF1LavdnyJ3KByOIhpNT6SX4UDHYnN8g==", "+38762044321", false, null, false, "selma.babic", false },
                    { 12, 0, "92d31bab-151d-4c0f-b8cc-61eed433e9ab", new DateTime(2025, 12, 22, 18, 29, 16, 272, DateTimeKind.Utc).AddTicks(6746), new DateTime(1994, 12, 2, 0, 0, 0, 0, DateTimeKind.Utc), "nedim.ceric@etravel.com", true, "Nedim", "test.jpg", null, "Ćerić", false, null, "NEDIM.CERIC@ETRAVEL.COM", "NEDIM.CERIC", "AQAAAAIAAYagAAAAEFBaQ3TgFMjfgTDF7RCwC6dvVlzv8MhBNe7AbQPvPW5EVYleUjVYdEqDs82Ud0Wz1g==", "+38762077311", false, null, false, "nedim.ceric", false },
                    { 13, 0, "92512a16-b9f0-46c5-bb03-0474a05a3f9f", new DateTime(2025, 12, 22, 18, 29, 16, 379, DateTimeKind.Utc).AddTicks(6479), new DateTime(1996, 11, 9, 0, 0, 0, 0, DateTimeKind.Utc), "alma.vujic@etravel.com", true, "Alma", "test.jpg", null, "Vujić", false, null, "ALMA.VUJIC@ETRAVEL.COM", "ALMA.VUJIC", "AQAAAAIAAYagAAAAEBjRaeH95zxNsvYHUctUXmf2Q0Qu6o2EhmmuGl5cxs8W0oBjf9DEUIgidjtETJieNQ==", "+38762111333", false, null, false, "alma.vujic", false },
                    { 14, 0, "6d72df08-b234-4865-a29a-e1fd6367b67f", new DateTime(2025, 12, 22, 18, 29, 16, 460, DateTimeKind.Utc).AddTicks(2730), new DateTime(1992, 7, 4, 0, 0, 0, 0, DateTimeKind.Utc), "mirza.drace@etravel.com", true, "Mirza", "test.jpg", null, "DračE", false, null, "MIRZA.DRACE@ETRAVEL.COM", "MIRZA.DRACE", "AQAAAAIAAYagAAAAEJaUqmug7jMKDnNv06bktAmyUKaB5hW6QJnJSbMcWg05uTmIQAFlricMKET6sUmGeQ==", "+38762144555", false, null, false, "mirza.drace", false },
                    { 15, 0, "5e213739-74cd-4653-92e4-9b3dae1fd406", new DateTime(2025, 12, 22, 18, 29, 16, 561, DateTimeKind.Utc).AddTicks(5308), new DateTime(2000, 6, 17, 0, 0, 0, 0, DateTimeKind.Utc), "melisa.nuhanovic@etravel.com", true, "Melisa", "test.jpg", null, "Nuhanović", false, null, "MELISA.NUHANOVIC@ETRAVEL.COM", "MELISA.NUHANOVIC", "AQAAAAIAAYagAAAAEHIFwL2Jtqo3AKdkWXZ//6nRl6w5CkA71/NMaWL4jdiRNxM8Ijw01YTRKVIcWYdsAw==", "+38762200333", false, null, false, "melisa.nuhanovic", false },
                    { 16, 0, "a170f0f5-9787-4abd-87c3-10cc3d38c9b6", new DateTime(2025, 12, 22, 18, 29, 16, 642, DateTimeKind.Utc).AddTicks(7469), new DateTime(1991, 3, 29, 0, 0, 0, 0, DateTimeKind.Utc), "almin.kosuta@etravel.com", true, "Almin", "test.jpg", null, "Košuta", false, null, "ALMIN.KOSUTA@ETRAVEL.COM", "ALMIN.KOSUTA", "AQAAAAIAAYagAAAAEOVSD5tAmoE2pqQuIaklHWyZoJ7TKl1j8iJa1Ce11H7+m+nF8oQuE4IFkhoZT6kn7Q==", "+38762255677", false, null, false, "almin.kosuta", false },
                    { 17, 0, "80149eb2-7eac-4edf-983d-929942135b95", new DateTime(2025, 12, 22, 18, 29, 16, 719, DateTimeKind.Utc).AddTicks(3184), new DateTime(1998, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), "dina.hodzic@etravel.com", true, "Dina", "test.jpg", null, "Hodžić", false, null, "DINA.HODZIC@ETRAVEL.COM", "DINA.HODZIC", "AQAAAAIAAYagAAAAEPfoFJGiNrHhSpxOf5zb7DCujghE7qXSHa6usFvcIJFvYVTSnZIyahgSAToXqZ2xKA==", "+38762277991", false, null, false, "dina.hodzic", false },
                    { 18, 0, "93b8fea5-a835-4bcd-9341-1ddde003517b", new DateTime(2025, 12, 22, 18, 29, 16, 779, DateTimeKind.Utc).AddTicks(8079), new DateTime(1997, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "alem.celik@etravel.com", true, "Alem", "test.jpg", null, "Čelik", false, null, "ALEM.CELIK@ETRAVEL.COM", "ALEM.CELIK", "AQAAAAIAAYagAAAAEF1g/32XWQu2vIBKKpIGIIJrZqFl96j9wmscfgr/fmzZgJafRbYiX+3XWJ6tWwuxmQ==", "+38762300990", false, null, false, "alem.celik", false },
                    { 19, 0, "bd94269c-d4ee-4a48-8558-d697d2becca4", new DateTime(2025, 12, 22, 18, 29, 16, 844, DateTimeKind.Utc).AddTicks(237), new DateTime(2001, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "lejla.avdic@etravel.com", true, "Lejla", "test.jpg", null, "Avdić", false, null, "LEJLA.AVDIC@ETRAVEL.COM", "LEJLA.AVDIC", "AQAAAAIAAYagAAAAEPVvHw0EH33efGnHeSI9CJRKQZAYJJu7dyoO/tMTYIZUQLJ2OzQhB3usq0a4M9va0w==", "+38762355123", false, null, false, "lejla.avdic", false },
                    { 20, 0, "1e3ba736-eb4b-4d1c-b45d-eefe31f46450", new DateTime(2025, 12, 22, 18, 29, 16, 909, DateTimeKind.Utc).AddTicks(7319), new DateTime(1999, 9, 3, 0, 0, 0, 0, DateTimeKind.Utc), "adnan.pasalic@etravel.com", true, "Adnan", "test.jpg", null, "Pašalić", false, null, "ADNAN.PASALIC@ETRAVEL.COM", "ADNAN.PASALIC", "AQAAAAIAAYagAAAAEBpDYAKtxbxQ0Jgi3Hw8Dmsd/wJi+gWE/zfcTIZz6kf0YV0xpZxTAE5qplNzonvqvg==", "+38762388321", false, null, false, "adnan.pasalic", false },
                    { 21, 0, "f3fee1c0-ac96-4999-a58e-c66981e1bcc4", new DateTime(2025, 12, 22, 18, 29, 16, 975, DateTimeKind.Utc).AddTicks(2303), new DateTime(1996, 4, 14, 0, 0, 0, 0, DateTimeKind.Utc), "inez.kantic@etravel.com", true, "Inez", "test.jpg", null, "Kantić", false, null, "INEZ.KANTIC@ETRAVEL.COM", "INEZ.KANTIC", "AQAAAAIAAYagAAAAEHyKSswvUebAWq9WaNEDlGDlUOlh6c2FANRXZfVYQcuf5WqtjNo45H93MPJtIGAGrQ==", "+38762444123", false, null, false, "inez.kantic", false },
                    { 22, 0, "b9c0b974-fa90-42c4-a8a0-7b12d44e14d6", new DateTime(2025, 12, 22, 18, 29, 17, 51, DateTimeKind.Utc).AddTicks(8212), new DateTime(1993, 11, 19, 0, 0, 0, 0, DateTimeKind.Utc), "amir.halilovic@etravel.com", true, "Amir", "test.jpg", null, "Halilović", false, null, "AMIR.HALILOVIC@ETRAVEL.COM", "AMIR.HALILOVIC", "AQAAAAIAAYagAAAAEHwMaDb6SgUs76NKBD9rmmv8/mxlr/6bY+RnK5wHi4c1Mf4IOjwhLp5OdH40elIGVg==", "+38762477331", false, null, false, "amir.halilovic", false },
                    { 23, 0, "035e94d9-c584-4961-8452-62e62e3b1623", new DateTime(2025, 12, 22, 18, 29, 17, 125, DateTimeKind.Utc).AddTicks(1155), new DateTime(2002, 12, 21, 0, 0, 0, 0, DateTimeKind.Utc), "lamija.kreso@etravel.com", true, "Lamija", "test.jpg", null, "Krešo", false, null, "LAMIJA.KRESO@ETRAVEL.COM", "LAMIJA.KRESO", "AQAAAAIAAYagAAAAEACMMOz8X7gXqt16SIPPvvfl5375aRtqsE0BSJCb/ImeIBqxQQ2EcdeU8VijZ//zbA==", "+38762555991", false, null, false, "lamija.kreso", false },
                    { 24, 0, "65c87448-94de-445c-81cf-c6d1be9b37a0", new DateTime(2025, 12, 22, 18, 29, 17, 189, DateTimeKind.Utc).AddTicks(5155), new DateTime(1998, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), "omer.smajic@etravel.com", true, "Omer", "test.jpg", null, "Smajić", false, null, "OMER.SMAJIC@ETRAVEL.COM", "OMER.SMAJIC", "AQAAAAIAAYagAAAAEPzOMcisKjdBY43jIB0HXSx+p6vmtJgbznDMkAJ8gkQYYF7jGj54ki1KtG2EVSv7xA==", "+38762666112", false, null, false, "omer.smajic", false }
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "Address", "CalculatedPrice", "Name", "Stars" },
                values: new object[,]
                {
                    { 100, "Via Roma 12", 0m, "Hotel Medici", 4 },
                    { 101, "Piazza Duomo 2", 0m, "Hotel Firenze Centro", 3 },
                    { 102, "Ponte Vecchio 5", 0m, "Hotel Ponte Vecchio", 5 },
                    { 110, "Santorini Beach 9", 0m, "Blue Dome Resort", 5 },
                    { 111, "Oia Street 44", 0m, "Aegean View", 4 },
                    { 112, "Fira 21", 0m, "White Cave Hotel", 3 },
                    { 120, "Sultanahmet 1", 0m, "Hotel Sultanahmet", 4 },
                    { 121, "Galata 5", 0m, "Galata Inn", 3 },
                    { 122, "Bosfor Blvd 7", 0m, "Bosfor Palace Hotel", 5 },
                    { 200, "La Rambla 12", 0m, "Hotel Condal Barcelona", 3 },
                    { 201, "Rue de Rivoli 99", 0m, "Hotel Louvre Rivoli", 4 },
                    { 202, "Mostecká 7", 0m, "Hotel Charles Bridge Inn", 4 },
                    { 203, "Frankenberggasse 10", 0m, "Hotel Kaiserhof Wien", 4 },
                    { 204, "Keizersgracht 84", 0m, "Hotel Amsterdam Canal View", 5 },
                    { 205, "Victoria Street 22", 0m, "Hotel Westminster London", 4 },
                    { 206, "Dubai Marina Walk 5", 0m, "Dubai Marina Pearl Hotel", 5 },
                    { 207, "Pyramid Street 18", 0m, "Cairo Pyramids View Hotel", 4 },
                    { 208, "Váci Utca 33", 0m, "Budapest Royal Center Hotel", 3 },
                    { 209, "Floriańska 15", 0m, "Krakow Old Town Plaza Hotel", 4 },
                    { 210, "Kendwa Beach 1", 0m, "Zanzibar Blue Lagoon Resort", 5 },
                    { 211, "Sheraton Road 55", 0m, "Hurghada Golden Sand Resort", 5 },
                    { 212, "Rua dos Remédios 21", 0m, "Lisbon Alfama Boutique Hotel", 4 },
                    { 213, "Dionysiou Areopagitou 8", 0m, "Acropolis View Hotel Athens", 3 },
                    { 214, "Obala Hrvatskog Narodnog 17", 0m, "Hotel Adriatic Split", 4 }
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
                table: "Rate",
                columns: new[] { "Id", "Name", "OrderNumber" },
                values: new object[,]
                {
                    { 1, "Prva rata", 1 },
                    { 2, "Druga rata", 2 },
                    { 3, "Treća rata", 3 },
                    { 4, "Puni iznos", 0 },
                    { 5, "Preostali iznos", 0 }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "RoomType" },
                values: new object[,]
                {
                    { 1, "Dvokrevetna" },
                    { 2, "Trokrevetna" },
                    { 3, "Petokrevetna" },
                    { 4, "Cetverokrevetna" }
                });

            migrationBuilder.InsertData(
                table: "Vouchers",
                columns: new[] { "Id", "Discount", "VoucherCode", "priceInTokens" },
                values: new object[,]
                {
                    { 1, 0.20m, "ETRAVEL20", 40 },
                    { 2, 0.50m, "ETRAVEL50", 80 },
                    { 3, 0.70m, "ETRAVEL70", 120 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId", "CreatedAt", "Description", "IsActive" },
                values: new object[,]
                {
                    { 2, 1, new DateTime(2025, 12, 22, 18, 29, 17, 256, DateTimeKind.Utc).AddTicks(5213), "", true },
                    { 3, 2, new DateTime(2025, 12, 22, 18, 29, 17, 256, DateTimeKind.Utc).AddTicks(5225), "", true },
                    { 1, 4, new DateTime(2025, 12, 22, 18, 29, 17, 256, DateTimeKind.Utc).AddTicks(5292), "", true },
                    { 1, 5, new DateTime(2025, 12, 22, 18, 29, 17, 256, DateTimeKind.Utc).AddTicks(5293), "", true },
                    { 1, 6, new DateTime(2025, 12, 22, 18, 29, 17, 256, DateTimeKind.Utc).AddTicks(5293), "", true },
                    { 1, 7, new DateTime(2025, 12, 22, 18, 29, 17, 256, DateTimeKind.Utc).AddTicks(5294), "", true },
                    { 1, 8, new DateTime(2025, 12, 22, 18, 29, 17, 256, DateTimeKind.Utc).AddTicks(5296), "", true },
                    { 1, 9, new DateTime(2025, 12, 22, 18, 29, 17, 256, DateTimeKind.Utc).AddTicks(5296), "", true },
                    { 1, 10, new DateTime(2025, 12, 22, 18, 29, 17, 256, DateTimeKind.Utc).AddTicks(5297), "", true },
                    { 1, 11, new DateTime(2025, 12, 22, 18, 29, 17, 256, DateTimeKind.Utc).AddTicks(5298), "", true },
                    { 1, 12, new DateTime(2025, 12, 22, 18, 29, 17, 256, DateTimeKind.Utc).AddTicks(5299), "", true },
                    { 1, 13, new DateTime(2025, 12, 22, 18, 29, 17, 256, DateTimeKind.Utc).AddTicks(5300), "", true },
                    { 1, 14, new DateTime(2025, 12, 22, 18, 29, 17, 256, DateTimeKind.Utc).AddTicks(5301), "", true },
                    { 1, 15, new DateTime(2025, 12, 22, 18, 29, 17, 256, DateTimeKind.Utc).AddTicks(5301), "", true },
                    { 1, 16, new DateTime(2025, 12, 22, 18, 29, 17, 256, DateTimeKind.Utc).AddTicks(5302), "", true },
                    { 1, 17, new DateTime(2025, 12, 22, 18, 29, 17, 256, DateTimeKind.Utc).AddTicks(5303), "", true },
                    { 1, 18, new DateTime(2025, 12, 22, 18, 29, 17, 256, DateTimeKind.Utc).AddTicks(5304), "", true },
                    { 1, 19, new DateTime(2025, 12, 22, 18, 29, 17, 256, DateTimeKind.Utc).AddTicks(5305), "", true },
                    { 1, 20, new DateTime(2025, 12, 22, 18, 29, 17, 256, DateTimeKind.Utc).AddTicks(5306), "", true },
                    { 1, 21, new DateTime(2025, 12, 22, 18, 29, 17, 256, DateTimeKind.Utc).AddTicks(5307), "", true },
                    { 1, 22, new DateTime(2025, 12, 22, 18, 29, 17, 256, DateTimeKind.Utc).AddTicks(5307), "", true },
                    { 1, 23, new DateTime(2025, 12, 22, 18, 29, 17, 256, DateTimeKind.Utc).AddTicks(5308), "", true },
                    { 1, 24, new DateTime(2025, 12, 22, 18, 29, 17, 256, DateTimeKind.Utc).AddTicks(5309), "", true }
                });

            migrationBuilder.InsertData(
                table: "HotelImages",
                columns: new[] { "Id", "HotelId", "ImageUrl", "IsMain" },
                values: new object[,]
                {
                    { 1, 100, "hotel100_main.jpg", true },
                    { 2, 100, "hotel100_1.jpg", false },
                    { 3, 100, "hotel100_2.jpg", false },
                    { 4, 100, "hotel100_3.jpg", false },
                    { 5, 101, "hotel101_main.jpg", true },
                    { 6, 101, "hotel101_1.jpg", false },
                    { 7, 101, "hotel101_2.jpg", false },
                    { 8, 101, "hotel101_3.jpg", false },
                    { 9, 102, "hotel102_main.jpg", true },
                    { 10, 102, "hotel102_1.jpg", false },
                    { 11, 102, "hotel102_2.jpg", false },
                    { 12, 102, "hotel102_3.jpg", false },
                    { 13, 110, "hotel110_main.jpg", true },
                    { 14, 110, "hotel110_1.jpg", false },
                    { 15, 110, "hotel110_2.jpg", false },
                    { 16, 110, "hotel110_3.jpg", false },
                    { 17, 111, "hotel111_main.jpg", true },
                    { 18, 111, "hotel111_1.jpg", false },
                    { 19, 111, "hotel111_2.jpg", false },
                    { 20, 111, "hotel111_3.jpg", false },
                    { 21, 112, "hotel112_main.jpg", true },
                    { 22, 112, "hotel112_1.jpg", false },
                    { 23, 112, "hotel112_2.jpg", false },
                    { 24, 112, "hotel112_3.jpg", false },
                    { 25, 120, "hotel120_main.jpg", true },
                    { 26, 120, "hotel120_1.jpg", false },
                    { 27, 120, "hotel120_2.jpg", false },
                    { 28, 120, "hotel120_3.jpg", false },
                    { 29, 121, "hotel121_main.jpg", true },
                    { 30, 121, "hotel121_1.jpg", false },
                    { 31, 121, "hotel121_2.jpg", false },
                    { 32, 121, "hotel121_3.jpg", false },
                    { 33, 122, "hotel122_main.jpg", true },
                    { 34, 122, "hotel122_1.jpg", false },
                    { 35, 122, "hotel122_2.jpg", false },
                    { 36, 122, "hotel122_3.jpg", false },
                    { 6000, 200, "primjer.jpg", true },
                    { 6001, 201, "primjer.jpg", true },
                    { 6002, 202, "primjer.jpg", true },
                    { 6003, 203, "primjer.jpg", true },
                    { 6004, 204, "primjer.jpg", true },
                    { 6005, 205, "primjer.jpg", true },
                    { 6006, 206, "primjer.jpg", true },
                    { 6007, 207, "primjer.jpg", true },
                    { 6008, 208, "primjer.jpg", true },
                    { 6009, 209, "primjer.jpg", true },
                    { 6010, 210, "primjer.jpg", true },
                    { 6011, 211, "primjer.jpg", true },
                    { 6012, 212, "primjer.jpg", true },
                    { 6013, 213, "primjer.jpg", true },
                    { 6014, 214, "primjer.jpg", true }
                });

            migrationBuilder.InsertData(
                table: "HotelRooms",
                columns: new[] { "HotelId", "RoomId", "RoomsLeft" },
                values: new object[,]
                {
                    { 100, 1, 10 },
                    { 100, 2, 6 },
                    { 100, 3, 4 },
                    { 100, 4, 8 },
                    { 101, 1, 10 },
                    { 101, 2, 6 },
                    { 101, 3, 4 },
                    { 101, 4, 8 },
                    { 102, 1, 10 },
                    { 102, 2, 6 },
                    { 102, 3, 4 },
                    { 102, 4, 8 },
                    { 110, 1, 10 },
                    { 110, 2, 6 },
                    { 110, 3, 4 },
                    { 110, 4, 8 },
                    { 111, 1, 10 },
                    { 111, 2, 6 },
                    { 111, 3, 4 },
                    { 111, 4, 8 },
                    { 112, 1, 10 },
                    { 112, 2, 6 },
                    { 112, 3, 4 },
                    { 112, 4, 8 },
                    { 120, 1, 10 },
                    { 120, 2, 6 },
                    { 120, 3, 4 },
                    { 120, 4, 8 },
                    { 121, 1, 10 },
                    { 121, 2, 6 },
                    { 121, 3, 4 },
                    { 121, 4, 8 },
                    { 122, 1, 10 },
                    { 122, 2, 6 },
                    { 122, 3, 4 },
                    { 122, 4, 8 }
                });

            migrationBuilder.InsertData(
                table: "OfferSubCategories",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { -1, 2, "Bez podkategorije" },
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
                table: "UserTokens",
                columns: new[] { "UserId", "Equity" },
                values: new object[,]
                {
                    { 4, 120 },
                    { 5, 340 },
                    { 6, 75 },
                    { 7, 210 },
                    { 8, 500 },
                    { 9, 160 },
                    { 10, 90 },
                    { 11, 430 },
                    { 12, 60 },
                    { 13, 280 },
                    { 14, 150 },
                    { 15, 390 },
                    { 16, 45 },
                    { 17, 310 },
                    { 18, 200 },
                    { 19, 470 },
                    { 20, 130 },
                    { 21, 260 },
                    { 22, 80 },
                    { 23, 350 },
                    { 24, 170 }
                });

            migrationBuilder.InsertData(
                table: "UserVouchers",
                columns: new[] { "UserId", "VoucherId", "isUsed" },
                values: new object[,]
                {
                    { 5, 1, false },
                    { 5, 2, true },
                    { 6, 1, false },
                    { 7, 3, false },
                    { 8, 2, true },
                    { 9, 1, false },
                    { 9, 3, true },
                    { 10, 2, false },
                    { 11, 1, false },
                    { 12, 3, false }
                });

            migrationBuilder.InsertData(
                table: "WorkApplications",
                columns: new[] { "Id", "AppliedAt", "CvFileName", "UserId", "letter" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 12, 20, 18, 29, 17, 257, DateTimeKind.Utc).AddTicks(468), "test.pdf", 4, "Smisleno me motiviše vaša moderna organizacija i želja da radim na projektima koji imaju stvarni utjecaj. Vjerujem da mogu doprinijeti svojim radnim navikama i voljom za učenjem." },
                    { 2, new DateTime(2025, 12, 18, 18, 29, 17, 257, DateTimeKind.Utc).AddTicks(480), "test.pdf", 5, "Privukla me prilika da radim u dinamičnom okruženju gdje se cijeni timski rad i napredak. Želim biti dio profesionalne i pozitivne radne zajednice." },
                    { 3, new DateTime(2025, 12, 15, 18, 29, 17, 257, DateTimeKind.Utc).AddTicks(482), "test.pdf", 6, "Vaša kompanija je prepoznata po kvalitetnom radu i inovacijama. Smatram da mogu mnogo naučiti i istovremeno doprinijeti svojim iskustvom i posvećenošću." },
                    { 4, new DateTime(2025, 12, 12, 18, 29, 17, 257, DateTimeKind.Utc).AddTicks(484), "test.pdf", 7, "Želim raditi u sredini koja podstiče razvoj i podržava kreativnost. Vaša firma upravo to nudi, i zato bih voljela biti dio vašeg tima." },
                    { 5, new DateTime(2025, 12, 10, 18, 29, 17, 257, DateTimeKind.Utc).AddTicks(485), "test.pdf", 8, "Vašu kompaniju vidim kao mjesto gdje se talenat i rad cijene. Motivisan sam da se usavršavam i doprinosim vašem rastu." },
                    { 6, new DateTime(2025, 12, 8, 18, 29, 17, 257, DateTimeKind.Utc).AddTicks(486), "test.pdf", 9, "Tražim priliku da radim u profesionalnoj sredini gdje mogu napredovati. Posebno me privlači vaša organizacijska kultura i pristup radu." },
                    { 7, new DateTime(2025, 12, 5, 18, 29, 17, 257, DateTimeKind.Utc).AddTicks(488), "test.pdf", 10, "Motiviše me želja da učim nove tehnologije i doprinesem timskim rezultatima. Vjerujem da bih se dobro uklopio u vaše okruženje." },
                    { 8, new DateTime(2025, 12, 4, 18, 29, 17, 257, DateTimeKind.Utc).AddTicks(490), "test.pdf", 11, "Smatram da je vaša kompanija idealno mjesto za profesionalni rast. Cijenim vaš pristup organizaciji i inovativnosti." },
                    { 9, new DateTime(2025, 12, 2, 18, 29, 17, 257, DateTimeKind.Utc).AddTicks(491), "test.pdf", 12, "Zainteresovana sam za rad kod vas jer nudite stabilno i ugodno radno okruženje u kojem se prepoznaje trud i zalaganje." },
                    { 10, new DateTime(2025, 11, 30, 18, 29, 17, 257, DateTimeKind.Utc).AddTicks(493), "test.pdf", 13, "Želim biti dio tima koji teži kvaliteti i rastu. Vaša firma mi djeluje kao pravo mjesto za to." },
                    { 11, new DateTime(2025, 11, 29, 18, 29, 17, 257, DateTimeKind.Utc).AddTicks(494), "test.pdf", 14, "Motivisan sam mogućnošću da radim u kompaniji koja cijeni profesionalizam i timski rad. Spreman sam da dam svoj maksimum." },
                    { 12, new DateTime(2025, 11, 28, 18, 29, 17, 257, DateTimeKind.Utc).AddTicks(495), "test.pdf", 15, "Privlači me prilika da radim sa stručnim i kreativnim timom. Vaš način rada me posebno inspiriše." },
                    { 13, new DateTime(2025, 11, 27, 18, 29, 17, 257, DateTimeKind.Utc).AddTicks(497), "test.pdf", 16, "Vaša kompanija nudi odlične mogućnosti za rast i razvoj, što je glavni razlog moje prijave." },
                    { 14, new DateTime(2025, 11, 26, 18, 29, 17, 257, DateTimeKind.Utc).AddTicks(498), "test.pdf", 17, "Motivisan sam da učim, radim i napredujem. Vjerujem da bih bio odličan dodatak vašem timu." },
                    { 15, new DateTime(2025, 11, 25, 18, 29, 17, 257, DateTimeKind.Utc).AddTicks(501), "test.pdf", 18, "Želim raditi u okruženju gdje se cijeni inicijativa, kreativnost i kvalitetan rad. Vaša firma ispunjava sve te kriterije." },
                    { 16, new DateTime(2025, 11, 24, 18, 29, 17, 257, DateTimeKind.Utc).AddTicks(502), "test.pdf", 19, "Vidim veliku vrijednost u vašim projektima i načinu rada. Želim biti dio tima koji radi sa strašću." },
                    { 17, new DateTime(2025, 11, 23, 18, 29, 17, 257, DateTimeKind.Utc).AddTicks(503), "test.pdf", 20, "Prijavljujem se jer vjerujem da bih u vašoj kompaniji mogao postići veliki profesionalni iskorak." },
                    { 18, new DateTime(2025, 11, 22, 18, 29, 17, 257, DateTimeKind.Utc).AddTicks(504), "test.pdf", 21, "Motiviše me želja da radim u stabilnoj i ozbiljnoj organizaciji koja nudi perspektivu i razvoj." },
                    { 19, new DateTime(2025, 11, 21, 18, 29, 17, 257, DateTimeKind.Utc).AddTicks(506), "test.pdf", 22, "Vaša kompanija mi djeluje kao pravo mjesto da pokažem svoje vještine i dodatno ih unaprijedim." },
                    { 20, new DateTime(2025, 11, 20, 18, 29, 17, 257, DateTimeKind.Utc).AddTicks(507), "test.pdf", 23, "Privlači me vaš profesionalan pristup, moderna organizacija i atmosfera koja podstiče učenje." },
                    { 21, new DateTime(2025, 11, 19, 18, 29, 17, 257, DateTimeKind.Utc).AddTicks(508), "test.pdf", 24, "Motivisana sam da radim u vašoj firmi jer cijenim vaše vrijednosti i način poslovanja. Vjerujem da bih se idealno uklopila." }
                });

            migrationBuilder.InsertData(
                table: "Offers",
                columns: new[] { "Id", "DaysInTotal", "SubCategoryId", "Title", "WayOfTravel" },
                values: new object[,]
                {
                    { 1, 5, 1, "Firenca", "Autobus" },
                    { 2, 7, 1, "Santorini", "Avion" },
                    { 3, 4, 1, "Istanbul", "Autobus" },
                    { 4, 6, 7, "Barcelona", "Avion" },
                    { 5, 5, 3, "Pariz", "Avion" },
                    { 6, 4, 12, "Prag", "Autobus" },
                    { 7, 3, 9, "Beč", "Autobus" },
                    { 8, 5, 2, "Amsterdam", "Avion" },
                    { 9, 6, 15, "London", "Avion" },
                    { 10, 7, 10, "Dubai", "Avion" },
                    { 11, 5, 6, "Kairo", "Avion" },
                    { 12, 3, 4, "Budimpešta", "Autobus" },
                    { 13, 4, 11, "Krakow", "Autobus" },
                    { 14, 8, 1, "Zanzibar", "Avion" },
                    { 15, 7, 13, "Hurgada", "Avion" },
                    { 16, 6, 8, "Lisabon", "Avion" },
                    { 17, 5, 5, "Atina", "Avion" },
                    { 18, 3, 14, "Split", "Autobus" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "comment", "offerId", "starRate", "userId" },
                values: new object[,]
                {
                    { 1, "Predivno putovanje! Organizacija odlična, vodič fenomenalan.", 1, 5, 4 },
                    { 2, "Sve super osim hotela koji je mogao biti bolji.", 1, 4, 5 },
                    { 3, "Santorini je san! Svaka preporuka, ponovo bih išao.", 2, 5, 6 },
                    { 4, "Lijepo putovanje, ali dosta gužve.", 2, 4, 7 },
                    { 5, "Veoma interesantna tura, vodič je znao sve detalje.", 3, 5, 8 },
                    { 6, "Odlična cijena za ono što se dobije.", 3, 4, 9 },
                    { 7, "Barcelona je bila fantastična! Hotel blizu centra.", 4, 5, 10 },
                    { 8, "Predugo putovanje avionom, ali sve ostalo vrhunski.", 4, 4, 11 },
                    { 9, "Grad svjetlosti je ispunio sva očekivanja!", 5, 5, 12 },
                    { 10, "Skupo, ali vrijedilo je svakog dinara.", 5, 5, 13 },
                    { 11, "Mirno i ugodno putovanje, savršeno za vikend bijeg.", 7, 4, 14 },
                    { 12, "Sve je bilo dobro organizovano.", 7, 4, 15 },
                    { 13, "Dubai je nevjerovatno iskustvo! Top organizacija.", 10, 5, 16 },
                    { 14, "Hotel vrhunski, vodič odličan. Preporučujem!", 10, 5, 17 },
                    { 15, "Lijep grad, ali premalo vremena u slobodnoj zoni.", 12, 3, 18 },
                    { 30, "Uživao sam u svakom trenutku. Posebno mi se dopala posjeta galeriji Uffizi. Preporučujem!", 1, 5, 6 },
                    { 40, "Prelijep grad, ali je bilo dosta gužve. Vodič je bio odličan i puno nam je olakšao obilazak.", 1, 4, 7 },
                    { 50, "Firenca je idealna destinacija za ljubitelje historije i arhitekture. Sve pohvale eTravel timu!", 1, 5, 8 },
                    { 60, "Putovanje je bilo divno, ali mislim da je moglo trajati dan duže. Grad nudi previše toga da se vidi.", 1, 4, 9 },
                    { 70, "Odlična organizacija, lijep hotel i savršeno isplanirana tura. Definitivno ponovo putujem preko vas!", 1, 5, 10 },
                    { 80, "Predivna atmosfera u gradu. Najviše mi se dopao obilazak Duoma. Vrijedi svake marke!", 1, 5, 11 },
                    { 90, "Vrlo profesionalna agencija. Sve je bilo ispoštovano prema planu. Putovanje bez brige!", 1, 4, 12 },
                    { 100, "Iznad očekivanja! Hrana u Firenci je savršena, a vodič izuzetno ljubazan i informisan.", 1, 5, 13 },
                    { 110, "Sve je bilo odlično, ali raspored je bio malo pretrpan. Ipak, uživao sam u obilasku grada.", 1, 3, 14 },
                    { 120, "Najbolje putovanje do sada! Preporučujem svakome ko želi spoj kulture, hrane i dobre organizacije.", 1, 5, 15 },
                    { 130, "Bilo je super, ali mislim da bi bilo bolje da smo imali više slobodnog vremena za samostalno istraživanje.", 1, 4, 16 },
                    { 140, "Savršeno iskustvo! Firenca je čarobna, sve preporuke za ovaj aranžman.", 1, 5, 17 },
                    { 150, "Putovanje je prošlo bez ikakvih problema. Sve na vrijeme, sve tačno onako kako je opisano.", 1, 5, 5 },
                    { 160, "Moja druga posjeta Firenci, ali prvi put preko ove agencije. Organizacija vrhunska!", 1, 5, 6 },
                    { 170, "Uživala sam! Preporučila bih ovo putovanje svima koji žele opuštajuće i edukativno iskustvo.", 1, 5, 7 },
                    { 180, "Sve preporuke! Jedino što bih promijenio jeste dužina pauza tokom obilaska.", 1, 4, 8 },
                    { 190, "Perfektno putovanje, odličan vodič i fenomenalne lokacije. Firenca je nevjerovatna!", 1, 5, 9 },
                    { 200, "Sve je bilo super, ali smještaj bi mogao biti malo bliže centru grada.", 1, 4, 10 },
                    { 210, "Najbolje organizovan aranžman na kojem sam bila. Svaka čast eTravel timu!", 1, 5, 11 }
                });

            migrationBuilder.InsertData(
                table: "OfferDetails",
                columns: new[] { "OfferId", "City", "Country", "Description", "MinimalPrice", "ResidenceTaxPerDay", "ResidenceTotal", "TotalCountOfReservations", "TravelInsuranceTotal" },
                values: new object[,]
                {
                    { 1, "Firenca", "Italija", "Putovanje u prekrasnu Firencu.", 450m, 2.00m, 10m, 98, 15m },
                    { 2, "Santorini", "Grčka", "Santorini – raj na zemlji.", 900m, 3.00m, 21m, 65, 25m },
                    { 3, "Istanbul", "Turska", "Istanbul – čarolija dva kontinenta.", 350m, 1.50m, 6m, 53, 12m },
                    { 4, "Barcelona", "Španija", "Barcelona – vibrantan grad umjetnosti i mora.", 750m, 3.50m, 21m, 91, 20m },
                    { 5, "Pariz", "Francuska", "Pariz – grad svjetlosti i romantike.", 820m, 2.50m, 12.5m, 140, 18m },
                    { 6, "Prag", "Češka", "Prag – grad stotinu tornjeva.", 390m, 1.20m, 4.8m, 67, 10m },
                    { 7, "Beč", "Austrija", "Beč – istorija, umjetnost i kraljevska arhitektura.", 250m, 1.80m, 5.4m, 102, 10m },
                    { 8, "Amsterdam", "Nizozemska", "Amsterdam – kanali, muzeji i jedinstvena atmosfera.", 860m, 3.00m, 15m, 119, 22m },
                    { 9, "London", "Ujedinjeno Kraljevstvo", "London – tradicija i moderna kultura.", 950m, 3.20m, 19.2m, 155, 24m },
                    { 10, "Dubai", "UAE", "Dubai – luksuz, pustinja i avantura.", 1300m, 5.00m, 35m, 174, 30m },
                    { 11, "Kairo", "Egipat", "Kairo – drevne piramide i Nil.", 780m, 2.20m, 11m, 94, 20m },
                    { 12, "Budimpešta", "Mađarska", "Budimpešta – čuvena termalna oaza.", 230m, 1.50m, 4.5m, 88, 9m },
                    { 13, "Krakow", "Poljska", "Krakow – historijski dragulj Poljske.", 310m, 1.40m, 5.6m, 73, 11m },
                    { 14, "Zanzibar", "Tanzanija", "Zanzibar – egzotični raj u Indijskom okeanu.", 1600m, 4.00m, 32m, 61, 35m },
                    { 15, "Hurgada", "Egipat", "Hurgada – idealan izbor za all inclusive odmor.", 1100m, 3.00m, 21m, 108, 25m },
                    { 16, "Lisabon", "Portugal", "Lisabon – šarmantni grad na obali Atlantika.", 780m, 2.50m, 15m, 97, 22m },
                    { 17, "Atina", "Grčka", "Atina – kolijevka civilizacije.", 520m, 2.00m, 10m, 121, 16m },
                    { 18, "Split", "Hrvatska", "Split – uživanje na jadranskoj obali.", 180m, 1.00m, 3m, 112, 8m }
                });

            migrationBuilder.InsertData(
                table: "OfferHotels",
                columns: new[] { "HotelId", "OfferDetailsId", "DepartureDate", "ReturnDate" },
                values: new object[,]
                {
                    { 100, 1, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 101, 1, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 102, 1, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 110, 2, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 111, 2, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 112, 2, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 120, 3, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 121, 3, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 122, 3, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 200, 4, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 201, 5, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 202, 6, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 13, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 203, 7, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 11, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 204, 8, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 11, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 205, 9, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 206, 10, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 207, 11, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 12, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 208, 12, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 209, 13, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 210, 14, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 211, 15, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 212, 16, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 213, 17, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 214, 18, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "OfferImages",
                columns: new[] { "Id", "ImageUrl", "OfferId", "isMain" },
                values: new object[,]
                {
                    { 1, "firenca_main.jpg", 1, true },
                    { 2, "firenca_1.jpg", 1, false },
                    { 3, "firenca_2.jpg", 1, false },
                    { 4, "firenca_3.jpg", 1, false },
                    { 5, "santorini_main.jpg", 2, true },
                    { 6, "santorini_1.jpg", 2, false },
                    { 7, "santorini_2.jpg", 2, false },
                    { 8, "santorini_3.jpg", 2, false },
                    { 9, "istanbul_main.jpg", 3, true },
                    { 10, "istanbul_1.jpg", 3, false },
                    { 11, "istanbul_2.jpg", 3, false },
                    { 12, "istanbul_3.jpg", 3, false },
                    { 1000, "primjer.jpg", 4, true },
                    { 1001, "primjer.jpg", 5, true },
                    { 1002, "primjer.jpg", 6, true },
                    { 1003, "primjer.jpg", 7, true },
                    { 1004, "primjer.jpg", 8, true },
                    { 1005, "primjer.jpg", 9, true },
                    { 1006, "primjer.jpg", 10, true },
                    { 1007, "primjer.jpg", 11, true },
                    { 1008, "primjer.jpg", 12, true },
                    { 1009, "primjer.jpg", 13, true },
                    { 1010, "primjer.jpg", 14, true },
                    { 1011, "primjer.jpg", 15, true },
                    { 1012, "primjer.jpg", 16, true },
                    { 1013, "primjer.jpg", 17, true },
                    { 1014, "primjer.jpg", 18, true }
                });

            migrationBuilder.InsertData(
                table: "OfferPlanDays",
                columns: new[] { "DayNumber", "OfferDetailsId", "DayDescription", "DayTitle" },
                values: new object[,]
                {
                    { 1, 1, "Polazak u ranim jutarnjim satima. Pauze tokom puta. Dolazak u Firencu u poslijepodnevnim satima. Smještaj u hotel i slobodno vrijeme za odmor ili šetnju centrom grada.", "Polazak i dolazak u Firencu" },
                    { 2, 1, "Doručak. Organizovano razgledanje: Katedrala Santa Maria del Fiore, Piazza della Signoria, Palazzo Vecchio i Ponte Vecchio. Popodne slobodno vrijeme za individualno istraživanje i kupovinu suvenira.", "Upoznavanje sa starim gradom" },
                    { 3, 1, "Posjeta čuvenoj galeriji Uffizi – remek-djela renesansnih umjetnika: Botticelli, Michelangelo, Da Vinci. Nakon obilaska, slobodno vrijeme za ručak u lokalnim restoranima i uživanje u talijanskoj kuhinji.", "Galerija Uffizi i slobodno popodne" },
                    { 4, 1, "Mogućnost fakultativnog izleta u Pisu i posjete Krivom tornju. Alternativno, slobodan dan u Firenci za šoping, obilazak muzeja, degustaciju vina ili šetnju slikovitim ulicama.", "Izlet u Pisu ili slobodan dan" },
                    { 5, 1, "Check-out iz hotela i polazak prema kući. Pauze tokom puta. Dolazak u kasnim večernjim satima.", "Povratak kući" },
                    { 1, 2, "Let ili transfer do Santorinija. Smještaj u hotel. Slobodno vrijeme za odmor, kupanje ili večernju šetnju rivom u Firi.", "Dolazak na Santorini" },
                    { 2, 2, "Nakon doručka slijedi obilazak Fire: uske bijele ulice, crkve sa plavim kupolama i prekrasni vidikovci. Popodne slobodno vrijeme za kupovinu ili posjetu lokalnim tavernama.", "Fira – glavni grad ostrva" },
                    { 3, 2, "Prijepodnevno slobodno vrijeme za plažu. U poslijepodnevnim satima odlazak u Oiu – najpoznatije mjesto na Santoriniju. Uživanje u fantastičnom zalasku sunca.", "Oia – najljepši zalazak sunca" },
                    { 4, 2, "Obilazak vulkanskih plaža: Red Beach i Perissa (Black Beach). Slobodno vrijeme za kupanje i sunčanje. Povratak u hotel u večernjim satima.", "Crna i crvena plaža" },
                    { 5, 2, "Izlet brodom do vulkanskog ostrva Nea Kameni, šetnja kraterom i kupanje u toplim termalnim izvorima. Povratak brodom u luku.", "Vulkansko ostrvo i termalni izvori" },
                    { 6, 2, "Dan predviđen za odmor, kupanje ili fakultativne aktivnosti – iznajmljivanje quada, degustacije vina, panoramska vožnja ostrvom.", "Slobodan dan" },
                    { 7, 2, "Odjava iz hotela i povratak kući prema planu putovanja.", "Povratak kući" },
                    { 1, 3, "Dolazak u Istanbul i smještaj u hotel. Slobodno vrijeme za odmor. Uveče mogućnost odlaska na večeru u tradicionalni turski restoran.", "Dolazak u Istanbul" },
                    { 2, 3, "Obilazak najvećih atrakcija: Aja Sofija, Sultanahmet džamija, Hipodrom i Topkapi palata. Popodne slobodno vrijeme ili posjeta Grand Bazaru.", "Stari dio Istanbula – Sultanahmet" },
                    { 3, 3, "Jutarnje krstarenje Bosforom – pogled na palače, mostove i obalu. U popodnevnim satima odlazak na Taksim trg i šetnja Istiklal ulicom.", "Bosfor krstarenje i Taksim" },
                    { 4, 3, "Slobodno vrijeme do polaska. Odjava iz hotela i povratak kući.", "Povratak kući" }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "CreatedAt", "HotelId", "IncludeInsurance", "IsActive", "OfferId", "PriceLeftToPay", "RoomId", "TotalPrice", "UserId", "addedNeeds", "isFirstRatePaid", "isFullPaid" },
                values: new object[,]
                {
                    { 2000, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 100, true, true, 1, 150m, 1, 450m, 5, "Vegetarijanski meni", true, false },
                    { 2001, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 110, false, true, 2, 900m, 2, 900m, 6, "", false, true },
                    { 2002, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 120, true, true, 3, 50m, 1, 350m, 7, "", true, false },
                    { 2003, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 111, false, true, 2, 0m, 3, 900m, 8, "", false, true },
                    { 2004, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 101, true, true, 1, 150m, 2, 450m, 9, "", true, false },
                    { 2005, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 122, false, true, 3, 50m, 1, 350m, 10, "", true, false },
                    { 2006, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 102, true, true, 1, 0m, 4, 450m, 11, "", false, true },
                    { 2007, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 112, true, true, 2, 600m, 1, 900m, 12, "Pristup teretani", true, false },
                    { 2008, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 120, false, true, 3, 0m, 2, 350m, 13, "", false, true },
                    { 2009, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 101, true, true, 1, 150m, 3, 450m, 14, "", true, false },
                    { 2010, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 110, false, true, 2, 600m, 4, 900m, 15, "", true, false },
                    { 2011, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 121, true, true, 3, 50m, 2, 350m, 16, "", true, false },
                    { 2012, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 102, false, true, 1, 150m, 2, 450m, 17, "", true, false },
                    { 2013, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 111, true, true, 2, 0m, 2, 900m, 18, "Bez glutena", false, true },
                    { 2014, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 120, true, true, 3, 50m, 3, 350m, 19, "", true, false },
                    { 2015, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 101, true, true, 1, 0m, 1, 450m, 20, "", false, true },
                    { 2016, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 112, true, true, 2, 600m, 1, 900m, 21, "", true, false },
                    { 2017, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 122, true, true, 3, 50m, 1, 350m, 22, "", true, false },
                    { 2018, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 100, false, true, 1, 150m, 1, 450m, 23, "", true, false },
                    { 2019, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 111, true, true, 2, 0m, 3, 900m, 24, "", false, true },
                    { 3000, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 100, false, true, 1, 0m, 1, 0m, 6, "", true, true },
                    { 3001, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 110, false, true, 2, 0m, 2, 0m, 6, "", true, true },
                    { 3002, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 120, false, true, 3, 0m, 1, 0m, 6, "", true, true }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "RateId", "ReservationId", "Amount", "DeadlineExtended", "IsConfirmed", "PaymentDate", "PaymentDeadline", "PaymentMethod" },
                values: new object[,]
                {
                    { 1, 2000, 100m, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kartica" },
                    { 2, 2000, 200m, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kartica" },
                    { 5, 2000, 150m, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kartica" },
                    { 1, 2001, 100m, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kartica" },
                    { 2, 2001, 200m, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kartica" },
                    { 5, 2001, 600m, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kartica" },
                    { 1, 2002, 100m, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kartica" },
                    { 2, 2002, 200m, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "uplatnica" },
                    { 1, 2003, 100m, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kartica" },
                    { 2, 2003, 200m, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kartica" },
                    { 5, 2003, 600m, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "uplatnica" },
                    { 4, 2004, 450m, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "uplatnica" },
                    { 1, 2005, 100m, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kartica" },
                    { 5, 2005, 400m, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "uplatnica" },
                    { 1, 2006, 100m, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kartica" },
                    { 2, 2006, 200m, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kartica" },
                    { 5, 2006, 150m, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kartica" },
                    { 1, 2007, 100m, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kartica" },
                    { 2, 2007, 200m, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kartica" },
                    { 1, 2008, 100m, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kartica" },
                    { 2, 2008, 200m, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kartica" },
                    { 5, 2008, 50m, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kartica" },
                    { 1, 2009, 100m, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kartica" },
                    { 2, 2009, 200m, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kartica" },
                    { 5, 2009, 150m, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kartica" },
                    { 4, 2010, 900m, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kartica" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_offerId",
                table: "Comments",
                column: "offerId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_userId",
                table: "Comments",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelImages_HotelId",
                table: "HotelImages",
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
                name: "IX_Offers_SubCategoryId",
                table: "Offers",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferSubCategories_CategoryId",
                table: "OfferSubCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_RateId",
                table: "Payments",
                column: "RateId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_HotelId",
                table: "Reservations",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_OfferId",
                table: "Reservations",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_RoomId",
                table: "Reservations",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVouchers_VoucherId",
                table: "UserVouchers",
                column: "VoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkApplications_UserId",
                table: "WorkApplications",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Comments");

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
                name: "Payments");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "UserVouchers");

            migrationBuilder.DropTable(
                name: "WorkApplications");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Rate");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Vouchers");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "OfferDetails");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "OfferSubCategories");

            migrationBuilder.DropTable(
                name: "OfferCategories");
        }
    }
}

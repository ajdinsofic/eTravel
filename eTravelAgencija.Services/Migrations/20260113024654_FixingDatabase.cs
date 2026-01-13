using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eTravelAgencija.Services.Migrations
{
    /// <inheritdoc />
    public partial class FixingDatabase : Migration
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
                    PasswordResetToken = table.Column<string>(type: "text", nullable: true),
                    PasswordResetTokenExpires = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
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
                    isDiscountUsed = table.Column<bool>(type: "boolean", nullable: false),
                    DiscountValue = table.Column<double>(type: "double precision", nullable: false),
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
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "DateBirth", "Email", "EmailConfirmed", "FirstName", "ImageUrl", "LastLoginAt", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PasswordResetToken", "PasswordResetTokenExpires", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "isBlocked" },
                values: new object[,]
                {
                    { 1, 0, "6f7be04d-df69-4403-b47c-3123edc9ee16", new DateTime(2026, 1, 13, 2, 46, 50, 450, DateTimeKind.Utc).AddTicks(6061), new DateTime(1990, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), "radnik@etravel.com", true, "Marko", "testing.jpg", null, "Radnik", false, null, "RADNIK@ETRAVEL.COM", "RADNIK", "AQAAAAIAAYagAAAAELMXGTcnTQRghDKtbeT5sjX+fgn6/Mcyaa13buL6Vv+Vj+iThamXVWqrwRCvWFaIXA==", null, null, "+38761111111", false, "f22b5432-ad22-4443-8d5b-4e1945205b3f", false, "radnik", false },
                    { 2, 0, "97c256de-71e5-42d8-ab67-62b76eb0c34b", new DateTime(2026, 1, 13, 2, 46, 50, 529, DateTimeKind.Utc).AddTicks(6991), new DateTime(1985, 3, 20, 0, 0, 0, 0, DateTimeKind.Utc), "direktor@etravel.com", true, "Amir", "testing.jpg", null, "Direktor", false, null, "DIREKTOR@ETRAVEL.COM", "DIREKTOR", "AQAAAAIAAYagAAAAEOX9Y56AdttpC0WBA2fFb0J3NjThzjQXn91gqcQ33MhWePoPt5Iw86MJHRcOi+uUPA==", null, null, "+38762222222", false, "85b0b984-5d5d-404e-a83c-eebcd50a2c1b", false, "direktor", false },
                    { 4, 0, "e4ae137c-3a76-44ca-95c4-4a2dd1d40b6b", new DateTime(2026, 1, 13, 2, 46, 50, 610, DateTimeKind.Utc).AddTicks(5689), new DateTime(2002, 11, 5, 0, 0, 0, 0, DateTimeKind.Utc), "korisnik@etravel.com", true, "Ajdin", "testing.jpg", null, "Korisnik", false, null, "KORISNIK@ETRAVEL.COM", "KORISNIK", "AQAAAAIAAYagAAAAEJFCtDBhnmq6f1QsaMIHIpZTaCHdn1B91GXBNqnciaycq037aoMvgl3F90QmQ5CPjg==", null, null, "+38763333333", false, null, false, "korisnik", false },
                    { 5, 0, "0f533dec-fd64-4d6d-9f1c-e66d438e9c8d", new DateTime(2026, 1, 13, 2, 46, 50, 712, DateTimeKind.Utc).AddTicks(8511), new DateTime(1998, 4, 12, 0, 0, 0, 0, DateTimeKind.Utc), "maja.petrovic@etravel.com", true, "Maja", "testing.jpg", null, "Petrović", false, null, "MAJA.PETROVIC@ETRAVEL.COM", "MAJAPETROVIC55", "AQAAAAIAAYagAAAAENLpjlIxLSOGc8T/5RqD4fxD8IRmdengBBgZi/cTM4ZqjOI5vxUBWQEe3wlGdFKn6g==", null, null, "+38761555111", false, null, false, "majapetrovic55", false },
                    { 6, 0, "ee9352e9-d05c-49a1-80af-434510f2083a", new DateTime(2026, 1, 13, 2, 46, 50, 845, DateTimeKind.Utc).AddTicks(2003), new DateTime(1995, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "edinmesic5566@gmail.com", true, "Edin", "testing.jpg", null, "Mešić", false, null, "EDIN.MESIC@ETRAVEL.COM", "EDINMESIC55", "AQAAAAIAAYagAAAAEItQujnMgV1zEARthuI+wdtPMsV97SKslgxX/nZ9ZZVLLfBkVJW9tubUfRR/6BUmIA==", null, null, "+38761666123", false, null, false, "edinmesic55", false },
                    { 7, 0, "d696ede6-c94f-4f90-9a6a-a31932ea1fa8", new DateTime(2026, 1, 13, 2, 46, 50, 964, DateTimeKind.Utc).AddTicks(5440), new DateTime(2000, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "lana.kovac@etravel.com", true, "Lana", "testing.jpg", null, "Kovač", false, null, "LANA.KOVAC@ETRAVEL.COM", "LANAKOVAC55", "AQAAAAIAAYagAAAAECKdkC0EY1awYH+pr11EMtuUXVOL+H7D7AbYboLb7j34wBUpvIsbD/L/HdK0NAnWlg==", null, null, "+38761777141", false, null, false, "lanakovac55", false },
                    { 8, 0, "701be6ab-dee4-46f3-a254-0735d2b25ead", new DateTime(2026, 1, 13, 2, 46, 51, 118, DateTimeKind.Utc).AddTicks(2392), new DateTime(1993, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), "haris.becirovic@etravel.com", true, "Haris", "testing.jpg", null, "Bećirović", false, null, "HARIS.BECIROVIC@ETRAVEL.COM", "HARISBECIROVIC55", "AQAAAAIAAYagAAAAEPuMlQ/DgCp6apuRCccfsybJ63K7rncvK4zLftqbIN2SDUWokb7+3GJb85HP2f6LwQ==", null, null, "+38761888222", false, null, false, "harisbecirovic55", false },
                    { 9, 0, "2cc1b9d9-5441-4289-90c7-588bb7a7d020", new DateTime(2026, 1, 13, 2, 46, 51, 286, DateTimeKind.Utc).AddTicks(9136), new DateTime(1999, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "amira.karic@etravel.com", true, "Amira", "testing.jpg", null, "Karić", false, null, "AMIRA.KARIC@ETRAVEL.COM", "AMIRAKARIC55", "AQAAAAIAAYagAAAAELQF4RVYXjfXE3Kp1juj5HrFIDa7QzN+T4H7O78ovzmZVXGAv1t2dIJjIBoL3B3cXg==", null, null, "+38761999444", false, null, false, "amirakaric55", false },
                    { 10, 0, "ced83172-830a-41bc-bc86-82a5ed33c27d", new DateTime(2026, 1, 13, 2, 46, 51, 396, DateTimeKind.Utc).AddTicks(5171), new DateTime(1997, 5, 19, 0, 0, 0, 0, DateTimeKind.Utc), "tarik.suljic@etravel.com", true, "Tarik", "testing.jpg", null, "Suljić", false, null, "TARIK.SULJIC@ETRAVEL.COM", "TARIKSULJIC55", "AQAAAAIAAYagAAAAEMsSFr1/QlceR6uV/EgBzw3qvt0UaxgR5q2uS0JHlJgOpsF+9bC4UmvfMAtK2uCAyA==", null, null, "+38762011223", false, null, false, "tariksuljic55", false },
                    { 11, 0, "00b3b25e-c9bf-450d-b6b0-89b2e6586095", new DateTime(2026, 1, 13, 2, 46, 51, 511, DateTimeKind.Utc).AddTicks(871), new DateTime(2001, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "selma.babic@etravel.com", true, "Selma", "testing.jpg", null, "Babić", false, null, "SELMA.BABIC@ETRAVEL.COM", "SELMABABIC55", "AQAAAAIAAYagAAAAECCtPhU1I4Zj7p/jeCEnmBOixpYH3ss7XTJEQvzaT9ty9LQ5SNrIwm5XbWmJuEoxpQ==", null, null, "+38762044321", false, null, false, "selmababic55", false },
                    { 12, 0, "b01dac28-4aea-4767-972e-a5bee18a37e1", new DateTime(2026, 1, 13, 2, 46, 51, 610, DateTimeKind.Utc).AddTicks(4273), new DateTime(1994, 12, 2, 0, 0, 0, 0, DateTimeKind.Utc), "nedim.ceric@etravel.com", true, "Nedim", "testing.jpg", null, "Ćerić", false, null, "NEDIM.CERIC@ETRAVEL.COM", "NEDIMCERIC55", "AQAAAAIAAYagAAAAEO/ZJJURwcQ3HZLrTZ0NPkl6gDhKhTd7UATUIWOhWL3NB6NT79HNV/qhHIIaGTTmzA==", null, null, "+38762077311", false, null, false, "nedimceric55", false },
                    { 13, 0, "7283b4f0-6910-4f5b-874b-c41fb54783e2", new DateTime(2026, 1, 13, 2, 46, 51, 704, DateTimeKind.Utc).AddTicks(7053), new DateTime(1996, 11, 9, 0, 0, 0, 0, DateTimeKind.Utc), "alma.vujic@etravel.com", true, "Alma", "testing.jpg", null, "Vujić", false, null, "ALMA.VUJIC@ETRAVEL.COM", "ALMAVUJIC55", "AQAAAAIAAYagAAAAEJ4PVGD9x3yz8C1Kn05n+fzBXDOYrwbCjZG0TDwOdTIPJdN9VSLiAzd8IQd3j8huUg==", null, null, "+38762111333", false, null, false, "almavujic55", false },
                    { 14, 0, "483f1ed4-b5ba-4ac7-854d-69ab3321e281", new DateTime(2026, 1, 13, 2, 46, 51, 791, DateTimeKind.Utc).AddTicks(6857), new DateTime(1992, 7, 4, 0, 0, 0, 0, DateTimeKind.Utc), "mirza.drace@etravel.com", true, "Mirza", "testing.jpg", null, "DračE", false, null, "MIRZA.DRACE@ETRAVEL.COM", "MIRZADRACE55", "AQAAAAIAAYagAAAAEPXKABgMn9+S/b+gdLQe4MaKywWShb71V3HfGoo3vwbBTHwqz6i3SXCohhrb6vNNWg==", null, null, "+38762144555", false, null, false, "mirzadrace55", false },
                    { 15, 0, "4bda009c-a30d-4173-bf22-e361700de1f9", new DateTime(2026, 1, 13, 2, 46, 51, 882, DateTimeKind.Utc).AddTicks(9806), new DateTime(2000, 6, 17, 0, 0, 0, 0, DateTimeKind.Utc), "melisa.nuhanovic@etravel.com", true, "Melisa", "testing.jpg", null, "Nuhanović", false, null, "MELISA.NUHANOVIC@ETRAVEL.COM", "MELISANUHANOVIC55", "AQAAAAIAAYagAAAAEE77Nuc0gFNA6C195hFl66OnK10hkY5a898NJNUrU7BGcwfqQ2Ct7tS9aaFZtW0ZLg==", null, null, "+38762200333", false, null, false, "melisanuhanovic55", false },
                    { 16, 0, "c0b02c6c-b624-4c03-8be5-44f61c7cb6e9", new DateTime(2026, 1, 13, 2, 46, 51, 978, DateTimeKind.Utc).AddTicks(991), new DateTime(1991, 3, 29, 0, 0, 0, 0, DateTimeKind.Utc), "almin.kosuta@etravel.com", true, "Almin", "testing.jpg", null, "Košuta", false, null, "ALMIN.KOSUTA@ETRAVEL.COM", "ALMINKOSUTA55", "AQAAAAIAAYagAAAAECiSE1UdA+XLpYxzNFXlKPuX58AVkjcRwVzHgErMEmiAfEHAtGLx6Zv/yv4Rj0mSoA==", null, null, "+38762255677", false, null, false, "alminkosuta55", false },
                    { 17, 0, "e143f057-f567-4443-bbb2-a4d8f7a34438", new DateTime(2026, 1, 13, 2, 46, 52, 66, DateTimeKind.Utc).AddTicks(2464), new DateTime(1998, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), "dina.hodzic@etravel.com", true, "Dina", "testing.jpg", null, "Hodžić", false, null, "DINA.HODZIC@ETRAVEL.COM", "DINAHODZIC55", "AQAAAAIAAYagAAAAEHjxATFrgBGftOm5YvA6/jPz5LjTSubnLleRGj46T8m7szMtRrhu14xUMpcTlsSNQw==", null, null, "+38762277991", false, null, false, "dinahodzic55", false },
                    { 18, 0, "18c0ad0e-ff44-42dd-bc8d-044b47b91cfe", new DateTime(2026, 1, 13, 2, 46, 52, 166, DateTimeKind.Utc).AddTicks(9749), new DateTime(1997, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "alem.celik@etravel.com", true, "Alem", "testing.jpg", null, "Čelik", false, null, "ALEM.CELIK@ETRAVEL.COM", "ALEMCELIK55", "AQAAAAIAAYagAAAAEMv5/4lWYu3kX1mhEDiJ09wxftYQO7yypdCJ0ilVmI4v3x/rlE9QsxdIlDE273DuLA==", null, null, "+38762300990", false, null, false, "alemcelik55", false },
                    { 19, 0, "d05fd48d-0963-4695-ad24-cf30b14944da", new DateTime(2026, 1, 13, 2, 46, 52, 270, DateTimeKind.Utc).AddTicks(5614), new DateTime(2001, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "lejla.avdic@etravel.com", true, "Lejla", "testing.jpg", null, "Avdić", false, null, "LEJLA.AVDIC@ETRAVEL.COM", "LEJLAAVDIC55", "AQAAAAIAAYagAAAAEOr2C2mqwlt2J1zSiekZkDskCM6c8e2zFiX+dLXnV3TU2o4rQAi+LfdeOrpvw6vq5g==", null, null, "+38762355123", false, null, false, "lejlaavdic55", false },
                    { 20, 0, "739b65cf-4dce-47d8-805c-ba4b491d2415", new DateTime(2026, 1, 13, 2, 46, 52, 361, DateTimeKind.Utc).AddTicks(3359), new DateTime(1999, 9, 3, 0, 0, 0, 0, DateTimeKind.Utc), "adnan.pasalic@etravel.com", true, "Adnan", "testing.jpg", null, "Pašalić", false, null, "ADNAN.PASALIC@ETRAVEL.COM", "ADNANPASALIC55", "AQAAAAIAAYagAAAAENKS8OgVjM87oPfFgv07AJEpFWCHL1Bpf4s4qgBBaSQCXUE1w7qL/SuMyUc5LOU9Pg==", null, null, "+38762388321", false, null, false, "adnanpasalic55", false },
                    { 21, 0, "abe1e907-e3f2-43b8-a65d-6b87978692a0", new DateTime(2026, 1, 13, 2, 46, 52, 480, DateTimeKind.Utc).AddTicks(9065), new DateTime(1996, 4, 14, 0, 0, 0, 0, DateTimeKind.Utc), "inez.kantic@etravel.com", true, "Inez", "testing.jpg", null, "Kantić", false, null, "INEZ.KANTIC@ETRAVEL.COM", "INEZKANTIC55", "AQAAAAIAAYagAAAAEFaw9FyVG6kYN0qXuHtcvpQ9BbKV3QdECN7LbjTQG5Pq0pDc0Q+qbZarN/EkirMQlw==", null, null, "+38762444123", false, null, false, "inezkantic55", false },
                    { 22, 0, "e9a788e5-dd67-4dc7-8605-bf7ec6f885e3", new DateTime(2026, 1, 13, 2, 46, 52, 613, DateTimeKind.Utc).AddTicks(6092), new DateTime(1993, 11, 19, 0, 0, 0, 0, DateTimeKind.Utc), "amir.halilovic@etravel.com", true, "Amir", "testing.jpg", null, "Halilović", false, null, "AMIR.HALILOVIC@ETRAVEL.COM", "AMIRHALILOVIC55", "AQAAAAIAAYagAAAAEET07KdXui+9imcZg/EW/IT0DJOsFBZIW7aG8/I5qEmV4aW3Q/oyhwya9TZMZIAlRw==", null, null, "+38762477331", false, null, false, "amirhalilovic55", false },
                    { 23, 0, "0f5f9002-fede-4210-a7c9-050812994040", new DateTime(2026, 1, 13, 2, 46, 52, 729, DateTimeKind.Utc).AddTicks(5671), new DateTime(2002, 12, 21, 0, 0, 0, 0, DateTimeKind.Utc), "lamija.kreso@etravel.com", true, "Lamija", "testing.jpg", null, "Krešo", false, null, "LAMIJA.KRESO@ETRAVEL.COM", "LAMIJAKRESO55", "AQAAAAIAAYagAAAAEN61ibs7Xp/vZm/JRsDe/lVK7hJdNqZCaLmc+W3qvVEwtODpZl8+IDfNk2+MId7+Cg==", null, null, "+38762555991", false, null, false, "lamijakreso55", false },
                    { 24, 0, "ef856a39-0b32-4249-bc42-7c0cc96b8f7b", new DateTime(2026, 1, 13, 2, 46, 52, 839, DateTimeKind.Utc).AddTicks(580), new DateTime(1998, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), "omer.smajic@etravel.com", true, "Omer", "testing.jpg", null, "Smajić", false, null, "OMER.SMAJIC@ETRAVEL.COM", "OMERSMAJIC55", "AQAAAAIAAYagAAAAEMYXmVfeguH6JwjrTQ4pTe1SKl3K2eyp4Ve2thKqaguLhp1O731O0YclVvayRrvuiA==", null, null, "+38762666112", false, null, false, "omersmajic55", false }
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "Address", "CalculatedPrice", "Name", "Stars" },
                values: new object[,]
                {
                    { 100, "Via Roma 12", 0m, "Hotel Medici", 4 },
                    { 101, "Piazza Duomo 2", 0m, "Hotel Firenze Centro", 3 },
                    { 102, "Ponte Vecchio 5", 0m, "Hotel Ponte Vecchio", 5 },
                    { 103, "Via Dante 18", 0m, "Florence Art Boutique", 3 },
                    { 104, "Lungarno Acciaiuoli 10", 0m, "Arno River Grand Hotel", 4 },
                    { 105, "Via Tornabuoni 7", 0m, "Palazzo Renaissance Luxury", 5 },
                    { 106, "Via Nazionale 44", 0m, "Tuscan Comfort Inn", 3 },
                    { 107, "Via dei Calzaiuoli 21", 0m, "Florence Heritage Plaza", 4 },
                    { 108, "Via dei Servi 9", 0m, "Duomo Crown Luxury Hotel", 5 },
                    { 110, "Santorini Beach 9", 0m, "Blue Dome Resort", 5 },
                    { 111, "Oia Street 44", 0m, "Aegean View", 4 },
                    { 112, "Fira 21", 0m, "White Cave Hotel", 3 },
                    { 113, "Oia Cliffs 3", 0m, "Sunset Caldera Suites", 3 },
                    { 114, "Imerovigli 19", 0m, "Santorini Horizon Resort", 4 },
                    { 115, "Caldera Rim 1", 0m, "Volcano View Luxury Villas", 5 },
                    { 116, "Kamari 11", 0m, "Cyclades Budget Stay", 3 },
                    { 117, "Perissa 6", 0m, "Aegean Pearl Hotel", 4 },
                    { 118, "Oia Heights 1", 0m, "Santorini Royal Infinity", 5 },
                    { 120, "Sultanahmet 1", 0m, "Hotel Sultanahmet", 4 },
                    { 121, "Galata 5", 0m, "Galata Inn", 3 },
                    { 122, "Bosfor Blvd 7", 0m, "Bosfor Palace Hotel", 5 },
                    { 123, "Eminönü 14", 0m, "Golden Horn City Hotel", 3 },
                    { 124, "Kapalıçarşı 8", 0m, "Istanbul Grand Bazaar Hotel", 4 },
                    { 125, "Dolmabahçe 2", 0m, "Ottoman Imperial Luxury", 5 },
                    { 126, "Üsküdar 19", 0m, "Bosphorus Comfort Stay", 3 },
                    { 127, "Beyoğlu 6", 0m, "Historic Pera Plaza", 4 },
                    { 128, "Sultanahmet 22", 0m, "Golden Minaret Palace", 5 },
                    { 200, "La Rambla 12", 0m, "Hotel Condal Barcelona", 3 },
                    { 215, "Carrer de Pelai 45", 0m, "Barcelona Central Plaza", 4 },
                    { 216, "Carrer de Mallorca 401", 0m, "Sagrada Familia View Hotel", 5 },
                    { 217, "Carrer Ferran 18", 0m, "Gothic Quarter Stay", 3 },
                    { 218, "Port Olímpic 5", 0m, "Barcelona Marina Hotel", 4 },
                    { 219, "Passeig de Gràcia 12", 0m, "Catalonia Grand Luxury", 5 },
                    { 220, "Gran Via 90", 0m, "Barcelona City Budget", 3 },
                    { 221, "Diagonal 110", 0m, "Mediterranean Plaza Hotel", 4 },
                    { 222, "La Rambla 1", 0m, "Royal Ramblas Palace", 5 },
                    { 245, "Rue Saint-Denis 45", 0m, "Paris City Budget Hotel", 3 },
                    { 246, "Quai de la Seine 12", 0m, "Seine Riverside Hotel", 4 },
                    { 247, "Avenue Montaigne 8", 0m, "Champs-Élysées Luxury Palace", 5 },
                    { 248, "Boulevard du Montparnasse 60", 0m, "Montparnasse Comfort Stay", 3 },
                    { 249, "Rue Auber 18", 0m, "Paris Opera Grand Hotel", 4 },
                    { 250, "Place du Louvre 1", 0m, "Royal Louvre Prestige", 5 },
                    { 251, "Karlova 22", 0m, "Prague Old Town Budget", 3 },
                    { 252, "Smetanovo nábřeží 14", 0m, "Vltava Riverside Hotel", 4 },
                    { 253, "Pařížská 5", 0m, "Bohemian Crown Palace", 5 },
                    { 254, "Jindřišská 33", 0m, "New Town Comfort Inn", 3 },
                    { 255, "Na Příkopě 19", 0m, "Prague Heritage Plaza", 4 },
                    { 256, "Kampa 1", 0m, "Charles Bridge Royal Suites", 5 },
                    { 257, "Favoritenstraße 80", 0m, "Vienna City Budget Stay", 3 },
                    { 258, "Schottenring 6", 0m, "Ringstrasse Classic Hotel", 4 },
                    { 259, "Kärntner Ring 16", 0m, "Imperial Vienna Palace", 5 },
                    { 260, "Praterstraße 28", 0m, "Danube Comfort Inn", 3 },
                    { 261, "Herrengasse 12", 0m, "Vienna Historic Grand Hotel", 4 },
                    { 262, "Schönbrunner Straße 1", 0m, "Schonbrunn Royal Luxury", 5 },
                    { 263, "Nieuwendijk 55", 0m, "Amsterdam Central Budget", 3 },
                    { 264, "Prinsengracht 90", 0m, "Canal House Boutique Hotel", 4 },
                    { 265, "Dam Square 1", 0m, "Royal Dam Palace", 5 },
                    { 266, "Van Baerlestraat 20", 0m, "Museum Quarter Comfort Stay", 3 },
                    { 267, "Kalverstraat 130", 0m, "Amsterdam Heritage Plaza", 4 },
                    { 268, "Herengracht 2", 0m, "Golden Tulip Luxury Suites", 5 },
                    { 269, "Paddington Street 40", 0m, "London City Budget Hotel", 3 },
                    { 270, "South Bank 15", 0m, "Thames Riverside Hotel", 4 },
                    { 271, "Whitehall 1", 0m, "Royal Westminster Palace", 5 },
                    { 272, "Camden Road 22", 0m, "Camden Comfort Stay", 3 },
                    { 273, "Long Acre 9", 0m, "Covent Garden Grand Hotel", 4 },
                    { 274, "The Mall 3", 0m, "Buckingham Crown Luxury", 5 },
                    { 275, "Deira Street 12", 0m, "Dubai City Budget Inn", 3 },
                    { 276, "Palm Crescent 5", 0m, "Palm Jumeirah Resort", 4 },
                    { 277, "Sheikh Zayed Road 1", 0m, "Arabian Royal Palace", 5 },
                    { 278, "Creek Road 30", 0m, "Dubai Creek Comfort Hotel", 3 },
                    { 279, "Jumeirah Beach Walk 8", 0m, "JBR Beach Grand Hotel", 4 },
                    { 280, "Umm Suqeim 1", 0m, "Burj Al Arab Prestige Suites", 5 },
                    { 281, "Giza Road 22", 0m, "Cairo City Budget Stay", 3 },
                    { 282, "Zamalek 14", 0m, "Nile View Classic Hotel", 4 },
                    { 283, "Pyramid Plateau 1", 0m, "Pharaoh Royal Palace", 5 },
                    { 284, "Ramses Street 33", 0m, "Downtown Cairo Comfort", 3 },
                    { 285, "Garden City 10", 0m, "Cairo Heritage Grand Hotel", 4 },
                    { 286, "Corniche El Nil 5", 0m, "Golden Nile Luxury Suites", 5 },
                    { 287, "Erzsébet Körút 21", 0m, "Budapest City Budget Hotel", 3 },
                    { 288, "Margit Rakpart 9", 0m, "Danube Classic Stay", 4 },
                    { 289, "Széchenyi tér 1", 0m, "Royal Thermal Palace", 5 },
                    { 290, "Rákóczi út 44", 0m, "Pest Comfort Inn", 3 },
                    { 291, "Castle Hill 3", 0m, "Buda Castle Grand Hotel", 4 },
                    { 292, "Andrássy út 2", 0m, "Hungarian Crown Luxury", 5 },
                    { 293, "Grodzka 30", 0m, "Krakow City Budget Inn", 3 },
                    { 294, "Bulwar Czerwieński 6", 0m, "Vistula Riverside Hotel", 4 },
                    { 295, "Wawel Hill 1", 0m, "Royal Wawel Palace", 5 },
                    { 296, "Szeroka 18", 0m, "Kazimierz Comfort Stay", 3 },
                    { 297, "Rynek Główny 10", 0m, "Krakow Heritage Plaza", 4 },
                    { 298, "Floriańska 1", 0m, "Golden Dragon Luxury Suites", 5 },
                    { 299, "Pwani Road 7", 0m, "Zanzibar Budget Beach Stay", 3 },
                    { 300, "Kiwengwa Beach 4", 0m, "Indian Ocean View Hotel", 4 },
                    { 301, "Michamvi Peninsula 1", 0m, "Sultan Sands Royal Resort", 5 },
                    { 302, "Malindi Road 9", 0m, "Stone Town Comfort Inn", 3 },
                    { 303, "Forodhani Gardens 2", 0m, "Zanzibar Heritage Grand Hotel", 4 },
                    { 304, "Nungwi Point 1", 0m, "Royal Zanzibar Infinity Villas", 5 },
                    { 305, "Airport Road 12", 0m, "Hurghada City Budget Hotel", 3 },
                    { 306, "El Gouna Road 5", 0m, "Red Sea View Resort", 4 },
                    { 307, "Makadi Bay 1", 0m, "Pharaoh Beach Luxury Palace", 5 },
                    { 308, "Village Road 44", 0m, "Hurghada Comfort Inn", 3 },
                    { 309, "Sahl Hasheesh 8", 0m, "Coral Bay Grand Hotel", 4 },
                    { 310, "Safaga Road 2", 0m, "Golden Red Sea Prestige", 5 },
                    { 311, "Alcântara 22", 0m, "Lisbon City Budget Stay", 3 },
                    { 312, "Cais do Sodré 9", 0m, "Tagus Riverside Hotel", 4 },
                    { 313, "Mosteiro dos Jerónimos 1", 0m, "Belem Royal Palace", 5 },
                    { 314, "Rua da Rosa 33", 0m, "Bairro Alto Comfort Inn", 3 },
                    { 315, "Avenida da Liberdade 18", 0m, "Lisbon Heritage Grand Hotel", 4 },
                    { 316, "Praça do Comércio 1", 0m, "Atlantic Crown Luxury Suites", 5 },
                    { 317, "Omonia Square 12", 0m, "Athens City Budget Hotel", 3 },
                    { 318, "Makrygianni 10", 0m, "Acropolis Classic Stay", 4 },
                    { 319, "Syntagma Avenue 1", 0m, "Olympian Royal Palace", 5 },
                    { 320, "Kidathineon 22", 0m, "Plaka Comfort Inn", 3 },
                    { 321, "Ermou Street 9", 0m, "Athens Heritage Plaza", 4 },
                    { 322, "Dionysiou Areopagitou 1", 0m, "Golden Acropolis Luxury Suites", 5 },
                    { 323, "Poljička Cesta 40", 0m, "Split City Budget Hotel", 3 },
                    { 324, "Žnjan Beach 7", 0m, "Adriatic Coastline Resort", 4 },
                    { 325, "Riva 1", 0m, "Dalmatian Royal Palace", 5 },
                    { 326, "Radunica 22", 0m, "Old Town Comfort Stay", 3 },
                    { 327, "Marmontova 10", 0m, "Split Heritage Grand Hotel", 4 },
                    { 328, "Marjan Hill 1", 0m, "Golden Adriatic Luxury Suites", 5 },
                    { 550, "Avenue de Suffren 18", 0m, "Hotel Eiffel Panorama", 4 },
                    { 551, "Rue Lepic 52", 0m, "Montmartre Boutique Stay", 3 },
                    { 552, "Quai Voltaire 7", 0m, "Seine Royal Collection", 5 },
                    { 553, "Staroměstské náměstí 8", 0m, "Old Town Astronomical Hotel", 4 },
                    { 554, "Nerudova 15", 0m, "Prague Castle View Inn", 3 },
                    { 555, "Malá Strana 2", 0m, "Bohemian Luxury Riverside", 5 },
                    { 556, "Opernring 11", 0m, "Vienna Imperial Ring Hotel", 4 },
                    { 557, "Museumsplatz 6", 0m, "Museum Quarter City Stay", 3 },
                    { 558, "Donaukanal 4", 0m, "Danube Crown Prestige", 5 },
                    { 560, "Amstel 32", 0m, "Amsterdam River View Hotel", 4 },
                    { 561, "Lindengracht 85", 0m, "Jordaan Cozy Stay", 3 },
                    { 562, "Keizersgracht 10", 0m, "Royal Canal Crown", 5 },
                    { 563, "Tooley Street 18", 0m, "London Bridge View Hotel", 4 },
                    { 564, "Dean Street 41", 0m, "Soho Urban Comfort", 3 },
                    { 565, "Park Lane 7", 0m, "Hyde Park Royal Suites", 5 },
                    { 566, "Marina Walk 22", 0m, "Dubai Marina Skyline Hotel", 4 },
                    { 567, "Business Bay 14", 0m, "Downtown Dubai City Stay", 3 },
                    { 568, "Palm Jumeirah West", 0m, "Palm Crown Elite Resort", 5 },
                    { 570, "Al Haram Street 55", 0m, "Cairo Pyramids View Hotel", 4 },
                    { 571, "Dokki 18", 0m, "Nile Sunset Budget Stay", 3 },
                    { 572, "Tahrir Square 1", 0m, "Cairo Royal Heritage Palace", 5 },
                    { 573, "Belgrád Rakpart 12", 0m, "Budapest Riverside View", 4 },
                    { 574, "Király utca 29", 0m, "City Center Budget Stay", 3 },
                    { 575, "Chain Bridge Road 1", 0m, "Danube Crown Luxury Hotel", 5 },
                    { 576, "Świętego Jana 8", 0m, "Krakow Old Town View", 4 },
                    { 577, "Szewska 14", 0m, "Historic Market Budget Inn", 3 },
                    { 578, "Bulwar Inflancki 1", 0m, "Vistula Royal Prestige", 5 },
                    { 579, "Kendwa Beach 3", 0m, "Zanzibar Sunset Beach Hotel", 4 },
                    { 580, "Shangani Street 11", 0m, "Stone Town Budget Lodge", 3 },
                    { 581, "Mnemba Island", 0m, "Indian Ocean Crown Resort", 5 },
                    { 600, "Sheraton Road 18", 0m, "Hurghada Sunrise Beach Hotel", 4 },
                    { 601, "Downtown Hurghada 7", 0m, "Red Sea Budget Lodge", 3 },
                    { 602, "Makadi Bay Coastline", 0m, "Coral Reef Royal Resort", 5 },
                    { 603, "Graça 21", 0m, "Lisbon Hills View Hotel", 4 },
                    { 604, "Rua dos Remédios 14", 0m, "Alfama Budget Stay", 3 },
                    { 605, "Belém Riverside 1", 0m, "Lisbon Atlantic Royal", 5 },
                    { 606, "Lycabettus Hill 6", 0m, "Athens Panorama View", 4 },
                    { 607, "Ifestou Street 11", 0m, "Monastiraki Budget Inn", 3 },
                    { 608, "Poseidonos Avenue 1", 0m, "Aegean Crown Palace", 5 },
                    { 609, "Bačvice Beach 9", 0m, "Split Seaside View Hotel", 4 },
                    { 610, "Bosanska 4", 0m, "Diocletian Budget Stay", 3 },
                    { 611, "Kašjuni Bay 1", 0m, "Adriatic Crown Luxury Resort", 5 }
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
                    { 2, 1, new DateTime(2026, 1, 13, 2, 46, 52, 958, DateTimeKind.Utc).AddTicks(6640), "", true },
                    { 3, 2, new DateTime(2026, 1, 13, 2, 46, 52, 958, DateTimeKind.Utc).AddTicks(6675), "", true },
                    { 1, 4, new DateTime(2026, 1, 13, 2, 46, 52, 958, DateTimeKind.Utc).AddTicks(6680), "", true },
                    { 1, 5, new DateTime(2026, 1, 13, 2, 46, 52, 958, DateTimeKind.Utc).AddTicks(6788), "", true },
                    { 1, 6, new DateTime(2026, 1, 13, 2, 46, 52, 958, DateTimeKind.Utc).AddTicks(6790), "", true },
                    { 1, 7, new DateTime(2026, 1, 13, 2, 46, 52, 958, DateTimeKind.Utc).AddTicks(6791), "", true },
                    { 1, 8, new DateTime(2026, 1, 13, 2, 46, 52, 958, DateTimeKind.Utc).AddTicks(6792), "", true },
                    { 1, 9, new DateTime(2026, 1, 13, 2, 46, 52, 958, DateTimeKind.Utc).AddTicks(6794), "", true },
                    { 1, 10, new DateTime(2026, 1, 13, 2, 46, 52, 958, DateTimeKind.Utc).AddTicks(6795), "", true },
                    { 1, 11, new DateTime(2026, 1, 13, 2, 46, 52, 958, DateTimeKind.Utc).AddTicks(6796), "", true },
                    { 1, 12, new DateTime(2026, 1, 13, 2, 46, 52, 958, DateTimeKind.Utc).AddTicks(6798), "", true },
                    { 1, 13, new DateTime(2026, 1, 13, 2, 46, 52, 958, DateTimeKind.Utc).AddTicks(6799), "", true },
                    { 1, 14, new DateTime(2026, 1, 13, 2, 46, 52, 958, DateTimeKind.Utc).AddTicks(6800), "", true },
                    { 1, 15, new DateTime(2026, 1, 13, 2, 46, 52, 958, DateTimeKind.Utc).AddTicks(6801), "", true },
                    { 1, 16, new DateTime(2026, 1, 13, 2, 46, 52, 958, DateTimeKind.Utc).AddTicks(6807), "", true },
                    { 1, 17, new DateTime(2026, 1, 13, 2, 46, 52, 958, DateTimeKind.Utc).AddTicks(6808), "", true },
                    { 1, 18, new DateTime(2026, 1, 13, 2, 46, 52, 958, DateTimeKind.Utc).AddTicks(6810), "", true },
                    { 1, 19, new DateTime(2026, 1, 13, 2, 46, 52, 958, DateTimeKind.Utc).AddTicks(6811), "", true },
                    { 1, 20, new DateTime(2026, 1, 13, 2, 46, 52, 958, DateTimeKind.Utc).AddTicks(6812), "", true },
                    { 1, 21, new DateTime(2026, 1, 13, 2, 46, 52, 958, DateTimeKind.Utc).AddTicks(6813), "", true },
                    { 1, 22, new DateTime(2026, 1, 13, 2, 46, 52, 958, DateTimeKind.Utc).AddTicks(6814), "", true },
                    { 1, 23, new DateTime(2026, 1, 13, 2, 46, 52, 958, DateTimeKind.Utc).AddTicks(6815), "", true },
                    { 1, 24, new DateTime(2026, 1, 13, 2, 46, 52, 958, DateTimeKind.Utc).AddTicks(6817), "", true }
                });

            migrationBuilder.InsertData(
                table: "HotelImages",
                columns: new[] { "Id", "HotelId", "ImageUrl", "IsMain" },
                values: new object[,]
                {
                    { 1, 100, "https://picsum.photos/seed/hotel-100-1/800/500", true },
                    { 2, 100, "https://picsum.photos/seed/hotel-100-2/800/500", false },
                    { 3, 100, "https://picsum.photos/seed/hotel-100-3/800/500", false },
                    { 4, 100, "https://picsum.photos/seed/hotel-100-4/800/500", false },
                    { 5, 101, "https://picsum.photos/seed/hotel-101-1/800/500", true },
                    { 6, 101, "https://picsum.photos/seed/hotel-101-2/800/500", false },
                    { 7, 101, "https://picsum.photos/seed/hotel-101-3/800/500", false },
                    { 8, 101, "https://picsum.photos/seed/hotel-101-4/800/500", false },
                    { 9, 102, "https://picsum.photos/seed/hotel-102-1/800/500", true },
                    { 10, 102, "https://picsum.photos/seed/hotel-102-2/800/500", false },
                    { 11, 102, "https://picsum.photos/seed/hotel-102-3/800/500", false },
                    { 12, 102, "https://picsum.photos/seed/hotel-102-4/800/500", false },
                    { 13, 103, "https://picsum.photos/seed/hotel-103-1/800/500", true },
                    { 14, 103, "https://picsum.photos/seed/hotel-103-2/800/500", false },
                    { 15, 103, "https://picsum.photos/seed/hotel-103-3/800/500", false },
                    { 16, 103, "https://picsum.photos/seed/hotel-103-4/800/500", false },
                    { 17, 104, "https://picsum.photos/seed/hotel-104-1/800/500", true },
                    { 18, 104, "https://picsum.photos/seed/hotel-104-2/800/500", false },
                    { 19, 104, "https://picsum.photos/seed/hotel-104-3/800/500", false },
                    { 20, 104, "https://picsum.photos/seed/hotel-104-4/800/500", false },
                    { 21, 105, "https://picsum.photos/seed/hotel-105-1/800/500", true },
                    { 22, 105, "https://picsum.photos/seed/hotel-105-2/800/500", false },
                    { 23, 105, "https://picsum.photos/seed/hotel-105-3/800/500", false },
                    { 24, 105, "https://picsum.photos/seed/hotel-105-4/800/500", false },
                    { 25, 106, "https://picsum.photos/seed/hotel-106-1/800/500", true },
                    { 26, 106, "https://picsum.photos/seed/hotel-106-2/800/500", false },
                    { 27, 106, "https://picsum.photos/seed/hotel-106-3/800/500", false },
                    { 28, 106, "https://picsum.photos/seed/hotel-106-4/800/500", false },
                    { 29, 107, "https://picsum.photos/seed/hotel-107-1/800/500", true },
                    { 30, 107, "https://picsum.photos/seed/hotel-107-2/800/500", false },
                    { 31, 107, "https://picsum.photos/seed/hotel-107-3/800/500", false },
                    { 32, 107, "https://picsum.photos/seed/hotel-107-4/800/500", false },
                    { 33, 108, "https://picsum.photos/seed/hotel-108-1/800/500", true },
                    { 34, 108, "https://picsum.photos/seed/hotel-108-2/800/500", false },
                    { 35, 108, "https://picsum.photos/seed/hotel-108-3/800/500", false },
                    { 36, 108, "https://picsum.photos/seed/hotel-108-4/800/500", false },
                    { 37, 110, "https://picsum.photos/seed/hotel-110-1/800/500", true },
                    { 38, 110, "https://picsum.photos/seed/hotel-110-2/800/500", false },
                    { 39, 110, "https://picsum.photos/seed/hotel-110-3/800/500", false },
                    { 40, 110, "https://picsum.photos/seed/hotel-110-4/800/500", false },
                    { 41, 111, "https://picsum.photos/seed/hotel-111-1/800/500", true },
                    { 42, 111, "https://picsum.photos/seed/hotel-111-2/800/500", false },
                    { 43, 111, "https://picsum.photos/seed/hotel-111-3/800/500", false },
                    { 44, 111, "https://picsum.photos/seed/hotel-111-4/800/500", false },
                    { 45, 112, "https://picsum.photos/seed/hotel-112-1/800/500", true },
                    { 46, 112, "https://picsum.photos/seed/hotel-112-2/800/500", false },
                    { 47, 112, "https://picsum.photos/seed/hotel-112-3/800/500", false },
                    { 48, 112, "https://picsum.photos/seed/hotel-112-4/800/500", false },
                    { 49, 113, "https://picsum.photos/seed/hotel-113-1/800/500", true },
                    { 50, 113, "https://picsum.photos/seed/hotel-113-2/800/500", false },
                    { 51, 113, "https://picsum.photos/seed/hotel-113-3/800/500", false },
                    { 52, 113, "https://picsum.photos/seed/hotel-113-4/800/500", false },
                    { 53, 114, "https://picsum.photos/seed/hotel-114-1/800/500", true },
                    { 54, 114, "https://picsum.photos/seed/hotel-114-2/800/500", false },
                    { 55, 114, "https://picsum.photos/seed/hotel-114-3/800/500", false },
                    { 56, 114, "https://picsum.photos/seed/hotel-114-4/800/500", false },
                    { 57, 115, "https://picsum.photos/seed/hotel-115-1/800/500", true },
                    { 58, 115, "https://picsum.photos/seed/hotel-115-2/800/500", false },
                    { 59, 115, "https://picsum.photos/seed/hotel-115-3/800/500", false },
                    { 60, 115, "https://picsum.photos/seed/hotel-115-4/800/500", false },
                    { 61, 116, "https://picsum.photos/seed/hotel-116-1/800/500", true },
                    { 62, 116, "https://picsum.photos/seed/hotel-116-2/800/500", false },
                    { 63, 116, "https://picsum.photos/seed/hotel-116-3/800/500", false },
                    { 64, 116, "https://picsum.photos/seed/hotel-116-4/800/500", false },
                    { 65, 117, "https://picsum.photos/seed/hotel-117-1/800/500", true },
                    { 66, 117, "https://picsum.photos/seed/hotel-117-2/800/500", false },
                    { 67, 117, "https://picsum.photos/seed/hotel-117-3/800/500", false },
                    { 68, 117, "https://picsum.photos/seed/hotel-117-4/800/500", false },
                    { 69, 118, "https://picsum.photos/seed/hotel-118-1/800/500", true },
                    { 70, 118, "https://picsum.photos/seed/hotel-118-2/800/500", false },
                    { 71, 118, "https://picsum.photos/seed/hotel-118-3/800/500", false },
                    { 72, 118, "https://picsum.photos/seed/hotel-118-4/800/500", false },
                    { 73, 120, "https://picsum.photos/seed/hotel-120-1/800/500", true },
                    { 74, 120, "https://picsum.photos/seed/hotel-120-2/800/500", false },
                    { 75, 120, "https://picsum.photos/seed/hotel-120-3/800/500", false },
                    { 76, 120, "https://picsum.photos/seed/hotel-120-4/800/500", false },
                    { 77, 121, "https://picsum.photos/seed/hotel-121-1/800/500", true },
                    { 78, 121, "https://picsum.photos/seed/hotel-121-2/800/500", false },
                    { 79, 121, "https://picsum.photos/seed/hotel-121-3/800/500", false },
                    { 80, 121, "https://picsum.photos/seed/hotel-121-4/800/500", false },
                    { 81, 122, "https://picsum.photos/seed/hotel-122-1/800/500", true },
                    { 82, 122, "https://picsum.photos/seed/hotel-122-2/800/500", false },
                    { 83, 122, "https://picsum.photos/seed/hotel-122-3/800/500", false },
                    { 84, 122, "https://picsum.photos/seed/hotel-122-4/800/500", false },
                    { 85, 123, "https://picsum.photos/seed/hotel-123-1/800/500", true },
                    { 86, 123, "https://picsum.photos/seed/hotel-123-2/800/500", false },
                    { 87, 123, "https://picsum.photos/seed/hotel-123-3/800/500", false },
                    { 88, 123, "https://picsum.photos/seed/hotel-123-4/800/500", false },
                    { 89, 124, "https://picsum.photos/seed/hotel-124-1/800/500", true },
                    { 90, 124, "https://picsum.photos/seed/hotel-124-2/800/500", false },
                    { 91, 124, "https://picsum.photos/seed/hotel-124-3/800/500", false },
                    { 92, 124, "https://picsum.photos/seed/hotel-124-4/800/500", false },
                    { 93, 125, "https://picsum.photos/seed/hotel-125-1/800/500", true },
                    { 94, 125, "https://picsum.photos/seed/hotel-125-2/800/500", false },
                    { 95, 125, "https://picsum.photos/seed/hotel-125-3/800/500", false },
                    { 96, 125, "https://picsum.photos/seed/hotel-125-4/800/500", false },
                    { 97, 126, "https://picsum.photos/seed/hotel-126-1/800/500", true },
                    { 98, 126, "https://picsum.photos/seed/hotel-126-2/800/500", false },
                    { 99, 126, "https://picsum.photos/seed/hotel-126-3/800/500", false },
                    { 100, 126, "https://picsum.photos/seed/hotel-126-4/800/500", false },
                    { 101, 127, "https://picsum.photos/seed/hotel-127-1/800/500", true },
                    { 102, 127, "https://picsum.photos/seed/hotel-127-2/800/500", false },
                    { 103, 127, "https://picsum.photos/seed/hotel-127-3/800/500", false },
                    { 104, 127, "https://picsum.photos/seed/hotel-127-4/800/500", false },
                    { 105, 128, "https://picsum.photos/seed/hotel-128-1/800/500", true },
                    { 106, 128, "https://picsum.photos/seed/hotel-128-2/800/500", false },
                    { 107, 128, "https://picsum.photos/seed/hotel-128-3/800/500", false },
                    { 108, 128, "https://picsum.photos/seed/hotel-128-4/800/500", false },
                    { 109, 200, "https://picsum.photos/seed/hotel-200-1/800/500", true },
                    { 110, 200, "https://picsum.photos/seed/hotel-200-2/800/500", false },
                    { 111, 200, "https://picsum.photos/seed/hotel-200-3/800/500", false },
                    { 112, 200, "https://picsum.photos/seed/hotel-200-4/800/500", false },
                    { 113, 215, "https://picsum.photos/seed/hotel-215-1/800/500", true },
                    { 114, 215, "https://picsum.photos/seed/hotel-215-2/800/500", false },
                    { 115, 215, "https://picsum.photos/seed/hotel-215-3/800/500", false },
                    { 116, 215, "https://picsum.photos/seed/hotel-215-4/800/500", false },
                    { 117, 216, "https://picsum.photos/seed/hotel-216-1/800/500", true },
                    { 118, 216, "https://picsum.photos/seed/hotel-216-2/800/500", false },
                    { 119, 216, "https://picsum.photos/seed/hotel-216-3/800/500", false },
                    { 120, 216, "https://picsum.photos/seed/hotel-216-4/800/500", false },
                    { 121, 217, "https://picsum.photos/seed/hotel-217-1/800/500", true },
                    { 122, 217, "https://picsum.photos/seed/hotel-217-2/800/500", false },
                    { 123, 217, "https://picsum.photos/seed/hotel-217-3/800/500", false },
                    { 124, 217, "https://picsum.photos/seed/hotel-217-4/800/500", false },
                    { 125, 218, "https://picsum.photos/seed/hotel-218-1/800/500", true },
                    { 126, 218, "https://picsum.photos/seed/hotel-218-2/800/500", false },
                    { 127, 218, "https://picsum.photos/seed/hotel-218-3/800/500", false },
                    { 128, 218, "https://picsum.photos/seed/hotel-218-4/800/500", false },
                    { 129, 219, "https://picsum.photos/seed/hotel-219-1/800/500", true },
                    { 130, 219, "https://picsum.photos/seed/hotel-219-2/800/500", false },
                    { 131, 219, "https://picsum.photos/seed/hotel-219-3/800/500", false },
                    { 132, 219, "https://picsum.photos/seed/hotel-219-4/800/500", false },
                    { 133, 220, "https://picsum.photos/seed/hotel-220-1/800/500", true },
                    { 134, 220, "https://picsum.photos/seed/hotel-220-2/800/500", false },
                    { 135, 220, "https://picsum.photos/seed/hotel-220-3/800/500", false },
                    { 136, 220, "https://picsum.photos/seed/hotel-220-4/800/500", false },
                    { 137, 221, "https://picsum.photos/seed/hotel-221-1/800/500", true },
                    { 138, 221, "https://picsum.photos/seed/hotel-221-2/800/500", false },
                    { 139, 221, "https://picsum.photos/seed/hotel-221-3/800/500", false },
                    { 140, 221, "https://picsum.photos/seed/hotel-221-4/800/500", false },
                    { 141, 222, "https://picsum.photos/seed/hotel-222-1/800/500", true },
                    { 142, 222, "https://picsum.photos/seed/hotel-222-2/800/500", false },
                    { 143, 222, "https://picsum.photos/seed/hotel-222-3/800/500", false },
                    { 144, 222, "https://picsum.photos/seed/hotel-222-4/800/500", false },
                    { 145, 245, "https://picsum.photos/seed/hotel-245-1/800/500", true },
                    { 146, 245, "https://picsum.photos/seed/hotel-245-2/800/500", false },
                    { 147, 245, "https://picsum.photos/seed/hotel-245-3/800/500", false },
                    { 148, 245, "https://picsum.photos/seed/hotel-245-4/800/500", false },
                    { 149, 246, "https://picsum.photos/seed/hotel-246-1/800/500", true },
                    { 150, 246, "https://picsum.photos/seed/hotel-246-2/800/500", false },
                    { 151, 246, "https://picsum.photos/seed/hotel-246-3/800/500", false },
                    { 152, 246, "https://picsum.photos/seed/hotel-246-4/800/500", false },
                    { 153, 247, "https://picsum.photos/seed/hotel-247-1/800/500", true },
                    { 154, 247, "https://picsum.photos/seed/hotel-247-2/800/500", false },
                    { 155, 247, "https://picsum.photos/seed/hotel-247-3/800/500", false },
                    { 156, 247, "https://picsum.photos/seed/hotel-247-4/800/500", false },
                    { 157, 248, "https://picsum.photos/seed/hotel-248-1/800/500", true },
                    { 158, 248, "https://picsum.photos/seed/hotel-248-2/800/500", false },
                    { 159, 248, "https://picsum.photos/seed/hotel-248-3/800/500", false },
                    { 160, 248, "https://picsum.photos/seed/hotel-248-4/800/500", false },
                    { 161, 249, "https://picsum.photos/seed/hotel-249-1/800/500", true },
                    { 162, 249, "https://picsum.photos/seed/hotel-249-2/800/500", false },
                    { 163, 249, "https://picsum.photos/seed/hotel-249-3/800/500", false },
                    { 164, 249, "https://picsum.photos/seed/hotel-249-4/800/500", false },
                    { 165, 250, "https://picsum.photos/seed/hotel-250-1/800/500", true },
                    { 166, 250, "https://picsum.photos/seed/hotel-250-2/800/500", false },
                    { 167, 250, "https://picsum.photos/seed/hotel-250-3/800/500", false },
                    { 168, 250, "https://picsum.photos/seed/hotel-250-4/800/500", false },
                    { 169, 550, "https://picsum.photos/seed/hotel-550-1/800/500", true },
                    { 170, 550, "https://picsum.photos/seed/hotel-550-2/800/500", false },
                    { 171, 550, "https://picsum.photos/seed/hotel-550-3/800/500", false },
                    { 172, 550, "https://picsum.photos/seed/hotel-550-4/800/500", false },
                    { 173, 551, "https://picsum.photos/seed/hotel-551-1/800/500", true },
                    { 174, 551, "https://picsum.photos/seed/hotel-551-2/800/500", false },
                    { 175, 551, "https://picsum.photos/seed/hotel-551-3/800/500", false },
                    { 176, 551, "https://picsum.photos/seed/hotel-551-4/800/500", false },
                    { 177, 552, "https://picsum.photos/seed/hotel-552-1/800/500", true },
                    { 178, 552, "https://picsum.photos/seed/hotel-552-2/800/500", false },
                    { 179, 552, "https://picsum.photos/seed/hotel-552-3/800/500", false },
                    { 180, 552, "https://picsum.photos/seed/hotel-552-4/800/500", false },
                    { 181, 251, "https://picsum.photos/seed/hotel-251-1/800/500", true },
                    { 182, 251, "https://picsum.photos/seed/hotel-251-2/800/500", false },
                    { 183, 251, "https://picsum.photos/seed/hotel-251-3/800/500", false },
                    { 184, 251, "https://picsum.photos/seed/hotel-251-4/800/500", false },
                    { 185, 252, "https://picsum.photos/seed/hotel-252-1/800/500", true },
                    { 186, 252, "https://picsum.photos/seed/hotel-252-2/800/500", false },
                    { 187, 252, "https://picsum.photos/seed/hotel-252-3/800/500", false },
                    { 188, 252, "https://picsum.photos/seed/hotel-252-4/800/500", false },
                    { 189, 253, "https://picsum.photos/seed/hotel-253-1/800/500", true },
                    { 190, 253, "https://picsum.photos/seed/hotel-253-2/800/500", false },
                    { 191, 253, "https://picsum.photos/seed/hotel-253-3/800/500", false },
                    { 192, 253, "https://picsum.photos/seed/hotel-253-4/800/500", false },
                    { 193, 254, "https://picsum.photos/seed/hotel-254-1/800/500", true },
                    { 194, 254, "https://picsum.photos/seed/hotel-254-2/800/500", false },
                    { 195, 254, "https://picsum.photos/seed/hotel-254-3/800/500", false },
                    { 196, 254, "https://picsum.photos/seed/hotel-254-4/800/500", false },
                    { 197, 255, "https://picsum.photos/seed/hotel-255-1/800/500", true },
                    { 198, 255, "https://picsum.photos/seed/hotel-255-2/800/500", false },
                    { 199, 255, "https://picsum.photos/seed/hotel-255-3/800/500", false },
                    { 200, 255, "https://picsum.photos/seed/hotel-255-4/800/500", false },
                    { 201, 256, "https://picsum.photos/seed/hotel-256-1/800/500", true },
                    { 202, 256, "https://picsum.photos/seed/hotel-256-2/800/500", false },
                    { 203, 256, "https://picsum.photos/seed/hotel-256-3/800/500", false },
                    { 204, 256, "https://picsum.photos/seed/hotel-256-4/800/500", false },
                    { 205, 553, "https://picsum.photos/seed/hotel-553-1/800/500", true },
                    { 206, 553, "https://picsum.photos/seed/hotel-553-2/800/500", false },
                    { 207, 553, "https://picsum.photos/seed/hotel-553-3/800/500", false },
                    { 208, 553, "https://picsum.photos/seed/hotel-553-4/800/500", false },
                    { 209, 554, "https://picsum.photos/seed/hotel-554-1/800/500", true },
                    { 210, 554, "https://picsum.photos/seed/hotel-554-2/800/500", false },
                    { 211, 554, "https://picsum.photos/seed/hotel-554-3/800/500", false },
                    { 212, 554, "https://picsum.photos/seed/hotel-554-4/800/500", false },
                    { 213, 555, "https://picsum.photos/seed/hotel-555-1/800/500", true },
                    { 214, 555, "https://picsum.photos/seed/hotel-555-2/800/500", false },
                    { 215, 555, "https://picsum.photos/seed/hotel-555-3/800/500", false },
                    { 216, 555, "https://picsum.photos/seed/hotel-555-4/800/500", false },
                    { 217, 257, "https://picsum.photos/seed/hotel-257-1/800/500", true },
                    { 218, 257, "https://picsum.photos/seed/hotel-257-2/800/500", false },
                    { 219, 257, "https://picsum.photos/seed/hotel-257-3/800/500", false },
                    { 220, 257, "https://picsum.photos/seed/hotel-257-4/800/500", false },
                    { 221, 258, "https://picsum.photos/seed/hotel-258-1/800/500", true },
                    { 222, 258, "https://picsum.photos/seed/hotel-258-2/800/500", false },
                    { 223, 258, "https://picsum.photos/seed/hotel-258-3/800/500", false },
                    { 224, 258, "https://picsum.photos/seed/hotel-258-4/800/500", false },
                    { 225, 259, "https://picsum.photos/seed/hotel-259-1/800/500", true },
                    { 226, 259, "https://picsum.photos/seed/hotel-259-2/800/500", false },
                    { 227, 259, "https://picsum.photos/seed/hotel-259-3/800/500", false },
                    { 228, 259, "https://picsum.photos/seed/hotel-259-4/800/500", false },
                    { 229, 260, "https://picsum.photos/seed/hotel-260-1/800/500", true },
                    { 230, 260, "https://picsum.photos/seed/hotel-260-2/800/500", false },
                    { 231, 260, "https://picsum.photos/seed/hotel-260-3/800/500", false },
                    { 232, 260, "https://picsum.photos/seed/hotel-260-4/800/500", false },
                    { 233, 261, "https://picsum.photos/seed/hotel-261-1/800/500", true },
                    { 234, 261, "https://picsum.photos/seed/hotel-261-2/800/500", false },
                    { 235, 261, "https://picsum.photos/seed/hotel-261-3/800/500", false },
                    { 236, 261, "https://picsum.photos/seed/hotel-261-4/800/500", false },
                    { 237, 262, "https://picsum.photos/seed/hotel-262-1/800/500", true },
                    { 238, 262, "https://picsum.photos/seed/hotel-262-2/800/500", false },
                    { 239, 262, "https://picsum.photos/seed/hotel-262-3/800/500", false },
                    { 240, 262, "https://picsum.photos/seed/hotel-262-4/800/500", false },
                    { 241, 556, "https://picsum.photos/seed/hotel-556-1/800/500", true },
                    { 242, 556, "https://picsum.photos/seed/hotel-556-2/800/500", false },
                    { 243, 556, "https://picsum.photos/seed/hotel-556-3/800/500", false },
                    { 244, 556, "https://picsum.photos/seed/hotel-556-4/800/500", false },
                    { 245, 557, "https://picsum.photos/seed/hotel-557-1/800/500", true },
                    { 246, 557, "https://picsum.photos/seed/hotel-557-2/800/500", false },
                    { 247, 557, "https://picsum.photos/seed/hotel-557-3/800/500", false },
                    { 248, 557, "https://picsum.photos/seed/hotel-557-4/800/500", false },
                    { 249, 558, "https://picsum.photos/seed/hotel-558-1/800/500", true },
                    { 250, 558, "https://picsum.photos/seed/hotel-558-2/800/500", false },
                    { 251, 558, "https://picsum.photos/seed/hotel-558-3/800/500", false },
                    { 252, 558, "https://picsum.photos/seed/hotel-558-4/800/500", false },
                    { 253, 263, "https://picsum.photos/seed/hotel-263-1/800/500", true },
                    { 254, 263, "https://picsum.photos/seed/hotel-263-2/800/500", false },
                    { 255, 263, "https://picsum.photos/seed/hotel-263-3/800/500", false },
                    { 256, 263, "https://picsum.photos/seed/hotel-263-4/800/500", false },
                    { 257, 264, "https://picsum.photos/seed/hotel-264-1/800/500", true },
                    { 258, 264, "https://picsum.photos/seed/hotel-264-2/800/500", false },
                    { 259, 264, "https://picsum.photos/seed/hotel-264-3/800/500", false },
                    { 260, 264, "https://picsum.photos/seed/hotel-264-4/800/500", false },
                    { 261, 265, "https://picsum.photos/seed/hotel-265-1/800/500", true },
                    { 262, 265, "https://picsum.photos/seed/hotel-265-2/800/500", false },
                    { 263, 265, "https://picsum.photos/seed/hotel-265-3/800/500", false },
                    { 264, 265, "https://picsum.photos/seed/hotel-265-4/800/500", false },
                    { 265, 266, "https://picsum.photos/seed/hotel-266-1/800/500", true },
                    { 266, 266, "https://picsum.photos/seed/hotel-266-2/800/500", false },
                    { 267, 266, "https://picsum.photos/seed/hotel-266-3/800/500", false },
                    { 268, 266, "https://picsum.photos/seed/hotel-266-4/800/500", false },
                    { 269, 267, "https://picsum.photos/seed/hotel-267-1/800/500", true },
                    { 270, 267, "https://picsum.photos/seed/hotel-267-2/800/500", false },
                    { 271, 267, "https://picsum.photos/seed/hotel-267-3/800/500", false },
                    { 272, 267, "https://picsum.photos/seed/hotel-267-4/800/500", false },
                    { 273, 268, "https://picsum.photos/seed/hotel-268-1/800/500", true },
                    { 274, 268, "https://picsum.photos/seed/hotel-268-2/800/500", false },
                    { 275, 268, "https://picsum.photos/seed/hotel-268-3/800/500", false },
                    { 276, 268, "https://picsum.photos/seed/hotel-268-4/800/500", false },
                    { 277, 560, "https://picsum.photos/seed/hotel-560-1/800/500", true },
                    { 278, 560, "https://picsum.photos/seed/hotel-560-2/800/500", false },
                    { 279, 560, "https://picsum.photos/seed/hotel-560-3/800/500", false },
                    { 280, 560, "https://picsum.photos/seed/hotel-560-4/800/500", false },
                    { 281, 561, "https://picsum.photos/seed/hotel-561-1/800/500", true },
                    { 282, 561, "https://picsum.photos/seed/hotel-561-2/800/500", false },
                    { 283, 561, "https://picsum.photos/seed/hotel-561-3/800/500", false },
                    { 284, 561, "https://picsum.photos/seed/hotel-561-4/800/500", false },
                    { 285, 562, "https://picsum.photos/seed/hotel-562-1/800/500", true },
                    { 286, 562, "https://picsum.photos/seed/hotel-562-2/800/500", false },
                    { 287, 562, "https://picsum.photos/seed/hotel-562-3/800/500", false },
                    { 288, 562, "https://picsum.photos/seed/hotel-562-4/800/500", false },
                    { 289, 269, "https://picsum.photos/seed/hotel-269-1/800/500", true },
                    { 290, 269, "https://picsum.photos/seed/hotel-269-2/800/500", false },
                    { 291, 269, "https://picsum.photos/seed/hotel-269-3/800/500", false },
                    { 292, 269, "https://picsum.photos/seed/hotel-269-4/800/500", false },
                    { 293, 270, "https://picsum.photos/seed/hotel-270-1/800/500", true },
                    { 294, 270, "https://picsum.photos/seed/hotel-270-2/800/500", false },
                    { 295, 270, "https://picsum.photos/seed/hotel-270-3/800/500", false },
                    { 296, 270, "https://picsum.photos/seed/hotel-270-4/800/500", false },
                    { 297, 271, "https://picsum.photos/seed/hotel-271-1/800/500", true },
                    { 298, 271, "https://picsum.photos/seed/hotel-271-2/800/500", false },
                    { 299, 271, "https://picsum.photos/seed/hotel-271-3/800/500", false },
                    { 300, 271, "https://picsum.photos/seed/hotel-271-4/800/500", false },
                    { 301, 272, "https://picsum.photos/seed/hotel-272-1/800/500", true },
                    { 302, 272, "https://picsum.photos/seed/hotel-272-2/800/500", false },
                    { 303, 272, "https://picsum.photos/seed/hotel-272-3/800/500", false },
                    { 304, 272, "https://picsum.photos/seed/hotel-272-4/800/500", false },
                    { 305, 273, "https://picsum.photos/seed/hotel-273-1/800/500", true },
                    { 306, 273, "https://picsum.photos/seed/hotel-273-2/800/500", false },
                    { 307, 273, "https://picsum.photos/seed/hotel-273-3/800/500", false },
                    { 308, 273, "https://picsum.photos/seed/hotel-273-4/800/500", false },
                    { 309, 274, "https://picsum.photos/seed/hotel-274-1/800/500", true },
                    { 310, 274, "https://picsum.photos/seed/hotel-274-2/800/500", false },
                    { 311, 274, "https://picsum.photos/seed/hotel-274-3/800/500", false },
                    { 312, 274, "https://picsum.photos/seed/hotel-274-4/800/500", false },
                    { 313, 563, "https://picsum.photos/seed/hotel-563-1/800/500", true },
                    { 314, 563, "https://picsum.photos/seed/hotel-563-2/800/500", false },
                    { 315, 563, "https://picsum.photos/seed/hotel-563-3/800/500", false },
                    { 316, 563, "https://picsum.photos/seed/hotel-563-4/800/500", false },
                    { 317, 564, "https://picsum.photos/seed/hotel-564-1/800/500", true },
                    { 318, 564, "https://picsum.photos/seed/hotel-564-2/800/500", false },
                    { 319, 564, "https://picsum.photos/seed/hotel-564-3/800/500", false },
                    { 320, 564, "https://picsum.photos/seed/hotel-564-4/800/500", false },
                    { 321, 565, "https://picsum.photos/seed/hotel-565-1/800/500", true },
                    { 322, 565, "https://picsum.photos/seed/hotel-565-2/800/500", false },
                    { 323, 565, "https://picsum.photos/seed/hotel-565-3/800/500", false },
                    { 324, 565, "https://picsum.photos/seed/hotel-565-4/800/500", false },
                    { 325, 275, "https://picsum.photos/seed/hotel-275-1/800/500", true },
                    { 326, 275, "https://picsum.photos/seed/hotel-275-2/800/500", false },
                    { 327, 275, "https://picsum.photos/seed/hotel-275-3/800/500", false },
                    { 328, 275, "https://picsum.photos/seed/hotel-275-4/800/500", false },
                    { 329, 276, "https://picsum.photos/seed/hotel-276-1/800/500", true },
                    { 330, 276, "https://picsum.photos/seed/hotel-276-2/800/500", false },
                    { 331, 276, "https://picsum.photos/seed/hotel-276-3/800/500", false },
                    { 332, 276, "https://picsum.photos/seed/hotel-276-4/800/500", false },
                    { 333, 277, "https://picsum.photos/seed/hotel-277-1/800/500", true },
                    { 334, 277, "https://picsum.photos/seed/hotel-277-2/800/500", false },
                    { 335, 277, "https://picsum.photos/seed/hotel-277-3/800/500", false },
                    { 336, 277, "https://picsum.photos/seed/hotel-277-4/800/500", false },
                    { 337, 278, "https://picsum.photos/seed/hotel-278-1/800/500", true },
                    { 338, 278, "https://picsum.photos/seed/hotel-278-2/800/500", false },
                    { 339, 278, "https://picsum.photos/seed/hotel-278-3/800/500", false },
                    { 340, 278, "https://picsum.photos/seed/hotel-278-4/800/500", false },
                    { 341, 279, "https://picsum.photos/seed/hotel-279-1/800/500", true },
                    { 342, 279, "https://picsum.photos/seed/hotel-279-2/800/500", false },
                    { 343, 279, "https://picsum.photos/seed/hotel-279-3/800/500", false },
                    { 344, 279, "https://picsum.photos/seed/hotel-279-4/800/500", false },
                    { 345, 280, "https://picsum.photos/seed/hotel-280-1/800/500", true },
                    { 346, 280, "https://picsum.photos/seed/hotel-280-2/800/500", false },
                    { 347, 280, "https://picsum.photos/seed/hotel-280-3/800/500", false },
                    { 348, 280, "https://picsum.photos/seed/hotel-280-4/800/500", false },
                    { 349, 566, "https://picsum.photos/seed/hotel-566-1/800/500", true },
                    { 350, 566, "https://picsum.photos/seed/hotel-566-2/800/500", false },
                    { 351, 566, "https://picsum.photos/seed/hotel-566-3/800/500", false },
                    { 352, 566, "https://picsum.photos/seed/hotel-566-4/800/500", false },
                    { 353, 567, "https://picsum.photos/seed/hotel-567-1/800/500", true },
                    { 354, 567, "https://picsum.photos/seed/hotel-567-2/800/500", false },
                    { 355, 567, "https://picsum.photos/seed/hotel-567-3/800/500", false },
                    { 356, 567, "https://picsum.photos/seed/hotel-567-4/800/500", false },
                    { 357, 568, "https://picsum.photos/seed/hotel-568-1/800/500", true },
                    { 358, 568, "https://picsum.photos/seed/hotel-568-2/800/500", false },
                    { 359, 568, "https://picsum.photos/seed/hotel-568-3/800/500", false },
                    { 360, 568, "https://picsum.photos/seed/hotel-568-4/800/500", false },
                    { 361, 281, "https://picsum.photos/seed/hotel-281-1/800/500", true },
                    { 362, 281, "https://picsum.photos/seed/hotel-281-2/800/500", false },
                    { 363, 281, "https://picsum.photos/seed/hotel-281-3/800/500", false },
                    { 364, 281, "https://picsum.photos/seed/hotel-281-4/800/500", false },
                    { 365, 282, "https://picsum.photos/seed/hotel-282-1/800/500", true },
                    { 366, 282, "https://picsum.photos/seed/hotel-282-2/800/500", false },
                    { 367, 282, "https://picsum.photos/seed/hotel-282-3/800/500", false },
                    { 368, 282, "https://picsum.photos/seed/hotel-282-4/800/500", false },
                    { 369, 283, "https://picsum.photos/seed/hotel-283-1/800/500", true },
                    { 370, 283, "https://picsum.photos/seed/hotel-283-2/800/500", false },
                    { 371, 283, "https://picsum.photos/seed/hotel-283-3/800/500", false },
                    { 372, 283, "https://picsum.photos/seed/hotel-283-4/800/500", false },
                    { 373, 284, "https://picsum.photos/seed/hotel-284-1/800/500", true },
                    { 374, 284, "https://picsum.photos/seed/hotel-284-2/800/500", false },
                    { 375, 284, "https://picsum.photos/seed/hotel-284-3/800/500", false },
                    { 376, 284, "https://picsum.photos/seed/hotel-284-4/800/500", false },
                    { 377, 285, "https://picsum.photos/seed/hotel-285-1/800/500", true },
                    { 378, 285, "https://picsum.photos/seed/hotel-285-2/800/500", false },
                    { 379, 285, "https://picsum.photos/seed/hotel-285-3/800/500", false },
                    { 380, 285, "https://picsum.photos/seed/hotel-285-4/800/500", false },
                    { 381, 286, "https://picsum.photos/seed/hotel-286-1/800/500", true },
                    { 382, 286, "https://picsum.photos/seed/hotel-286-2/800/500", false },
                    { 383, 286, "https://picsum.photos/seed/hotel-286-3/800/500", false },
                    { 384, 286, "https://picsum.photos/seed/hotel-286-4/800/500", false },
                    { 385, 570, "https://picsum.photos/seed/hotel-570-1/800/500", true },
                    { 386, 570, "https://picsum.photos/seed/hotel-570-2/800/500", false },
                    { 387, 570, "https://picsum.photos/seed/hotel-570-3/800/500", false },
                    { 388, 570, "https://picsum.photos/seed/hotel-570-4/800/500", false },
                    { 389, 571, "https://picsum.photos/seed/hotel-571-1/800/500", true },
                    { 390, 571, "https://picsum.photos/seed/hotel-571-2/800/500", false },
                    { 391, 571, "https://picsum.photos/seed/hotel-571-3/800/500", false },
                    { 392, 571, "https://picsum.photos/seed/hotel-571-4/800/500", false },
                    { 393, 572, "https://picsum.photos/seed/hotel-572-1/800/500", true },
                    { 394, 572, "https://picsum.photos/seed/hotel-572-2/800/500", false },
                    { 395, 572, "https://picsum.photos/seed/hotel-572-3/800/500", false },
                    { 396, 572, "https://picsum.photos/seed/hotel-572-4/800/500", false },
                    { 397, 287, "https://picsum.photos/seed/hotel-287-1/800/500", true },
                    { 398, 287, "https://picsum.photos/seed/hotel-287-2/800/500", false },
                    { 399, 287, "https://picsum.photos/seed/hotel-287-3/800/500", false },
                    { 400, 287, "https://picsum.photos/seed/hotel-287-4/800/500", false },
                    { 401, 288, "https://picsum.photos/seed/hotel-288-1/800/500", true },
                    { 402, 288, "https://picsum.photos/seed/hotel-288-2/800/500", false },
                    { 403, 288, "https://picsum.photos/seed/hotel-288-3/800/500", false },
                    { 404, 288, "https://picsum.photos/seed/hotel-288-4/800/500", false },
                    { 405, 289, "https://picsum.photos/seed/hotel-289-1/800/500", true },
                    { 406, 289, "https://picsum.photos/seed/hotel-289-2/800/500", false },
                    { 407, 289, "https://picsum.photos/seed/hotel-289-3/800/500", false },
                    { 408, 289, "https://picsum.photos/seed/hotel-289-4/800/500", false },
                    { 409, 290, "https://picsum.photos/seed/hotel-290-1/800/500", true },
                    { 410, 290, "https://picsum.photos/seed/hotel-290-2/800/500", false },
                    { 411, 290, "https://picsum.photos/seed/hotel-290-3/800/500", false },
                    { 412, 290, "https://picsum.photos/seed/hotel-290-4/800/500", false },
                    { 413, 291, "https://picsum.photos/seed/hotel-291-1/800/500", true },
                    { 414, 291, "https://picsum.photos/seed/hotel-291-2/800/500", false },
                    { 415, 291, "https://picsum.photos/seed/hotel-291-3/800/500", false },
                    { 416, 291, "https://picsum.photos/seed/hotel-291-4/800/500", false },
                    { 417, 292, "https://picsum.photos/seed/hotel-292-1/800/500", true },
                    { 418, 292, "https://picsum.photos/seed/hotel-292-2/800/500", false },
                    { 419, 292, "https://picsum.photos/seed/hotel-292-3/800/500", false },
                    { 420, 292, "https://picsum.photos/seed/hotel-292-4/800/500", false },
                    { 421, 573, "https://picsum.photos/seed/hotel-573-1/800/500", true },
                    { 422, 573, "https://picsum.photos/seed/hotel-573-2/800/500", false },
                    { 423, 573, "https://picsum.photos/seed/hotel-573-3/800/500", false },
                    { 424, 573, "https://picsum.photos/seed/hotel-573-4/800/500", false },
                    { 425, 574, "https://picsum.photos/seed/hotel-574-1/800/500", true },
                    { 426, 574, "https://picsum.photos/seed/hotel-574-2/800/500", false },
                    { 427, 574, "https://picsum.photos/seed/hotel-574-3/800/500", false },
                    { 428, 574, "https://picsum.photos/seed/hotel-574-4/800/500", false },
                    { 429, 575, "https://picsum.photos/seed/hotel-575-1/800/500", true },
                    { 430, 575, "https://picsum.photos/seed/hotel-575-2/800/500", false },
                    { 431, 575, "https://picsum.photos/seed/hotel-575-3/800/500", false },
                    { 432, 575, "https://picsum.photos/seed/hotel-575-4/800/500", false },
                    { 433, 293, "https://picsum.photos/seed/hotel-293-1/800/500", true },
                    { 434, 293, "https://picsum.photos/seed/hotel-293-2/800/500", false },
                    { 435, 293, "https://picsum.photos/seed/hotel-293-3/800/500", false },
                    { 436, 293, "https://picsum.photos/seed/hotel-293-4/800/500", false },
                    { 437, 294, "https://picsum.photos/seed/hotel-294-1/800/500", true },
                    { 438, 294, "https://picsum.photos/seed/hotel-294-2/800/500", false },
                    { 439, 294, "https://picsum.photos/seed/hotel-294-3/800/500", false },
                    { 440, 294, "https://picsum.photos/seed/hotel-294-4/800/500", false },
                    { 441, 295, "https://picsum.photos/seed/hotel-295-1/800/500", true },
                    { 442, 295, "https://picsum.photos/seed/hotel-295-2/800/500", false },
                    { 443, 295, "https://picsum.photos/seed/hotel-295-3/800/500", false },
                    { 444, 295, "https://picsum.photos/seed/hotel-295-4/800/500", false },
                    { 445, 296, "https://picsum.photos/seed/hotel-296-1/800/500", true },
                    { 446, 296, "https://picsum.photos/seed/hotel-296-2/800/500", false },
                    { 447, 296, "https://picsum.photos/seed/hotel-296-3/800/500", false },
                    { 448, 296, "https://picsum.photos/seed/hotel-296-4/800/500", false },
                    { 449, 297, "https://picsum.photos/seed/hotel-297-1/800/500", true },
                    { 450, 297, "https://picsum.photos/seed/hotel-297-2/800/500", false },
                    { 451, 297, "https://picsum.photos/seed/hotel-297-3/800/500", false },
                    { 452, 297, "https://picsum.photos/seed/hotel-297-4/800/500", false },
                    { 453, 298, "https://picsum.photos/seed/hotel-298-1/800/500", true },
                    { 454, 298, "https://picsum.photos/seed/hotel-298-2/800/500", false },
                    { 455, 298, "https://picsum.photos/seed/hotel-298-3/800/500", false },
                    { 456, 298, "https://picsum.photos/seed/hotel-298-4/800/500", false },
                    { 457, 576, "https://picsum.photos/seed/hotel-576-1/800/500", true },
                    { 458, 576, "https://picsum.photos/seed/hotel-576-2/800/500", false },
                    { 459, 576, "https://picsum.photos/seed/hotel-576-3/800/500", false },
                    { 460, 576, "https://picsum.photos/seed/hotel-576-4/800/500", false },
                    { 461, 577, "https://picsum.photos/seed/hotel-577-1/800/500", true },
                    { 462, 577, "https://picsum.photos/seed/hotel-577-2/800/500", false },
                    { 463, 577, "https://picsum.photos/seed/hotel-577-3/800/500", false },
                    { 464, 577, "https://picsum.photos/seed/hotel-577-4/800/500", false },
                    { 465, 578, "https://picsum.photos/seed/hotel-578-1/800/500", true },
                    { 466, 578, "https://picsum.photos/seed/hotel-578-2/800/500", false },
                    { 467, 578, "https://picsum.photos/seed/hotel-578-3/800/500", false },
                    { 468, 578, "https://picsum.photos/seed/hotel-578-4/800/500", false },
                    { 469, 299, "https://picsum.photos/seed/hotel-299-1/800/500", true },
                    { 470, 299, "https://picsum.photos/seed/hotel-299-2/800/500", false },
                    { 471, 299, "https://picsum.photos/seed/hotel-299-3/800/500", false },
                    { 472, 299, "https://picsum.photos/seed/hotel-299-4/800/500", false },
                    { 473, 300, "https://picsum.photos/seed/hotel-300-1/800/500", true },
                    { 474, 300, "https://picsum.photos/seed/hotel-300-2/800/500", false },
                    { 475, 300, "https://picsum.photos/seed/hotel-300-3/800/500", false },
                    { 476, 300, "https://picsum.photos/seed/hotel-300-4/800/500", false },
                    { 477, 301, "https://picsum.photos/seed/hotel-301-1/800/500", true },
                    { 478, 301, "https://picsum.photos/seed/hotel-301-2/800/500", false },
                    { 479, 301, "https://picsum.photos/seed/hotel-301-3/800/500", false },
                    { 480, 301, "https://picsum.photos/seed/hotel-301-4/800/500", false },
                    { 481, 302, "https://picsum.photos/seed/hotel-302-1/800/500", true },
                    { 482, 302, "https://picsum.photos/seed/hotel-302-2/800/500", false },
                    { 483, 302, "https://picsum.photos/seed/hotel-302-3/800/500", false },
                    { 484, 302, "https://picsum.photos/seed/hotel-302-4/800/500", false },
                    { 485, 303, "https://picsum.photos/seed/hotel-303-1/800/500", true },
                    { 486, 303, "https://picsum.photos/seed/hotel-303-2/800/500", false },
                    { 487, 303, "https://picsum.photos/seed/hotel-303-3/800/500", false },
                    { 488, 303, "https://picsum.photos/seed/hotel-303-4/800/500", false },
                    { 489, 304, "https://picsum.photos/seed/hotel-304-1/800/500", true },
                    { 490, 304, "https://picsum.photos/seed/hotel-304-2/800/500", false },
                    { 491, 304, "https://picsum.photos/seed/hotel-304-3/800/500", false },
                    { 492, 304, "https://picsum.photos/seed/hotel-304-4/800/500", false },
                    { 493, 579, "https://picsum.photos/seed/hotel-579-1/800/500", true },
                    { 494, 579, "https://picsum.photos/seed/hotel-579-2/800/500", false },
                    { 495, 579, "https://picsum.photos/seed/hotel-579-3/800/500", false },
                    { 496, 579, "https://picsum.photos/seed/hotel-579-4/800/500", false },
                    { 497, 580, "https://picsum.photos/seed/hotel-580-1/800/500", true },
                    { 498, 580, "https://picsum.photos/seed/hotel-580-2/800/500", false },
                    { 499, 580, "https://picsum.photos/seed/hotel-580-3/800/500", false },
                    { 500, 580, "https://picsum.photos/seed/hotel-580-4/800/500", false },
                    { 501, 581, "https://picsum.photos/seed/hotel-581-1/800/500", true },
                    { 502, 581, "https://picsum.photos/seed/hotel-581-2/800/500", false },
                    { 503, 581, "https://picsum.photos/seed/hotel-581-3/800/500", false },
                    { 504, 581, "https://picsum.photos/seed/hotel-581-4/800/500", false },
                    { 505, 305, "https://picsum.photos/seed/hotel-305-1/800/500", true },
                    { 506, 305, "https://picsum.photos/seed/hotel-305-2/800/500", false },
                    { 507, 305, "https://picsum.photos/seed/hotel-305-3/800/500", false },
                    { 508, 305, "https://picsum.photos/seed/hotel-305-4/800/500", false },
                    { 509, 306, "https://picsum.photos/seed/hotel-306-1/800/500", true },
                    { 510, 306, "https://picsum.photos/seed/hotel-306-2/800/500", false },
                    { 511, 306, "https://picsum.photos/seed/hotel-306-3/800/500", false },
                    { 512, 306, "https://picsum.photos/seed/hotel-306-4/800/500", false },
                    { 513, 307, "https://picsum.photos/seed/hotel-307-1/800/500", true },
                    { 514, 307, "https://picsum.photos/seed/hotel-307-2/800/500", false },
                    { 515, 307, "https://picsum.photos/seed/hotel-307-3/800/500", false },
                    { 516, 307, "https://picsum.photos/seed/hotel-307-4/800/500", false },
                    { 517, 308, "https://picsum.photos/seed/hotel-308-1/800/500", true },
                    { 518, 308, "https://picsum.photos/seed/hotel-308-2/800/500", false },
                    { 519, 308, "https://picsum.photos/seed/hotel-308-3/800/500", false },
                    { 520, 308, "https://picsum.photos/seed/hotel-308-4/800/500", false },
                    { 521, 309, "https://picsum.photos/seed/hotel-309-1/800/500", true },
                    { 522, 309, "https://picsum.photos/seed/hotel-309-2/800/500", false },
                    { 523, 309, "https://picsum.photos/seed/hotel-309-3/800/500", false },
                    { 524, 309, "https://picsum.photos/seed/hotel-309-4/800/500", false },
                    { 525, 310, "https://picsum.photos/seed/hotel-310-1/800/500", true },
                    { 526, 310, "https://picsum.photos/seed/hotel-310-2/800/500", false },
                    { 527, 310, "https://picsum.photos/seed/hotel-310-3/800/500", false },
                    { 528, 310, "https://picsum.photos/seed/hotel-310-4/800/500", false },
                    { 529, 600, "https://picsum.photos/seed/hotel-600-1/800/500", true },
                    { 530, 600, "https://picsum.photos/seed/hotel-600-2/800/500", false },
                    { 531, 600, "https://picsum.photos/seed/hotel-600-3/800/500", false },
                    { 532, 600, "https://picsum.photos/seed/hotel-600-4/800/500", false },
                    { 533, 601, "https://picsum.photos/seed/hotel-601-1/800/500", true },
                    { 534, 601, "https://picsum.photos/seed/hotel-601-2/800/500", false },
                    { 535, 601, "https://picsum.photos/seed/hotel-601-3/800/500", false },
                    { 536, 601, "https://picsum.photos/seed/hotel-601-4/800/500", false },
                    { 537, 602, "https://picsum.photos/seed/hotel-602-1/800/500", true },
                    { 538, 602, "https://picsum.photos/seed/hotel-602-2/800/500", false },
                    { 539, 602, "https://picsum.photos/seed/hotel-602-3/800/500", false },
                    { 540, 602, "https://picsum.photos/seed/hotel-602-4/800/500", false },
                    { 541, 311, "https://picsum.photos/seed/hotel-311-1/800/500", true },
                    { 542, 311, "https://picsum.photos/seed/hotel-311-2/800/500", false },
                    { 543, 311, "https://picsum.photos/seed/hotel-311-3/800/500", false },
                    { 544, 311, "https://picsum.photos/seed/hotel-311-4/800/500", false },
                    { 545, 312, "https://picsum.photos/seed/hotel-312-1/800/500", true },
                    { 546, 312, "https://picsum.photos/seed/hotel-312-2/800/500", false },
                    { 547, 312, "https://picsum.photos/seed/hotel-312-3/800/500", false },
                    { 548, 312, "https://picsum.photos/seed/hotel-312-4/800/500", false },
                    { 549, 313, "https://picsum.photos/seed/hotel-313-1/800/500", true },
                    { 550, 313, "https://picsum.photos/seed/hotel-313-2/800/500", false },
                    { 551, 313, "https://picsum.photos/seed/hotel-313-3/800/500", false },
                    { 552, 313, "https://picsum.photos/seed/hotel-313-4/800/500", false },
                    { 553, 314, "https://picsum.photos/seed/hotel-314-1/800/500", true },
                    { 554, 314, "https://picsum.photos/seed/hotel-314-2/800/500", false },
                    { 555, 314, "https://picsum.photos/seed/hotel-314-3/800/500", false },
                    { 556, 314, "https://picsum.photos/seed/hotel-314-4/800/500", false },
                    { 557, 315, "https://picsum.photos/seed/hotel-315-1/800/500", true },
                    { 558, 315, "https://picsum.photos/seed/hotel-315-2/800/500", false },
                    { 559, 315, "https://picsum.photos/seed/hotel-315-3/800/500", false },
                    { 560, 315, "https://picsum.photos/seed/hotel-315-4/800/500", false },
                    { 561, 316, "https://picsum.photos/seed/hotel-316-1/800/500", true },
                    { 562, 316, "https://picsum.photos/seed/hotel-316-2/800/500", false },
                    { 563, 316, "https://picsum.photos/seed/hotel-316-3/800/500", false },
                    { 564, 316, "https://picsum.photos/seed/hotel-316-4/800/500", false },
                    { 565, 603, "https://picsum.photos/seed/hotel-603-1/800/500", true },
                    { 566, 603, "https://picsum.photos/seed/hotel-603-2/800/500", false },
                    { 567, 603, "https://picsum.photos/seed/hotel-603-3/800/500", false },
                    { 568, 603, "https://picsum.photos/seed/hotel-603-4/800/500", false },
                    { 569, 604, "https://picsum.photos/seed/hotel-604-1/800/500", true },
                    { 570, 604, "https://picsum.photos/seed/hotel-604-2/800/500", false },
                    { 571, 604, "https://picsum.photos/seed/hotel-604-3/800/500", false },
                    { 572, 604, "https://picsum.photos/seed/hotel-604-4/800/500", false },
                    { 573, 605, "https://picsum.photos/seed/hotel-605-1/800/500", true },
                    { 574, 605, "https://picsum.photos/seed/hotel-605-2/800/500", false },
                    { 575, 605, "https://picsum.photos/seed/hotel-605-3/800/500", false },
                    { 576, 605, "https://picsum.photos/seed/hotel-605-4/800/500", false },
                    { 577, 317, "https://picsum.photos/seed/hotel-317-1/800/500", true },
                    { 578, 317, "https://picsum.photos/seed/hotel-317-2/800/500", false },
                    { 579, 317, "https://picsum.photos/seed/hotel-317-3/800/500", false },
                    { 580, 317, "https://picsum.photos/seed/hotel-317-4/800/500", false },
                    { 581, 318, "https://picsum.photos/seed/hotel-318-1/800/500", true },
                    { 582, 318, "https://picsum.photos/seed/hotel-318-2/800/500", false },
                    { 583, 318, "https://picsum.photos/seed/hotel-318-3/800/500", false },
                    { 584, 318, "https://picsum.photos/seed/hotel-318-4/800/500", false },
                    { 585, 319, "https://picsum.photos/seed/hotel-319-1/800/500", true },
                    { 586, 319, "https://picsum.photos/seed/hotel-319-2/800/500", false },
                    { 587, 319, "https://picsum.photos/seed/hotel-319-3/800/500", false },
                    { 588, 319, "https://picsum.photos/seed/hotel-319-4/800/500", false },
                    { 589, 320, "https://picsum.photos/seed/hotel-320-1/800/500", true },
                    { 590, 320, "https://picsum.photos/seed/hotel-320-2/800/500", false },
                    { 591, 320, "https://picsum.photos/seed/hotel-320-3/800/500", false },
                    { 592, 320, "https://picsum.photos/seed/hotel-320-4/800/500", false },
                    { 593, 321, "https://picsum.photos/seed/hotel-321-1/800/500", true },
                    { 594, 321, "https://picsum.photos/seed/hotel-321-2/800/500", false },
                    { 595, 321, "https://picsum.photos/seed/hotel-321-3/800/500", false },
                    { 596, 321, "https://picsum.photos/seed/hotel-321-4/800/500", false },
                    { 597, 322, "https://picsum.photos/seed/hotel-322-1/800/500", true },
                    { 598, 322, "https://picsum.photos/seed/hotel-322-2/800/500", false },
                    { 599, 322, "https://picsum.photos/seed/hotel-322-3/800/500", false },
                    { 600, 322, "https://picsum.photos/seed/hotel-322-4/800/500", false },
                    { 601, 606, "https://picsum.photos/seed/hotel-606-1/800/500", true },
                    { 602, 606, "https://picsum.photos/seed/hotel-606-2/800/500", false },
                    { 603, 606, "https://picsum.photos/seed/hotel-606-3/800/500", false },
                    { 604, 606, "https://picsum.photos/seed/hotel-606-4/800/500", false },
                    { 605, 607, "https://picsum.photos/seed/hotel-607-1/800/500", true },
                    { 606, 607, "https://picsum.photos/seed/hotel-607-2/800/500", false },
                    { 607, 607, "https://picsum.photos/seed/hotel-607-3/800/500", false },
                    { 608, 607, "https://picsum.photos/seed/hotel-607-4/800/500", false },
                    { 609, 608, "https://picsum.photos/seed/hotel-608-1/800/500", true },
                    { 610, 608, "https://picsum.photos/seed/hotel-608-2/800/500", false },
                    { 611, 608, "https://picsum.photos/seed/hotel-608-3/800/500", false },
                    { 612, 608, "https://picsum.photos/seed/hotel-608-4/800/500", false },
                    { 613, 323, "https://picsum.photos/seed/hotel-323-1/800/500", true },
                    { 614, 323, "https://picsum.photos/seed/hotel-323-2/800/500", false },
                    { 615, 323, "https://picsum.photos/seed/hotel-323-3/800/500", false },
                    { 616, 323, "https://picsum.photos/seed/hotel-323-4/800/500", false },
                    { 617, 324, "https://picsum.photos/seed/hotel-324-1/800/500", true },
                    { 618, 324, "https://picsum.photos/seed/hotel-324-2/800/500", false },
                    { 619, 324, "https://picsum.photos/seed/hotel-324-3/800/500", false },
                    { 620, 324, "https://picsum.photos/seed/hotel-324-4/800/500", false },
                    { 621, 325, "https://picsum.photos/seed/hotel-325-1/800/500", true },
                    { 622, 325, "https://picsum.photos/seed/hotel-325-2/800/500", false },
                    { 623, 325, "https://picsum.photos/seed/hotel-325-3/800/500", false },
                    { 624, 325, "https://picsum.photos/seed/hotel-325-4/800/500", false },
                    { 625, 326, "https://picsum.photos/seed/hotel-326-1/800/500", true },
                    { 626, 326, "https://picsum.photos/seed/hotel-326-2/800/500", false },
                    { 627, 326, "https://picsum.photos/seed/hotel-326-3/800/500", false },
                    { 628, 326, "https://picsum.photos/seed/hotel-326-4/800/500", false },
                    { 629, 327, "https://picsum.photos/seed/hotel-327-1/800/500", true },
                    { 630, 327, "https://picsum.photos/seed/hotel-327-2/800/500", false },
                    { 631, 327, "https://picsum.photos/seed/hotel-327-3/800/500", false },
                    { 632, 327, "https://picsum.photos/seed/hotel-327-4/800/500", false },
                    { 633, 328, "https://picsum.photos/seed/hotel-328-1/800/500", true },
                    { 634, 328, "https://picsum.photos/seed/hotel-328-2/800/500", false },
                    { 635, 328, "https://picsum.photos/seed/hotel-328-3/800/500", false },
                    { 636, 328, "https://picsum.photos/seed/hotel-328-4/800/500", false },
                    { 637, 609, "https://picsum.photos/seed/hotel-609-1/800/500", true },
                    { 638, 609, "https://picsum.photos/seed/hotel-609-2/800/500", false },
                    { 639, 609, "https://picsum.photos/seed/hotel-609-3/800/500", false },
                    { 640, 609, "https://picsum.photos/seed/hotel-609-4/800/500", false },
                    { 641, 610, "https://picsum.photos/seed/hotel-610-1/800/500", true },
                    { 642, 610, "https://picsum.photos/seed/hotel-610-2/800/500", false },
                    { 643, 610, "https://picsum.photos/seed/hotel-610-3/800/500", false },
                    { 644, 610, "https://picsum.photos/seed/hotel-610-4/800/500", false },
                    { 645, 611, "https://picsum.photos/seed/hotel-611-1/800/500", true },
                    { 646, 611, "https://picsum.photos/seed/hotel-611-2/800/500", false },
                    { 647, 611, "https://picsum.photos/seed/hotel-611-3/800/500", false },
                    { 648, 611, "https://picsum.photos/seed/hotel-611-4/800/500", false }
                });

            migrationBuilder.InsertData(
                table: "HotelRooms",
                columns: new[] { "HotelId", "RoomId", "RoomsLeft" },
                values: new object[,]
                {
                    { 100, 1, 5 },
                    { 100, 2, 8 },
                    { 100, 3, 19 },
                    { 100, 4, 20 },
                    { 101, 1, 19 },
                    { 101, 2, 4 },
                    { 101, 3, 15 },
                    { 101, 4, 18 },
                    { 102, 1, 14 },
                    { 102, 2, 9 },
                    { 102, 3, 19 },
                    { 102, 4, 6 },
                    { 103, 1, 10 },
                    { 103, 2, 12 },
                    { 103, 3, 5 },
                    { 103, 4, 8 },
                    { 104, 1, 12 },
                    { 104, 2, 5 },
                    { 104, 3, 12 },
                    { 104, 4, 17 },
                    { 105, 1, 3 },
                    { 105, 2, 19 },
                    { 105, 3, 10 },
                    { 105, 4, 17 },
                    { 106, 1, 7 },
                    { 106, 2, 14 },
                    { 106, 3, 17 },
                    { 106, 4, 3 },
                    { 107, 1, 5 },
                    { 107, 2, 16 },
                    { 107, 3, 5 },
                    { 107, 4, 5 },
                    { 108, 1, 3 },
                    { 108, 2, 17 },
                    { 108, 3, 4 },
                    { 108, 4, 13 },
                    { 110, 1, 7 },
                    { 110, 2, 4 },
                    { 110, 3, 8 },
                    { 110, 4, 8 },
                    { 111, 1, 18 },
                    { 111, 2, 13 },
                    { 111, 3, 8 },
                    { 111, 4, 12 },
                    { 112, 1, 4 },
                    { 112, 2, 20 },
                    { 112, 3, 14 },
                    { 112, 4, 6 },
                    { 113, 1, 4 },
                    { 113, 2, 20 },
                    { 113, 3, 17 },
                    { 113, 4, 18 },
                    { 114, 1, 3 },
                    { 114, 2, 7 },
                    { 114, 3, 18 },
                    { 114, 4, 7 },
                    { 115, 1, 18 },
                    { 115, 2, 4 },
                    { 115, 3, 16 },
                    { 115, 4, 8 },
                    { 116, 1, 7 },
                    { 116, 2, 14 },
                    { 116, 3, 15 },
                    { 116, 4, 18 },
                    { 117, 1, 6 },
                    { 117, 2, 17 },
                    { 117, 3, 6 },
                    { 117, 4, 14 },
                    { 118, 1, 10 },
                    { 118, 2, 13 },
                    { 118, 3, 3 },
                    { 118, 4, 10 },
                    { 120, 1, 17 },
                    { 120, 2, 6 },
                    { 120, 3, 19 },
                    { 120, 4, 10 },
                    { 121, 1, 14 },
                    { 121, 2, 18 },
                    { 121, 3, 15 },
                    { 121, 4, 8 },
                    { 122, 1, 20 },
                    { 122, 2, 14 },
                    { 122, 3, 19 },
                    { 122, 4, 5 },
                    { 123, 1, 20 },
                    { 123, 2, 8 },
                    { 123, 3, 4 },
                    { 123, 4, 16 },
                    { 124, 1, 19 },
                    { 124, 2, 17 },
                    { 124, 3, 15 },
                    { 124, 4, 5 },
                    { 125, 1, 9 },
                    { 125, 2, 20 },
                    { 125, 3, 4 },
                    { 125, 4, 6 },
                    { 126, 1, 18 },
                    { 126, 2, 10 },
                    { 126, 3, 8 },
                    { 126, 4, 8 },
                    { 127, 1, 16 },
                    { 127, 2, 20 },
                    { 127, 3, 16 },
                    { 127, 4, 12 },
                    { 128, 1, 20 },
                    { 128, 2, 9 },
                    { 128, 3, 3 },
                    { 128, 4, 18 },
                    { 200, 1, 8 },
                    { 200, 2, 10 },
                    { 200, 3, 14 },
                    { 200, 4, 3 },
                    { 215, 1, 10 },
                    { 215, 2, 11 },
                    { 215, 3, 8 },
                    { 215, 4, 14 },
                    { 216, 1, 15 },
                    { 216, 2, 12 },
                    { 216, 3, 19 },
                    { 216, 4, 19 },
                    { 217, 1, 15 },
                    { 217, 2, 10 },
                    { 217, 3, 15 },
                    { 217, 4, 13 },
                    { 218, 1, 18 },
                    { 218, 2, 18 },
                    { 218, 3, 3 },
                    { 218, 4, 17 },
                    { 219, 1, 5 },
                    { 219, 2, 16 },
                    { 219, 3, 12 },
                    { 219, 4, 6 },
                    { 220, 1, 12 },
                    { 220, 2, 10 },
                    { 220, 3, 12 },
                    { 220, 4, 20 },
                    { 221, 1, 18 },
                    { 221, 2, 10 },
                    { 221, 3, 6 },
                    { 221, 4, 13 },
                    { 222, 1, 8 },
                    { 222, 2, 7 },
                    { 222, 3, 11 },
                    { 222, 4, 11 },
                    { 245, 1, 6 },
                    { 245, 2, 14 },
                    { 245, 3, 16 },
                    { 245, 4, 19 },
                    { 246, 1, 15 },
                    { 246, 2, 10 },
                    { 246, 3, 11 },
                    { 246, 4, 9 },
                    { 247, 1, 11 },
                    { 247, 2, 10 },
                    { 247, 3, 13 },
                    { 247, 4, 9 },
                    { 248, 1, 8 },
                    { 248, 2, 5 },
                    { 248, 3, 14 },
                    { 248, 4, 4 },
                    { 249, 1, 8 },
                    { 249, 2, 6 },
                    { 249, 3, 15 },
                    { 249, 4, 13 },
                    { 250, 1, 19 },
                    { 250, 2, 10 },
                    { 250, 3, 12 },
                    { 250, 4, 20 },
                    { 251, 1, 5 },
                    { 251, 2, 5 },
                    { 251, 3, 5 },
                    { 251, 4, 15 },
                    { 252, 1, 7 },
                    { 252, 2, 6 },
                    { 252, 3, 16 },
                    { 252, 4, 5 },
                    { 253, 1, 18 },
                    { 253, 2, 6 },
                    { 253, 3, 15 },
                    { 253, 4, 15 },
                    { 254, 1, 17 },
                    { 254, 2, 5 },
                    { 254, 3, 7 },
                    { 254, 4, 4 },
                    { 255, 1, 12 },
                    { 255, 2, 18 },
                    { 255, 3, 13 },
                    { 255, 4, 16 },
                    { 256, 1, 5 },
                    { 256, 2, 17 },
                    { 256, 3, 20 },
                    { 256, 4, 9 },
                    { 257, 1, 4 },
                    { 257, 2, 3 },
                    { 257, 3, 8 },
                    { 257, 4, 15 },
                    { 258, 1, 15 },
                    { 258, 2, 9 },
                    { 258, 3, 5 },
                    { 258, 4, 16 },
                    { 259, 1, 14 },
                    { 259, 2, 4 },
                    { 259, 3, 12 },
                    { 259, 4, 6 },
                    { 260, 1, 5 },
                    { 260, 2, 13 },
                    { 260, 3, 19 },
                    { 260, 4, 4 },
                    { 261, 1, 14 },
                    { 261, 2, 15 },
                    { 261, 3, 4 },
                    { 261, 4, 8 },
                    { 262, 1, 5 },
                    { 262, 2, 16 },
                    { 262, 3, 3 },
                    { 262, 4, 3 },
                    { 263, 1, 13 },
                    { 263, 2, 19 },
                    { 263, 3, 3 },
                    { 263, 4, 16 },
                    { 264, 1, 15 },
                    { 264, 2, 6 },
                    { 264, 3, 16 },
                    { 264, 4, 13 },
                    { 265, 1, 15 },
                    { 265, 2, 17 },
                    { 265, 3, 20 },
                    { 265, 4, 6 },
                    { 266, 1, 6 },
                    { 266, 2, 8 },
                    { 266, 3, 11 },
                    { 266, 4, 3 },
                    { 267, 1, 8 },
                    { 267, 2, 14 },
                    { 267, 3, 3 },
                    { 267, 4, 8 },
                    { 268, 1, 20 },
                    { 268, 2, 7 },
                    { 268, 3, 12 },
                    { 268, 4, 3 },
                    { 269, 1, 14 },
                    { 269, 2, 9 },
                    { 269, 3, 14 },
                    { 269, 4, 19 },
                    { 270, 1, 3 },
                    { 270, 2, 11 },
                    { 270, 3, 9 },
                    { 270, 4, 9 },
                    { 271, 1, 15 },
                    { 271, 2, 5 },
                    { 271, 3, 11 },
                    { 271, 4, 12 },
                    { 272, 1, 6 },
                    { 272, 2, 20 },
                    { 272, 3, 4 },
                    { 272, 4, 13 },
                    { 273, 1, 20 },
                    { 273, 2, 15 },
                    { 273, 3, 13 },
                    { 273, 4, 8 },
                    { 274, 1, 9 },
                    { 274, 2, 3 },
                    { 274, 3, 16 },
                    { 274, 4, 7 },
                    { 275, 1, 20 },
                    { 275, 2, 5 },
                    { 275, 3, 8 },
                    { 275, 4, 11 },
                    { 276, 1, 19 },
                    { 276, 2, 5 },
                    { 276, 3, 9 },
                    { 276, 4, 18 },
                    { 277, 1, 19 },
                    { 277, 2, 11 },
                    { 277, 3, 17 },
                    { 277, 4, 5 },
                    { 278, 1, 3 },
                    { 278, 2, 10 },
                    { 278, 3, 4 },
                    { 278, 4, 19 },
                    { 279, 1, 17 },
                    { 279, 2, 7 },
                    { 279, 3, 9 },
                    { 279, 4, 14 },
                    { 280, 1, 13 },
                    { 280, 2, 10 },
                    { 280, 3, 15 },
                    { 280, 4, 8 },
                    { 281, 1, 9 },
                    { 281, 2, 4 },
                    { 281, 3, 10 },
                    { 281, 4, 7 },
                    { 282, 1, 14 },
                    { 282, 2, 19 },
                    { 282, 3, 9 },
                    { 282, 4, 17 },
                    { 283, 1, 19 },
                    { 283, 2, 8 },
                    { 283, 3, 13 },
                    { 283, 4, 9 },
                    { 284, 1, 17 },
                    { 284, 2, 16 },
                    { 284, 3, 20 },
                    { 284, 4, 12 },
                    { 285, 1, 9 },
                    { 285, 2, 16 },
                    { 285, 3, 9 },
                    { 285, 4, 13 },
                    { 286, 1, 10 },
                    { 286, 2, 3 },
                    { 286, 3, 10 },
                    { 286, 4, 5 },
                    { 287, 1, 18 },
                    { 287, 2, 5 },
                    { 287, 3, 20 },
                    { 287, 4, 15 },
                    { 288, 1, 4 },
                    { 288, 2, 18 },
                    { 288, 3, 9 },
                    { 288, 4, 16 },
                    { 289, 1, 6 },
                    { 289, 2, 6 },
                    { 289, 3, 20 },
                    { 289, 4, 17 },
                    { 290, 1, 15 },
                    { 290, 2, 7 },
                    { 290, 3, 5 },
                    { 290, 4, 18 },
                    { 291, 1, 16 },
                    { 291, 2, 3 },
                    { 291, 3, 10 },
                    { 291, 4, 9 },
                    { 292, 1, 14 },
                    { 292, 2, 7 },
                    { 292, 3, 15 },
                    { 292, 4, 4 },
                    { 293, 1, 16 },
                    { 293, 2, 15 },
                    { 293, 3, 12 },
                    { 293, 4, 12 },
                    { 294, 1, 14 },
                    { 294, 2, 15 },
                    { 294, 3, 19 },
                    { 294, 4, 9 },
                    { 295, 1, 14 },
                    { 295, 2, 11 },
                    { 295, 3, 13 },
                    { 295, 4, 10 },
                    { 296, 1, 7 },
                    { 296, 2, 15 },
                    { 296, 3, 5 },
                    { 296, 4, 17 },
                    { 297, 1, 16 },
                    { 297, 2, 5 },
                    { 297, 3, 5 },
                    { 297, 4, 13 },
                    { 298, 1, 10 },
                    { 298, 2, 19 },
                    { 298, 3, 4 },
                    { 298, 4, 20 },
                    { 299, 1, 11 },
                    { 299, 2, 18 },
                    { 299, 3, 15 },
                    { 299, 4, 20 },
                    { 300, 1, 9 },
                    { 300, 2, 8 },
                    { 300, 3, 10 },
                    { 300, 4, 4 },
                    { 301, 1, 12 },
                    { 301, 2, 15 },
                    { 301, 3, 8 },
                    { 301, 4, 3 },
                    { 302, 1, 13 },
                    { 302, 2, 16 },
                    { 302, 3, 18 },
                    { 302, 4, 4 },
                    { 303, 1, 5 },
                    { 303, 2, 5 },
                    { 303, 3, 17 },
                    { 303, 4, 17 },
                    { 304, 1, 14 },
                    { 304, 2, 12 },
                    { 304, 3, 17 },
                    { 304, 4, 18 },
                    { 305, 1, 10 },
                    { 305, 2, 5 },
                    { 305, 3, 6 },
                    { 305, 4, 5 },
                    { 306, 1, 11 },
                    { 306, 2, 20 },
                    { 306, 3, 11 },
                    { 306, 4, 4 },
                    { 307, 1, 11 },
                    { 307, 2, 3 },
                    { 307, 3, 20 },
                    { 307, 4, 18 },
                    { 308, 1, 11 },
                    { 308, 2, 11 },
                    { 308, 3, 8 },
                    { 308, 4, 10 },
                    { 309, 1, 4 },
                    { 309, 2, 8 },
                    { 309, 3, 10 },
                    { 309, 4, 19 },
                    { 310, 1, 4 },
                    { 310, 2, 18 },
                    { 310, 3, 8 },
                    { 310, 4, 20 },
                    { 311, 1, 19 },
                    { 311, 2, 14 },
                    { 311, 3, 8 },
                    { 311, 4, 14 },
                    { 312, 1, 4 },
                    { 312, 2, 15 },
                    { 312, 3, 9 },
                    { 312, 4, 14 },
                    { 313, 1, 10 },
                    { 313, 2, 18 },
                    { 313, 3, 18 },
                    { 313, 4, 10 },
                    { 314, 1, 18 },
                    { 314, 2, 11 },
                    { 314, 3, 7 },
                    { 314, 4, 7 },
                    { 315, 1, 14 },
                    { 315, 2, 9 },
                    { 315, 3, 16 },
                    { 315, 4, 13 },
                    { 316, 1, 18 },
                    { 316, 2, 7 },
                    { 316, 3, 13 },
                    { 316, 4, 10 },
                    { 317, 1, 20 },
                    { 317, 2, 17 },
                    { 317, 3, 18 },
                    { 317, 4, 10 },
                    { 318, 1, 11 },
                    { 318, 2, 15 },
                    { 318, 3, 12 },
                    { 318, 4, 15 },
                    { 319, 1, 7 },
                    { 319, 2, 16 },
                    { 319, 3, 12 },
                    { 319, 4, 9 },
                    { 320, 1, 15 },
                    { 320, 2, 6 },
                    { 320, 3, 18 },
                    { 320, 4, 4 },
                    { 321, 1, 6 },
                    { 321, 2, 4 },
                    { 321, 3, 5 },
                    { 321, 4, 15 },
                    { 322, 1, 4 },
                    { 322, 2, 19 },
                    { 322, 3, 18 },
                    { 322, 4, 20 },
                    { 323, 1, 12 },
                    { 323, 2, 9 },
                    { 323, 3, 4 },
                    { 323, 4, 5 },
                    { 324, 1, 15 },
                    { 324, 2, 19 },
                    { 324, 3, 5 },
                    { 324, 4, 3 },
                    { 325, 1, 13 },
                    { 325, 2, 20 },
                    { 325, 3, 17 },
                    { 325, 4, 10 },
                    { 326, 1, 13 },
                    { 326, 2, 9 },
                    { 326, 3, 7 },
                    { 326, 4, 16 },
                    { 327, 1, 6 },
                    { 327, 2, 18 },
                    { 327, 3, 9 },
                    { 327, 4, 4 },
                    { 328, 1, 20 },
                    { 328, 2, 18 },
                    { 328, 3, 3 },
                    { 328, 4, 13 },
                    { 550, 1, 19 },
                    { 550, 2, 8 },
                    { 550, 3, 17 },
                    { 550, 4, 8 },
                    { 551, 1, 9 },
                    { 551, 2, 9 },
                    { 551, 3, 14 },
                    { 551, 4, 11 },
                    { 552, 1, 20 },
                    { 552, 2, 6 },
                    { 552, 3, 10 },
                    { 552, 4, 7 },
                    { 553, 1, 14 },
                    { 553, 2, 6 },
                    { 553, 3, 3 },
                    { 553, 4, 4 },
                    { 554, 1, 16 },
                    { 554, 2, 5 },
                    { 554, 3, 9 },
                    { 554, 4, 4 },
                    { 555, 1, 15 },
                    { 555, 2, 10 },
                    { 555, 3, 19 },
                    { 555, 4, 6 },
                    { 556, 1, 6 },
                    { 556, 2, 14 },
                    { 556, 3, 9 },
                    { 556, 4, 15 },
                    { 557, 1, 17 },
                    { 557, 2, 14 },
                    { 557, 3, 20 },
                    { 557, 4, 10 },
                    { 558, 1, 6 },
                    { 558, 2, 3 },
                    { 558, 3, 20 },
                    { 558, 4, 12 },
                    { 560, 1, 3 },
                    { 560, 2, 10 },
                    { 560, 3, 20 },
                    { 560, 4, 3 },
                    { 561, 1, 5 },
                    { 561, 2, 4 },
                    { 561, 3, 3 },
                    { 561, 4, 4 },
                    { 562, 1, 13 },
                    { 562, 2, 10 },
                    { 562, 3, 12 },
                    { 562, 4, 16 },
                    { 563, 1, 7 },
                    { 563, 2, 16 },
                    { 563, 3, 11 },
                    { 563, 4, 13 },
                    { 564, 1, 17 },
                    { 564, 2, 19 },
                    { 564, 3, 14 },
                    { 564, 4, 17 },
                    { 565, 1, 16 },
                    { 565, 2, 15 },
                    { 565, 3, 9 },
                    { 565, 4, 20 },
                    { 566, 1, 3 },
                    { 566, 2, 17 },
                    { 566, 3, 13 },
                    { 566, 4, 16 },
                    { 567, 1, 12 },
                    { 567, 2, 15 },
                    { 567, 3, 16 },
                    { 567, 4, 11 },
                    { 568, 1, 13 },
                    { 568, 2, 5 },
                    { 568, 3, 13 },
                    { 568, 4, 18 },
                    { 570, 1, 13 },
                    { 570, 2, 13 },
                    { 570, 3, 8 },
                    { 570, 4, 7 },
                    { 571, 1, 15 },
                    { 571, 2, 9 },
                    { 571, 3, 13 },
                    { 571, 4, 18 },
                    { 572, 1, 17 },
                    { 572, 2, 6 },
                    { 572, 3, 12 },
                    { 572, 4, 16 },
                    { 573, 1, 13 },
                    { 573, 2, 4 },
                    { 573, 3, 5 },
                    { 573, 4, 13 },
                    { 574, 1, 15 },
                    { 574, 2, 15 },
                    { 574, 3, 13 },
                    { 574, 4, 14 },
                    { 575, 1, 7 },
                    { 575, 2, 7 },
                    { 575, 3, 15 },
                    { 575, 4, 7 },
                    { 576, 1, 16 },
                    { 576, 2, 17 },
                    { 576, 3, 3 },
                    { 576, 4, 12 },
                    { 577, 1, 14 },
                    { 577, 2, 8 },
                    { 577, 3, 13 },
                    { 577, 4, 10 },
                    { 578, 1, 13 },
                    { 578, 2, 18 },
                    { 578, 3, 5 },
                    { 578, 4, 4 },
                    { 579, 1, 14 },
                    { 579, 2, 10 },
                    { 579, 3, 16 },
                    { 579, 4, 8 },
                    { 580, 1, 19 },
                    { 580, 2, 5 },
                    { 580, 3, 18 },
                    { 580, 4, 9 },
                    { 581, 1, 13 },
                    { 581, 2, 4 },
                    { 581, 3, 8 },
                    { 581, 4, 18 },
                    { 600, 1, 12 },
                    { 600, 2, 4 },
                    { 600, 3, 6 },
                    { 600, 4, 9 },
                    { 601, 1, 17 },
                    { 601, 2, 20 },
                    { 601, 3, 10 },
                    { 601, 4, 12 },
                    { 602, 1, 10 },
                    { 602, 2, 20 },
                    { 602, 3, 14 },
                    { 602, 4, 20 },
                    { 603, 1, 17 },
                    { 603, 2, 4 },
                    { 603, 3, 8 },
                    { 603, 4, 12 },
                    { 604, 1, 13 },
                    { 604, 2, 10 },
                    { 604, 3, 10 },
                    { 604, 4, 11 },
                    { 605, 1, 17 },
                    { 605, 2, 9 },
                    { 605, 3, 12 },
                    { 605, 4, 10 },
                    { 606, 1, 9 },
                    { 606, 2, 18 },
                    { 606, 3, 3 },
                    { 606, 4, 20 },
                    { 607, 1, 11 },
                    { 607, 2, 10 },
                    { 607, 3, 13 },
                    { 607, 4, 12 },
                    { 608, 1, 19 },
                    { 608, 2, 18 },
                    { 608, 3, 7 },
                    { 608, 4, 18 },
                    { 609, 1, 14 },
                    { 609, 2, 12 },
                    { 609, 3, 6 },
                    { 609, 4, 18 },
                    { 610, 1, 5 },
                    { 610, 2, 3 },
                    { 610, 3, 10 },
                    { 610, 4, 18 },
                    { 611, 1, 19 },
                    { 611, 2, 3 },
                    { 611, 3, 12 },
                    { 611, 4, 18 }
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
                    { 1, new DateTime(2026, 1, 11, 2, 46, 52, 963, DateTimeKind.Utc).AddTicks(9835), "test.pdf", 4, "Smisleno me motiviše vaša moderna organizacija i želja da radim na projektima koji imaju stvarni utjecaj. Vjerujem da mogu doprinijeti svojim radnim navikama i voljom za učenjem." },
                    { 2, new DateTime(2026, 1, 9, 2, 46, 52, 963, DateTimeKind.Utc).AddTicks(9853), "test.pdf", 5, "Privukla me prilika da radim u dinamičnom okruženju gdje se cijeni timski rad i napredak. Želim biti dio profesionalne i pozitivne radne zajednice." },
                    { 3, new DateTime(2026, 1, 6, 2, 46, 52, 963, DateTimeKind.Utc).AddTicks(9855), "test.pdf", 6, "Vaša kompanija je prepoznata po kvalitetnom radu i inovacijama. Smatram da mogu mnogo naučiti i istovremeno doprinijeti svojim iskustvom i posvećenošću." },
                    { 4, new DateTime(2026, 1, 3, 2, 46, 52, 963, DateTimeKind.Utc).AddTicks(9856), "test.pdf", 7, "Želim raditi u sredini koja podstiče razvoj i podržava kreativnost. Vaša firma upravo to nudi, i zato bih voljela biti dio vašeg tima." },
                    { 5, new DateTime(2026, 1, 1, 2, 46, 52, 963, DateTimeKind.Utc).AddTicks(9857), "test.pdf", 8, "Vašu kompaniju vidim kao mjesto gdje se talenat i rad cijene. Motivisan sam da se usavršavam i doprinosim vašem rastu." },
                    { 6, new DateTime(2025, 12, 30, 2, 46, 52, 963, DateTimeKind.Utc).AddTicks(9859), "test.pdf", 9, "Tražim priliku da radim u profesionalnoj sredini gdje mogu napredovati. Posebno me privlači vaša organizacijska kultura i pristup radu." },
                    { 7, new DateTime(2025, 12, 27, 2, 46, 52, 963, DateTimeKind.Utc).AddTicks(9860), "test.pdf", 10, "Motiviše me želja da učim nove tehnologije i doprinesem timskim rezultatima. Vjerujem da bih se dobro uklopio u vaše okruženje." },
                    { 8, new DateTime(2025, 12, 26, 2, 46, 52, 963, DateTimeKind.Utc).AddTicks(9862), "test.pdf", 11, "Smatram da je vaša kompanija idealno mjesto za profesionalni rast. Cijenim vaš pristup organizaciji i inovativnosti." },
                    { 9, new DateTime(2025, 12, 24, 2, 46, 52, 963, DateTimeKind.Utc).AddTicks(9864), "test.pdf", 12, "Zainteresovana sam za rad kod vas jer nudite stabilno i ugodno radno okruženje u kojem se prepoznaje trud i zalaganje." },
                    { 10, new DateTime(2025, 12, 22, 2, 46, 52, 963, DateTimeKind.Utc).AddTicks(9865), "test.pdf", 13, "Želim biti dio tima koji teži kvaliteti i rastu. Vaša firma mi djeluje kao pravo mjesto za to." },
                    { 11, new DateTime(2025, 12, 21, 2, 46, 52, 963, DateTimeKind.Utc).AddTicks(9866), "test.pdf", 14, "Motivisan sam mogućnošću da radim u kompaniji koja cijeni profesionalizam i timski rad. Spreman sam da dam svoj maksimum." },
                    { 12, new DateTime(2025, 12, 20, 2, 46, 52, 963, DateTimeKind.Utc).AddTicks(9868), "test.pdf", 15, "Privlači me prilika da radim sa stručnim i kreativnim timom. Vaš način rada me posebno inspiriše." },
                    { 13, new DateTime(2025, 12, 19, 2, 46, 52, 963, DateTimeKind.Utc).AddTicks(9869), "test.pdf", 16, "Vaša kompanija nudi odlične mogućnosti za rast i razvoj, što je glavni razlog moje prijave." },
                    { 14, new DateTime(2025, 12, 18, 2, 46, 52, 963, DateTimeKind.Utc).AddTicks(9871), "test.pdf", 17, "Motivisan sam da učim, radim i napredujem. Vjerujem da bih bio odličan dodatak vašem timu." },
                    { 15, new DateTime(2025, 12, 17, 2, 46, 52, 963, DateTimeKind.Utc).AddTicks(9872), "test.pdf", 18, "Želim raditi u okruženju gdje se cijeni inicijativa, kreativnost i kvalitetan rad. Vaša firma ispunjava sve te kriterije." },
                    { 16, new DateTime(2025, 12, 16, 2, 46, 52, 963, DateTimeKind.Utc).AddTicks(9874), "test.pdf", 19, "Vidim veliku vrijednost u vašim projektima i načinu rada. Želim biti dio tima koji radi sa strašću." },
                    { 17, new DateTime(2025, 12, 15, 2, 46, 52, 963, DateTimeKind.Utc).AddTicks(9875), "test.pdf", 20, "Prijavljujem se jer vjerujem da bih u vašoj kompaniji mogao postići veliki profesionalni iskorak." },
                    { 18, new DateTime(2025, 12, 14, 2, 46, 52, 963, DateTimeKind.Utc).AddTicks(9876), "test.pdf", 21, "Motiviše me želja da radim u stabilnoj i ozbiljnoj organizaciji koja nudi perspektivu i razvoj." },
                    { 19, new DateTime(2025, 12, 13, 2, 46, 52, 963, DateTimeKind.Utc).AddTicks(9878), "test.pdf", 22, "Vaša kompanija mi djeluje kao pravo mjesto da pokažem svoje vještine i dodatno ih unaprijedim." },
                    { 20, new DateTime(2025, 12, 12, 2, 46, 52, 963, DateTimeKind.Utc).AddTicks(9879), "test.pdf", 23, "Privlači me vaš profesionalan pristup, moderna organizacija i atmosfera koja podstiče učenje." },
                    { 21, new DateTime(2025, 12, 11, 2, 46, 52, 963, DateTimeKind.Utc).AddTicks(9881), "test.pdf", 24, "Motivisana sam da radim u vašoj firmi jer cijenim vaše vrijednosti i način poslovanja. Vjerujem da bih se idealno uklopila." }
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
                    { 3, "Firenca je čarobna! Hrana, arhitektura i atmosfera su nevjerovatni.", 1, 5, 6 },
                    { 4, "Santorini je san! Zalazak sunca u Oiji je nešto posebno.", 2, 5, 7 },
                    { 5, "Prelijepo ostrvo, ali dosta turista. Organizacija korektna.", 2, 4, 8 },
                    { 6, "Istanbul nudi spoj istoka i zapada. Tura je bila vrlo zanimljiva.", 3, 5, 9 },
                    { 7, "Dobra cijena za ono što se dobije. Posebno mi se dopao Bosfor.", 3, 4, 10 },
                    { 8, "Barcelona je bila fantastična! Hotel blizu centra.", 4, 5, 11 },
                    { 9, "Grad pun energije. Raspored obilazaka dobro isplaniran.", 4, 4, 12 },
                    { 10, "Grad svjetlosti je ispunio sva očekivanja!", 5, 5, 13 },
                    { 11, "Skupo, ali vrijedilo je svakog dinara.", 5, 5, 14 },
                    { 12, "Prelijep stari grad i romantična atmosfera.", 6, 5, 15 },
                    { 13, "Odličan omjer cijene i kvaliteta.", 6, 4, 16 },
                    { 14, "Mirno i ugodno putovanje, savršeno za vikend bijeg.", 7, 4, 17 },
                    { 15, "Beč je elegantan i kulturno bogat grad.", 7, 5, 18 },
                    { 16, "Kanali i arhitektura su predivni. Preporučujem!", 8, 5, 19 },
                    { 17, "Grad je zanimljiv, ali dosta gužve.", 8, 4, 20 },
                    { 18, "London je impresivan, puno znamenitosti.", 9, 5, 21 },
                    { 19, "Kratko putovanje, ali vrlo sadržajno.", 9, 4, 22 },
                    { 20, "Dubai je nevjerovatno iskustvo! Top organizacija.", 10, 5, 23 },
                    { 21, "Luksuz na svakom koraku. Hotel vrhunski.", 10, 5, 24 },
                    { 22, "Piramide su fascinantne. Putovanje vrijedno iskustva.", 11, 5, 5 },
                    { 23, "Zanimljivo, ali dosta naporno zbog vrućine.", 11, 4, 6 },
                    { 24, "Lijep grad i odlična atmosfera.", 12, 4, 7 },
                    { 25, "Termalne banje su pun pogodak.", 12, 5, 8 },
                    { 26, "Historijski vrlo zanimljiv grad.", 13, 5, 9 },
                    { 27, "Dobra organizacija i povoljne cijene.", 13, 4, 10 },
                    { 28, "Rajska destinacija! Plaže su nestvarne.", 14, 5, 11 },
                    { 29, "Savršeno za odmor i opuštanje.", 14, 5, 12 },
                    { 30, "More i resort su bili odlični.", 15, 4, 13 },
                    { 31, "Idealan izbor za porodični odmor.", 15, 5, 14 },
                    { 32, "Lisabon je šarmantan i topao grad.", 16, 5, 15 },
                    { 33, "Preporučujem svima koji vole opuštena putovanja.", 16, 4, 16 },
                    { 34, "Akropolj je nešto što se mora vidjeti.", 17, 5, 17 },
                    { 35, "Dobar balans između obilazaka i slobodnog vremena.", 17, 4, 18 },
                    { 36, "Predivan grad i odlična atmosfera.", 18, 5, 19 },
                    { 37, "Idealno ljetno putovanje.", 18, 5, 20 }
                });

            migrationBuilder.InsertData(
                table: "OfferDetails",
                columns: new[] { "OfferId", "City", "Country", "Description", "MinimalPrice", "ResidenceTaxPerDay", "ResidenceTotal", "TotalCountOfReservations", "TravelInsuranceTotal" },
                values: new object[,]
                {
                    { 1, "Firenca", "Italija", "Firenca je jedno od najljepših kulturnih središta Evrope i kolijevka renesanse. Grad obiluje historijskim znamenitostima, muzejima i umjetničkim djelima svjetskog značaja. Posjetioci imaju priliku uživati u šetnjama starim gradskim ulicama i trgovima. Posebnu čar pružaju rijeka Arno i čuveni most Ponte Vecchio. Firenca je poznata i po vrhunskoj italijanskoj kuhinji i lokalnim vinima. Ovo putovanje idealno je za ljubitelje kulture, umjetnosti i istorije.", 450m, 2.00m, 10m, 98, 15m },
                    { 2, "Santorini", "Grčka", "Santorini je jedno od najromantičnijih ostrva na Mediteranu, poznato po bijelim kućama i plavim kupolama. Ostrvo pruža spektakularne zalaske sunca koji ostavljaju bez daha. Kristalno čisto more i vulkanske plaže čine ga jedinstvenim. Posjetioci mogu uživati u mirnoj atmosferi i luksuznim smještajima. Lokalna kuhinja i vina dodatno obogaćuju iskustvo boravka. Santorini je savršen izbor za opuštanje i romantična putovanja.", 900m, 3.00m, 21m, 65, 25m },
                    { 3, "Istanbul", "Turska", "Istanbul je grad koji spaja Evropu i Aziju, nudeći jedinstveno kulturno iskustvo. Bogata istorija vidljiva je na svakom koraku kroz džamije, palate i bazare. Grad je poznat po živopisnoj atmosferi i gostoprimstvu. Šetnja Bosforom pruža nezaboravne poglede na grad. Turska kuhinja i tradicionalni čajevi dodatno oplemenjuju boravak. Istanbul je idealna destinacija za ljubitelje istorije i raznolikih kultura.", 350m, 1.50m, 6m, 53, 12m },
                    { 4, "Barcelona", "Španija", "Barcelona je grad koji spaja umjetnost, arhitekturu i more u savršen sklad. Poznata je po djelima Antonija Gaudija i jedinstvenoj arhitekturi. Grad nudi živahne ulice, plaže i bogat noćni život. Posjetioci mogu uživati u tapasima i mediteranskoj kuhinji. Barcelona je savršena kombinacija odmora i urbanog doživljaja. Ova destinacija idealna je za sve generacije putnika.", 750m, 3.50m, 21m, 91, 20m },
                    { 5, "Pariz", "Francuska", "Pariz je poznat kao grad ljubavi, umjetnosti i mode. Njegove znamenitosti poput Ajfelovog tornja i Luvra privlače milione turista. Grad odiše romantikom i elegancijom. Šetnje uz Seinu pružaju poseban doživljaj. Francuska kuhinja i slastičarstvo svjetski su poznati. Pariz je savršena destinacija za romantična i kulturna putovanja.", 820m, 2.50m, 12.5m, 140, 18m },
                    { 6, "Prag", "Češka", "Prag je grad bogate istorije i prepoznatljive arhitekture. Njegovi mostovi, dvorci i stari trgovi očaravaju posjetioce. Grad nudi mirnu, ali živu atmosferu. Češka kuhinja i pivo poznati su širom svijeta. Prag je idealan za kraća gradska putovanja. Ova destinacija pruža savršen balans tradicije i modernog života.", 390m, 1.20m, 4.8m, 67, 10m },
                    { 7, "Beč", "Austrija", "Beč je grad muzike, kulture i carske istorije. Poznat je po operama, koncertima i muzejima. Grad odiše elegancijom i uređenim ambijentom. Šetnje bečkim ulicama pružaju osjećaj luksuza. Tradicionalne slastice poput Sacher torte su nezaobilazne. Beč je savršena destinacija za kulturni i opuštajući odmor.", 250m, 1.80m, 5.4m, 102, 10m },
                    { 8, "Amsterdam", "Nizozemska", "Amsterdam je poznat po svojim kanalima i jedinstvenoj gradskoj arhitekturi. Grad nudi opuštenu i kosmopolitsku atmosferu. Brojni muzeji i galerije privlače ljubitelje umjetnosti. Vožnja biciklom kroz grad je poseban doživljaj. Lokalna kuhinja i kafići doprinose ugodnom boravku. Amsterdam je idealan za urbani i alternativni odmor.", 860m, 3.00m, 15m, 119, 22m },
                    { 9, "London", "Ujedinjeno Kraljevstvo", "London je jedan od najpoznatijih svjetskih gradova sa bogatom istorijom. Grad nudi brojne znamenitosti poput Big Bena i Buckinghamske palate. Multikulturalnost Londona vidljiva je u svakodnevnom životu. Posjetioci mogu uživati u muzejima i pozorištima. Londonska kuhinja nudi raznovrsne ukuse iz cijelog svijeta. Ova destinacija savršena je za ljubitelje velikih gradova.", 950m, 3.20m, 19.2m, 155, 24m },
                    { 10, "Dubai", "UAE", "Dubai je simbol luksuza, modernog dizajna i futurističke arhitekture. Grad nudi spektakularne nebodere i tržne centre. Pustinjski safari pruža nezaboravno iskustvo avanture. Dubai je poznat po vrhunskom smještaju i uslugama. Posjetioci mogu uživati u kombinaciji tradicije i savremenog života. Ova destinacija idealna je za luksuzan i egzotičan odmor.", 1300m, 5.00m, 35m, 174, 30m },
                    { 11, "Kairo", "Egipat", "Kairo je grad bogate istorije i drevnih civilizacija. Poznat je po piramidama i sfingi koje privlače turiste iz cijelog svijeta. Grad nudi autentičan uvid u egipatsku kulturu. Šetnje uz Nil pružaju poseban doživljaj. Lokalna kuhinja obiluje tradicionalnim jelima. Kairo je savršena destinacija za ljubitelje istorije.", 780m, 2.20m, 11m, 94, 20m },
                    { 12, "Budimpešta", "Mađarska", "Budimpešta je poznata po termalnim kupalištima i arhitekturi. Grad se prostire uz rijeku Dunav i nudi prekrasne poglede. Historijske građevine čine grad veoma atraktivnim. Termalne banje pružaju opuštanje tokom cijele godine. Mađarska kuhinja dodatno obogaćuje iskustvo. Budimpešta je idealna za relaksirajući gradski odmor.", 230m, 1.50m, 4.5m, 88, 9m },
                    { 13, "Krakow", "Poljska", "Krakow je jedan od najstarijih gradova u Poljskoj sa bogatom tradicijom. Njegova stara gradska jezgra je pod zaštitom UNESCO-a. Grad odiše srednjovjekovnim šarmom. Brojni muzeji i dvorci privlače posjetioce. Lokalna kultura i gastronomija su veoma autentični. Krakow je idealan za kulturna putovanja.", 310m, 1.40m, 5.6m, 73, 11m },
                    { 14, "Zanzibar", "Tanzanija", "Zanzibar je egzotična destinacija sa bijelim pješčanim plažama. Ostrvo nudi kristalno čisto more i tropsku klimu. Idealno je za odmor i bijeg od svakodnevnice. Lokalna kultura je spoj afričkih i arapskih uticaja. Posjetioci mogu uživati u ronjenju i izletima. Zanzibar je savršen izbor za luksuzan i opuštajući odmor.", 1600m, 4.00m, 32m, 61, 35m },
                    { 15, "Hurgada", "Egipat", "Hurgada je popularna destinacija na Crvenom moru. Poznata je po all inclusive resortima i sunčanim plažama. Kristalno more idealno je za ronjenje i snorkeling. Grad nudi brojne izlete i aktivnosti. Klima je povoljna tokom cijele godine. Hurgada je savršen izbor za porodični odmor.", 1100m, 3.00m, 21m, 108, 25m },
                    { 16, "Lisabon", "Portugal", "Lisabon je šarmantan grad sa bogatom pomorskom istorijom. Njegove ulice i tramvaji nude poseban ugođaj. Grad se nalazi na obali Atlantika sa prelijepim pogledima. Lokalna kuhinja je bogata morskim specijalitetima. Lisabon nudi opuštenu atmosferu. Idealna je destinacija za gradski i kulturni odmor.", 780m, 2.50m, 15m, 97, 22m },
                    { 17, "Atina", "Grčka", "Atina je kolijevka zapadne civilizacije i demokratije. Grad je prepun antičkih spomenika i muzeja. Akropolj je nezaobilazna znamenitost. Grad nudi spoj istorije i modernog života. Grčka kuhinja je poznata i ukusna. Atina je savršena destinacija za ljubitelje istorije.", 520m, 2.00m, 10m, 121, 16m },
                    { 18, "Split", "Hrvatska", "Split je prelijep grad na jadranskoj obali. Poznat je po Dioklecijanovoj palati i staroj gradskoj jezgri. Grad nudi kristalno čisto more i plaže. Mediteranska klima čini boravak ugodnim. Lokalna kuhinja obiluje morskim specijalitetima. Split je idealna destinacija za ljetni odmor.", 180m, 1.00m, 3m, 112, 8m }
                });

            migrationBuilder.InsertData(
                table: "OfferHotels",
                columns: new[] { "HotelId", "OfferDetailsId", "DepartureDate", "ReturnDate" },
                values: new object[,]
                {
                    { 100, 1, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 101, 1, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 102, 1, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 103, 1, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 104, 1, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 105, 1, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 106, 1, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 107, 1, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 108, 1, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 110, 2, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 111, 2, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 112, 2, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 113, 2, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 114, 2, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 115, 2, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 116, 2, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 117, 2, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 118, 2, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 120, 3, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 121, 3, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 122, 3, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 123, 3, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 19, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 124, 3, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 19, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 125, 3, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 19, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 126, 3, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 127, 3, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 128, 3, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 200, 4, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 215, 4, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 216, 4, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 217, 4, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 21, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 218, 4, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 21, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 219, 4, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 21, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 220, 4, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 221, 4, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 222, 4, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 245, 5, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 246, 5, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 247, 5, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 248, 5, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 249, 5, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 250, 5, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 550, 5, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 551, 5, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 552, 5, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 251, 6, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 19, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 252, 6, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 19, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 253, 6, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 19, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 254, 6, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 255, 6, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 256, 6, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 553, 6, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 554, 6, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 555, 6, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 257, 7, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 18, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 258, 7, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 18, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 259, 7, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 18, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 260, 7, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 261, 7, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 262, 7, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 556, 7, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 557, 7, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 558, 7, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 263, 8, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 264, 8, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 265, 8, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 266, 8, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 267, 8, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 268, 8, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 560, 8, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 561, 8, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 562, 8, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 269, 9, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 21, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 270, 9, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 21, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 271, 9, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 21, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 272, 9, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 273, 9, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 274, 9, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 563, 9, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 564, 9, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 565, 9, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 275, 10, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 276, 10, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 277, 10, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 278, 10, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 279, 10, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 280, 10, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 566, 10, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 567, 10, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 568, 10, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 281, 11, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 282, 11, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 283, 11, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 284, 11, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 285, 11, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 286, 11, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 570, 11, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 571, 11, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 572, 11, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 287, 12, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 18, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 288, 12, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 18, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 289, 12, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 18, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 290, 12, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 291, 12, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 292, 12, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 573, 12, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 574, 12, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 575, 12, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 293, 13, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 19, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 294, 13, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 19, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 295, 13, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 19, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 296, 13, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 297, 13, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 298, 13, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 576, 13, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 577, 13, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 578, 13, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 299, 14, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 23, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 300, 14, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 23, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 301, 14, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 23, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 302, 14, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 303, 14, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 304, 14, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 579, 14, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 580, 14, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 581, 14, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 305, 15, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 306, 15, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 307, 15, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 308, 15, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 309, 15, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 310, 15, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 600, 15, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 601, 15, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 602, 15, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 311, 16, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 21, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 312, 16, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 21, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 313, 16, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 21, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 314, 16, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 315, 16, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 316, 16, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 603, 16, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 604, 16, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 605, 16, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 317, 17, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 318, 17, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 319, 17, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 320, 17, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 321, 17, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 322, 17, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 606, 17, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 607, 17, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 608, 17, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 323, 18, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 18, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 324, 18, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 18, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 325, 18, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 18, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 326, 18, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 327, 18, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 328, 18, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 609, 18, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 610, 18, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 611, 18, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "OfferImages",
                columns: new[] { "Id", "ImageUrl", "OfferId", "isMain" },
                values: new object[,]
                {
                    { 1, "https://picsum.photos/seed/offer-1-1/900/600", 1, true },
                    { 2, "https://picsum.photos/seed/offer-1-2/900/600", 1, false },
                    { 3, "https://picsum.photos/seed/offer-1-3/900/600", 1, false },
                    { 4, "https://picsum.photos/seed/offer-1-4/900/600", 1, false },
                    { 5, "https://picsum.photos/seed/offer-2-1/900/600", 2, true },
                    { 6, "https://picsum.photos/seed/offer-2-2/900/600", 2, false },
                    { 7, "https://picsum.photos/seed/offer-2-3/900/600", 2, false },
                    { 8, "https://picsum.photos/seed/offer-2-4/900/600", 2, false },
                    { 9, "https://picsum.photos/seed/offer-3-1/900/600", 3, true },
                    { 10, "https://picsum.photos/seed/offer-3-2/900/600", 3, false },
                    { 11, "https://picsum.photos/seed/offer-3-3/900/600", 3, false },
                    { 12, "https://picsum.photos/seed/offer-3-4/900/600", 3, false },
                    { 13, "https://picsum.photos/seed/offer-4-1/900/600", 4, true },
                    { 14, "https://picsum.photos/seed/offer-4-2/900/600", 4, false },
                    { 15, "https://picsum.photos/seed/offer-4-3/900/600", 4, false },
                    { 16, "https://picsum.photos/seed/offer-4-4/900/600", 4, false },
                    { 17, "https://picsum.photos/seed/offer-5-1/900/600", 5, true },
                    { 18, "https://picsum.photos/seed/offer-5-2/900/600", 5, false },
                    { 19, "https://picsum.photos/seed/offer-5-3/900/600", 5, false },
                    { 20, "https://picsum.photos/seed/offer-5-4/900/600", 5, false },
                    { 21, "https://picsum.photos/seed/offer-6-1/900/600", 6, true },
                    { 22, "https://picsum.photos/seed/offer-6-2/900/600", 6, false },
                    { 23, "https://picsum.photos/seed/offer-6-3/900/600", 6, false },
                    { 24, "https://picsum.photos/seed/offer-6-4/900/600", 6, false },
                    { 25, "https://picsum.photos/seed/offer-7-1/900/600", 7, true },
                    { 26, "https://picsum.photos/seed/offer-7-2/900/600", 7, false },
                    { 27, "https://picsum.photos/seed/offer-7-3/900/600", 7, false },
                    { 28, "https://picsum.photos/seed/offer-7-4/900/600", 7, false },
                    { 29, "https://picsum.photos/seed/offer-8-1/900/600", 8, true },
                    { 30, "https://picsum.photos/seed/offer-8-2/900/600", 8, false },
                    { 31, "https://picsum.photos/seed/offer-8-3/900/600", 8, false },
                    { 32, "https://picsum.photos/seed/offer-8-4/900/600", 8, false },
                    { 33, "https://picsum.photos/seed/offer-9-1/900/600", 9, true },
                    { 34, "https://picsum.photos/seed/offer-9-2/900/600", 9, false },
                    { 35, "https://picsum.photos/seed/offer-9-3/900/600", 9, false },
                    { 36, "https://picsum.photos/seed/offer-9-4/900/600", 9, false },
                    { 37, "https://picsum.photos/seed/offer-10-1/900/600", 10, true },
                    { 38, "https://picsum.photos/seed/offer-10-2/900/600", 10, false },
                    { 39, "https://picsum.photos/seed/offer-10-3/900/600", 10, false },
                    { 40, "https://picsum.photos/seed/offer-10-4/900/600", 10, false },
                    { 41, "https://picsum.photos/seed/offer-11-1/900/600", 11, true },
                    { 42, "https://picsum.photos/seed/offer-11-2/900/600", 11, false },
                    { 43, "https://picsum.photos/seed/offer-11-3/900/600", 11, false },
                    { 44, "https://picsum.photos/seed/offer-11-4/900/600", 11, false },
                    { 45, "https://picsum.photos/seed/offer-12-1/900/600", 12, true },
                    { 46, "https://picsum.photos/seed/offer-12-2/900/600", 12, false },
                    { 47, "https://picsum.photos/seed/offer-12-3/900/600", 12, false },
                    { 48, "https://picsum.photos/seed/offer-12-4/900/600", 12, false },
                    { 49, "https://picsum.photos/seed/offer-13-1/900/600", 13, true },
                    { 50, "https://picsum.photos/seed/offer-13-2/900/600", 13, false },
                    { 51, "https://picsum.photos/seed/offer-13-3/900/600", 13, false },
                    { 52, "https://picsum.photos/seed/offer-13-4/900/600", 13, false },
                    { 53, "https://picsum.photos/seed/offer-14-1/900/600", 14, true },
                    { 54, "https://picsum.photos/seed/offer-14-2/900/600", 14, false },
                    { 55, "https://picsum.photos/seed/offer-14-3/900/600", 14, false },
                    { 56, "https://picsum.photos/seed/offer-14-4/900/600", 14, false },
                    { 57, "https://picsum.photos/seed/offer-15-1/900/600", 15, true },
                    { 58, "https://picsum.photos/seed/offer-15-2/900/600", 15, false },
                    { 59, "https://picsum.photos/seed/offer-15-3/900/600", 15, false },
                    { 60, "https://picsum.photos/seed/offer-15-4/900/600", 15, false },
                    { 61, "https://picsum.photos/seed/offer-16-1/900/600", 16, true },
                    { 62, "https://picsum.photos/seed/offer-16-2/900/600", 16, false },
                    { 63, "https://picsum.photos/seed/offer-16-3/900/600", 16, false },
                    { 64, "https://picsum.photos/seed/offer-16-4/900/600", 16, false },
                    { 65, "https://picsum.photos/seed/offer-17-1/900/600", 17, true },
                    { 66, "https://picsum.photos/seed/offer-17-2/900/600", 17, false },
                    { 67, "https://picsum.photos/seed/offer-17-3/900/600", 17, false },
                    { 68, "https://picsum.photos/seed/offer-17-4/900/600", 17, false },
                    { 69, "https://picsum.photos/seed/offer-18-1/900/600", 18, true },
                    { 70, "https://picsum.photos/seed/offer-18-2/900/600", 18, false },
                    { 71, "https://picsum.photos/seed/offer-18-3/900/600", 18, false },
                    { 72, "https://picsum.photos/seed/offer-18-4/900/600", 18, false }
                });

            migrationBuilder.InsertData(
                table: "OfferPlanDays",
                columns: new[] { "DayNumber", "OfferDetailsId", "DayDescription", "DayTitle" },
                values: new object[,]
                {
                    { 1, 1, "Polazak u ranim jutarnjim satima. Pauze tokom puta. Dolazak u Firencu u poslijepodnevnim satima. Smještaj u hotel i slobodno vrijeme.", "Polazak i dolazak u Firencu" },
                    { 2, 1, "Razgledanje: Katedrala Santa Maria del Fiore, Piazza della Signoria, Palazzo Vecchio i Ponte Vecchio.", "Upoznavanje sa starim gradom" },
                    { 3, 1, "Posjeta galeriji Uffizi i slobodno vrijeme za individualne aktivnosti.", "Galerija Uffizi" },
                    { 4, 1, "Fakultativni izlet u Pisu ili slobodno vrijeme u Firenci.", "Izlet u Pisu ili slobodan dan" },
                    { 5, 1, "Odjava iz hotela i povratak kući.", "Povratak kući" },
                    { 1, 2, "Dolazak i smještaj u hotel.", "Dolazak na Santorini" },
                    { 2, 2, "Obilazak Fire i slobodno vrijeme.", "Fira – glavni grad ostrva" },
                    { 3, 2, "Posjeta Oiji i uživanje u zalasku sunca.", "Oia – zalazak sunca" },
                    { 4, 2, "Obilazak crne i crvene plaže.", "Vulkanske plaže" },
                    { 5, 2, "Izlet brodom i termalni izvori.", "Vulkansko ostrvo" },
                    { 6, 2, "Odmor ili fakultativne aktivnosti.", "Slobodan dan" },
                    { 7, 2, "Odjava iz hotela i povratak kući.", "Povratak kući" },
                    { 1, 3, "Dolazak i smještaj u hotel.", "Dolazak u Istanbul" },
                    { 2, 3, "Aja Sofija, Plava džamija i Topkapi palata.", "Sultanahmet" },
                    { 3, 3, "Krstarenje Bosforom i šetnja Istiklal ulicom.", "Bosfor i Taksim" },
                    { 4, 3, "Odjava iz hotela i povratak kući.", "Povratak kući" },
                    { 1, 4, "Dolazak u Barcelona, smještaj u hotel i slobodno vrijeme.", "Dolazak u Barcelona" },
                    { 2, 4, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Barcelona.", "Boravak u Barcelona" },
                    { 3, 4, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Barcelona.", "Boravak u Barcelona" },
                    { 4, 4, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Barcelona.", "Boravak u Barcelona" },
                    { 5, 4, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Barcelona.", "Boravak u Barcelona" },
                    { 6, 4, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Barcelona.", "Boravak u Barcelona" },
                    { 7, 4, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Barcelona.", "Boravak u Barcelona" },
                    { 8, 4, "Odjava iz hotela i povratak kući prema planu putovanja.", "Povratak kući" },
                    { 1, 5, "Dolazak u Paris, smještaj u hotel i slobodno vrijeme.", "Dolazak u Paris" },
                    { 2, 5, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Paris.", "Boravak u Paris" },
                    { 3, 5, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Paris.", "Boravak u Paris" },
                    { 4, 5, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Paris.", "Boravak u Paris" },
                    { 5, 5, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Paris.", "Boravak u Paris" },
                    { 6, 5, "Odjava iz hotela i povratak kući prema planu putovanja.", "Povratak kući" },
                    { 1, 6, "Dolazak u Prague, smještaj u hotel i slobodno vrijeme.", "Dolazak u Prague" },
                    { 2, 6, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Prague.", "Boravak u Prague" },
                    { 3, 6, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Prague.", "Boravak u Prague" },
                    { 4, 6, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Prague.", "Boravak u Prague" },
                    { 5, 6, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Prague.", "Boravak u Prague" },
                    { 6, 6, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Prague.", "Boravak u Prague" },
                    { 7, 6, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Prague.", "Boravak u Prague" },
                    { 8, 6, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Prague.", "Boravak u Prague" },
                    { 9, 6, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Prague.", "Boravak u Prague" },
                    { 10, 6, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Prague.", "Boravak u Prague" },
                    { 11, 6, "Odjava iz hotela i povratak kući prema planu putovanja.", "Povratak kući" },
                    { 1, 7, "Dolazak u Vienna, smještaj u hotel i slobodno vrijeme.", "Dolazak u Vienna" },
                    { 2, 7, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Vienna.", "Boravak u Vienna" },
                    { 3, 7, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Vienna.", "Boravak u Vienna" },
                    { 4, 7, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Vienna.", "Boravak u Vienna" },
                    { 5, 7, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Vienna.", "Boravak u Vienna" },
                    { 6, 7, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Vienna.", "Boravak u Vienna" },
                    { 7, 7, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Vienna.", "Boravak u Vienna" },
                    { 8, 7, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Vienna.", "Boravak u Vienna" },
                    { 9, 7, "Odjava iz hotela i povratak kući prema planu putovanja.", "Povratak kući" },
                    { 1, 8, "Dolazak u Amsterdam, smještaj u hotel i slobodno vrijeme.", "Dolazak u Amsterdam" },
                    { 2, 8, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Amsterdam.", "Boravak u Amsterdam" },
                    { 3, 8, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Amsterdam.", "Boravak u Amsterdam" },
                    { 4, 8, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Amsterdam.", "Boravak u Amsterdam" },
                    { 5, 8, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Amsterdam.", "Boravak u Amsterdam" },
                    { 6, 8, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Amsterdam.", "Boravak u Amsterdam" },
                    { 7, 8, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Amsterdam.", "Boravak u Amsterdam" },
                    { 8, 8, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Amsterdam.", "Boravak u Amsterdam" },
                    { 9, 8, "Odjava iz hotela i povratak kući prema planu putovanja.", "Povratak kući" },
                    { 1, 9, "Dolazak u London, smještaj u hotel i slobodno vrijeme.", "Dolazak u London" },
                    { 2, 9, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu London.", "Boravak u London" },
                    { 3, 9, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu London.", "Boravak u London" },
                    { 4, 9, "Odjava iz hotela i povratak kući prema planu putovanja.", "Povratak kući" },
                    { 1, 10, "Dolazak u Dubai, smještaj u hotel i slobodno vrijeme.", "Dolazak u Dubai" },
                    { 2, 10, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Dubai.", "Boravak u Dubai" },
                    { 3, 10, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Dubai.", "Boravak u Dubai" },
                    { 4, 10, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Dubai.", "Boravak u Dubai" },
                    { 5, 10, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Dubai.", "Boravak u Dubai" },
                    { 6, 10, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Dubai.", "Boravak u Dubai" },
                    { 7, 10, "Odjava iz hotela i povratak kući prema planu putovanja.", "Povratak kući" },
                    { 1, 11, "Dolazak u Cairo, smještaj u hotel i slobodno vrijeme.", "Dolazak u Cairo" },
                    { 2, 11, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Cairo.", "Boravak u Cairo" },
                    { 3, 11, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Cairo.", "Boravak u Cairo" },
                    { 4, 11, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Cairo.", "Boravak u Cairo" },
                    { 5, 11, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Cairo.", "Boravak u Cairo" },
                    { 6, 11, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Cairo.", "Boravak u Cairo" },
                    { 7, 11, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Cairo.", "Boravak u Cairo" },
                    { 8, 11, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Cairo.", "Boravak u Cairo" },
                    { 9, 11, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Cairo.", "Boravak u Cairo" },
                    { 10, 11, "Odjava iz hotela i povratak kući prema planu putovanja.", "Povratak kući" },
                    { 1, 12, "Dolazak u Budapest, smještaj u hotel i slobodno vrijeme.", "Dolazak u Budapest" },
                    { 2, 12, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Budapest.", "Boravak u Budapest" },
                    { 3, 12, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Budapest.", "Boravak u Budapest" },
                    { 4, 12, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Budapest.", "Boravak u Budapest" },
                    { 5, 12, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Budapest.", "Boravak u Budapest" },
                    { 6, 12, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Budapest.", "Boravak u Budapest" },
                    { 7, 12, "Odjava iz hotela i povratak kući prema planu putovanja.", "Povratak kući" },
                    { 1, 13, "Dolazak u Krakow, smještaj u hotel i slobodno vrijeme.", "Dolazak u Krakow" },
                    { 2, 13, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Krakow.", "Boravak u Krakow" },
                    { 3, 13, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Krakow.", "Boravak u Krakow" },
                    { 4, 13, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Krakow.", "Boravak u Krakow" },
                    { 5, 13, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Krakow.", "Boravak u Krakow" },
                    { 6, 13, "Odjava iz hotela i povratak kući prema planu putovanja.", "Povratak kući" },
                    { 1, 14, "Dolazak u Zanzibar, smještaj u hotel i slobodno vrijeme.", "Dolazak u Zanzibar" },
                    { 2, 14, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Zanzibar.", "Boravak u Zanzibar" },
                    { 3, 14, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Zanzibar.", "Boravak u Zanzibar" },
                    { 4, 14, "Odjava iz hotela i povratak kući prema planu putovanja.", "Povratak kući" },
                    { 1, 15, "Dolazak u Hurghada, smještaj u hotel i slobodno vrijeme.", "Dolazak u Hurghada" },
                    { 2, 15, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Hurghada.", "Boravak u Hurghada" },
                    { 3, 15, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Hurghada.", "Boravak u Hurghada" },
                    { 4, 15, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Hurghada.", "Boravak u Hurghada" },
                    { 5, 15, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Hurghada.", "Boravak u Hurghada" },
                    { 6, 15, "Odjava iz hotela i povratak kući prema planu putovanja.", "Povratak kući" },
                    { 1, 16, "Dolazak u Lisbon, smještaj u hotel i slobodno vrijeme.", "Dolazak u Lisbon" },
                    { 2, 16, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Lisbon.", "Boravak u Lisbon" },
                    { 3, 16, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Lisbon.", "Boravak u Lisbon" },
                    { 4, 16, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Lisbon.", "Boravak u Lisbon" },
                    { 5, 16, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Lisbon.", "Boravak u Lisbon" },
                    { 6, 16, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Lisbon.", "Boravak u Lisbon" },
                    { 7, 16, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Lisbon.", "Boravak u Lisbon" },
                    { 8, 16, "Odjava iz hotela i povratak kući prema planu putovanja.", "Povratak kući" },
                    { 1, 17, "Dolazak u Athens, smještaj u hotel i slobodno vrijeme.", "Dolazak u Athens" },
                    { 2, 17, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Athens.", "Boravak u Athens" },
                    { 3, 17, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Athens.", "Boravak u Athens" },
                    { 4, 17, "Odjava iz hotela i povratak kući prema planu putovanja.", "Povratak kući" },
                    { 1, 18, "Dolazak u Split, smještaj u hotel i slobodno vrijeme.", "Dolazak u Split" },
                    { 2, 18, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Split.", "Boravak u Split" },
                    { 3, 18, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Split.", "Boravak u Split" },
                    { 4, 18, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Split.", "Boravak u Split" },
                    { 5, 18, "Slobodno vrijeme za razgledanje, izlete i uživanje u gradu Split.", "Boravak u Split" },
                    { 6, 18, "Odjava iz hotela i povratak kući prema planu putovanja.", "Povratak kući" }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "CreatedAt", "DiscountValue", "HotelId", "IncludeInsurance", "IsActive", "OfferId", "PriceLeftToPay", "RoomId", "TotalPrice", "UserId", "addedNeeds", "isDiscountUsed", "isFirstRatePaid", "isFullPaid" },
                values: new object[,]
                {
                    { 2000, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0.0, 100, true, true, 1, 150m, 1, 450m, 5, "Vegetarijanski meni", false, true, false },
                    { 2001, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0.0, 110, false, true, 2, 900m, 2, 900m, 6, "", false, false, true },
                    { 2002, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0.0, 120, true, true, 3, 50m, 1, 350m, 7, "", false, true, false },
                    { 2003, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0.0, 111, false, true, 2, 0m, 3, 900m, 8, "", false, false, true },
                    { 2004, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0.0, 101, true, true, 1, 150m, 2, 450m, 9, "", false, true, false },
                    { 2005, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0.0, 122, false, true, 3, 50m, 1, 350m, 10, "", false, true, false },
                    { 2006, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0.0, 102, true, true, 1, 0m, 4, 450m, 11, "", false, false, true },
                    { 2007, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0.0, 112, true, true, 2, 600m, 1, 900m, 12, "Pristup teretani", false, true, false },
                    { 2008, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0.0, 120, false, true, 3, 0m, 2, 350m, 13, "", false, false, true },
                    { 2009, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0.0, 101, true, true, 1, 150m, 3, 450m, 14, "", false, true, false },
                    { 2010, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0.0, 110, false, true, 2, 600m, 4, 900m, 15, "", false, true, false },
                    { 2011, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0.0, 121, true, true, 3, 50m, 2, 350m, 16, "", false, true, false },
                    { 2012, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0.0, 102, false, true, 1, 150m, 2, 450m, 17, "", false, true, false },
                    { 2013, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0.0, 111, true, true, 2, 0m, 2, 900m, 18, "Bez glutena", false, false, true },
                    { 2014, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0.0, 120, true, true, 3, 50m, 3, 350m, 19, "", false, true, false },
                    { 2015, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0.0, 101, true, true, 1, 0m, 1, 450m, 20, "", false, false, true },
                    { 2016, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0.0, 112, true, true, 2, 600m, 1, 900m, 21, "", false, true, false },
                    { 2017, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0.0, 122, true, true, 3, 50m, 1, 350m, 6, "", false, true, false },
                    { 2018, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0.0, 100, false, true, 1, 150m, 1, 450m, 6, "", false, true, false },
                    { 2019, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0.0, 111, true, true, 2, 0m, 3, 900m, 6, "", false, false, true },
                    { 3000, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 0.0, 100, false, true, 1, 0m, 1, 0m, 6, "", false, true, true },
                    { 3001, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 0.0, 110, false, true, 2, 0m, 2, 0m, 6, "", false, true, true },
                    { 3002, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 0.0, 120, false, true, 3, 0m, 1, 0m, 6, "", false, true, true },
                    { 3003, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 0.0, 282, false, false, 9, 0m, 2, 0m, 6, "", false, true, true },
                    { 3004, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 0.0, 288, false, false, 10, 0m, 2, 0m, 6, "", false, true, true }
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

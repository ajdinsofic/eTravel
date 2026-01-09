using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eTravelAgencija.Services.Migrations
{
    /// <inheritdoc />
    public partial class fixingMainDatabase : Migration
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
                    table.PrimaryKey("PK_OfferHotels", x => new { x.OfferDetailsId, x.HotelId, x.DepartureDate });
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
                    { 1, 0, "1d6c2450-13d6-426a-9161-f6c37c90b0dc", new DateTime(2025, 12, 29, 20, 48, 12, 726, DateTimeKind.Utc).AddTicks(7420), new DateTime(1990, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), "radnik@etravel.com", true, "Marko", "test.jpg", null, "Radnik", false, null, "RADNIK@ETRAVEL.COM", "RADNIK", "AQAAAAIAAYagAAAAEGsvY9qh6eP5MEnbS4iljrYPRpP3abZfXyTxaR8mfZCbewXnaOAqfgoFHwIdlROxQw==", null, null, "+38761111111", false, null, false, "radnik", false },
                    { 2, 0, "880eb383-1c40-4e52-942a-1db105d528a2", new DateTime(2025, 12, 29, 20, 48, 12, 796, DateTimeKind.Utc).AddTicks(2254), new DateTime(1985, 3, 20, 0, 0, 0, 0, DateTimeKind.Utc), "direktor@etravel.com", true, "Amir", "test.jpg", null, "Direktor", false, null, "DIREKTOR@ETRAVEL.COM", "DIREKTOR", "AQAAAAIAAYagAAAAEN7yLxbr5aCz+RNAtIMMCSe0PTNfJAZde44gPb0DkDRhNw1M0ead5izf45LZA/nbiA==", null, null, "+38762222222", false, null, false, "direktor", false },
                    { 4, 0, "e8d4d7fe-2c41-4026-9d1b-c10d584deaf4", new DateTime(2025, 12, 29, 20, 48, 12, 867, DateTimeKind.Utc).AddTicks(4508), new DateTime(2002, 11, 5, 0, 0, 0, 0, DateTimeKind.Utc), "korisnik@etravel.com", true, "Ajdin", "test.jpg", null, "Korisnik", false, null, "KORISNIK@ETRAVEL.COM", "KORISNIK", "AQAAAAIAAYagAAAAEF7/D1/Y+8M5Sicpj27CblHnfEi0NdWoStTaxyoEzlpSiDLLY7H9HwsSoIgbk+SRuA==", null, null, "+38763333333", false, null, false, "korisnik", false },
                    { 5, 0, "c9200a49-e7c3-4df6-ae45-d018f73e4bc4", new DateTime(2025, 12, 29, 20, 48, 12, 932, DateTimeKind.Utc).AddTicks(694), new DateTime(1998, 4, 12, 0, 0, 0, 0, DateTimeKind.Utc), "maja.petrovic@etravel.com", true, "Maja", "test.jpg", null, "Petrović", false, null, "MAJA.PETROVIC@ETRAVEL.COM", "MAJA.PETROVIC", "AQAAAAIAAYagAAAAEKrZJPZ/F/kqUbj00V9ZT+fkn2q8DKtVmX/knOXsO1J2eoqbqpKSi8VGgBUzXfUtiQ==", null, null, "+38761555111", false, null, false, "maja.petrovic", false },
                    { 6, 0, "55c4a623-5aca-48fb-b349-590f3350b192", new DateTime(2025, 12, 29, 20, 48, 12, 997, DateTimeKind.Utc).AddTicks(8773), new DateTime(1995, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "edin.mesic@etravel.com", true, "Edin", "test.jpg", null, "Mešić", false, null, "EDIN.MESIC@ETRAVEL.COM", "EDIN.MESIC", "AQAAAAIAAYagAAAAEAfUMHxKwA0FGvXEjj0lYrCRgktJzJ7O7i0YYVlEI+z0oPvbYa1T8gVGaUv225a1oQ==", null, null, "+38761666123", false, null, false, "edin.mesic", false },
                    { 7, 0, "e650112c-483d-4135-b45b-6c117193d844", new DateTime(2025, 12, 29, 20, 48, 13, 69, DateTimeKind.Utc).AddTicks(278), new DateTime(2000, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "lana.kovac@etravel.com", true, "Lana", "test.jpg", null, "Kovač", false, null, "LANA.KOVAC@ETRAVEL.COM", "LANA.KOVAC", "AQAAAAIAAYagAAAAEN0ACIKMTi54YBcUFc0DLcCRt+OEhBNVgCrn6snek9DvJTHHr4paGDk/F+sDeTfYIQ==", null, null, "+38761777141", false, null, false, "lana.kovac", false },
                    { 8, 0, "1fbe7bc7-e7f6-4931-8a8c-426ef905f7fe", new DateTime(2025, 12, 29, 20, 48, 13, 137, DateTimeKind.Utc).AddTicks(2929), new DateTime(1993, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), "haris.becirovic@etravel.com", true, "Haris", "test.jpg", null, "Bećirović", false, null, "HARIS.BECIROVIC@ETRAVEL.COM", "HARIS.BECIROVIC", "AQAAAAIAAYagAAAAEDXlqveoLfs1swsgoebwg03C/aFur/BEN5Rj4WZlHl6zDrLGKh6iJ9UrrC46SPjz0w==", null, null, "+38761888222", false, null, false, "haris.becirovic", false },
                    { 9, 0, "39bd67ab-6a51-47ba-a928-9db1c50619ae", new DateTime(2025, 12, 29, 20, 48, 13, 203, DateTimeKind.Utc).AddTicks(9991), new DateTime(1999, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "amira.karic@etravel.com", true, "Amira", "test.jpg", null, "Karić", false, null, "AMIRA.KARIC@ETRAVEL.COM", "AMIRA.KARIC", "AQAAAAIAAYagAAAAENqsyoXgb5qZXqJpunY2mmCmF70s+retGZpR2l2jxJtQ2qb7ui/tSMeO8OQ2lCVF/g==", null, null, "+38761999444", false, null, false, "amira.karic", false },
                    { 10, 0, "93c07501-b22b-48c9-ab7e-1363bd7e40b5", new DateTime(2025, 12, 29, 20, 48, 13, 261, DateTimeKind.Utc).AddTicks(2920), new DateTime(1997, 5, 19, 0, 0, 0, 0, DateTimeKind.Utc), "tarik.suljic@etravel.com", true, "Tarik", "test.jpg", null, "Suljić", false, null, "TARIK.SULJIC@ETRAVEL.COM", "TARIK.SULJIC", "AQAAAAIAAYagAAAAEIB2BTObUfPB8kEGRx38FJwP5CmLhWG61TboODc5DN3OrU8mjTqFl3ZenqNyChMxgA==", null, null, "+38762011223", false, null, false, "tarik.suljic", false },
                    { 11, 0, "2832c4db-f595-47d9-a0e2-8bc65f6d8483", new DateTime(2025, 12, 29, 20, 48, 13, 322, DateTimeKind.Utc).AddTicks(6381), new DateTime(2001, 10, 11, 0, 0, 0, 0, DateTimeKind.Utc), "selma.babic@etravel.com", true, "Selma", "test.jpg", null, "Babić", false, null, "SELMA.BABIC@ETRAVEL.COM", "SELMA.BABIC", "AQAAAAIAAYagAAAAEOqRtXjIR+Utlzu1kPeCPT6msSbKopb5PUun3R93Ymxq1j7KQdOOoQrmrR9h+QUDmA==", null, null, "+38762044321", false, null, false, "selma.babic", false },
                    { 12, 0, "a627a657-af0c-4091-9df6-e4a17083eefd", new DateTime(2025, 12, 29, 20, 48, 13, 382, DateTimeKind.Utc).AddTicks(6747), new DateTime(1994, 12, 2, 0, 0, 0, 0, DateTimeKind.Utc), "nedim.ceric@etravel.com", true, "Nedim", "test.jpg", null, "Ćerić", false, null, "NEDIM.CERIC@ETRAVEL.COM", "NEDIM.CERIC", "AQAAAAIAAYagAAAAEJUS1+vjm/My2dzBF1LbS7wyGyTV6b1tNpuY3rmxqhaBCVOtY30PIxYe/LBLqsM0Ug==", null, null, "+38762077311", false, null, false, "nedim.ceric", false },
                    { 13, 0, "baacd044-aa6b-48d7-925e-939d91fdcdf2", new DateTime(2025, 12, 29, 20, 48, 13, 442, DateTimeKind.Utc).AddTicks(2599), new DateTime(1996, 11, 9, 0, 0, 0, 0, DateTimeKind.Utc), "alma.vujic@etravel.com", true, "Alma", "test.jpg", null, "Vujić", false, null, "ALMA.VUJIC@ETRAVEL.COM", "ALMA.VUJIC", "AQAAAAIAAYagAAAAED+OnVXVBpjECuI4YlJXG5Vlvm7AVbN+GsHgMOmIopebWEvTCW3v6HwfznmDa/01sg==", null, null, "+38762111333", false, null, false, "alma.vujic", false },
                    { 14, 0, "8cd1bfa6-8d81-4859-a0be-b76897a1a4c0", new DateTime(2025, 12, 29, 20, 48, 13, 504, DateTimeKind.Utc).AddTicks(9690), new DateTime(1992, 7, 4, 0, 0, 0, 0, DateTimeKind.Utc), "mirza.drace@etravel.com", true, "Mirza", "test.jpg", null, "DračE", false, null, "MIRZA.DRACE@ETRAVEL.COM", "MIRZA.DRACE", "AQAAAAIAAYagAAAAEBI0yD4+mjdDXu3DuwdoEVDcjBT2qqbbVei6ZsCXupY25896RwxXqQN06t4GUkfJPQ==", null, null, "+38762144555", false, null, false, "mirza.drace", false },
                    { 15, 0, "68a4bd03-0f11-4f2d-8361-76909d14fc11", new DateTime(2025, 12, 29, 20, 48, 13, 566, DateTimeKind.Utc).AddTicks(2228), new DateTime(2000, 6, 17, 0, 0, 0, 0, DateTimeKind.Utc), "melisa.nuhanovic@etravel.com", true, "Melisa", "test.jpg", null, "Nuhanović", false, null, "MELISA.NUHANOVIC@ETRAVEL.COM", "MELISA.NUHANOVIC", "AQAAAAIAAYagAAAAEJeeQi4rh3dXv+9i3qlEYvW2z1R4pptCtAfcFZi4OQyBK6Ds8m3FP7EI6lXKRdNW5g==", null, null, "+38762200333", false, null, false, "melisa.nuhanovic", false },
                    { 16, 0, "61ec2961-bfe2-4b27-ba26-acff4fed9a64", new DateTime(2025, 12, 29, 20, 48, 13, 690, DateTimeKind.Utc).AddTicks(1543), new DateTime(1991, 3, 29, 0, 0, 0, 0, DateTimeKind.Utc), "almin.kosuta@etravel.com", true, "Almin", "test.jpg", null, "Košuta", false, null, "ALMIN.KOSUTA@ETRAVEL.COM", "ALMIN.KOSUTA", "AQAAAAIAAYagAAAAEJEImTKS5eaWPMinqsjzsvbMN+mVi6E3I7jmDuQ0fLIeT/ArxXGiWHx3n7s7XqQ+gQ==", null, null, "+38762255677", false, null, false, "almin.kosuta", false },
                    { 17, 0, "e760d706-97b8-49d1-b01a-cef5258710c0", new DateTime(2025, 12, 29, 20, 48, 13, 761, DateTimeKind.Utc).AddTicks(4654), new DateTime(1998, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), "dina.hodzic@etravel.com", true, "Dina", "test.jpg", null, "Hodžić", false, null, "DINA.HODZIC@ETRAVEL.COM", "DINA.HODZIC", "AQAAAAIAAYagAAAAEBvV6uAf5MwcUABoh00vBWIZiC+KvrwpqcgrbJXY8jxVqaLAAGZt8S9ZtIwJh9qkNQ==", null, null, "+38762277991", false, null, false, "dina.hodzic", false },
                    { 18, 0, "b4021f64-3f92-4384-b4ee-ff631c235cae", new DateTime(2025, 12, 29, 20, 48, 13, 831, DateTimeKind.Utc).AddTicks(3737), new DateTime(1997, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "alem.celik@etravel.com", true, "Alem", "test.jpg", null, "Čelik", false, null, "ALEM.CELIK@ETRAVEL.COM", "ALEM.CELIK", "AQAAAAIAAYagAAAAECPSCNBwJUMaLyNSITh3YmiboBy/GC9cFmS09EMiaZFBDf+UC9wsVms10MpQi7rYMQ==", null, null, "+38762300990", false, null, false, "alem.celik", false },
                    { 19, 0, "ddfcbec5-0e7e-4d02-aad8-78c4f3c7c39f", new DateTime(2025, 12, 29, 20, 48, 13, 898, DateTimeKind.Utc).AddTicks(1920), new DateTime(2001, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "lejla.avdic@etravel.com", true, "Lejla", "test.jpg", null, "Avdić", false, null, "LEJLA.AVDIC@ETRAVEL.COM", "LEJLA.AVDIC", "AQAAAAIAAYagAAAAEK4/Ftctehlt0aD6ofV5wJGBdEDrd/n4bO44Hg2Ll5f2E0Yg+LJ2SU5pHZ+EHxbd2w==", null, null, "+38762355123", false, null, false, "lejla.avdic", false },
                    { 20, 0, "2fd5af14-f78c-4251-995c-3ab28ef035f9", new DateTime(2025, 12, 29, 20, 48, 13, 979, DateTimeKind.Utc).AddTicks(2112), new DateTime(1999, 9, 3, 0, 0, 0, 0, DateTimeKind.Utc), "adnan.pasalic@etravel.com", true, "Adnan", "test.jpg", null, "Pašalić", false, null, "ADNAN.PASALIC@ETRAVEL.COM", "ADNAN.PASALIC", "AQAAAAIAAYagAAAAEI+bMovbXNJPwvgVfRqJ1IosC94vSvT6fnPF3yn9rRGtFxrIsg08glpzVzFYftBxyQ==", null, null, "+38762388321", false, null, false, "adnan.pasalic", false },
                    { 21, 0, "5f807300-e3de-4737-b5b4-162ff330bbea", new DateTime(2025, 12, 29, 20, 48, 14, 50, DateTimeKind.Utc).AddTicks(8471), new DateTime(1996, 4, 14, 0, 0, 0, 0, DateTimeKind.Utc), "inez.kantic@etravel.com", true, "Inez", "test.jpg", null, "Kantić", false, null, "INEZ.KANTIC@ETRAVEL.COM", "INEZ.KANTIC", "AQAAAAIAAYagAAAAEO9Vwi+0muwOeCuMYiZJsabTsvbyKxkjZ+Ocd2cgFnA51I6kP61qf+MWugW7HMoMEw==", null, null, "+38762444123", false, null, false, "inez.kantic", false },
                    { 22, 0, "73cfebb6-d956-4bc4-949d-ccf31054144a", new DateTime(2025, 12, 29, 20, 48, 14, 118, DateTimeKind.Utc).AddTicks(8907), new DateTime(1993, 11, 19, 0, 0, 0, 0, DateTimeKind.Utc), "amir.halilovic@etravel.com", true, "Amir", "test.jpg", null, "Halilović", false, null, "AMIR.HALILOVIC@ETRAVEL.COM", "AMIR.HALILOVIC", "AQAAAAIAAYagAAAAEGFImPWVOnac/EMWvxu8/wUSCn4K1/PhqipOjCk/DNVMBG67ggU4IXZJ7RwlhY53Kw==", null, null, "+38762477331", false, null, false, "amir.halilovic", false },
                    { 23, 0, "eab517d1-336b-4ae1-b2b4-fddd290028e9", new DateTime(2025, 12, 29, 20, 48, 14, 186, DateTimeKind.Utc).AddTicks(9028), new DateTime(2002, 12, 21, 0, 0, 0, 0, DateTimeKind.Utc), "lamija.kreso@etravel.com", true, "Lamija", "test.jpg", null, "Krešo", false, null, "LAMIJA.KRESO@ETRAVEL.COM", "LAMIJA.KRESO", "AQAAAAIAAYagAAAAEJjrJbgEvrAq1oIKEz7DtREYzIOh6F8yLFNTvh/qDKCHxa8jYAaWXUhHBGx3QyK7VA==", null, null, "+38762555991", false, null, false, "lamija.kreso", false },
                    { 24, 0, "3d0a9c90-615c-453b-acde-327aa4c51070", new DateTime(2025, 12, 29, 20, 48, 14, 253, DateTimeKind.Utc).AddTicks(9126), new DateTime(1998, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), "omer.smajic@etravel.com", true, "Omer", "test.jpg", null, "Smajić", false, null, "OMER.SMAJIC@ETRAVEL.COM", "OMER.SMAJIC", "AQAAAAIAAYagAAAAEGCL6c7uHv+Ks//0klQ57FD+aEr5uvbAUC0fcjHzI4iZwKcX054E92HWByzvjLYZ5g==", null, null, "+38762666112", false, null, false, "omer.smajic", false }
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
                    { 214, "Obala Hrvatskog Narodnog 17", 0m, "Hotel Adriatic Split", 4 },
                    { 215, "Carrer de Pelai 45", 0m, "Barcelona Central Plaza", 4 },
                    { 216, "Carrer de Mallorca 401", 0m, "Sagrada Familia View Hotel", 5 },
                    { 217, "Avenue de Suffren 22", 0m, "Eiffel Tower Boutique Hotel", 5 },
                    { 218, "Rue Lepic 88", 0m, "Montmartre Comfort Inn", 3 },
                    { 219, "Staroměstské náměstí 15", 0m, "Old Town Square Hotel", 5 },
                    { 220, "Vodičkova 12", 0m, "Prague City Stay", 3 },
                    { 221, "Opernring 5", 0m, "Vienna Opera House Hotel", 5 },
                    { 222, "Handelskai 94", 0m, "Danube Riverside Hotel", 3 },
                    { 223, "Damrak 50", 0m, "Dam Square Boutique Hotel", 4 },
                    { 224, "Leidsekade 70", 0m, "Amsterdam City Budget Hotel", 3 },
                    { 225, "Piccadilly 33", 0m, "Piccadilly Central Hotel", 5 },
                    { 226, "Tooley Street 18", 0m, "London Bridge Inn", 3 },
                    { 227, "Downtown Dubai 1", 0m, "Burj Khalifa View Hotel", 5 },
                    { 228, "Al Seef Street 9", 0m, "Dubai Creek City Hotel", 4 },
                    { 229, "Corniche El Nil 101", 0m, "Nile Riverside Palace", 5 },
                    { 230, "Tahrir Square 6", 0m, "Downtown Cairo Hotel", 3 },
                    { 231, "Bem Rakpart 15", 0m, "Danube Panorama Hotel", 4 },
                    { 232, "Andrássy út 45", 0m, "Thermal Spa Boutique Hotel", 5 },
                    { 233, "Kanonicza 8", 0m, "Wawel Castle View Hotel", 5 },
                    { 234, "Dietla 60", 0m, "Krakow City Budget Stay", 3 },
                    { 235, "Mkunazini Street 14", 0m, "Stone Town Heritage Hotel", 4 },
                    { 236, "Nungwi Coast 7", 0m, "Zanzibar Beach Paradise", 5 },
                    { 237, "El Mamsha 22", 0m, "Red Sea Coral Resort", 4 },
                    { 238, "Village Road 9", 0m, "Hurghada Sunlight Hotel", 3 },
                    { 239, "Avenida Brasília 78", 0m, "Belem Riverside Hotel", 5 },
                    { 240, "Rua Augusta 120", 0m, "Lisbon Downtown Stay", 3 },
                    { 241, "Adrianou 30", 0m, "Plaka Heritage Hotel", 4 },
                    { 242, "Syntagma Square 3", 0m, "Athens Central Palace", 5 },
                    { 243, "Peristil 2", 0m, "Diocletian Palace Boutique Hotel", 5 },
                    { 244, "Bačvice 14", 0m, "Split City Beach Hotel", 3 }
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
                    { 2, 1, new DateTime(2025, 12, 29, 20, 48, 14, 319, DateTimeKind.Utc).AddTicks(7222), "", true },
                    { 3, 2, new DateTime(2025, 12, 29, 20, 48, 14, 319, DateTimeKind.Utc).AddTicks(7234), "", true },
                    { 1, 4, new DateTime(2025, 12, 29, 20, 48, 14, 319, DateTimeKind.Utc).AddTicks(7235), "", true },
                    { 1, 5, new DateTime(2025, 12, 29, 20, 48, 14, 319, DateTimeKind.Utc).AddTicks(7292), "", true },
                    { 1, 6, new DateTime(2025, 12, 29, 20, 48, 14, 319, DateTimeKind.Utc).AddTicks(7293), "", true },
                    { 1, 7, new DateTime(2025, 12, 29, 20, 48, 14, 319, DateTimeKind.Utc).AddTicks(7294), "", true },
                    { 1, 8, new DateTime(2025, 12, 29, 20, 48, 14, 319, DateTimeKind.Utc).AddTicks(7295), "", true },
                    { 1, 9, new DateTime(2025, 12, 29, 20, 48, 14, 319, DateTimeKind.Utc).AddTicks(7296), "", true },
                    { 1, 10, new DateTime(2025, 12, 29, 20, 48, 14, 319, DateTimeKind.Utc).AddTicks(7297), "", true },
                    { 1, 11, new DateTime(2025, 12, 29, 20, 48, 14, 319, DateTimeKind.Utc).AddTicks(7298), "", true },
                    { 1, 12, new DateTime(2025, 12, 29, 20, 48, 14, 319, DateTimeKind.Utc).AddTicks(7299), "", true },
                    { 1, 13, new DateTime(2025, 12, 29, 20, 48, 14, 319, DateTimeKind.Utc).AddTicks(7300), "", true },
                    { 1, 14, new DateTime(2025, 12, 29, 20, 48, 14, 319, DateTimeKind.Utc).AddTicks(7331), "", true },
                    { 1, 15, new DateTime(2025, 12, 29, 20, 48, 14, 319, DateTimeKind.Utc).AddTicks(7332), "", true },
                    { 1, 16, new DateTime(2025, 12, 29, 20, 48, 14, 319, DateTimeKind.Utc).AddTicks(7333), "", true },
                    { 1, 17, new DateTime(2025, 12, 29, 20, 48, 14, 319, DateTimeKind.Utc).AddTicks(7333), "", true },
                    { 1, 18, new DateTime(2025, 12, 29, 20, 48, 14, 319, DateTimeKind.Utc).AddTicks(7334), "", true },
                    { 1, 19, new DateTime(2025, 12, 29, 20, 48, 14, 319, DateTimeKind.Utc).AddTicks(7335), "", true },
                    { 1, 20, new DateTime(2025, 12, 29, 20, 48, 14, 319, DateTimeKind.Utc).AddTicks(7337), "", true },
                    { 1, 21, new DateTime(2025, 12, 29, 20, 48, 14, 319, DateTimeKind.Utc).AddTicks(7338), "", true },
                    { 1, 22, new DateTime(2025, 12, 29, 20, 48, 14, 319, DateTimeKind.Utc).AddTicks(7338), "", true },
                    { 1, 23, new DateTime(2025, 12, 29, 20, 48, 14, 319, DateTimeKind.Utc).AddTicks(7339), "", true },
                    { 1, 24, new DateTime(2025, 12, 29, 20, 48, 14, 319, DateTimeKind.Utc).AddTicks(7340), "", true }
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
                    { 13, 110, "https://picsum.photos/seed/hotel-110-1/800/500", true },
                    { 14, 110, "https://picsum.photos/seed/hotel-110-2/800/500", false },
                    { 15, 110, "https://picsum.photos/seed/hotel-110-3/800/500", false },
                    { 16, 110, "https://picsum.photos/seed/hotel-110-4/800/500", false },
                    { 17, 111, "https://picsum.photos/seed/hotel-111-1/800/500", true },
                    { 18, 111, "https://picsum.photos/seed/hotel-111-2/800/500", false },
                    { 19, 111, "https://picsum.photos/seed/hotel-111-3/800/500", false },
                    { 20, 111, "https://picsum.photos/seed/hotel-111-4/800/500", false },
                    { 21, 112, "https://picsum.photos/seed/hotel-112-1/800/500", true },
                    { 22, 112, "https://picsum.photos/seed/hotel-112-2/800/500", false },
                    { 23, 112, "https://picsum.photos/seed/hotel-112-3/800/500", false },
                    { 24, 112, "https://picsum.photos/seed/hotel-112-4/800/500", false },
                    { 25, 120, "https://picsum.photos/seed/hotel-120-1/800/500", true },
                    { 26, 120, "https://picsum.photos/seed/hotel-120-2/800/500", false },
                    { 27, 120, "https://picsum.photos/seed/hotel-120-3/800/500", false },
                    { 28, 120, "https://picsum.photos/seed/hotel-120-4/800/500", false },
                    { 29, 121, "https://picsum.photos/seed/hotel-121-1/800/500", true },
                    { 30, 121, "https://picsum.photos/seed/hotel-121-2/800/500", false },
                    { 31, 121, "https://picsum.photos/seed/hotel-121-3/800/500", false },
                    { 32, 121, "https://picsum.photos/seed/hotel-121-4/800/500", false },
                    { 33, 122, "https://picsum.photos/seed/hotel-122-1/800/500", true },
                    { 34, 122, "https://picsum.photos/seed/hotel-122-2/800/500", false },
                    { 35, 122, "https://picsum.photos/seed/hotel-122-3/800/500", false },
                    { 36, 122, "https://picsum.photos/seed/hotel-122-4/800/500", false },
                    { 37, 200, "https://picsum.photos/seed/hotel-200-1/800/500", true },
                    { 38, 200, "https://picsum.photos/seed/hotel-200-2/800/500", false },
                    { 39, 200, "https://picsum.photos/seed/hotel-200-3/800/500", false },
                    { 40, 200, "https://picsum.photos/seed/hotel-200-4/800/500", false },
                    { 41, 215, "https://picsum.photos/seed/hotel-215-1/800/500", true },
                    { 42, 215, "https://picsum.photos/seed/hotel-215-2/800/500", false },
                    { 43, 215, "https://picsum.photos/seed/hotel-215-3/800/500", false },
                    { 44, 215, "https://picsum.photos/seed/hotel-215-4/800/500", false },
                    { 45, 216, "https://picsum.photos/seed/hotel-216-1/800/500", true },
                    { 46, 216, "https://picsum.photos/seed/hotel-216-2/800/500", false },
                    { 47, 216, "https://picsum.photos/seed/hotel-216-3/800/500", false },
                    { 48, 216, "https://picsum.photos/seed/hotel-216-4/800/500", false },
                    { 49, 201, "https://picsum.photos/seed/hotel-201-1/800/500", true },
                    { 50, 201, "https://picsum.photos/seed/hotel-201-2/800/500", false },
                    { 51, 201, "https://picsum.photos/seed/hotel-201-3/800/500", false },
                    { 52, 201, "https://picsum.photos/seed/hotel-201-4/800/500", false },
                    { 53, 217, "https://picsum.photos/seed/hotel-217-1/800/500", true },
                    { 54, 217, "https://picsum.photos/seed/hotel-217-2/800/500", false },
                    { 55, 217, "https://picsum.photos/seed/hotel-217-3/800/500", false },
                    { 56, 217, "https://picsum.photos/seed/hotel-217-4/800/500", false },
                    { 57, 218, "https://picsum.photos/seed/hotel-218-1/800/500", true },
                    { 58, 218, "https://picsum.photos/seed/hotel-218-2/800/500", false },
                    { 59, 218, "https://picsum.photos/seed/hotel-218-3/800/500", false },
                    { 60, 218, "https://picsum.photos/seed/hotel-218-4/800/500", false },
                    { 61, 202, "https://picsum.photos/seed/hotel-202-1/800/500", true },
                    { 62, 202, "https://picsum.photos/seed/hotel-202-2/800/500", false },
                    { 63, 202, "https://picsum.photos/seed/hotel-202-3/800/500", false },
                    { 64, 202, "https://picsum.photos/seed/hotel-202-4/800/500", false },
                    { 65, 219, "https://picsum.photos/seed/hotel-219-1/800/500", true },
                    { 66, 219, "https://picsum.photos/seed/hotel-219-2/800/500", false },
                    { 67, 219, "https://picsum.photos/seed/hotel-219-3/800/500", false },
                    { 68, 219, "https://picsum.photos/seed/hotel-219-4/800/500", false },
                    { 69, 220, "https://picsum.photos/seed/hotel-220-1/800/500", true },
                    { 70, 220, "https://picsum.photos/seed/hotel-220-2/800/500", false },
                    { 71, 220, "https://picsum.photos/seed/hotel-220-3/800/500", false },
                    { 72, 220, "https://picsum.photos/seed/hotel-220-4/800/500", false },
                    { 73, 203, "https://picsum.photos/seed/hotel-203-1/800/500", true },
                    { 74, 203, "https://picsum.photos/seed/hotel-203-2/800/500", false },
                    { 75, 203, "https://picsum.photos/seed/hotel-203-3/800/500", false },
                    { 76, 203, "https://picsum.photos/seed/hotel-203-4/800/500", false },
                    { 77, 221, "https://picsum.photos/seed/hotel-221-1/800/500", true },
                    { 78, 221, "https://picsum.photos/seed/hotel-221-2/800/500", false },
                    { 79, 221, "https://picsum.photos/seed/hotel-221-3/800/500", false },
                    { 80, 221, "https://picsum.photos/seed/hotel-221-4/800/500", false },
                    { 81, 222, "https://picsum.photos/seed/hotel-222-1/800/500", true },
                    { 82, 222, "https://picsum.photos/seed/hotel-222-2/800/500", false },
                    { 83, 222, "https://picsum.photos/seed/hotel-222-3/800/500", false },
                    { 84, 222, "https://picsum.photos/seed/hotel-222-4/800/500", false },
                    { 85, 204, "https://picsum.photos/seed/hotel-204-1/800/500", true },
                    { 86, 204, "https://picsum.photos/seed/hotel-204-2/800/500", false },
                    { 87, 204, "https://picsum.photos/seed/hotel-204-3/800/500", false },
                    { 88, 204, "https://picsum.photos/seed/hotel-204-4/800/500", false },
                    { 89, 223, "https://picsum.photos/seed/hotel-223-1/800/500", true },
                    { 90, 223, "https://picsum.photos/seed/hotel-223-2/800/500", false },
                    { 91, 223, "https://picsum.photos/seed/hotel-223-3/800/500", false },
                    { 92, 223, "https://picsum.photos/seed/hotel-223-4/800/500", false },
                    { 93, 224, "https://picsum.photos/seed/hotel-224-1/800/500", true },
                    { 94, 224, "https://picsum.photos/seed/hotel-224-2/800/500", false },
                    { 95, 224, "https://picsum.photos/seed/hotel-224-3/800/500", false },
                    { 96, 224, "https://picsum.photos/seed/hotel-224-4/800/500", false },
                    { 97, 205, "https://picsum.photos/seed/hotel-205-1/800/500", true },
                    { 98, 205, "https://picsum.photos/seed/hotel-205-2/800/500", false },
                    { 99, 205, "https://picsum.photos/seed/hotel-205-3/800/500", false },
                    { 100, 205, "https://picsum.photos/seed/hotel-205-4/800/500", false },
                    { 101, 225, "https://picsum.photos/seed/hotel-225-1/800/500", true },
                    { 102, 225, "https://picsum.photos/seed/hotel-225-2/800/500", false },
                    { 103, 225, "https://picsum.photos/seed/hotel-225-3/800/500", false },
                    { 104, 225, "https://picsum.photos/seed/hotel-225-4/800/500", false },
                    { 105, 226, "https://picsum.photos/seed/hotel-226-1/800/500", true },
                    { 106, 226, "https://picsum.photos/seed/hotel-226-2/800/500", false },
                    { 107, 226, "https://picsum.photos/seed/hotel-226-3/800/500", false },
                    { 108, 226, "https://picsum.photos/seed/hotel-226-4/800/500", false },
                    { 109, 206, "https://picsum.photos/seed/hotel-206-1/800/500", true },
                    { 110, 206, "https://picsum.photos/seed/hotel-206-2/800/500", false },
                    { 111, 206, "https://picsum.photos/seed/hotel-206-3/800/500", false },
                    { 112, 206, "https://picsum.photos/seed/hotel-206-4/800/500", false },
                    { 113, 227, "https://picsum.photos/seed/hotel-227-1/800/500", true },
                    { 114, 227, "https://picsum.photos/seed/hotel-227-2/800/500", false },
                    { 115, 227, "https://picsum.photos/seed/hotel-227-3/800/500", false },
                    { 116, 227, "https://picsum.photos/seed/hotel-227-4/800/500", false },
                    { 117, 228, "https://picsum.photos/seed/hotel-228-1/800/500", true },
                    { 118, 228, "https://picsum.photos/seed/hotel-228-2/800/500", false },
                    { 119, 228, "https://picsum.photos/seed/hotel-228-3/800/500", false },
                    { 120, 228, "https://picsum.photos/seed/hotel-228-4/800/500", false },
                    { 121, 207, "https://picsum.photos/seed/hotel-207-1/800/500", true },
                    { 122, 207, "https://picsum.photos/seed/hotel-207-2/800/500", false },
                    { 123, 207, "https://picsum.photos/seed/hotel-207-3/800/500", false },
                    { 124, 207, "https://picsum.photos/seed/hotel-207-4/800/500", false },
                    { 125, 229, "https://picsum.photos/seed/hotel-229-1/800/500", true },
                    { 126, 229, "https://picsum.photos/seed/hotel-229-2/800/500", false },
                    { 127, 229, "https://picsum.photos/seed/hotel-229-3/800/500", false },
                    { 128, 229, "https://picsum.photos/seed/hotel-229-4/800/500", false },
                    { 129, 230, "https://picsum.photos/seed/hotel-230-1/800/500", true },
                    { 130, 230, "https://picsum.photos/seed/hotel-230-2/800/500", false },
                    { 131, 230, "https://picsum.photos/seed/hotel-230-3/800/500", false },
                    { 132, 230, "https://picsum.photos/seed/hotel-230-4/800/500", false },
                    { 133, 208, "https://picsum.photos/seed/hotel-208-1/800/500", true },
                    { 134, 208, "https://picsum.photos/seed/hotel-208-2/800/500", false },
                    { 135, 208, "https://picsum.photos/seed/hotel-208-3/800/500", false },
                    { 136, 208, "https://picsum.photos/seed/hotel-208-4/800/500", false },
                    { 137, 231, "https://picsum.photos/seed/hotel-231-1/800/500", true },
                    { 138, 231, "https://picsum.photos/seed/hotel-231-2/800/500", false },
                    { 139, 231, "https://picsum.photos/seed/hotel-231-3/800/500", false },
                    { 140, 231, "https://picsum.photos/seed/hotel-231-4/800/500", false },
                    { 141, 232, "https://picsum.photos/seed/hotel-232-1/800/500", true },
                    { 142, 232, "https://picsum.photos/seed/hotel-232-2/800/500", false },
                    { 143, 232, "https://picsum.photos/seed/hotel-232-3/800/500", false },
                    { 144, 232, "https://picsum.photos/seed/hotel-232-4/800/500", false },
                    { 145, 209, "https://picsum.photos/seed/hotel-209-1/800/500", true },
                    { 146, 209, "https://picsum.photos/seed/hotel-209-2/800/500", false },
                    { 147, 209, "https://picsum.photos/seed/hotel-209-3/800/500", false },
                    { 148, 209, "https://picsum.photos/seed/hotel-209-4/800/500", false },
                    { 149, 233, "https://picsum.photos/seed/hotel-233-1/800/500", true },
                    { 150, 233, "https://picsum.photos/seed/hotel-233-2/800/500", false },
                    { 151, 233, "https://picsum.photos/seed/hotel-233-3/800/500", false },
                    { 152, 233, "https://picsum.photos/seed/hotel-233-4/800/500", false },
                    { 153, 234, "https://picsum.photos/seed/hotel-234-1/800/500", true },
                    { 154, 234, "https://picsum.photos/seed/hotel-234-2/800/500", false },
                    { 155, 234, "https://picsum.photos/seed/hotel-234-3/800/500", false },
                    { 156, 234, "https://picsum.photos/seed/hotel-234-4/800/500", false },
                    { 157, 210, "https://picsum.photos/seed/hotel-210-1/800/500", true },
                    { 158, 210, "https://picsum.photos/seed/hotel-210-2/800/500", false },
                    { 159, 210, "https://picsum.photos/seed/hotel-210-3/800/500", false },
                    { 160, 210, "https://picsum.photos/seed/hotel-210-4/800/500", false },
                    { 161, 235, "https://picsum.photos/seed/hotel-235-1/800/500", true },
                    { 162, 235, "https://picsum.photos/seed/hotel-235-2/800/500", false },
                    { 163, 235, "https://picsum.photos/seed/hotel-235-3/800/500", false },
                    { 164, 235, "https://picsum.photos/seed/hotel-235-4/800/500", false },
                    { 165, 236, "https://picsum.photos/seed/hotel-236-1/800/500", true },
                    { 166, 236, "https://picsum.photos/seed/hotel-236-2/800/500", false },
                    { 167, 236, "https://picsum.photos/seed/hotel-236-3/800/500", false },
                    { 168, 236, "https://picsum.photos/seed/hotel-236-4/800/500", false },
                    { 169, 211, "https://picsum.photos/seed/hotel-211-1/800/500", true },
                    { 170, 211, "https://picsum.photos/seed/hotel-211-2/800/500", false },
                    { 171, 211, "https://picsum.photos/seed/hotel-211-3/800/500", false },
                    { 172, 211, "https://picsum.photos/seed/hotel-211-4/800/500", false },
                    { 173, 237, "https://picsum.photos/seed/hotel-237-1/800/500", true },
                    { 174, 237, "https://picsum.photos/seed/hotel-237-2/800/500", false },
                    { 175, 237, "https://picsum.photos/seed/hotel-237-3/800/500", false },
                    { 176, 237, "https://picsum.photos/seed/hotel-237-4/800/500", false },
                    { 177, 238, "https://picsum.photos/seed/hotel-238-1/800/500", true },
                    { 178, 238, "https://picsum.photos/seed/hotel-238-2/800/500", false },
                    { 179, 238, "https://picsum.photos/seed/hotel-238-3/800/500", false },
                    { 180, 238, "https://picsum.photos/seed/hotel-238-4/800/500", false },
                    { 181, 212, "https://picsum.photos/seed/hotel-212-1/800/500", true },
                    { 182, 212, "https://picsum.photos/seed/hotel-212-2/800/500", false },
                    { 183, 212, "https://picsum.photos/seed/hotel-212-3/800/500", false },
                    { 184, 212, "https://picsum.photos/seed/hotel-212-4/800/500", false },
                    { 185, 239, "https://picsum.photos/seed/hotel-239-1/800/500", true },
                    { 186, 239, "https://picsum.photos/seed/hotel-239-2/800/500", false },
                    { 187, 239, "https://picsum.photos/seed/hotel-239-3/800/500", false },
                    { 188, 239, "https://picsum.photos/seed/hotel-239-4/800/500", false },
                    { 189, 240, "https://picsum.photos/seed/hotel-240-1/800/500", true },
                    { 190, 240, "https://picsum.photos/seed/hotel-240-2/800/500", false },
                    { 191, 240, "https://picsum.photos/seed/hotel-240-3/800/500", false },
                    { 192, 240, "https://picsum.photos/seed/hotel-240-4/800/500", false },
                    { 193, 213, "https://picsum.photos/seed/hotel-213-1/800/500", true },
                    { 194, 213, "https://picsum.photos/seed/hotel-213-2/800/500", false },
                    { 195, 213, "https://picsum.photos/seed/hotel-213-3/800/500", false },
                    { 196, 213, "https://picsum.photos/seed/hotel-213-4/800/500", false },
                    { 197, 241, "https://picsum.photos/seed/hotel-241-1/800/500", true },
                    { 198, 241, "https://picsum.photos/seed/hotel-241-2/800/500", false },
                    { 199, 241, "https://picsum.photos/seed/hotel-241-3/800/500", false },
                    { 200, 241, "https://picsum.photos/seed/hotel-241-4/800/500", false },
                    { 201, 242, "https://picsum.photos/seed/hotel-242-1/800/500", true },
                    { 202, 242, "https://picsum.photos/seed/hotel-242-2/800/500", false },
                    { 203, 242, "https://picsum.photos/seed/hotel-242-3/800/500", false },
                    { 204, 242, "https://picsum.photos/seed/hotel-242-4/800/500", false },
                    { 205, 214, "https://picsum.photos/seed/hotel-214-1/800/500", true },
                    { 206, 214, "https://picsum.photos/seed/hotel-214-2/800/500", false },
                    { 207, 214, "https://picsum.photos/seed/hotel-214-3/800/500", false },
                    { 208, 214, "https://picsum.photos/seed/hotel-214-4/800/500", false },
                    { 209, 243, "https://picsum.photos/seed/hotel-243-1/800/500", true },
                    { 210, 243, "https://picsum.photos/seed/hotel-243-2/800/500", false },
                    { 211, 243, "https://picsum.photos/seed/hotel-243-3/800/500", false },
                    { 212, 243, "https://picsum.photos/seed/hotel-243-4/800/500", false },
                    { 213, 244, "https://picsum.photos/seed/hotel-244-1/800/500", true },
                    { 214, 244, "https://picsum.photos/seed/hotel-244-2/800/500", false },
                    { 215, 244, "https://picsum.photos/seed/hotel-244-3/800/500", false },
                    { 216, 244, "https://picsum.photos/seed/hotel-244-4/800/500", false }
                });

            migrationBuilder.InsertData(
                table: "HotelRooms",
                columns: new[] { "HotelId", "RoomId", "RoomsLeft" },
                values: new object[,]
                {
                    { 100, 1, 6 },
                    { 100, 2, 8 },
                    { 100, 3, 15 },
                    { 100, 4, 15 },
                    { 101, 1, 15 },
                    { 101, 2, 5 },
                    { 101, 3, 12 },
                    { 101, 4, 14 },
                    { 102, 1, 11 },
                    { 102, 2, 8 },
                    { 102, 3, 14 },
                    { 102, 4, 7 },
                    { 110, 1, 9 },
                    { 110, 2, 10 },
                    { 110, 3, 6 },
                    { 110, 4, 8 },
                    { 111, 1, 10 },
                    { 111, 2, 6 },
                    { 111, 3, 10 },
                    { 111, 4, 13 },
                    { 112, 1, 5 },
                    { 112, 2, 14 },
                    { 112, 3, 9 },
                    { 112, 4, 13 },
                    { 120, 1, 7 },
                    { 120, 2, 11 },
                    { 120, 3, 13 },
                    { 120, 4, 5 },
                    { 121, 1, 6 },
                    { 121, 2, 13 },
                    { 121, 3, 6 },
                    { 121, 4, 6 },
                    { 122, 1, 5 },
                    { 122, 2, 13 },
                    { 122, 3, 5 },
                    { 122, 4, 11 },
                    { 200, 1, 7 },
                    { 200, 2, 6 },
                    { 200, 3, 8 },
                    { 200, 4, 8 },
                    { 201, 1, 5 },
                    { 201, 2, 15 },
                    { 201, 3, 13 },
                    { 201, 4, 14 },
                    { 202, 1, 7 },
                    { 202, 2, 12 },
                    { 202, 3, 12 },
                    { 202, 4, 14 },
                    { 203, 1, 13 },
                    { 203, 2, 7 },
                    { 203, 3, 15 },
                    { 203, 4, 9 },
                    { 204, 1, 15 },
                    { 204, 2, 8 },
                    { 204, 3, 5 },
                    { 204, 4, 13 },
                    { 205, 1, 14 },
                    { 205, 2, 9 },
                    { 205, 3, 8 },
                    { 205, 4, 8 },
                    { 206, 1, 8 },
                    { 206, 2, 9 },
                    { 206, 3, 12 },
                    { 206, 4, 5 },
                    { 207, 1, 12 },
                    { 207, 2, 9 },
                    { 207, 3, 12 },
                    { 207, 4, 11 },
                    { 208, 1, 10 },
                    { 208, 2, 9 },
                    { 208, 3, 10 },
                    { 208, 4, 15 },
                    { 209, 1, 6 },
                    { 209, 2, 12 },
                    { 209, 3, 13 },
                    { 209, 4, 15 },
                    { 210, 1, 8 },
                    { 210, 2, 6 },
                    { 210, 3, 11 },
                    { 210, 4, 5 },
                    { 211, 1, 15 },
                    { 211, 2, 8 },
                    { 211, 3, 13 },
                    { 211, 4, 8 },
                    { 212, 1, 6 },
                    { 212, 2, 6 },
                    { 212, 3, 6 },
                    { 212, 4, 12 },
                    { 213, 1, 13 },
                    { 213, 2, 6 },
                    { 213, 3, 7 },
                    { 213, 4, 5 },
                    { 214, 1, 12 },
                    { 214, 2, 7 },
                    { 214, 3, 5 },
                    { 214, 4, 6 },
                    { 215, 1, 14 },
                    { 215, 2, 11 },
                    { 215, 3, 8 },
                    { 215, 4, 10 },
                    { 216, 1, 5 },
                    { 216, 2, 15 },
                    { 216, 3, 11 },
                    { 216, 4, 7 },
                    { 217, 1, 5 },
                    { 217, 2, 7 },
                    { 217, 3, 14 },
                    { 217, 4, 7 },
                    { 218, 1, 14 },
                    { 218, 2, 6 },
                    { 218, 3, 13 },
                    { 218, 4, 8 },
                    { 219, 1, 7 },
                    { 219, 2, 13 },
                    { 219, 3, 7 },
                    { 219, 4, 11 },
                    { 220, 1, 9 },
                    { 220, 2, 11 },
                    { 220, 3, 5 },
                    { 220, 4, 9 },
                    { 221, 1, 11 },
                    { 221, 2, 14 },
                    { 221, 3, 12 },
                    { 221, 4, 8 },
                    { 222, 1, 15 },
                    { 222, 2, 11 },
                    { 222, 3, 15 },
                    { 222, 4, 6 },
                    { 223, 1, 15 },
                    { 223, 2, 14 },
                    { 223, 3, 12 },
                    { 223, 4, 6 },
                    { 224, 1, 8 },
                    { 224, 2, 15 },
                    { 224, 3, 5 },
                    { 224, 4, 7 },
                    { 225, 1, 13 },
                    { 225, 2, 15 },
                    { 225, 3, 13 },
                    { 225, 4, 10 },
                    { 226, 1, 15 },
                    { 226, 2, 9 },
                    { 226, 3, 5 },
                    { 226, 4, 14 },
                    { 227, 1, 9 },
                    { 227, 2, 9 },
                    { 227, 3, 8 },
                    { 227, 4, 12 },
                    { 228, 1, 12 },
                    { 228, 2, 10 },
                    { 228, 3, 15 },
                    { 228, 4, 15 },
                    { 229, 1, 14 },
                    { 229, 2, 14 },
                    { 229, 3, 5 },
                    { 229, 4, 13 },
                    { 230, 1, 6 },
                    { 230, 2, 13 },
                    { 230, 3, 10 },
                    { 230, 4, 7 },
                    { 231, 1, 14 },
                    { 231, 2, 9 },
                    { 231, 3, 7 },
                    { 231, 4, 11 },
                    { 232, 1, 8 },
                    { 232, 2, 7 },
                    { 232, 3, 10 },
                    { 232, 4, 10 },
                    { 233, 1, 12 },
                    { 233, 2, 9 },
                    { 233, 3, 10 },
                    { 233, 4, 8 },
                    { 234, 1, 10 },
                    { 234, 2, 9 },
                    { 234, 3, 11 },
                    { 234, 4, 8 },
                    { 235, 1, 8 },
                    { 235, 2, 7 },
                    { 235, 3, 12 },
                    { 235, 4, 11 },
                    { 236, 1, 14 },
                    { 236, 2, 9 },
                    { 236, 3, 10 },
                    { 236, 4, 15 },
                    { 237, 1, 8 },
                    { 237, 2, 9 },
                    { 237, 3, 12 },
                    { 237, 4, 10 },
                    { 238, 1, 15 },
                    { 238, 2, 7 },
                    { 238, 3, 9 },
                    { 238, 4, 7 },
                    { 239, 1, 7 },
                    { 239, 2, 7 },
                    { 239, 3, 13 },
                    { 239, 4, 6 },
                    { 240, 1, 14 },
                    { 240, 2, 6 },
                    { 240, 3, 12 },
                    { 240, 4, 12 },
                    { 241, 1, 10 },
                    { 241, 2, 14 },
                    { 241, 3, 11 },
                    { 241, 4, 13 },
                    { 242, 1, 6 },
                    { 242, 2, 13 },
                    { 242, 3, 15 },
                    { 242, 4, 9 },
                    { 243, 1, 13 },
                    { 243, 2, 6 },
                    { 243, 3, 9 },
                    { 243, 4, 5 },
                    { 244, 1, 12 },
                    { 244, 2, 9 },
                    { 244, 3, 15 },
                    { 244, 4, 7 }
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
                    { 1, new DateTime(2025, 12, 27, 20, 48, 14, 321, DateTimeKind.Utc).AddTicks(8241), "test.pdf", 4, "Smisleno me motiviše vaša moderna organizacija i želja da radim na projektima koji imaju stvarni utjecaj. Vjerujem da mogu doprinijeti svojim radnim navikama i voljom za učenjem." },
                    { 2, new DateTime(2025, 12, 25, 20, 48, 14, 321, DateTimeKind.Utc).AddTicks(8244), "test.pdf", 5, "Privukla me prilika da radim u dinamičnom okruženju gdje se cijeni timski rad i napredak. Želim biti dio profesionalne i pozitivne radne zajednice." },
                    { 3, new DateTime(2025, 12, 22, 20, 48, 14, 321, DateTimeKind.Utc).AddTicks(8245), "test.pdf", 6, "Vaša kompanija je prepoznata po kvalitetnom radu i inovacijama. Smatram da mogu mnogo naučiti i istovremeno doprinijeti svojim iskustvom i posvećenošću." },
                    { 4, new DateTime(2025, 12, 19, 20, 48, 14, 321, DateTimeKind.Utc).AddTicks(8246), "test.pdf", 7, "Želim raditi u sredini koja podstiče razvoj i podržava kreativnost. Vaša firma upravo to nudi, i zato bih voljela biti dio vašeg tima." },
                    { 5, new DateTime(2025, 12, 17, 20, 48, 14, 321, DateTimeKind.Utc).AddTicks(8248), "test.pdf", 8, "Vašu kompaniju vidim kao mjesto gdje se talenat i rad cijene. Motivisan sam da se usavršavam i doprinosim vašem rastu." },
                    { 6, new DateTime(2025, 12, 15, 20, 48, 14, 321, DateTimeKind.Utc).AddTicks(8249), "test.pdf", 9, "Tražim priliku da radim u profesionalnoj sredini gdje mogu napredovati. Posebno me privlači vaša organizacijska kultura i pristup radu." },
                    { 7, new DateTime(2025, 12, 12, 20, 48, 14, 321, DateTimeKind.Utc).AddTicks(8251), "test.pdf", 10, "Motiviše me želja da učim nove tehnologije i doprinesem timskim rezultatima. Vjerujem da bih se dobro uklopio u vaše okruženje." },
                    { 8, new DateTime(2025, 12, 11, 20, 48, 14, 321, DateTimeKind.Utc).AddTicks(8252), "test.pdf", 11, "Smatram da je vaša kompanija idealno mjesto za profesionalni rast. Cijenim vaš pristup organizaciji i inovativnosti." },
                    { 9, new DateTime(2025, 12, 9, 20, 48, 14, 321, DateTimeKind.Utc).AddTicks(8253), "test.pdf", 12, "Zainteresovana sam za rad kod vas jer nudite stabilno i ugodno radno okruženje u kojem se prepoznaje trud i zalaganje." },
                    { 10, new DateTime(2025, 12, 7, 20, 48, 14, 321, DateTimeKind.Utc).AddTicks(8255), "test.pdf", 13, "Želim biti dio tima koji teži kvaliteti i rastu. Vaša firma mi djeluje kao pravo mjesto za to." },
                    { 11, new DateTime(2025, 12, 6, 20, 48, 14, 321, DateTimeKind.Utc).AddTicks(8256), "test.pdf", 14, "Motivisan sam mogućnošću da radim u kompaniji koja cijeni profesionalizam i timski rad. Spreman sam da dam svoj maksimum." },
                    { 12, new DateTime(2025, 12, 5, 20, 48, 14, 321, DateTimeKind.Utc).AddTicks(8257), "test.pdf", 15, "Privlači me prilika da radim sa stručnim i kreativnim timom. Vaš način rada me posebno inspiriše." },
                    { 13, new DateTime(2025, 12, 4, 20, 48, 14, 321, DateTimeKind.Utc).AddTicks(8258), "test.pdf", 16, "Vaša kompanija nudi odlične mogućnosti za rast i razvoj, što je glavni razlog moje prijave." },
                    { 14, new DateTime(2025, 12, 3, 20, 48, 14, 321, DateTimeKind.Utc).AddTicks(8259), "test.pdf", 17, "Motivisan sam da učim, radim i napredujem. Vjerujem da bih bio odličan dodatak vašem timu." },
                    { 15, new DateTime(2025, 12, 2, 20, 48, 14, 321, DateTimeKind.Utc).AddTicks(8261), "test.pdf", 18, "Želim raditi u okruženju gdje se cijeni inicijativa, kreativnost i kvalitetan rad. Vaša firma ispunjava sve te kriterije." },
                    { 16, new DateTime(2025, 12, 1, 20, 48, 14, 321, DateTimeKind.Utc).AddTicks(8267), "test.pdf", 19, "Vidim veliku vrijednost u vašim projektima i načinu rada. Želim biti dio tima koji radi sa strašću." },
                    { 17, new DateTime(2025, 11, 30, 20, 48, 14, 321, DateTimeKind.Utc).AddTicks(8268), "test.pdf", 20, "Prijavljujem se jer vjerujem da bih u vašoj kompaniji mogao postići veliki profesionalni iskorak." },
                    { 18, new DateTime(2025, 11, 29, 20, 48, 14, 321, DateTimeKind.Utc).AddTicks(8269), "test.pdf", 21, "Motiviše me želja da radim u stabilnoj i ozbiljnoj organizaciji koja nudi perspektivu i razvoj." },
                    { 19, new DateTime(2025, 11, 28, 20, 48, 14, 321, DateTimeKind.Utc).AddTicks(8271), "test.pdf", 22, "Vaša kompanija mi djeluje kao pravo mjesto da pokažem svoje vještine i dodatno ih unaprijedim." },
                    { 20, new DateTime(2025, 11, 27, 20, 48, 14, 321, DateTimeKind.Utc).AddTicks(8272), "test.pdf", 23, "Privlači me vaš profesionalan pristup, moderna organizacija i atmosfera koja podstiče učenje." },
                    { 21, new DateTime(2025, 11, 26, 20, 48, 14, 321, DateTimeKind.Utc).AddTicks(8273), "test.pdf", 24, "Motivisana sam da radim u vašoj firmi jer cijenim vaše vrijednosti i način poslovanja. Vjerujem da bih se idealno uklopila." }
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
                columns: new[] { "DepartureDate", "HotelId", "OfferDetailsId", "ReturnDate" },
                values: new object[,]
                {
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 100, 1, new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 100, 1, new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 100, 1, new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 101, 1, new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 101, 1, new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 101, 1, new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 102, 1, new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 102, 1, new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 102, 1, new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 110, 2, new DateTime(2026, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 110, 2, new DateTime(2026, 5, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 110, 2, new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 111, 2, new DateTime(2026, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 111, 2, new DateTime(2026, 5, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 111, 2, new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 112, 2, new DateTime(2026, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 112, 2, new DateTime(2026, 5, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 112, 2, new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 120, 3, new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 120, 3, new DateTime(2026, 5, 19, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 120, 3, new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 121, 3, new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 121, 3, new DateTime(2026, 5, 19, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 121, 3, new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 122, 3, new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 122, 3, new DateTime(2026, 5, 19, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 122, 3, new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 200, 4, new DateTime(2026, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 200, 4, new DateTime(2026, 5, 21, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 200, 4, new DateTime(2026, 9, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 215, 4, new DateTime(2026, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 215, 4, new DateTime(2026, 5, 21, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 215, 4, new DateTime(2026, 9, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 216, 4, new DateTime(2026, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 216, 4, new DateTime(2026, 5, 21, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 216, 4, new DateTime(2026, 9, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 201, 5, new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 201, 5, new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 201, 5, new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 217, 5, new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 217, 5, new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 217, 5, new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 218, 5, new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 218, 5, new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 218, 5, new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 202, 6, new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 202, 6, new DateTime(2026, 5, 19, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 202, 6, new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 219, 6, new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 219, 6, new DateTime(2026, 5, 19, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 219, 6, new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 220, 6, new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 220, 6, new DateTime(2026, 5, 19, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 220, 6, new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 203, 7, new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 203, 7, new DateTime(2026, 5, 18, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 203, 7, new DateTime(2026, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 221, 7, new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 221, 7, new DateTime(2026, 5, 18, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 221, 7, new DateTime(2026, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 222, 7, new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 222, 7, new DateTime(2026, 5, 18, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 222, 7, new DateTime(2026, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 204, 8, new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 204, 8, new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 204, 8, new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 223, 8, new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 223, 8, new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 223, 8, new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 224, 8, new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 224, 8, new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 224, 8, new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 205, 9, new DateTime(2026, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 205, 9, new DateTime(2026, 5, 21, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 205, 9, new DateTime(2026, 9, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 225, 9, new DateTime(2026, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 225, 9, new DateTime(2026, 5, 21, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 225, 9, new DateTime(2026, 9, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 226, 9, new DateTime(2026, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 226, 9, new DateTime(2026, 5, 21, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 226, 9, new DateTime(2026, 9, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 206, 10, new DateTime(2026, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 206, 10, new DateTime(2026, 5, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 206, 10, new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 227, 10, new DateTime(2026, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 227, 10, new DateTime(2026, 5, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 227, 10, new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 228, 10, new DateTime(2026, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 228, 10, new DateTime(2026, 5, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 228, 10, new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 207, 11, new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 207, 11, new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 207, 11, new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 229, 11, new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 229, 11, new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 229, 11, new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 230, 11, new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 230, 11, new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 230, 11, new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 208, 12, new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 208, 12, new DateTime(2026, 5, 18, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 208, 12, new DateTime(2026, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 231, 12, new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 231, 12, new DateTime(2026, 5, 18, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 231, 12, new DateTime(2026, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 232, 12, new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 232, 12, new DateTime(2026, 5, 18, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 232, 12, new DateTime(2026, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 209, 13, new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 209, 13, new DateTime(2026, 5, 19, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 209, 13, new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 233, 13, new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 233, 13, new DateTime(2026, 5, 19, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 233, 13, new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 234, 13, new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 234, 13, new DateTime(2026, 5, 19, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 234, 13, new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 210, 14, new DateTime(2026, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 210, 14, new DateTime(2026, 5, 23, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 210, 14, new DateTime(2026, 9, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 235, 14, new DateTime(2026, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 235, 14, new DateTime(2026, 5, 23, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 235, 14, new DateTime(2026, 9, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 236, 14, new DateTime(2026, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 236, 14, new DateTime(2026, 5, 23, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 236, 14, new DateTime(2026, 9, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 211, 15, new DateTime(2026, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 211, 15, new DateTime(2026, 5, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 211, 15, new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 237, 15, new DateTime(2026, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 237, 15, new DateTime(2026, 5, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 237, 15, new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 238, 15, new DateTime(2026, 3, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 238, 15, new DateTime(2026, 5, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 238, 15, new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 212, 16, new DateTime(2026, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 212, 16, new DateTime(2026, 5, 21, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 212, 16, new DateTime(2026, 9, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 239, 16, new DateTime(2026, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 239, 16, new DateTime(2026, 5, 21, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 239, 16, new DateTime(2026, 9, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 240, 16, new DateTime(2026, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 240, 16, new DateTime(2026, 5, 21, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 240, 16, new DateTime(2026, 9, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 213, 17, new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 213, 17, new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 213, 17, new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 241, 17, new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 241, 17, new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 241, 17, new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 242, 17, new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 242, 17, new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 242, 17, new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 214, 18, new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 214, 18, new DateTime(2026, 5, 18, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 214, 18, new DateTime(2026, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 243, 18, new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 243, 18, new DateTime(2026, 5, 18, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 243, 18, new DateTime(2026, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 244, 18, new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 244, 18, new DateTime(2026, 5, 18, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc), 244, 18, new DateTime(2026, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc) }
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
                    { 2017, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0.0, 122, true, true, 3, 50m, 1, 350m, 22, "", false, true, false },
                    { 2018, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0.0, 100, false, true, 1, 150m, 1, 450m, 23, "", false, true, false },
                    { 2019, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0.0, 111, true, true, 2, 0m, 3, 900m, 24, "", false, false, true },
                    { 3000, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 0.0, 100, false, true, 1, 0m, 1, 0m, 6, "", false, true, true },
                    { 3001, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 0.0, 110, false, true, 2, 0m, 2, 0m, 6, "", false, true, true },
                    { 3002, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 0.0, 120, false, true, 3, 0m, 1, 0m, 6, "", false, true, true }
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

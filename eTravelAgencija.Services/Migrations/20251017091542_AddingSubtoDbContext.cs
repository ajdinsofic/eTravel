using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eTravelAgencija.Services.Migrations
{
    /// <inheritdoc />
    public partial class AddingSubtoDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "OfferSubCategories",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[] { -1, 1, "Bez podkategorije" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OfferSubCategories",
                keyColumn: "Id",
                keyValue: -1);
        }
    }
}

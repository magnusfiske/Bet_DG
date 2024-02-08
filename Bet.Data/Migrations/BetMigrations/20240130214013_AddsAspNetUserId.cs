using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bet.Data.Migrations.BetMigrations
{
    /// <inheritdoc />
    public partial class AddsAspNetUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.AddColumn<string>(
                name: "AspNetUserId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AspNetUserId",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Name", "Position" },
                values: new object[,]
                {
                    { 1, "Hammarby IF", 0 },
                    { 2, "Malmö FF", 0 },
                    { 3, "IF Elfsborg", 0 },
                    { 4, "BK Häcken", 0 },
                    { 5, "Djurgården", 0 },
                    { 6, "Kalmar FF", 0 },
                    { 7, "IFK Norrköping", 0 },
                    { 8, "IFK Värnamo", 0 },
                    { 9, "IK Sirius", 0 },
                    { 10, "Mjällby AIF", 0 },
                    { 11, "AIK", 0 },
                    { 12, "Hamlstad BK", 0 },
                    { 13, "IFK Göteborg", 0 },
                    { 14, "IF Brommapojkarna", 0 },
                    { 15, "Degerfors IF", 0 },
                    { 16, "Varberg BoIS", 0 }
                });
        }
    }
}

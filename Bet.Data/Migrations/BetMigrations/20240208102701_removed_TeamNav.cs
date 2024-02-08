using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bet.Data.Migrations.BetMigrations
{
    /// <inheritdoc />
    public partial class removed_TeamNav : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BetRows_TeamId",
                table: "BetRows");

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "IFK Norrköping FK");

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 12,
                column: "Name",
                value: "Halmstad");

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 16,
                column: "Name",
                value: "Varbergs BoIS FC");

            migrationBuilder.CreateIndex(
                name: "IX_BetRows_TeamId",
                table: "BetRows",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BetRows_TeamId",
                table: "BetRows");

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "IFK Norrköping");

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 12,
                column: "Name",
                value: "Hamlstad");

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 16,
                column: "Name",
                value: "Varberg BoIS FC");

            migrationBuilder.CreateIndex(
                name: "IX_BetRows_TeamId",
                table: "BetRows",
                column: "TeamId",
                unique: true);
        }
    }
}

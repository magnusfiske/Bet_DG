using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bet.Data.Migrations.BetMigrations
{
    /// <inheritdoc />
    public partial class AddingAspNetUserNav : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AspNetUserId",
                table: "User",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_AspNetUserId",
                table: "User",
                column: "AspNetUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_AspNetUsers_AspNetUserId",
                table: "User",
                column: "AspNetUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_AspNetUsers_AspNetUserId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_AspNetUserId",
                table: "User");

            migrationBuilder.AlterColumn<string>(
                name: "AspNetUserId",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}

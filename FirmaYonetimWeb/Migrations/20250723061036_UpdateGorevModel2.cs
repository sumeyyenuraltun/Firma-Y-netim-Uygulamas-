using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirmaYonetimWeb.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGorevModel2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GoreveAtananId",
                table: "Gorevler",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gorevler_GoreveAtananId",
                table: "Gorevler",
                column: "GoreveAtananId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gorevler_AspNetUsers_GoreveAtananId",
                table: "Gorevler",
                column: "GoreveAtananId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gorevler_AspNetUsers_GoreveAtananId",
                table: "Gorevler");

            migrationBuilder.DropIndex(
                name: "IX_Gorevler_GoreveAtananId",
                table: "Gorevler");

            migrationBuilder.DropColumn(
                name: "GoreveAtananId",
                table: "Gorevler");
        }
    }
}

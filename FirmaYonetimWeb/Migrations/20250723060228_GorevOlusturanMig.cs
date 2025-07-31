using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirmaYonetimWeb.Migrations
{
    /// <inheritdoc />
    public partial class GorevOlusturanMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GorevOlusturanId",
                table: "Gorevler",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Gorevler_GorevOlusturanId",
                table: "Gorevler",
                column: "GorevOlusturanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gorevler_AspNetUsers_GorevOlusturanId",
                table: "Gorevler",
                column: "GorevOlusturanId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gorevler_AspNetUsers_GorevOlusturanId",
                table: "Gorevler");

            migrationBuilder.DropIndex(
                name: "IX_Gorevler_GorevOlusturanId",
                table: "Gorevler");

            migrationBuilder.DropColumn(
                name: "GorevOlusturanId",
                table: "Gorevler");
        }
    }
}

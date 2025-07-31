using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirmaYonetimWeb.Migrations
{
    /// <inheritdoc />
    public partial class BelediyePersonelUpdateMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonelAdi",
                table: "Gorevler");

            migrationBuilder.AddColumn<int>(
                name: "BelediyePersonelId",
                table: "Gorevler",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Gorevler_BelediyePersonelId",
                table: "Gorevler",
                column: "BelediyePersonelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gorevler_BelediyePersonelleri_BelediyePersonelId",
                table: "Gorevler",
                column: "BelediyePersonelId",
                principalTable: "BelediyePersonelleri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gorevler_BelediyePersonelleri_BelediyePersonelId",
                table: "Gorevler");

            migrationBuilder.DropIndex(
                name: "IX_Gorevler_BelediyePersonelId",
                table: "Gorevler");

            migrationBuilder.DropColumn(
                name: "BelediyePersonelId",
                table: "Gorevler");

            migrationBuilder.AddColumn<string>(
                name: "PersonelAdi",
                table: "Gorevler",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}

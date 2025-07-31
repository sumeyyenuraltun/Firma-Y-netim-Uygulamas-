using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirmaYonetimWeb.Migrations
{
    /// <inheritdoc />
    public partial class GorevSonGuncelleme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Not",
                table: "Gorevler",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OnemDerecesi",
                table: "Gorevler",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PersonelAdi",
                table: "Gorevler",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Not",
                table: "Gorevler");

            migrationBuilder.DropColumn(
                name: "OnemDerecesi",
                table: "Gorevler");

            migrationBuilder.DropColumn(
                name: "PersonelAdi",
                table: "Gorevler");
        }
    }
}

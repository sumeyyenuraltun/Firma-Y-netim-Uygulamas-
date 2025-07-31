using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirmaYonetimWeb.Migrations
{
    /// <inheritdoc />
    public partial class FileUpdateFix3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DosyaAdi",
                table: "Gorevler");

            migrationBuilder.AddColumn<string>(
                name: "DosyaAdiJson",
                table: "Gorevler",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DosyaAdiJson",
                table: "Gorevler");

            migrationBuilder.AddColumn<List<string>>(
                name: "DosyaAdi",
                table: "Gorevler",
                type: "jsonb",
                nullable: false);
        }
    }
}

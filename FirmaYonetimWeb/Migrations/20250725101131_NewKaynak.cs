using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FirmaYonetimWeb.Migrations
{
    /// <inheritdoc />
    public partial class NewKaynak : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anies_KaynakGirisleri_KaynakGirisId",
                table: "Anies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Anies",
                table: "Anies");

            migrationBuilder.RenameTable(
                name: "Anies",
                newName: "Anys");

            migrationBuilder.RenameIndex(
                name: "IX_Anies_KaynakGirisId",
                table: "Anys",
                newName: "IX_Anys_KaynakGirisId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Anys",
                table: "Anys",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "GeoServers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KaynakGirisId = table.Column<int>(type: "integer", nullable: false),
                    IP = table.Column<string>(type: "text", nullable: false),
                    Port = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoServers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeoServers_KaynakGirisleri_KaynakGirisId",
                        column: x => x.KaynakGirisId,
                        principalTable: "KaynakGirisleri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeoServers_KaynakGirisId",
                table: "GeoServers",
                column: "KaynakGirisId");

            migrationBuilder.AddForeignKey(
                name: "FK_Anys_KaynakGirisleri_KaynakGirisId",
                table: "Anys",
                column: "KaynakGirisId",
                principalTable: "KaynakGirisleri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anys_KaynakGirisleri_KaynakGirisId",
                table: "Anys");

            migrationBuilder.DropTable(
                name: "GeoServers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Anys",
                table: "Anys");

            migrationBuilder.RenameTable(
                name: "Anys",
                newName: "Anies");

            migrationBuilder.RenameIndex(
                name: "IX_Anys_KaynakGirisId",
                table: "Anies",
                newName: "IX_Anies_KaynakGirisId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Anies",
                table: "Anies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Anies_KaynakGirisleri_KaynakGirisId",
                table: "Anies",
                column: "KaynakGirisId",
                principalTable: "KaynakGirisleri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

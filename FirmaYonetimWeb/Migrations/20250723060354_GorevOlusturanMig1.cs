using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirmaYonetimWeb.Migrations
{
    /// <inheritdoc />
    public partial class GorevOlusturanMig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gorevler_AspNetUsers_GorevOlusturanId",
                table: "Gorevler");

            migrationBuilder.AlterColumn<int>(
                name: "GorevOlusturanId",
                table: "Gorevler",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Gorevler_AspNetUsers_GorevOlusturanId",
                table: "Gorevler",
                column: "GorevOlusturanId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gorevler_AspNetUsers_GorevOlusturanId",
                table: "Gorevler");

            migrationBuilder.AlterColumn<int>(
                name: "GorevOlusturanId",
                table: "Gorevler",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Gorevler_AspNetUsers_GorevOlusturanId",
                table: "Gorevler",
                column: "GorevOlusturanId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

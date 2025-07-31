using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirmaYonetimWeb.Migrations
{
    /// <inheritdoc />
    public partial class BaseModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "VPNs");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "VPNAltTurleri");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "RDPs");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PostrgreSQLs");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Notlar");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "KaynakTurleri");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "KaynakGirisleri");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Gorevler");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Belediyeler");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "BelediyeKaynakları");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Anies");

            migrationBuilder.AlterColumn<string>(
                name: "DosyaAdiJson",
                table: "Gorevler",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "VPNs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "VPNAltTurleri",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Services",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "RDPs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "PostrgreSQLs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Notlar",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "KaynakTurleri",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "KaynakGirisleri",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "DosyaAdiJson",
                table: "Gorevler",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Gorevler",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Belediyeler",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "BelediyeKaynakları",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "AuditLogs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Anies",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}

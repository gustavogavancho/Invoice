using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invoice.Repository.Migrations
{
    public partial class Canceled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Issuers",
                keyColumn: "Id",
                keyValue: new Guid("83f3e3ab-ce7f-44d2-9fbb-a28206fd4139"));

            migrationBuilder.AddColumn<bool>(
                name: "Canceled",
                table: "Invoices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanceledReason",
                table: "Invoices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Issuers",
                columns: new[] { "Id", "Address", "BetaCertificate", "BetaCertificatePasword", "Department", "District", "EstablishmentCode", "GeoCode", "IssuerId", "IssuerName", "IssuerType", "ProdCertificate", "ProdCertificatePasword", "Province" },
                values: new object[] { new Guid("54f034c5-002a-49cf-95b2-9e8680fdd17f"), "PSJE. LIMATAMBO 121", null, null, "SAN MARTIN", "TARAPOTO", "0000", "220901", 20606022779m, "SWIFTLINE SAC", "6", null, null, "SAN MARTIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Issuers",
                keyColumn: "Id",
                keyValue: new Guid("54f034c5-002a-49cf-95b2-9e8680fdd17f"));

            migrationBuilder.DropColumn(
                name: "Canceled",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "CanceledReason",
                table: "Invoices");

            migrationBuilder.InsertData(
                table: "Issuers",
                columns: new[] { "Id", "Address", "BetaCertificate", "BetaCertificatePasword", "Department", "District", "EstablishmentCode", "GeoCode", "IssuerId", "IssuerName", "IssuerType", "ProdCertificate", "ProdCertificatePasword", "Province" },
                values: new object[] { new Guid("83f3e3ab-ce7f-44d2-9fbb-a28206fd4139"), "PSJE. LIMATAMBO 121", null, null, "SAN MARTIN", "TARAPOTO", "0000", "220901", 20606022779m, "SWIFTLINE SAC", "6", null, null, "SAN MARTIN" });
        }
    }
}

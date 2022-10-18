using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invoice.Repository.Migrations
{
    public partial class AddPassCert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Issuers",
                keyColumn: "Id",
                keyValue: new Guid("ca5b133a-2b17-427c-ae13-bb14951b0a32"));

            migrationBuilder.AddColumn<string>(
                name: "BetaCertificatePasword",
                table: "Issuers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProdCertificatePasword",
                table: "Issuers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Issuers",
                columns: new[] { "Id", "Address", "BetaCertificate", "BetaCertificatePasword", "Department", "District", "EstablishmentCode", "GeoCode", "IssuerId", "IssuerName", "IssuerType", "ProdCertificate", "ProdCertificatePasword", "Province" },
                values: new object[] { new Guid("c9567ea8-8ab4-4b72-b171-25a228386b9d"), "PSJE. LIMATAMBO 121", null, null, "SAN MARTIN", "TARAPOTO", "0000", "220901", 20606022779m, "SWIFTLINE SAC", "6", null, null, "SAN MARTIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Issuers",
                keyColumn: "Id",
                keyValue: new Guid("c9567ea8-8ab4-4b72-b171-25a228386b9d"));

            migrationBuilder.DropColumn(
                name: "BetaCertificatePasword",
                table: "Issuers");

            migrationBuilder.DropColumn(
                name: "ProdCertificatePasword",
                table: "Issuers");

            migrationBuilder.InsertData(
                table: "Issuers",
                columns: new[] { "Id", "Address", "BetaCertificate", "Department", "District", "EstablishmentCode", "GeoCode", "IssuerId", "IssuerName", "IssuerType", "ProdCertificate", "Province" },
                values: new object[] { new Guid("ca5b133a-2b17-427c-ae13-bb14951b0a32"), "PSJE. LIMATAMBO 121", null, "SAN MARTIN", "TARAPOTO", "0000", "220901", 20606022779m, "SWIFTLINE SAC", "6", null, "SAN MARTIN" });
        }
    }
}

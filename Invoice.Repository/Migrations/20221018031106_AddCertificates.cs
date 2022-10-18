using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invoice.Repository.Migrations
{
    public partial class AddCertificates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Issuers",
                keyColumn: "Id",
                keyValue: new Guid("4ae311ee-6a07-4111-b940-107d917a231f"));

            migrationBuilder.AddColumn<byte[]>(
                name: "BetaCertificate",
                table: "Issuers",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ProdCertificate",
                table: "Issuers",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Issuers",
                columns: new[] { "Id", "Address", "BetaCertificate", "Department", "District", "EstablishmentCode", "GeoCode", "IssuerId", "IssuerName", "IssuerType", "ProdCertificate", "Province" },
                values: new object[] { new Guid("ca5b133a-2b17-427c-ae13-bb14951b0a32"), "PSJE. LIMATAMBO 121", null, "SAN MARTIN", "TARAPOTO", "0000", "220901", 20606022779m, "SWIFTLINE SAC", "6", null, "SAN MARTIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Issuers",
                keyColumn: "Id",
                keyValue: new Guid("ca5b133a-2b17-427c-ae13-bb14951b0a32"));

            migrationBuilder.DropColumn(
                name: "BetaCertificate",
                table: "Issuers");

            migrationBuilder.DropColumn(
                name: "ProdCertificate",
                table: "Issuers");

            migrationBuilder.InsertData(
                table: "Issuers",
                columns: new[] { "Id", "Address", "Department", "District", "EstablishmentCode", "GeoCode", "IssuerId", "IssuerName", "IssuerType", "Province" },
                values: new object[] { new Guid("4ae311ee-6a07-4111-b940-107d917a231f"), "PSJE. LIMATAMBO 121", "SAN MARTIN", "TARAPOTO", "0000", "220901", 20606022779m, "SWIFTLINE SAC", "6", "SAN MARTIN" });
        }
    }
}

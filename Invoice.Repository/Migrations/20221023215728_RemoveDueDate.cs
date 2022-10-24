using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invoice.Repository.Migrations
{
    public partial class RemoveDueDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Issuers",
                keyColumn: "Id",
                keyValue: new Guid("ff30cf89-9a7a-47e9-8361-4f02f7a4a761"));

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Invoices");

            migrationBuilder.InsertData(
                table: "Issuers",
                columns: new[] { "Id", "Address", "BetaCertificate", "BetaCertificatePasword", "Department", "District", "EstablishmentCode", "GeoCode", "IssuerId", "IssuerName", "IssuerType", "ProdCertificate", "ProdCertificatePasword", "Province" },
                values: new object[] { new Guid("5bc6033b-e275-4cb6-97c1-1a43fda1c1dd"), "PSJE. LIMATAMBO 121", null, null, "SAN MARTIN", "TARAPOTO", "0000", "220901", 20606022779m, "SWIFTLINE SAC", "6", null, null, "SAN MARTIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Issuers",
                keyColumn: "Id",
                keyValue: new Guid("5bc6033b-e275-4cb6-97c1-1a43fda1c1dd"));

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Invoices",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Issuers",
                columns: new[] { "Id", "Address", "BetaCertificate", "BetaCertificatePasword", "Department", "District", "EstablishmentCode", "GeoCode", "IssuerId", "IssuerName", "IssuerType", "ProdCertificate", "ProdCertificatePasword", "Province" },
                values: new object[] { new Guid("ff30cf89-9a7a-47e9-8361-4f02f7a4a761"), "PSJE. LIMATAMBO 121", null, null, "SAN MARTIN", "TARAPOTO", "0000", "220901", 20606022779m, "SWIFTLINE SAC", "6", null, null, "SAN MARTIN" });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invoice.Repository.Migrations
{
    public partial class UpdateName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Issuers",
                keyColumn: "Id",
                keyValue: new Guid("3aa8665c-b43c-433e-bebb-a051a6ecf439"));

            migrationBuilder.RenameColumn(
                name: "SummaryApproved",
                table: "Invoices",
                newName: "SummarySended");

            migrationBuilder.InsertData(
                table: "Issuers",
                columns: new[] { "Id", "Address", "BetaCertificate", "BetaCertificatePasword", "Department", "District", "EstablishmentCode", "GeoCode", "IssuerId", "IssuerName", "IssuerType", "ProdCertificate", "ProdCertificatePasword", "Province" },
                values: new object[] { new Guid("c3393306-85df-4876-bafc-b05a98b09f2d"), "PSJE. LIMATAMBO 121", null, null, "SAN MARTIN", "TARAPOTO", "0000", "220901", 20606022779m, "SWIFTLINE SAC", "6", null, null, "SAN MARTIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Issuers",
                keyColumn: "Id",
                keyValue: new Guid("c3393306-85df-4876-bafc-b05a98b09f2d"));

            migrationBuilder.RenameColumn(
                name: "SummarySended",
                table: "Invoices",
                newName: "SummaryApproved");

            migrationBuilder.InsertData(
                table: "Issuers",
                columns: new[] { "Id", "Address", "BetaCertificate", "BetaCertificatePasword", "Department", "District", "EstablishmentCode", "GeoCode", "IssuerId", "IssuerName", "IssuerType", "ProdCertificate", "ProdCertificatePasword", "Province" },
                values: new object[] { new Guid("3aa8665c-b43c-433e-bebb-a051a6ecf439"), "PSJE. LIMATAMBO 121", null, null, "SAN MARTIN", "TARAPOTO", "0000", "220901", 20606022779m, "SWIFTLINE SAC", "6", null, null, "SAN MARTIN" });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invoice.Repository.Migrations
{
    public partial class ChangeInvoiceXmlName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Issuers",
                keyColumn: "Id",
                keyValue: new Guid("5bc6033b-e275-4cb6-97c1-1a43fda1c1dd"));

            migrationBuilder.RenameColumn(
                name: "InvoiceSendXml",
                table: "Invoices",
                newName: "InvoiceXml");

            migrationBuilder.InsertData(
                table: "Issuers",
                columns: new[] { "Id", "Address", "BetaCertificate", "BetaCertificatePasword", "Department", "District", "EstablishmentCode", "GeoCode", "IssuerId", "IssuerName", "IssuerType", "ProdCertificate", "ProdCertificatePasword", "Province" },
                values: new object[] { new Guid("ad370312-7e4b-44db-8ab5-b55661c613c2"), "PSJE. LIMATAMBO 121", null, null, "SAN MARTIN", "TARAPOTO", "0000", "220901", 20606022779m, "SWIFTLINE SAC", "6", null, null, "SAN MARTIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Issuers",
                keyColumn: "Id",
                keyValue: new Guid("ad370312-7e4b-44db-8ab5-b55661c613c2"));

            migrationBuilder.RenameColumn(
                name: "InvoiceXml",
                table: "Invoices",
                newName: "InvoiceSendXml");

            migrationBuilder.InsertData(
                table: "Issuers",
                columns: new[] { "Id", "Address", "BetaCertificate", "BetaCertificatePasword", "Department", "District", "EstablishmentCode", "GeoCode", "IssuerId", "IssuerName", "IssuerType", "ProdCertificate", "ProdCertificatePasword", "Province" },
                values: new object[] { new Guid("5bc6033b-e275-4cb6-97c1-1a43fda1c1dd"), "PSJE. LIMATAMBO 121", null, null, "SAN MARTIN", "TARAPOTO", "0000", "220901", 20606022779m, "SWIFTLINE SAC", "6", null, null, "SAN MARTIN" });
        }
    }
}

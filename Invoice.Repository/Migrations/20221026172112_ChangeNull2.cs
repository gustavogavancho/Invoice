using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invoice.Repository.Migrations
{
    public partial class ChangeNull2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Issuers",
                keyColumn: "Id",
                keyValue: new Guid("e268e6a7-99c2-4e8b-bd6d-2ee3be8b117f"));

            migrationBuilder.AlterColumn<string>(
                name: "NoteTypeCode",
                table: "InvoiceDetail",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(4)",
                oldMaxLength: 4);

            migrationBuilder.InsertData(
                table: "Issuers",
                columns: new[] { "Id", "Address", "BetaCertificate", "BetaCertificatePasword", "Department", "District", "EstablishmentCode", "GeoCode", "IssuerId", "IssuerName", "IssuerType", "ProdCertificate", "ProdCertificatePasword", "Province" },
                values: new object[] { new Guid("83f3e3ab-ce7f-44d2-9fbb-a28206fd4139"), "PSJE. LIMATAMBO 121", null, null, "SAN MARTIN", "TARAPOTO", "0000", "220901", 20606022779m, "SWIFTLINE SAC", "6", null, null, "SAN MARTIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Issuers",
                keyColumn: "Id",
                keyValue: new Guid("83f3e3ab-ce7f-44d2-9fbb-a28206fd4139"));

            migrationBuilder.AlterColumn<string>(
                name: "NoteTypeCode",
                table: "InvoiceDetail",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(4)",
                oldMaxLength: 4,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Issuers",
                columns: new[] { "Id", "Address", "BetaCertificate", "BetaCertificatePasword", "Department", "District", "EstablishmentCode", "GeoCode", "IssuerId", "IssuerName", "IssuerType", "ProdCertificate", "ProdCertificatePasword", "Province" },
                values: new object[] { new Guid("e268e6a7-99c2-4e8b-bd6d-2ee3be8b117f"), "PSJE. LIMATAMBO 121", null, null, "SAN MARTIN", "TARAPOTO", "0000", "220901", 20606022779m, "SWIFTLINE SAC", "6", null, null, "SAN MARTIN" });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invoice.Repository.Migrations
{
    public partial class ChangeName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Senders");

            migrationBuilder.CreateTable(
                name: "Issuers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IssuerId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    IssuerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssuerType = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    GeoCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    EstablishmentCode = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issuers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Issuers",
                columns: new[] { "Id", "Address", "Department", "District", "EstablishmentCode", "GeoCode", "IssuerId", "IssuerName", "IssuerType", "Province" },
                values: new object[] { new Guid("4ae311ee-6a07-4111-b940-107d917a231f"), "PSJE. LIMATAMBO 121", "SAN MARTIN", "TARAPOTO", "0000", "220901", 20606022779m, "SWIFTLINE SAC", "6", "SAN MARTIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Issuers");

            migrationBuilder.CreateTable(
                name: "Senders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstablishmentCode = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    GeoCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    SenderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderType = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Senders", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Senders",
                columns: new[] { "Id", "Address", "Department", "District", "EstablishmentCode", "GeoCode", "Province", "SenderId", "SenderName", "SenderType" },
                values: new object[] { new Guid("cce03168-f901-4b23-ae9c-fc031d9dc888"), "PSJE. LIMATAMBO 121", "SAN MARTIN", "TARAPOTO", "0000", "220901", "SAN MARTIN", 20606022779m, "SWIFTLINE SAC", "6" });
        }
    }
}

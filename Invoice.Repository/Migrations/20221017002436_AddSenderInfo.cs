using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invoice.Repository.Migrations
{
    public partial class AddSenderInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Senders",
                columns: new[] { "Id", "Address", "Department", "District", "EstablishmentCode", "GeoCode", "Province", "SenderId", "SenderName", "SenderType" },
                values: new object[] { new Guid("cce03168-f901-4b23-ae9c-fc031d9dc888"), "PSJE. LIMATAMBO 121", "SAN MARTIN", "TARAPOTO", "0000", "220901", "SAN MARTIN", 20606022779m, "SWIFTLINE SAC", "6" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Senders",
                keyColumn: "Id",
                keyValue: new Guid("cce03168-f901-4b23-ae9c-fc031d9dc888"));
        }
    }
}

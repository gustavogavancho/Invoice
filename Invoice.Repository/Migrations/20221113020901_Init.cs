using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invoice.Repository.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DespatchDeliveryCustomer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DespatchPartyId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    DespatchPartyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DespatchPartyType = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DespatchDeliveryCustomer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DespatchDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Serie = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    SerialNumber = table.Column<long>(type: "bigint", nullable: false),
                    CorrelativeNumber = table.Column<int>(type: "int", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    DocumentReferenceId = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    DocumentReferenceType = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    NoteType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DespatchDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DespatchSellerSupplier",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DespatchPartyId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    DespatchPartyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DespatchPartyType = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DespatchSellerSupplier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DespatchShipment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdNumber = table.Column<int>(type: "int", nullable: false),
                    HandlingCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Information = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrossWeightMeasure = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalTransportHandlingUnitQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SplitConsignmentIndicator = table.Column<bool>(type: "bit", nullable: false),
                    TransitPeriod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CarrierPartyType = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    CarrierPartyId = table.Column<long>(type: "bigint", nullable: false),
                    CarrierPartyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransportLicensePlate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriverIdType = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    DriveId = table.Column<int>(type: "int", nullable: false),
                    DeliveryGeoCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    DeliveryAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransportHandlingUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OriginGeoCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    OriginAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstArrivalPortLocation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DespatchShipment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Issuer",
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
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BetaCertificate = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    BetaCertificatePasword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProdCertificate = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ProdCertificatePasword = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issuer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TicketType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TicketNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TicketResponse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentsXml = table.Column<string>(type: "xml", nullable: false),
                    StatusCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusContent = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Despatch",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DespatchXml = table.Column<string>(type: "xml", nullable: false),
                    SunatResponse = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Accepted = table.Column<bool>(type: "bit", nullable: false),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssuerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UblVersionId = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    CustomizationId = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    DespatchDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeliveryCustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellerSupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Despatch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Despatch_DespatchDeliveryCustomer_DeliveryCustomerId",
                        column: x => x.DeliveryCustomerId,
                        principalTable: "DespatchDeliveryCustomer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Despatch_DespatchDetail_DespatchDetailId",
                        column: x => x.DespatchDetailId,
                        principalTable: "DespatchDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Despatch_DespatchSellerSupplier_SellerSupplierId",
                        column: x => x.SellerSupplierId,
                        principalTable: "DespatchSellerSupplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Despatch_DespatchShipment_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "DespatchShipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Despatch_Issuer_IssuerId",
                        column: x => x.IssuerId,
                        principalTable: "Issuer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceXml = table.Column<string>(type: "xml", nullable: false),
                    SunatResponse = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Accepted = table.Column<bool>(type: "bit", nullable: false),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssuerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UblVersionId = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    CustomizationId = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    TaxTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SummaryDocumentStatus = table.Column<bool>(type: "bit", nullable: true),
                    Ticket = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Canceled = table.Column<bool>(type: "bit", nullable: false),
                    CanceledReason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoice_Issuer_IssuerId",
                        column: x => x.IssuerId,
                        principalTable: "Issuer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DespatchProductDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SellerItemIdentification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemClassificationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DespatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DespatchProductDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DespatchProductDetails_Despatch_DespatchId",
                        column: x => x.DespatchId,
                        principalTable: "Despatch",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InvoiceDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Serie = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    SerialNumber = table.Column<long>(type: "bigint", nullable: false),
                    CorrelativeNumber = table.Column<int>(type: "int", nullable: false),
                    OperationType = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    NoteTypeCode = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    NoteType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceDetail_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoicePaymentTerms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePaymentTerms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoicePaymentTerms_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceProductDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceType = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    TaxPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxExemptionReasonCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    TaxId = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    TaxName = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    TaxCode = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    SellerItemIdentification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemClassificationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceProductDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceProductDetails_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceReceiver",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiverId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ReceiverName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverType = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    FullAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceReceiver", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceReceiver_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceTaxSubTotal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaxableAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxId = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    TaxName = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    TaxCode = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceTaxSubTotal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceTaxSubTotal_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Issuer",
                columns: new[] { "Id", "Address", "BetaCertificate", "BetaCertificatePasword", "Department", "District", "EstablishmentCode", "GeoCode", "IssuerId", "IssuerName", "IssuerType", "ProdCertificate", "ProdCertificatePasword", "Province" },
                values: new object[] { new Guid("357b93e4-03cb-435a-8482-cb0015ab8a8b"), "PSJE. LIMATAMBO 121", null, null, "SAN MARTIN", "TARAPOTO", "0000", "220901", 20606022779m, "SWIFTLINE SAC", "6", null, null, "SAN MARTIN" });

            migrationBuilder.CreateIndex(
                name: "IX_Despatch_DeliveryCustomerId",
                table: "Despatch",
                column: "DeliveryCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Despatch_DespatchDetailId",
                table: "Despatch",
                column: "DespatchDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Despatch_IssuerId",
                table: "Despatch",
                column: "IssuerId");

            migrationBuilder.CreateIndex(
                name: "IX_Despatch_SellerSupplierId",
                table: "Despatch",
                column: "SellerSupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Despatch_ShipmentId",
                table: "Despatch",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DespatchProductDetails_DespatchId",
                table: "DespatchProductDetails",
                column: "DespatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_IssuerId",
                table: "Invoice",
                column: "IssuerId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetail_InvoiceId",
                table: "InvoiceDetail",
                column: "InvoiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePaymentTerms_InvoiceId",
                table: "InvoicePaymentTerms",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceProductDetails_InvoiceId",
                table: "InvoiceProductDetails",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceReceiver_InvoiceId",
                table: "InvoiceReceiver",
                column: "InvoiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceTaxSubTotal_InvoiceId",
                table: "InvoiceTaxSubTotal",
                column: "InvoiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DespatchProductDetails");

            migrationBuilder.DropTable(
                name: "InvoiceDetail");

            migrationBuilder.DropTable(
                name: "InvoicePaymentTerms");

            migrationBuilder.DropTable(
                name: "InvoiceProductDetails");

            migrationBuilder.DropTable(
                name: "InvoiceReceiver");

            migrationBuilder.DropTable(
                name: "InvoiceTaxSubTotal");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "Despatch");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "DespatchDeliveryCustomer");

            migrationBuilder.DropTable(
                name: "DespatchDetail");

            migrationBuilder.DropTable(
                name: "DespatchSellerSupplier");

            migrationBuilder.DropTable(
                name: "DespatchShipment");

            migrationBuilder.DropTable(
                name: "Issuer");
        }
    }
}

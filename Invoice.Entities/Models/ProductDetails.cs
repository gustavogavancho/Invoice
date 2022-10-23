﻿using System.ComponentModel.DataAnnotations;

namespace Invoice.Entities.Models;

public class ProductDetails
{
    public Guid Id { get; set; }
    public string UnitCode { get; set; } = default!; //Sunat UNIT CODES
    [Range(0, 9999999999999999.99)] public decimal Quantity { get; set; }
    [Range(0, 9999999999999999.99)] public decimal UnitPrice { get; set; }
    [Range(0, 9999999999999999.99)] public decimal TaxAmount { get; set; }
    [MinLength(2), MaxLength(2)] public string PriceType { get; set; } = default!; //Catalog 16
    [Range(0, 99)] public decimal TaxPercentage { get; set; }
    [MaxLength(2)] public string TaxExemptionReasonCode { get; set; } = default!; //Catalog 7
    [MinLength(4), MaxLength(4)] public string TaxId { get; set; } = default!; //Catalog 5
    [MinLength(3), MaxLength(4)] public string TaxName { get; set; } = default!; //Catalog 5
    [MinLength(3), MaxLength(4)] public string TaxCode { get; set; } = default!; //Catalog 5
    public string SellerItemIdentification { get; set; } = default!; //Internal product code
    public string ItemClassificationCode { get; set; } = default!; //Codigo estandar 25172405
    public string Description { get; set; } = default!;
    public Guid InvoiceId { get; set; }
}